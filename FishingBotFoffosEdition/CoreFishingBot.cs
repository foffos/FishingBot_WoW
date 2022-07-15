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
        private const int EXECUTION_TIMEOUT = 18 * 1000;
        private const int EXECUTION_DELAY = 1000;
        private const int EXECUTION_DELAY_BEFORE_SEARCH = 3500;
        private const int EXECUTION_DELAY_FAST = 75;
        private const decimal DEFAULT_AUDIO_TRASHOLD = 0.3M;
        private const int DEFAULT_TASKS_TO_EXECUTE = 50;

        enum State
        {
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
        public ExecutionInfo executionInfo;
        public System.Diagnostics.Stopwatch iterationStopWatch;
        public System.Diagnostics.Stopwatch executionStopWatch;

        private decimal audioThreshold;
        private MouseUtility mouseUtilityBot;
        private List<string> templateFilePathList = new List<string>();
        public CoreFishingBot()
        {
            executionInfo = new ExecutionInfo();
            executionInfo.TaskToExecute = DEFAULT_TASKS_TO_EXECUTE;
            device = AudioManager.GetDefaultRenderDevice();
            audioMeter = AudioMeterInformation.FromDevice(device);
            audioThreshold = DEFAULT_AUDIO_TRASHOLD;

            executionStopWatch = new System.Diagnostics.Stopwatch();
            iterationStopWatch = new System.Diagnostics.Stopwatch();

            foreach (var templateFile in Directory.GetFiles(Resources.TemplateFolder))
            {
                templateFilePathList.Add(templateFile);
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

        public void ExecuteMainBotFunction(Action<string> logFunction, CancellationToken cancellationToken)
        {
            try
            {
                executionStopWatch = new System.Diagnostics.Stopwatch();
                executionStopWatch.Start();
                logFunction($"Starting new Execution...");
                mouseUtilityBot = new MouseUtility();
                State state = State.ReadyToFish;
                iterationStopWatch = new System.Diagnostics.Stopwatch();

                Thread.Sleep(EXECUTION_DELAY);

                iterationStopWatch.Start();

                while (executionInfo.TeskExecuted < executionInfo.TaskToExecute)
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
                            executionInfo.TeskExecuted++;
                            logFunction($"Execution timeout - restarting");
                            state = State.ReadyToFish;
                            mouseUtilityBot.KeyboardPressJump();
                            Thread.Sleep(EXECUTION_DELAY);
                        }

                        //States executions
                        switch (state)
                        {
                            //use fishing pole
                            case State.ReadyToFish:
                                Thread.Sleep(EXECUTION_DELAY);
                                iterationStopWatch.Restart();
                                mouseUtilityBot.KeyboardPressFishing();
                                state = State.SearchingForLure;
                                logFunction($"Fishing pole used");
                                break;

                            //search for target
                            case State.SearchingForLure:
                                Thread.Sleep(EXECUTION_DELAY_BEFORE_SEARCH);
                                logFunction($"searching for lure...");

                                SearchResult topResult = VideoManager.FindTemplateInCurrentScreenBestPrecision(templateFilePathList);

                                //Force Lure Finding >>> the precision must be more than minimalPrecisionRequired, or it will re-execute
                                if (forceLureFinding && topResult.precision < (double)minimalPrecisionRequired)
                                {
                                    logFunction($"lure NOT found: precision {(topResult.precision * 100).ToString("0.")}% over {(minimalPrecisionRequired * 100).ToString("0.")}% required - re-executing from first step");
                                    state = State.ReadyToFish;
                                }
                                else
                                {
                                    mouseUtilityBot.MoveMouseToPos(topResult.optimizedClickPoint);
                                    state = State.FishingLurePositionFound;
                                    logFunction($"lure found: precision {(topResult.precision * 100).ToString("0.")}%");
                                    logFunction($"Waiting for audio input > {audioThreshold}...");
                                }

                                break;

                            //check audio for click
                            case State.FishingLurePositionFound:
                                Thread.Sleep(EXECUTION_DELAY_FAST);

                                if (audioVolume > audioThreshold)
                                {
                                    mouseUtilityBot.RightMouseClick();
                                    logFunction(">CLICK_EXECUTED<");
                                    state = State.Fished;
                                }

                                break;

                            //restart
                            case State.Fished:
                                executionInfo.TaskSuccess++;
                                executionInfo.TeskExecuted++;
                                logFunction($"Execute_Fished, restarting...");
                                Thread.Sleep(EXECUTION_DELAY);
                                state = State.ReadyToFish;
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        File.WriteAllText($"CrashReport_{DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss")}.txt", ex.Message + "\n" + ex.StackTrace);
                        throw ex;
                    }

                }
                logFunction($"EXECUTION ENDED");
            }
            catch (Exception ex)
            {
                File.WriteAllText($"CrashReport_{DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss")}.txt", ex.Message + "\n" + ex.StackTrace);
                throw ex;
            }
        }

        public void ExecuteMonitorAudio()
        {
            audioVolume = (decimal)audioMeter.PeakValue;
        }

        public void UpdateAudioThreshold(decimal newValue)
        {
            audioThreshold = newValue;
        }
    }
}
