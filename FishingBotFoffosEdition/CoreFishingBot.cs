﻿using System;
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
        private const int EXECUTION_DELAY_BEFORE_SEARCH = 2500;
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
        public VideoManager videoManager;
        public ExecutionInfo executionInfo;
        public string selectedTemplateFolder;
        public System.Diagnostics.Stopwatch iterationStopWatch;
        public System.Diagnostics.Stopwatch executionStopWatch;

        private decimal audioThreshold;
        private MouseUtility mouseUtilityBot;
        private List<string> templateFilesPathList = new List<string>();

        public CoreFishingBot()
        {
            executionInfo = new ExecutionInfo();
            videoManager = new VideoManager();
            executionInfo.TaskToExecute = DEFAULT_TASKS_TO_EXECUTE;
            device = AudioManager.GetDefaultRenderDevice();
            audioMeter = AudioMeterInformation.FromDevice(device);
            audioThreshold = DEFAULT_AUDIO_TRASHOLD;

            executionStopWatch = new System.Diagnostics.Stopwatch();
            iterationStopWatch = new System.Diagnostics.Stopwatch();
        }

        public void ExecuteMainBotFunction(Action<string> logFunction, CancellationToken cancellationToken)
        {
            executionInfo.TaskExecuted = 0;
            executionStopWatch = new System.Diagnostics.Stopwatch();
            executionStopWatch.Start();
            logFunction($"Starting new Execution...");
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
                        return;
                    }
                    //timeout check
                    if (iterationStopWatch.ElapsedMilliseconds > EXECUTION_TIMEOUT)
                    {
                        executionInfo.TaskErrors++;
                        executionInfo.TaskExecuted++;
                        logFunction($"[{executionInfo.TaskExecuted + 1}] - Execution timeout - restarting");
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
                            logFunction($"[{executionInfo.TaskExecuted + 1}] - Fishing pole used");
                            break;

                        //search for target
                        case State.SearchingForLure:
                            Thread.Sleep(EXECUTION_DELAY_BEFORE_SEARCH);
                            logFunction($"[{executionInfo.TaskExecuted + 1}] - searching for lure with templates from {selectedTemplateFolder}");

                            SearchResult topResult = videoManager.FindTemplateInCurrentScreenBestPrecision(templateFilesPathList);

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
                            Thread.Sleep(EXECUTION_DELAY_FAST);

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
                            logFunction($"[{executionInfo.TaskExecuted + 1}] - Execute_Fished, restarting...");
                            Thread.Sleep(EXECUTION_DELAY);
                            state = State.ReadyToFish;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    File.WriteAllText($"CrashReport_{DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss")}.txt", "Exception in ExecuteMainBotFunction Execution at state " + state.ToString()+".  message:" + ex.Message + "\n" + ex.StackTrace);
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
                                SearchResult topResult = videoManager.FindTemplateInCurrentScreenBestPrecision(templateFilesPathList);
                                logFunction($"[{executionInfo.TaskExecuted + 1}] - lure found with precision {(topResult.precision * 100).ToString("0.")}%");
                                resultList.Add(topResult);
                                mouseUtilityBot.KeyboardPressJump();
                                executionInfo.TaskExecuted++;
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
    }
}
