using FishingBotFoffosEdition.Properties;

namespace FishingBotFoffosEdition
{
    partial class FishingBotGUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.RunButton = new System.Windows.Forms.Button();
            this.xLabel = new System.Windows.Forms.Label();
            this.keyBindingFishingLabel = new System.Windows.Forms.Label();
            this.stopButton = new System.Windows.Forms.Button();
            this.logTextBox = new System.Windows.Forms.RichTextBox();
            this.outputVolumeTrackerLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.VolumeTresholdNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.statsRichTextBox = new System.Windows.Forms.RichTextBox();
            this.ExecutionNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.timerTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.iterationTimerTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.audioDeviceComboBox = new System.Windows.Forms.ComboBox();
            this.exportLogButton = new System.Windows.Forms.Button();
            this.delTempFilesButton = new System.Windows.Forms.Button();
            this.saveConfigurationButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.ForceLureFindingCheckBox = new System.Windows.Forms.CheckBox();
            this.FindingPrecisionNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.clearExecutionInfoButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.VolumeTresholdNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExecutionNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FindingPrecisionNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // RunButton
            // 
            this.RunButton.Location = new System.Drawing.Point(32, 27);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(128, 71);
            this.RunButton.TabIndex = 1;
            this.RunButton.Text = "Stat Fishing";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // xLabel
            // 
            this.xLabel.AutoSize = true;
            this.xLabel.Location = new System.Drawing.Point(12, 370);
            this.xLabel.Name = "xLabel";
            this.xLabel.Size = new System.Drawing.Size(131, 13);
            this.xLabel.TabIndex = 2;
            this.xLabel.Text = "MOUSE POS TEMPLATE";
            // 
            // keyBindingFishingLabel
            // 
            this.keyBindingFishingLabel.AutoSize = true;
            this.keyBindingFishingLabel.Location = new System.Drawing.Point(60, 139);
            this.keyBindingFishingLabel.Name = "keyBindingFishingLabel";
            this.keyBindingFishingLabel.Size = new System.Drawing.Size(64, 13);
            this.keyBindingFishingLabel.TabIndex = 5;
            this.keyBindingFishingLabel.Text = "Fishing Key:";
            // 
            // stopButton
            // 
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(32, 104);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(128, 23);
            this.stopButton.TabIndex = 8;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // logTextBox
            // 
            this.logTextBox.Location = new System.Drawing.Point(186, 27);
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.Size = new System.Drawing.Size(372, 345);
            this.logTextBox.TabIndex = 9;
            this.logTextBox.Text = "";
            // 
            // outputVolumeTrackerLabel
            // 
            this.outputVolumeTrackerLabel.AutoSize = true;
            this.outputVolumeTrackerLabel.Location = new System.Drawing.Point(51, 301);
            this.outputVolumeTrackerLabel.Name = "outputVolumeTrackerLabel";
            this.outputVolumeTrackerLabel.Size = new System.Drawing.Size(73, 13);
            this.outputVolumeTrackerLabel.TabIndex = 11;
            this.outputVolumeTrackerLabel.Text = "audio treshold";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 330);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Volume Threshold";
            // 
            // VolumeTresholdNumericUpDown
            // 
            this.VolumeTresholdNumericUpDown.DecimalPlaces = 2;
            this.VolumeTresholdNumericUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.VolumeTresholdNumericUpDown.Location = new System.Drawing.Point(102, 328);
            this.VolumeTresholdNumericUpDown.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.VolumeTresholdNumericUpDown.Name = "VolumeTresholdNumericUpDown";
            this.VolumeTresholdNumericUpDown.Size = new System.Drawing.Size(67, 20);
            this.VolumeTresholdNumericUpDown.TabIndex = 14;
            this.VolumeTresholdNumericUpDown.ValueChanged += new System.EventHandler(this.VolumeTresholdNumericUpDown_valueChanged);
            // 
            // statsRichTextBox
            // 
            this.statsRichTextBox.Location = new System.Drawing.Point(577, 91);
            this.statsRichTextBox.Name = "statsRichTextBox";
            this.statsRichTextBox.Size = new System.Drawing.Size(171, 140);
            this.statsRichTextBox.TabIndex = 15;
            this.statsRichTextBox.Text = "";
            // 
            // ExecutionNumericUpDown
            // 
            this.ExecutionNumericUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.ExecutionNumericUpDown.Location = new System.Drawing.Point(587, 54);
            this.ExecutionNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.ExecutionNumericUpDown.Name = "ExecutionNumericUpDown";
            this.ExecutionNumericUpDown.Size = new System.Drawing.Size(59, 20);
            this.ExecutionNumericUpDown.TabIndex = 16;
            this.ExecutionNumericUpDown.ValueChanged += new System.EventHandler(this.ExecutionNumericUpDown_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(584, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Executions:";
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(63, 155);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(52, 20);
            this.textBox1.TabIndex = 18;
            this.textBox1.Text = "1";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // timerTextBox
            // 
            this.timerTextBox.Enabled = false;
            this.timerTextBox.Location = new System.Drawing.Point(611, 261);
            this.timerTextBox.Name = "timerTextBox";
            this.timerTextBox.Size = new System.Drawing.Size(100, 20);
            this.timerTextBox.TabIndex = 20;
            this.timerTextBox.Text = "0";
            this.timerTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(619, 245);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Execution Time:";
            // 
            // iterationTimerTextBox
            // 
            this.iterationTimerTextBox.Enabled = false;
            this.iterationTimerTextBox.Location = new System.Drawing.Point(678, 54);
            this.iterationTimerTextBox.Name = "iterationTimerTextBox";
            this.iterationTimerTextBox.Size = new System.Drawing.Size(62, 20);
            this.iterationTimerTextBox.TabIndex = 22;
            this.iterationTimerTextBox.Text = "0";
            this.iterationTimerTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(675, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Iteration timer:";
            // 
            // audioDeviceComboBox
            // 
            this.audioDeviceComboBox.FormattingEnabled = true;
            this.audioDeviceComboBox.Location = new System.Drawing.Point(32, 181);
            this.audioDeviceComboBox.Name = "audioDeviceComboBox";
            this.audioDeviceComboBox.Size = new System.Drawing.Size(121, 21);
            this.audioDeviceComboBox.TabIndex = 24;
            this.audioDeviceComboBox.SelectedIndexChanged += new System.EventHandler(this.audioDeviceComboBox_SelectedIndexChanged);
            // 
            // exportLogButton
            // 
            this.exportLogButton.Location = new System.Drawing.Point(622, 341);
            this.exportLogButton.Name = "exportLogButton";
            this.exportLogButton.Size = new System.Drawing.Size(75, 23);
            this.exportLogButton.TabIndex = 25;
            this.exportLogButton.Text = "Export Log";
            this.exportLogButton.UseVisualStyleBackColor = true;
            this.exportLogButton.Click += new System.EventHandler(this.exportLogButton_Click);
            // 
            // delTempFilesButton
            // 
            this.delTempFilesButton.Location = new System.Drawing.Point(606, 370);
            this.delTempFilesButton.Name = "delTempFilesButton";
            this.delTempFilesButton.Size = new System.Drawing.Size(105, 23);
            this.delTempFilesButton.TabIndex = 26;
            this.delTempFilesButton.Text = "Delete temp files";
            this.delTempFilesButton.UseVisualStyleBackColor = true;
            this.delTempFilesButton.Click += new System.EventHandler(this.delTempFilesButton_Click);
            // 
            // saveConfigurationButton
            // 
            this.saveConfigurationButton.Location = new System.Drawing.Point(577, 312);
            this.saveConfigurationButton.Name = "saveConfigurationButton";
            this.saveConfigurationButton.Size = new System.Drawing.Size(163, 23);
            this.saveConfigurationButton.TabIndex = 27;
            this.saveConfigurationButton.Text = "Save this configuration";
            this.saveConfigurationButton.UseVisualStyleBackColor = true;
            this.saveConfigurationButton.Click += new System.EventHandler(this.saveConfigurationButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(183, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "Execution Info:";
            // 
            // ForceLureFindingCheckBox
            // 
            this.ForceLureFindingCheckBox.AutoSize = true;
            this.ForceLureFindingCheckBox.Location = new System.Drawing.Point(32, 214);
            this.ForceLureFindingCheckBox.Name = "ForceLureFindingCheckBox";
            this.ForceLureFindingCheckBox.Size = new System.Drawing.Size(107, 17);
            this.ForceLureFindingCheckBox.TabIndex = 29;
            this.ForceLureFindingCheckBox.Text = "Force lure finding";
            this.ForceLureFindingCheckBox.UseVisualStyleBackColor = true;
            this.ForceLureFindingCheckBox.CheckedChanged += new System.EventHandler(this.ForceLureFindingCheckBox_CheckedChanged);
            // 
            // FindingPrecisionNumericUpDown
            // 
            this.FindingPrecisionNumericUpDown.DecimalPlaces = 2;
            this.FindingPrecisionNumericUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.FindingPrecisionNumericUpDown.Location = new System.Drawing.Point(54, 262);
            this.FindingPrecisionNumericUpDown.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.FindingPrecisionNumericUpDown.Name = "FindingPrecisionNumericUpDown";
            this.FindingPrecisionNumericUpDown.Size = new System.Drawing.Size(67, 20);
            this.FindingPrecisionNumericUpDown.TabIndex = 30;
            this.FindingPrecisionNumericUpDown.ValueChanged += new System.EventHandler(this.FindingPrecisionNumericUpDown_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 245);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(127, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "minimal precision required";
            // 
            // clearExecutionInfoButton
            // 
            this.clearExecutionInfoButton.Location = new System.Drawing.Point(436, 378);
            this.clearExecutionInfoButton.Name = "clearExecutionInfoButton";
            this.clearExecutionInfoButton.Size = new System.Drawing.Size(113, 23);
            this.clearExecutionInfoButton.TabIndex = 32;
            this.clearExecutionInfoButton.Text = "Clear Execution Info";
            this.clearExecutionInfoButton.UseVisualStyleBackColor = true;
            this.clearExecutionInfoButton.Click += new System.EventHandler(this.clearExecutionInfoButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 405);
            this.Controls.Add(this.clearExecutionInfoButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.FindingPrecisionNumericUpDown);
            this.Controls.Add(this.ForceLureFindingCheckBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.saveConfigurationButton);
            this.Controls.Add(this.delTempFilesButton);
            this.Controls.Add(this.exportLogButton);
            this.Controls.Add(this.audioDeviceComboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.iterationTimerTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.timerTextBox);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ExecutionNumericUpDown);
            this.Controls.Add(this.statsRichTextBox);
            this.Controls.Add(this.VolumeTresholdNumericUpDown);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.outputVolumeTrackerLabel);
            this.Controls.Add(this.logTextBox);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.keyBindingFishingLabel);
            this.Controls.Add(this.xLabel);
            this.Controls.Add(this.RunButton);
            this.Icon = global::FishingBotFoffosEdition.Properties.Resources.Icon;
            this.Name = "MainForm";
            this.Text = "Foffos Fishing Bot Wow 3.3.5 Beta 0.2.1";
            ((System.ComponentModel.ISupportInitialize)(this.VolumeTresholdNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExecutionNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FindingPrecisionNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button RunButton;
        private System.Windows.Forms.Label xLabel;
        private System.Windows.Forms.Label keyBindingFishingLabel;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.RichTextBox logTextBox;
        private System.Windows.Forms.Label outputVolumeTrackerLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown VolumeTresholdNumericUpDown;
        private System.Windows.Forms.RichTextBox statsRichTextBox;
        private System.Windows.Forms.NumericUpDown ExecutionNumericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox timerTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox iterationTimerTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox audioDeviceComboBox;
        private System.Windows.Forms.Button exportLogButton;
        private System.Windows.Forms.Button delTempFilesButton;
        private System.Windows.Forms.Button saveConfigurationButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox ForceLureFindingCheckBox;
        private System.Windows.Forms.NumericUpDown FindingPrecisionNumericUpDown;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button clearExecutionInfoButton;
    }
}

