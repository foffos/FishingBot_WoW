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
    public class Core
    {
        private const int EXECUTION_TIMEOUT = 18 * 1000;
        private const int EXECUTION_DELAY = 1000;
        private const int EXECUTION_DELAY_FAST = 100;
        private const int EXECUTION_DELAY_EXTENDED = 3000;
        private const decimal DEFAULT_AUDIO_TRASHOLD = 0.3M;
        private const int DEFAULT_TASKS_TO_EXECUTE = 50;

        enum State
        {
            ReadyToFish,
            FishingPoleUsed,
            FishingLurePositionFound,
            Fished
        };

        public MMDevice device;
        public decimal audioVolume;
        public AudioMeterInformation audioMeter;
        public ExecutionInfo executionInfo;
        public System.Diagnostics.Stopwatch iterationStopWatch;
        public System.Diagnostics.Stopwatch executionStopWatch;

        private decimal audioThreshold;
        private MouseUtility mouseUtilityBot;
        private List<string> templateFilePathList = new List<string>();
        public Core()
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

        public void ExecuteMainBotFunction(char fishingChar, Action<string> logFunction)
        {
            try
            {
                executionStopWatch = new System.Diagnostics.Stopwatch();
                executionStopWatch.Start();
                logFunction($"Starting new Execution...");
                mouseUtilityBot = new MouseUtility();
                State state = State.ReadyToFish;
                iterationStopWatch = new System.Diagnostics.Stopwatch();

                Thread.Sleep(EXECUTION_DELAY_EXTENDED);

                iterationStopWatch.Start();

                while (executionInfo.TeskExecuted < executionInfo.TaskToExecute)
                {
                    try
                    {
                        if (iterationStopWatch.ElapsedMilliseconds > EXECUTION_TIMEOUT)
                        {
                            executionInfo.TaskErrors++;
                            executionInfo.TeskExecuted++;
                            logFunction($"Execution timeout - restarting");
                            state = State.ReadyToFish;
                            mouseUtilityBot.KeyboardPressJump();
                            Thread.Sleep(EXECUTION_DELAY_EXTENDED);
                        }
                        switch (state)
                        {
                            //use fishing pole
                            case State.ReadyToFish:
                                Thread.Sleep(EXECUTION_DELAY);
                                iterationStopWatch.Restart();
                                mouseUtilityBot.KeyboardPressFishing();
                                state = State.FishingPoleUsed;
                                logFunction($"Fishing pole used");
                                break;

                            //search for target
                            case State.FishingPoleUsed:
                                Thread.Sleep(EXECUTION_DELAY);
                                logFunction($"searching for lure...");
                                string screenhotPath = VideoManager.TakeScreenshot();
                                List<SearchResult> searchResutList = new List<SearchResult>();
                                foreach (var template in templateFilePathList)
                                {
                                    SearchResult searchResult = VideoManager.newSearch(screenhotPath, template);
                                    searchResutList.Add(searchResult);
                                }

                                SearchResult topResult = searchResutList.OrderByDescending(x => x.precision).First();
                                var rect = topResult.rect;
                                if (rect.Width > 0 && rect.Height > 0)
                                {
                                    mouseUtilityBot.MoveMouseToPos(topResult.optimizedClickPoint);
                                    state = State.FishingLurePositionFound;
                                    logFunction($"lure found: precision {topResult.precision.ToString("00.00")}%");
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
                                Thread.Sleep(EXECUTION_DELAY_EXTENDED);
                                state = State.ReadyToFish;
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        File.WriteAllText($"CrashReport_{DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss")}.txt", ex.StackTrace);
                    }

                }
                logFunction($"EXECUTION ENDED");
            }
            catch (Exception ex)
            {
                File.WriteAllText($"CrashReport_{DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss")}.txt", ex.StackTrace);
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
