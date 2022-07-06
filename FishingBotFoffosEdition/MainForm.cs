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
            //Thread refreshMousePosThread = new Thread(DoWork);
            //refreshMousePosThread.Start();

            core = new Core();

            tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            Task t1 = Task.Factory.StartNew(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    core.ExecuteMonitorAudio();
                }
            },
            token);

            VolumeTresholdNumericUpDown.Value = core.GetAudioThreshold();

        }

        private void RunButton_Click(object sender, EventArgs e)
        {
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
            outputVolumeTrackerLabel.Text = $"Audio Level: {core.audioVolume.ToString("0.000000")}";
        }

        public void refreshExecuteInfo()
        {
            statsRichTextBox.Text = String.Empty;
            statsRichTextBox.AppendText($"Tasks to Execute: {core.executionInfo.TaskToExecute}");
            statsRichTextBox.AppendText("\r\n" + $"Tasks Executed: {core.executionInfo.TeskExecuted}");
            statsRichTextBox.AppendText("\r\n" + $"Tasks Success: {core.executionInfo.TaskSuccess}");
            statsRichTextBox.AppendText("\r\n" + $"Tasks Erros: {core.executionInfo.TaskErrors}");
        }


        System.Windows.Forms.Timer t = null;
        private void StartCursorPosTrackerTimer()
        {
            t = new System.Windows.Forms.Timer();
            t.Interval = 100;
            t.Tick += new EventHandler(CursorPosTracker_TickEvent);
            t.Enabled = true;
        }

        System.Windows.Forms.Timer executionTimer = null;
        private void StartAudioMonitorTimer()
        {
            executionTimer = new System.Windows.Forms.Timer();
            executionTimer.Interval = 100;
            executionTimer.Tick += new EventHandler(SoundMonitor_TickEvent);
            executionTimer.Enabled = true;
        }


        private void StartExecutionMonitorTimer()
        {
            executionTimer = new System.Windows.Forms.Timer();
            executionTimer.Interval = 1000;
            executionTimer.Tick += new EventHandler(ExecuteMonitor_TickEvent);
            executionTimer.Enabled = true;
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
            var a = Resources.ResourceManager.GetString("TemplateFolder");
            var files = Directory.GetFiles(a);
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
    }
}
