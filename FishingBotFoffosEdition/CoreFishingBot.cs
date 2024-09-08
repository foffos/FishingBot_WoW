using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CSCore.CoreAudioAPI;
using System.IO;
using FishingBotFoffosEdition.Properties;
using static FishingBotFoffosEdition.VideoManager;

namespace FishingBotFoffosEdition
{
	public class CoreFishingBot
	{
		private const int BUFF_DURATION = 10 * 60 * 1000;
		private const int EXECUTION_WAIT_WOW_FOCUS = 3000;
		private const int EXECUTION_TIMEOUT = 20 * 1000;
		private const int EXECUTION_DELAY = 1000;
		private const int EXECUTION_DELAY_APPLY_BUFF = 2000;
		public int EXECUTION_DELAY_BEFORE_SEARCH = 2500;
		private const int EXECUTION_DELAY_BEFORE_PICKUP_FISH_MIN = 60;
		private const int EXECUTION_DELAY_BEFORE_PICKUP_FISH_MAX = 800;
		private const int EXECUTION_DELAY_BEFORE_RESTART_MIN = 1000;
		private const int EXECUTION_DELAY_BEFORE_RESTART_MAX = 2000;
		private const decimal DEFAULT_AUDIO_TRASHOLD = 0.3M;
		private const int DEFAULT_TASKS_TO_EXECUTE = 50;


		private bool expiredBuff = false;
		enum State
		{
			AppliedBuff,
			ReadyToFish,
			SearchingForLure,
			FishingLurePositionFound,
			Fished
		};

		public MMDevice device;
		public decimal audioVolume;
		public bool forceLureFinding;
		public decimal minimalPrecisionRequired;
		public AudioMeterInformation audioMeter;
		public VideoManager videoManager;
		public ExecutionInfo executionInfo;
		public string selectedTemplateFolder;
		public System.Diagnostics.Stopwatch iterationStopWatch;
		public System.Diagnostics.Stopwatch executionStopWatch;
		public System.Diagnostics.Stopwatch buffRefreshStopWatch;

		public bool enableBuffRefresh = false;
		public bool consumablesOutOfStock = false;
		private bool buffRefreshedInLastExecution = false;
		private Random random;

		private decimal audioThreshold;
		private MouseUtility mouseUtilityBot;
		private List<string> templateFilesPathList = new List<string>();

		public CoreFishingBot()
		{
			random = new Random();
			mouseUtilityBot = new MouseUtility();
			executionInfo = new ExecutionInfo();
			videoManager = new VideoManager();
			executionInfo.TaskToExecute = DEFAULT_TASKS_TO_EXECUTE;
			device = AudioManager.GetDefaultRenderDevice();
			audioMeter = AudioMeterInformation.FromDevice(device);
			audioThreshold = DEFAULT_AUDIO_TRASHOLD;

			executionStopWatch = new System.Diagnostics.Stopwatch();
			iterationStopWatch = new System.Diagnostics.Stopwatch();
			buffRefreshStopWatch = new System.Diagnostics.Stopwatch();
		}

		public void ExecuteMainBotFunction(Action<string> logFunction, CancellationToken cancellationToken)
		{
			consumablesOutOfStock = false;
			executionInfo.TaskExecuted = 0;
			executionStopWatch = new System.Diagnostics.Stopwatch();
			executionStopWatch.Start();
			buffRefreshStopWatch = new System.Diagnostics.Stopwatch();
			buffRefreshStopWatch.Start();
			logFunction($"Starting new Execution...");
			State state = State.ReadyToFish;
			iterationStopWatch = new System.Diagnostics.Stopwatch();
			Thread.Sleep(EXECUTION_DELAY);

			iterationStopWatch.Start();

			while (executionInfo.TaskExecuted < executionInfo.TaskToExecute)
			{
				try
				{
					//thread cancellation check
					if (cancellationToken.IsCancellationRequested)
					{
						logFunction($"Execution Stopped");
						state = State.ReadyToFish;
						executionStopWatch.Stop();
						iterationStopWatch.Stop();
						return;
					}
					//timeout check
					if (iterationStopWatch.ElapsedMilliseconds > EXECUTION_TIMEOUT)
					{
						executionInfo.TaskErrors++;
						executionInfo.TaskExecuted++;
						iterationStopWatch.Restart();
						logFunction($"[{executionInfo.TaskExecuted + 1}] - Execution timeout - restarting");
						state = State.ReadyToFish;
						mouseUtilityBot.KeyboardPressJump();
						Thread.Sleep(random.Next(40) * 10 + EXECUTION_DELAY);
					}

					//States executions

					if (!VideoManager.IsWowOnFocus())
					{
						logFunction($"[{executionInfo.TaskExecuted + 1}] - Wow window is not on focus, waiting for focus");
						Thread.Sleep(EXECUTION_WAIT_WOW_FOCUS);
					}
					else
					{
						switch (state)
						{
							//use fishing pole
							case State.ReadyToFish:
								Thread.Sleep(EXECUTION_DELAY);

								//check buff
								//if (enableBuffRefresh && !consumablesOutOfStock && expiredBuff)
								if (enableBuffRefresh && buffRefreshStopWatch.ElapsedMilliseconds > BUFF_DURATION)
								{
									logFunction($"[{executionInfo.TaskExecuted + 1}] - Buff Expired > reapplying consumable");
									mouseUtilityBot.KeyboardPressBuff();
									Thread.Sleep(EXECUTION_DELAY_APPLY_BUFF);
									expiredBuff = false;
									buffRefreshedInLastExecution = true;
									buffRefreshStopWatch.Restart();
									state = State.ReadyToFish;
								}
								else
								{
									iterationStopWatch.Restart();
									mouseUtilityBot.KeyboardPressFishing();
									state = State.SearchingForLure;
									logFunction($"[{executionInfo.TaskExecuted + 1}] - Fishing pole used");
								}
								break;

							//search for target
							case State.SearchingForLure:
								mouseUtilityBot.MoveMouseToPos(0, 0);
								Thread.Sleep(EXECUTION_DELAY_BEFORE_SEARCH);
								logFunction($"[{executionInfo.TaskExecuted + 1}] - searching for lure with templates from {selectedTemplateFolder}");

								SearchResult topResult = videoManager.FindTemplateInCurrentScreenBestPrecision(templateFilesPathList, false);//TODO CHANGE

								if (topResult.missingBuff && buffRefreshedInLastExecution)
								{
									consumablesOutOfStock = true;
									logFunction($"[{executionInfo.TaskExecuted + 1}] - Consumables out of stock!!! no consumables will be used until restart");
								}


								expiredBuff = topResult.missingBuff;

								//Force Lure Finding >>> the precision must be more than minimalPrecisionRequired, or it will re-execute
								if (forceLureFinding && topResult.precision * 100 < (double)minimalPrecisionRequired)
								{
									logFunction($"[{executionInfo.TaskExecuted + 1}] - lure NOT found: precision {(topResult.precision * 100).ToString("0.")}% over {(minimalPrecisionRequired).ToString("0.")}% required - re-executing from first step");
									state = State.ReadyToFish;
								}
								else
								{
									mouseUtilityBot.MoveMouseToPos(topResult.optimizedClickPoint);
									state = State.FishingLurePositionFound;
									logFunction($"[{executionInfo.TaskExecuted + 1}] - lure found: precision {(topResult.precision * 100).ToString("0.")}%");
									logFunction($"[{executionInfo.TaskExecuted + 1}] - Waiting for audio input > {audioThreshold}...");
								}

								break;

							//check audio for click
							case State.FishingLurePositionFound:

								int randomSleepLure = random.Next(EXECUTION_DELAY_BEFORE_PICKUP_FISH_MIN, EXECUTION_DELAY_BEFORE_PICKUP_FISH_MAX);
								Thread.Sleep(randomSleepLure);

								if (audioVolume > audioThreshold)
								{
									mouseUtilityBot.RightMouseClick();
									logFunction($"[{executionInfo.TaskExecuted + 1}] - CLICK_EXECUTED");
									state = State.Fished;
								}

								break;

							//restart
							case State.Fished:
								executionInfo.TaskSuccess++;
								executionInfo.TaskExecuted++;
								iterationStopWatch.Restart();
								logFunction($"[{executionInfo.TaskExecuted + 1}] - Execute_Fished, restarting...");
								int randomSleepRestart = random.Next(EXECUTION_DELAY_BEFORE_RESTART_MIN, EXECUTION_DELAY_BEFORE_RESTART_MAX);
								Thread.Sleep(randomSleepRestart);
								state = State.ReadyToFish;
								break;
						}
					}
				}
				catch (Exception ex)
				{
					File.WriteAllText($"CrashReport_{DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss")}.txt", "Exception in ExecuteMainBotFunction Execution at state " + state.ToString() + ".  message:" + ex.Message + "\n" + ex.StackTrace);
					throw ex;
				}

			}
			logFunction($"[{executionInfo.TaskExecuted + 1}] - EXECUTION ENDED");

		}

		public List<SearchResult> ExecuteDebugBotFunction(Action<string> logFunction, CancellationToken cancellationToken)
		{
			try
			{

				executionInfo.TaskToExecute = Settings.Default.DebugRuns;
				List<SearchResult> resultList = new List<SearchResult>();
				executionStopWatch = new System.Diagnostics.Stopwatch();
				executionStopWatch.Start();
				logFunction($"Starting new Debug Execution...");
				mouseUtilityBot = new MouseUtility();
				State state = State.ReadyToFish;
				iterationStopWatch = new System.Diagnostics.Stopwatch();

				Thread.Sleep(EXECUTION_DELAY);

				iterationStopWatch.Start();

				while (executionInfo.TaskExecuted < executionInfo.TaskToExecute)
				{

					try
					{
						//thread cancellation check
						if (cancellationToken.IsCancellationRequested)
						{
							logFunction($"Execution Stopped");
							state = State.ReadyToFish;
							executionStopWatch.Stop();
							iterationStopWatch.Stop();
							return resultList;
						}

						//States executions
						if (!VideoManager.IsWowOnFocus())
						{
							logFunction($"[{executionInfo.TaskExecuted + 1}] - Wow window is not on focus, waiting for focus");
							Thread.Sleep(EXECUTION_WAIT_WOW_FOCUS);
						}
						else
						{
							switch (state)
							{
								//use fishing pole
								case State.ReadyToFish:
									Thread.Sleep(EXECUTION_DELAY);
									iterationStopWatch.Restart();
									mouseUtilityBot.KeyboardPressFishing();
									state = State.SearchingForLure;
									logFunction($"[{executionInfo.TaskExecuted + 1}] - Fishing pole used");
									break;

								//search for target
								case State.SearchingForLure:
									Thread.Sleep(EXECUTION_DELAY_BEFORE_SEARCH);
									logFunction($"[{executionInfo.TaskExecuted + 1}] - searching for lure...");
									SearchResult topResult = videoManager.FindTemplateInCurrentScreenBestPrecision(templateFilesPathList, false);
									logFunction($"[{executionInfo.TaskExecuted + 1}] - lure found with precision {(topResult.precision * 100).ToString("0.")}%");
									resultList.Add(topResult);
									mouseUtilityBot.KeyboardPressJump();
									executionInfo.TaskExecuted++;
									state = State.ReadyToFish;
									break;
							}
						}
					}
					catch (Exception ex)
					{
						File.WriteAllText($"CrashReport_{DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss")}.txt", ex.Message + "\n" + ex.StackTrace);
						throw ex;
					}

				}
				logFunction($"DEBUG EXECUTION ENDED");
				return resultList;

			}
			catch (Exception ex)
			{
				File.WriteAllText($"CrashReport_{DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss")}.txt", ex.Message + "\n" + ex.StackTrace);
				throw ex;
			}
		}

		public List<SearchResult> ExecuteNewTemplateFunction(Action<string> logFunction, CancellationToken cancellationToken)
		{
			try
			{

				executionInfo.TaskToExecute = 1;
				List<SearchResult> resultList = new List<SearchResult>();
				executionStopWatch = new System.Diagnostics.Stopwatch();
				executionStopWatch.Start();
				logFunction($"Starting new Debug Execution...");
				mouseUtilityBot = new MouseUtility();
				State state = State.ReadyToFish;
				iterationStopWatch = new System.Diagnostics.Stopwatch();

				Thread.Sleep(EXECUTION_DELAY);

				iterationStopWatch.Start();

				while (executionInfo.TaskExecuted < executionInfo.TaskToExecute)
				{
					try
					{
						//thread cancellation check
						if (cancellationToken.IsCancellationRequested)
						{
							logFunction($"Execution Stopped");
							state = State.ReadyToFish;
							executionStopWatch.Stop();
							iterationStopWatch.Stop();
							return resultList;
						}

						if (!VideoManager.IsWowOnFocus())
						{
							logFunction($"[{executionInfo.TaskExecuted + 1}] - Wow window is not on focus, waiting for focus");
							Thread.Sleep(EXECUTION_WAIT_WOW_FOCUS);
						}
						else
						{
							//States executions
							switch (state)
							{
								//use fishing pole
								case State.ReadyToFish:
									Thread.Sleep(EXECUTION_DELAY);
									iterationStopWatch.Restart();
									mouseUtilityBot.KeyboardPressFishing();
									state = State.SearchingForLure;
									logFunction($"[{executionInfo.TaskExecuted + 1}] - Fishing pole used");
									break;

								//take screenshot
								case State.SearchingForLure:
									Thread.Sleep(EXECUTION_DELAY_BEFORE_SEARCH);
									SearchResult searchResult = new SearchResult();
									searchResult.imagePath = TakeScreenshot();
									resultList.Add(searchResult);
									mouseUtilityBot.KeyboardPressJump();
									executionInfo.TaskExecuted++;
									state = State.ReadyToFish;
									break;
							}
						}
					}
					catch (Exception ex)
					{
						File.WriteAllText($"CrashReport_{DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss")}.txt", ex.Message + "\n" + ex.StackTrace);
						throw ex;
					}

				}
				logFunction($"DEBUG EXECUTION ENDED");
				return resultList;

			}
			catch (Exception ex)
			{
				File.WriteAllText($"CrashReport_{DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss")}.txt", ex.Message + "\n" + ex.StackTrace);
				throw ex;
			}
		}

		public void changeAudioDevice(MMDevice newDevice)
		{
			device = newDevice;
			audioMeter = AudioMeterInformation.FromDevice(device);
		}

		public decimal GetAudioThreshold()
		{
			return audioThreshold;
		}

		public void ExecuteMonitorAudio()
		{
			audioVolume = (decimal)audioMeter.PeakValue;
		}

		public void UpdateAudioThreshold(decimal newValue)
		{
			audioThreshold = newValue;
		}

		public int LoadTemplateFiles(string templateFolder)
		{
			selectedTemplateFolder = templateFolder;
			templateFilesPathList.Clear();
			Utils.Log($"LoadTemplateFiles > templatefolder: {selectedTemplateFolder}");
			foreach (var templateFile in Utils.GetTemplateFilesInFolder(selectedTemplateFolder))
			{
				templateFilesPathList.Add(templateFile);
				Utils.Log($"LoadTemplateFiles > templateFile: {templateFile}");
			}
			return templateFilesPathList.Count;
		}

		public void MouseLockOnWow(bool lockMouse)
		{
			mouseUtilityBot.ExternalLock = lockMouse;
		}
	}
}
