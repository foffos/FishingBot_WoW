using CSCore.CoreAudioAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FishingBotFoffosEdition.Properties;
using System.IO;

namespace FishingBotFoffosEdition
{
    public partial class MainForm : Form
    {
        public char fishingChar;
        public Form popupForm;
        public static int DURATION_MINUTES = 15;
        public Task currentExecutionTask;
        public CancellationTokenSource tokenSource;

        public Core core;
        public MainForm()
        {

            InitializeComponent();

            popupForm = new Form();
            TextBox keyTextbox = new TextBox();
            keyTextbox.KeyPress += myKeyPressEvent;
            popupForm.Controls.Add(keyTextbox);

            StartCursorPosTrackerTimer();
            StartAudioMonitorTimer();
            StartExecutionMonitorTimer();
            StartTimerMonitorTimer();
            //Thread refreshMousePosThread = new Thread(DoWork);
            //refreshMousePosThread.Start();

            core = new Core();

            tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;
            ExecutionNumericUpDown.Value = core.executionInfo.TaskToExecute;

            Task t1 = Task.Factory.StartNew(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    core.ExecuteMonitorAudio();
                }
            },
            token);

            VolumeTresholdNumericUpDown.Value = core.GetAudioThreshold();
            audioDeviceComboBox.Items.AddRange(AudioManager.GetRenderDevices().ToArray());
            audioDeviceComboBox.SelectedIndex = audioDeviceComboBox.FindString(AudioManager.GetDefaultRenderDevice().FriendlyName);

            loadConfiguration();

        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            if (audioDeviceComboBox.SelectedIndex != -1)
                core.changeAudioDevice((MMDevice)audioDeviceComboBox.SelectedItem);

            audioDeviceComboBox.Enabled = false;
            tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;
            Task t1 = Task.Factory.StartNew(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    core.ExecuteMainBotFunction(fishingChar, appendLog);
                }
            },
            token);
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


        System.Windows.Forms.Timer cursorPosTrackerTimer = null;
        private void StartCursorPosTrackerTimer()
        {
            cursorPosTrackerTimer = new System.Windows.Forms.Timer();
            cursorPosTrackerTimer.Interval = 100;
            cursorPosTrackerTimer.Tick += new EventHandler(CursorPosTracker_TickEvent);
            cursorPosTrackerTimer.Enabled = true;
        }

        System.Windows.Forms.Timer audioMonitorTimer = null;
        private void StartAudioMonitorTimer()
        {
            audioMonitorTimer = new System.Windows.Forms.Timer();
            audioMonitorTimer.Interval = 100;
            audioMonitorTimer.Tick += new EventHandler(SoundMonitor_TickEvent);
            audioMonitorTimer.Enabled = true;
        }

        System.Windows.Forms.Timer executionTimer = null;
        private void StartExecutionMonitorTimer()
        {
            executionTimer = new System.Windows.Forms.Timer();
            executionTimer.Interval = 100;
            executionTimer.Tick += new EventHandler(ExecuteMonitor_TickEvent);
            executionTimer.Enabled = true;
        }

        System.Windows.Forms.Timer timerTimer = null;
        private void StartTimerMonitorTimer()
        {
            timerTimer = new System.Windows.Forms.Timer();
            timerTimer.Interval = 80;
            timerTimer.Tick += new EventHandler(TimerUpdate_TickEvent);
            timerTimer.Enabled = true;
        }

        void ExecuteMonitor_TickEvent(object sender, EventArgs e)
        {
            refreshExecuteInfo();
        }

        void SoundMonitor_TickEvent(object sender, EventArgs e)
        {
            refreshAudioMonitor();
        }

        void CursorPosTracker_TickEvent(object sender, EventArgs e)
        {
            refreshCoordinates();
        }

        void TimerUpdate_TickEvent(object sender, EventArgs e)
        {
            refreshTimer();
        }


        private void BindFishingKey_Click(object sender, EventArgs e)
        {
            popupForm.Show(this);
        }

        private void myKeyPressEvent(object sender, KeyPressEventArgs e)
        {
            fishingChar = e.KeyChar;
            keyBindingFishingLabel.Text = "" + fishingChar;
            popupForm.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //var a = Resources.ResourceManager.GetString("TemplateFolder");
            //var files = Directory.GetFiles(a);
            //Console.WriteLine(files);
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            tokenSource.Cancel();
            //core = new Core();
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
            File.WriteAllText(Path.Combine(Resources.TempFolder, $"ExecutionLog_{DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss")}.txt"), statsRichTextBox.Text + "\n----------------------------\n"+ logTextBox.Text);
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
            Settings.Default.Save();
        }

        private void loadConfiguration()
        {
            VolumeTresholdNumericUpDown.Value = Settings.Default.DefaultVolumeTreshold;
            ExecutionNumericUpDown.Value = Settings.Default.DefaultExecuteTaskNumber;
            audioDeviceComboBox.SelectedIndex = Settings.Default.DefaultDevice;
        }
    }
}
