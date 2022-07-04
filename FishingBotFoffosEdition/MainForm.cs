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

namespace FishingBotFoffosEdition
{
    public partial class MainForm : Form
    {
        public char fishingChar;
        public Form popupForm;
        public MainForm()
        {
            InitializeComponent();

            popupForm = new Form();
            TextBox keyTextbox = new TextBox();
            keyTextbox.KeyPress += myKeyPressEvent;
            popupForm.Controls.Add(keyTextbox);

            StartTimer();
            //Thread refreshMousePosThread = new Thread(DoWork);
            //refreshMousePosThread.Start();
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            Core.ExecuteMainBotFunction(fishingChar);
        }
        public void refreshCoordinates()
        {
            var newMousePos = MouseUtility.GetCurrentCursorPosition();
            xLabel.Text = $"X: {newMousePos.X}    Y:{newMousePos.Y}";
        }

        System.Windows.Forms.Timer t = null;
        private void StartTimer()
        {
            t = new System.Windows.Forms.Timer();
            t.Interval = 100;
            t.Tick += new EventHandler(t_Tick);
            t.Enabled = true;
        }

        void t_Tick(object sender, EventArgs e)
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
            keyBindingFishingLabel.Text = ""+fishingChar;
            popupForm.Close();
        }

        [DllImport("winm.dll")]
        private static extern long mciSendString(string command, StringBuilder retstring, int Returnlenth, IntPtr callback);

    }
}
