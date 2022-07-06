using AForge.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.MobileControls;
using System.Web.UI.WebControls;
using System.Windows.Controls;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using CSCore;
using CSCore.SoundIn;
using CSCore.CoreAudioAPI;
using System.IO;
using FishingBotFoffosEdition.Properties;
using static FishingBotFoffosEdition.VideoManager;

namespace FishingBotFoffosEdition
{
    public class Core
    {
        private const int X_OFFSET = -3;
        private const int Y_OFFSET = -6;

        private const int EXECUTION_TIMEOUT = 18 * 1000;
        private const int EXECUTION_DELAY = 1000;
        private const int EXECUTION_DELAY_FAST = 100;
        private const int EXECUTION_DELAY_EXTENDED = 3000;
        private const decimal DEFAULT_AUDIO_TRASHOLD = 0.3M;
        private const int DEFAULT_TASKS_TO_EXECUTE = 50;
        //private const string LURE_FOLDER_PATH = "C:\\Users\\andry\\Desktop\\DebugScreens\\Templates\\";
        private const string LURE_FOLDER_PATH = "AppData\\Resources\\FishingLureTemplate\\";
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
        private decimal audioThreshold;
        public ExecutionInfo executionInfo;
        public System.Diagnostics.Stopwatch executionStopWatch;

        private MouseUtility mouseUtilityBot;
        public Core()
        {
            executionInfo = new ExecutionInfo();
            executionInfo.TaskToExecute = DEFAULT_TASKS_TO_EXECUTE;
            device = AudioManager.GetDefaultRenderDevice();
            audioMeter = AudioMeterInformation.FromDevice(device);
            audioThreshold = DEFAULT_AUDIO_TRASHOLD;
        }

        public decimal GetAudioThreshold()
        {
            return audioThreshold;
        }

        public void ExecuteMainBotFunction(char fishingChar, Action<string> logFunction)
        {
            logFunction($"Starting new Execution...");
            mouseUtilityBot = new MouseUtility();
            State state = State.ReadyToFish;
            executionStopWatch = new System.Diagnostics.Stopwatch();

            Thread.Sleep(EXECUTION_DELAY_EXTENDED);

            executionStopWatch.Start();
            
            while (executionInfo.TeskExecuted < executionInfo.TaskToExecute)
            {
                if(executionStopWatch.ElapsedMilliseconds > EXECUTION_TIMEOUT)
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
                        executionStopWatch.Restart();
                        logFunction($"Execute_ReadyToFish");
                        mouseUtilityBot.KeyboardPressFishing();
                        state = State.FishingPoleUsed;
                        break;

                    //search for target
                    case State.FishingPoleUsed:
                        Thread.Sleep(EXECUTION_DELAY);
                        logFunction($"Execute_FishingPoleUsed");
                        Point centerRectPoint = new Point();
                        string path = VideoManager.TakeScreenshot();
                        List<SearchResult> searchResutList = new List<SearchResult>();
                        foreach (var template in Directory.GetFiles(LURE_FOLDER_PATH))
                        {
                            SearchResult searchResult = VideoManager.newSearch(path, template);
                            searchResutList.Add(searchResult);
                        }

                        SearchResult topResult = searchResutList.OrderByDescending(x => x.precision).First();
                        logFunction($"image search best precision: {topResult.precision}");
                        var rect = topResult.rect;
                        if (rect.Width > 0 && rect.Height > 0)
                        {
                            centerRectPoint = new Point(rect.X + (rect.Width / 2) + X_OFFSET, rect.Y + (rect.Height / 2) + Y_OFFSET);
                            mouseUtilityBot.MoveMouseToPos(centerRectPoint.X, centerRectPoint.Y);
                            state = State.FishingLurePositionFound;
                            logFunction($"Execute_FishingLurePositionFound: Waiting for audio {audioThreshold}");
                        }

                        break;

                    //check audio for click
                    case State.FishingLurePositionFound:
                        Thread.Sleep(EXECUTION_DELAY_FAST);
                        
                        if (audioVolume > audioThreshold)
                        {
                            mouseUtilityBot.RightMouseClick();
                            logFunction("CLICK_EXECUTED");
                            state = State.Fished;
                        }

                        break;

                    //restart
                    case State.Fished:
                        executionInfo.TaskSuccess++;
                        executionInfo.TeskExecuted++;
                        logFunction($"Execute_Fished");
                        logFunction($"Restarting...");
                        Thread.Sleep(EXECUTION_DELAY_EXTENDED);
                        state = State.ReadyToFish;
                        break;
                }
            }
            logFunction($"EXECUTION ENDED");
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
