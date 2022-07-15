using CSCore.CoreAudioAPI;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FishingBotFoffosEdition.Properties;
using System.IO;

namespace FishingBotFoffosEdition
{
    public partial class FishingBotGUI : Form
    {
        public CancellationTokenSource tokenSource;
        public CancellationTokenSource tokenSourceAudio;
        private System.Windows.Forms.Timer cursorPosTrackerTimer = null;
        private System.Windows.Forms.Timer audioMonitorTimer = null;
        private System.Windows.Forms.Timer executionTimer = null;
        private System.Windows.Forms.Timer timerTimer = null;

        public CoreFishingBot core;
        public FishingBotGUI()
        {
            InitializeComponent();
            StartCursorPosTrackerTimer();
            StartAudioMonitorTimer();
            StartExecutionMonitorTimer();
            StartTimerMonitorTimer();

            core = new CoreFishingBot();

            StartAudioMonitor();

            VolumeTresholdNumericUpDown.Value = core.GetAudioThreshold();
            audioDeviceComboBox.Items.AddRange(AudioManager.GetRenderDevices().ToArray());
            audioDeviceComboBox.SelectedIndex = audioDeviceComboBox.FindString(AudioManager.GetDefaultRenderDevice().FriendlyName);
            loadConfiguration();
        }

        #region Events
        private void RunButton_Click(object sender, EventArgs e)
        {
            if (audioDeviceComboBox.SelectedIndex != -1) 
            tokenSource = new CancellationTokenSource();
            Task mainBotFunctionTask = Task.Factory.StartNew(() =>
            {
                core.ExecuteMainBotFunction(appendLog, tokenSource.Token);
            },
            tokenSource.Token);

            GuiUpdateExecutionStart();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            tokenSource.Cancel();
            appendLog("Execution Stopped, ending current fishing process...");
            GuiUpdateExecutionStopped();
        }

        private void VolumeTresholdNumericUpDown_valueChanged(object sender, EventArgs e)
        {
            core.UpdateAudioThreshold(VolumeTresholdNumericUpDown.Value);
        }

        private void ExecutionNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            core.executionInfo.TaskToExecute = (int)ExecutionNumericUpDown.Value;
        }

        private void exportLogButton_Click(object sender, EventArgs e)
        {
            File.WriteAllText(Path.Combine(Resources.TempFolder, $"ExecutionLog_{DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss")}.txt"), statsRichTextBox.Text + "\n----------------------------\n" + logTextBox.Text);
            exportLogButton.Text = "Done";
        }

        private void audioDeviceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            core.changeAudioDevice((MMDevice)audioDeviceComboBox.SelectedItem);
        }

        private void delTempFilesButton_Click(object sender, EventArgs e)
        {
            var tempFileList = Directory.GetFiles(Resources.TempFolder, "*.jpg");
            this.logTextBox.AppendText("\r\n" + $"Deleting temp image files: {tempFileList.Count()} files found");
            this.logTextBox.ScrollToCaret();
            foreach (var item in tempFileList)
            {
                File.Delete(item);
            }
            this.logTextBox.AppendText("\r\n" + $"temp image files deleted");
            this.logTextBox.ScrollToCaret();

            var processedFileList = Directory.GetFiles(Resources.ProcessedFolder, "*.jpg");
            this.logTextBox.AppendText("\r\n" + $"Deleting processed image files: {processedFileList.Count()} files found");
            this.logTextBox.ScrollToCaret();
            foreach (var item in processedFileList)
            {
                File.Delete(item);
            }
            this.logTextBox.AppendText("\r\n" + $"processed image files deleted");
            this.logTextBox.ScrollToCaret();

        }

        private void saveConfigurationButton_Click(object sender, EventArgs e)
        {
            Settings.Default.DefaultVolumeTreshold = VolumeTresholdNumericUpDown.Value;
            Settings.Default.DefaultExecuteTaskNumber = core.executionInfo.TaskToExecute;
            Settings.Default.DefaultDevice = audioDeviceComboBox.SelectedIndex;
            Settings.Default.ForceLureFinding = ForceLureFindingCheckBox.Checked;
            Settings.Default.MinimalPrecisionRequired = FindingPrecisionNumericUpDown.Value;
            Settings.Default.Save();
        }
        private void ForceLureFindingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            core.forceLureFinding = ForceLureFindingCheckBox.Checked;
        }

        private void FindingPrecisionNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            core.minimalPrecisionRequired = FindingPrecisionNumericUpDown.Value;
        }
        private void clearExecutionInfoButton_Click(object sender, EventArgs e)
        {
            logTextBox.Text = String.Empty;
        }
        #endregion

        #region Timers and Tick Events

        private void StartCursorPosTrackerTimer()
        {
            cursorPosTrackerTimer = new System.Windows.Forms.Timer();
            cursorPosTrackerTimer.Interval = 100;
            cursorPosTrackerTimer.Tick += new EventHandler(CursorPosTracker_TickEvent);
            cursorPosTrackerTimer.Enabled = true;
        }
        void CursorPosTracker_TickEvent(object sender, EventArgs e)
        {
            refreshCoordinates();
        }

        private void StartAudioMonitorTimer()
        {
            audioMonitorTimer = new System.Windows.Forms.Timer();
            audioMonitorTimer.Interval = 100;
            audioMonitorTimer.Tick += new EventHandler(SoundMonitor_TickEvent);
            audioMonitorTimer.Enabled = true;
        }
        void SoundMonitor_TickEvent(object sender, EventArgs e)
        {
            refreshAudioMonitor();
        }

        private void StartExecutionMonitorTimer()
        {
            executionTimer = new System.Windows.Forms.Timer();
            executionTimer.Interval = 100;
            executionTimer.Tick += new EventHandler(ExecuteMonitor_TickEvent);
            executionTimer.Enabled = true;
        }
        void ExecuteMonitor_TickEvent(object sender, EventArgs e)
        {
            refreshExecuteInfo();
        }


        private void StartTimerMonitorTimer()
        {
            timerTimer = new System.Windows.Forms.Timer();
            timerTimer.Interval = 80;
            timerTimer.Tick += new EventHandler(TimerUpdate_TickEvent);
            timerTimer.Enabled = true;
        }
        void TimerUpdate_TickEvent(object sender, EventArgs e)
        {
            refreshTimer();
        }
        #endregion

        #region GUI Updates
        public void GuiUpdateExecutionStart()
        {
            stopButton.Enabled = true;
            RunButton.Enabled = false;
            audioDeviceComboBox.Enabled = false;
        }

        public void GuiUpdateExecutionStopped()
        {
            stopButton.Enabled = false;
            RunButton.Enabled = true;
            audioDeviceComboBox.Enabled = true;
        }

        public void refreshCoordinates()
        {
            var newMousePos = MouseUtility.GetCurrentCursorPosition();
            xLabel.Text = $"Cursor Position: X: {newMousePos.X}    Y:{newMousePos.Y}";
        }

        public void refreshAudioMonitor()
        {
            outputVolumeTrackerLabel.Text = $"Audio Level: {core.audioVolume.ToString("0.000")}";
        }

        public void refreshExecuteInfo()
        {
            statsRichTextBox.Text = String.Empty;
            statsRichTextBox.AppendText($"Tasks to Execute: {core.executionInfo.TaskToExecute}");
            statsRichTextBox.AppendText("\r\n" + $"Tasks Executed: {core.executionInfo.TeskExecuted}");
            statsRichTextBox.AppendText("\r\n" + $"Tasks Success: {core.executionInfo.TaskSuccess}");
            statsRichTextBox.AppendText("\r\n" + $"Tasks Erros: {core.executionInfo.TaskErrors}");
        }
        public void refreshTimer()
        {
            if (core.executionStopWatch.IsRunning)
                timerTextBox.Text = core.executionStopWatch.Elapsed.ToString("hh\\:mm\\:ss");
            if (core.iterationStopWatch.IsRunning)
                iterationTimerTextBox.Text = core.iterationStopWatch.Elapsed.ToString("ss\\:fff");
        }

        #endregion

        private void loadConfiguration()
        {
            VolumeTresholdNumericUpDown.Value = Settings.Default.DefaultVolumeTreshold;
            ExecutionNumericUpDown.Value = Settings.Default.DefaultExecuteTaskNumber;
            audioDeviceComboBox.SelectedIndex = Settings.Default.DefaultDevice;
            ForceLureFindingCheckBox.Checked = Settings.Default.ForceLureFinding;
            FindingPrecisionNumericUpDown.Value = Settings.Default.MinimalPrecisionRequired;
        }

        public void appendLog(string log)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(appendLog), new object[] { log });
                return;
            }
            this.logTextBox.AppendText("\r\n" + log);
            this.logTextBox.ScrollToCaret();
        }
    
        private void StartAudioMonitor()
        {
            tokenSourceAudio = new CancellationTokenSource();
            CancellationToken token = tokenSourceAudio.Token;
            ExecutionNumericUpDown.Value = core.executionInfo.TaskToExecute;

            Task t1 = Task.Factory.StartNew(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    core.ExecuteMonitorAudio();
                }
            },
            token);
        }
    }
}
