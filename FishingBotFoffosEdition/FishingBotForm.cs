using CSCore.CoreAudioAPI;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FishingBotFoffosEdition.Properties;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using static FishingBotFoffosEdition.VideoManager;
using System.Configuration;

namespace FishingBotFoffosEdition
{
    public partial class FishingBotForm : Form
    {
        public CancellationTokenSource tokenSource;
        public CancellationTokenSource tokenSourceAudio;
        private System.Windows.Forms.Timer cursorPosTrackerTimer = null;
        private System.Windows.Forms.Timer audioMonitorTimer = null;
        private System.Windows.Forms.Timer executionTimer = null;
        private System.Windows.Forms.Timer timerTimer = null;

        public CoreFishingBot core;
        public FishingBotForm()
        {
            InitializeComponent();
            StartCursorPosTrackerTimer();
            StartAudioMonitorTimer();
            StartExecutionMonitorTimer();
            StartTimerMonitorTimer();

            core = new CoreFishingBot();

            StartAudioMonitor();

            VolumeTresholdNumericUpDown.Value = core.GetAudioThreshold();

            BindAudioDeviceCombobox();
            BindTemplateFolderCombobox();

            loadConfiguration();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;


        }

        #region Events

        #region ClickEvents
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
        private void saveConfigurationButton_Click(object sender, EventArgs e)
        {
            Settings.Default.DefaultVolumeTreshold = VolumeTresholdNumericUpDown.Value;
            Settings.Default.DefaultExecuteTaskNumber = core.executionInfo.TaskToExecute;
            Settings.Default.DefaultDevice = audioDeviceComboBox.SelectedIndex;
            Settings.Default.ForceLureFinding = ForceLureFindingCheckBox.Checked;
            Settings.Default.MinimalPrecisionRequired = FindingPrecisionNumericUpDown.Value;

            Settings.Default.XOffset = (int)OffsetXnumericUpDown.Value;
            Settings.Default.YOffset = (int)OffsetYnumericUpDown.Value;

            Settings.Default.DefaultTemplate = templateFolderComboBox.SelectedValue.ToString();

            Settings.Default.Save();
        }
        private void clearExecutionInfoButton_Click(object sender, EventArgs e)
        {
            logTextBox.Text = String.Empty;
        }
        #endregion

        #region ValueChangeEvents
        private void OffestXnumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            core.videoManager.offsetClickX = (int)OffsetXnumericUpDown.Value;
        }
        private void OffestYnumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            core.videoManager.offsetClickY = (int)OffsetYnumericUpDown.Value;
        }
        private void FindingPrecisionNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            core.minimalPrecisionRequired = FindingPrecisionNumericUpDown.Value;
        }
        private void ExecutionNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            core.executionInfo.TaskToExecute = (int)ExecutionNumericUpDown.Value;
        }
        private void VolumeTresholdNumericUpDown_valueChanged(object sender, EventArgs e)
        {
            core.UpdateAudioThreshold(VolumeTresholdNumericUpDown.Value);
        }

        #endregion

        #region ToolStripMenuEvents
        private void deleteTempFilesToolStripMenuItem_Click(object sender, EventArgs e)
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
        private void exportLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.WriteAllText(Path.Combine(Resources.TempFolder, $"ExecutionLog_{DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss")}.txt"), statsRichTextBox.Text + "\n----------------------------\n" + logTextBox.Text);
        }
        private void openTemplateFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Resources.TemplateFolder);
        }
        private void runDebugLureFinderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (audioDeviceComboBox.SelectedIndex != -1)
                tokenSource = new CancellationTokenSource();
            List<SearchResult> resultList = new List<SearchResult>();

            Task debugBotFunctionTask = Task.Factory.StartNew(() =>
            {
                resultList = core.ExecuteDebugBotFunction(appendLog, tokenSource.Token);
            },
            tokenSource.Token).ContinueWith(antecedent => ShowDebugResults(resultList));

            GuiUpdateExecutionStart();
        }

        #endregion

        private void audioDeviceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            core.changeAudioDevice((MMDevice)audioDeviceComboBox.SelectedItem);
        }
        private void ForceLureFindingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            core.forceLureFinding = ForceLureFindingCheckBox.Checked;
        }

        #endregion Events

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
            ForceLureFindingCheckBox.Checked = Settings.Default.ForceLureFinding;
            FindingPrecisionNumericUpDown.Value = Settings.Default.MinimalPrecisionRequired;
            OffsetXnumericUpDown.Value = Settings.Default.XOffset;
            OffsetYnumericUpDown.Value = Settings.Default.YOffset;

            if (audioDeviceComboBox.Items.Contains(Settings.Default.DefaultDevice))
                audioDeviceComboBox.SelectedIndex = Settings.Default.DefaultDevice;
            else
                audioDeviceComboBox.SelectedIndex = 0;

            if (templateFolderComboBox.Items.Contains(Settings.Default.DefaultTemplate))
                templateFolderComboBox.SelectedValue = Settings.Default.DefaultTemplate;
            else
                templateFolderComboBox.SelectedIndex = 0;
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

        private void ShowDebugResults(List<SearchResult> searchResultList)
        {
            Application.Run(new ImageViewerForm(searchResultList));
        }

        private void templateFolderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int fileCount = core.LoadTemplateFiles(templateFolderComboBox.SelectedItem.ToString());
            FileInfoLabel.Text = $"Files: {fileCount}";
        }

        private void BindTemplateFolderCombobox()
        {
            templateFolderComboBox.Items.Clear();

            List<string> templateFoldersList = Utils.getTemplateFolderNamesList();

            foreach (var directory in templateFoldersList)
            {
                templateFolderComboBox.Items.Add(directory);
            }
        }

        private void BindAudioDeviceCombobox()
        {
            audioDeviceComboBox.Items.Clear();
            audioDeviceComboBox.Items.AddRange(AudioManager.GetRenderDevices().ToArray());
            audioDeviceComboBox.SelectedIndex = audioDeviceComboBox.FindString(AudioManager.GetDefaultRenderDevice().FriendlyName);
        }

        private void refreshTemplatesButton_Click(object sender, EventArgs e)
        {
            BindTemplateFolderCombobox();
        }
    }
}
