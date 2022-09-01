using FishingBotFoffosEdition.Properties;

namespace FishingBotFoffosEdition
{
    partial class FishingBotForm
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
            this.saveConfigurationButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.ForceLureFindingCheckBox = new System.Windows.Forms.CheckBox();
            this.FindingPrecisionNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.clearExecutionInfoButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.OptionsStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteTempFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTemplateFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runDebugLureFinderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OffsetXnumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.OffsetYnumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.templateFolderComboBox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.refreshTemplatesButton = new System.Windows.Forms.Button();
            this.FileInfoLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.VolumeTresholdNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExecutionNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FindingPrecisionNumericUpDown)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OffsetXnumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OffsetYnumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // RunButton
            // 
            this.RunButton.Location = new System.Drawing.Point(31, 55);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(128, 47);
            this.RunButton.TabIndex = 1;
            this.RunButton.Text = "Stat Fishing";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // xLabel
            // 
            this.xLabel.AutoSize = true;
            this.xLabel.Location = new System.Drawing.Point(218, 422);
            this.xLabel.Name = "xLabel";
            this.xLabel.Size = new System.Drawing.Size(131, 13);
            this.xLabel.TabIndex = 2;
            this.xLabel.Text = "MOUSE POS TEMPLATE";
            // 
            // keyBindingFishingLabel
            // 
            this.keyBindingFishingLabel.AutoSize = true;
            this.keyBindingFishingLabel.Location = new System.Drawing.Point(59, 154);
            this.keyBindingFishingLabel.Name = "keyBindingFishingLabel";
            this.keyBindingFishingLabel.Size = new System.Drawing.Size(64, 13);
            this.keyBindingFishingLabel.TabIndex = 5;
            this.keyBindingFishingLabel.Text = "Fishing Key:";
            // 
            // stopButton
            // 
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(31, 108);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(128, 23);
            this.stopButton.TabIndex = 8;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // logTextBox
            // 
            this.logTextBox.Location = new System.Drawing.Point(198, 52);
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.Size = new System.Drawing.Size(372, 345);
            this.logTextBox.TabIndex = 9;
            this.logTextBox.Text = "";
            // 
            // outputVolumeTrackerLabel
            // 
            this.outputVolumeTrackerLabel.AutoSize = true;
            this.outputVolumeTrackerLabel.Location = new System.Drawing.Point(50, 199);
            this.outputVolumeTrackerLabel.Name = "outputVolumeTrackerLabel";
            this.outputVolumeTrackerLabel.Size = new System.Drawing.Size(76, 13);
            this.outputVolumeTrackerLabel.TabIndex = 11;
            this.outputVolumeTrackerLabel.Text = "Output Device";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 332);
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
            this.VolumeTresholdNumericUpDown.Location = new System.Drawing.Point(109, 330);
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
            this.statsRichTextBox.Location = new System.Drawing.Point(576, 119);
            this.statsRichTextBox.Name = "statsRichTextBox";
            this.statsRichTextBox.Size = new System.Drawing.Size(171, 93);
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
            this.ExecutionNumericUpDown.Location = new System.Drawing.Point(586, 82);
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
            this.label2.Location = new System.Drawing.Point(583, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Executions:";
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(62, 170);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(52, 20);
            this.textBox1.TabIndex = 18;
            this.textBox1.Text = "1";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // timerTextBox
            // 
            this.timerTextBox.Enabled = false;
            this.timerTextBox.Location = new System.Drawing.Point(610, 242);
            this.timerTextBox.Name = "timerTextBox";
            this.timerTextBox.Size = new System.Drawing.Size(100, 20);
            this.timerTextBox.TabIndex = 20;
            this.timerTextBox.Text = "0";
            this.timerTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(617, 226);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Execution Time:";
            // 
            // iterationTimerTextBox
            // 
            this.iterationTimerTextBox.Enabled = false;
            this.iterationTimerTextBox.Location = new System.Drawing.Point(677, 82);
            this.iterationTimerTextBox.Name = "iterationTimerTextBox";
            this.iterationTimerTextBox.Size = new System.Drawing.Size(62, 20);
            this.iterationTimerTextBox.TabIndex = 22;
            this.iterationTimerTextBox.Text = "0";
            this.iterationTimerTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(674, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Iteration timer:";
            // 
            // audioDeviceComboBox
            // 
            this.audioDeviceComboBox.FormattingEnabled = true;
            this.audioDeviceComboBox.Location = new System.Drawing.Point(31, 215);
            this.audioDeviceComboBox.Name = "audioDeviceComboBox";
            this.audioDeviceComboBox.Size = new System.Drawing.Size(121, 21);
            this.audioDeviceComboBox.TabIndex = 24;
            this.audioDeviceComboBox.SelectedIndexChanged += new System.EventHandler(this.audioDeviceComboBox_SelectedIndexChanged);
            // 
            // saveConfigurationButton
            // 
            this.saveConfigurationButton.Location = new System.Drawing.Point(584, 273);
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
            this.label5.Location = new System.Drawing.Point(182, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "Execution Info:";
            // 
            // ForceLureFindingCheckBox
            // 
            this.ForceLureFindingCheckBox.AutoSize = true;
            this.ForceLureFindingCheckBox.Location = new System.Drawing.Point(14, 261);
            this.ForceLureFindingCheckBox.Name = "ForceLureFindingCheckBox";
            this.ForceLureFindingCheckBox.Size = new System.Drawing.Size(107, 17);
            this.ForceLureFindingCheckBox.TabIndex = 29;
            this.ForceLureFindingCheckBox.Text = "Force lure finding";
            this.ForceLureFindingCheckBox.UseVisualStyleBackColor = true;
            this.ForceLureFindingCheckBox.CheckedChanged += new System.EventHandler(this.ForceLureFindingCheckBox_CheckedChanged);
            // 
            // FindingPrecisionNumericUpDown
            // 
            this.FindingPrecisionNumericUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.FindingPrecisionNumericUpDown.Location = new System.Drawing.Point(131, 289);
            this.FindingPrecisionNumericUpDown.Name = "FindingPrecisionNumericUpDown";
            this.FindingPrecisionNumericUpDown.Size = new System.Drawing.Size(45, 20);
            this.FindingPrecisionNumericUpDown.TabIndex = 30;
            this.FindingPrecisionNumericUpDown.ValueChanged += new System.EventHandler(this.FindingPrecisionNumericUpDown_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 291);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "Min precision required";
            // 
            // clearExecutionInfoButton
            // 
            this.clearExecutionInfoButton.Location = new System.Drawing.Point(457, 403);
            this.clearExecutionInfoButton.Name = "clearExecutionInfoButton";
            this.clearExecutionInfoButton.Size = new System.Drawing.Size(113, 23);
            this.clearExecutionInfoButton.TabIndex = 32;
            this.clearExecutionInfoButton.Text = "Clear Execution Info";
            this.clearExecutionInfoButton.UseVisualStyleBackColor = true;
            this.clearExecutionInfoButton.Click += new System.EventHandler(this.clearExecutionInfoButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OptionsStripMenuItem,
            this.configurationToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(760, 24);
            this.menuStrip1.TabIndex = 33;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // OptionsStripMenuItem
            // 
            this.OptionsStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteTempFilesToolStripMenuItem,
            this.exportLogToolStripMenuItem});
            this.OptionsStripMenuItem.Name = "OptionsStripMenuItem";
            this.OptionsStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.OptionsStripMenuItem.Text = "Options";
            // 
            // deleteTempFilesToolStripMenuItem
            // 
            this.deleteTempFilesToolStripMenuItem.Name = "deleteTempFilesToolStripMenuItem";
            this.deleteTempFilesToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.deleteTempFilesToolStripMenuItem.Text = "Delete temp files";
            this.deleteTempFilesToolStripMenuItem.Click += new System.EventHandler(this.deleteTempFilesToolStripMenuItem_Click);
            // 
            // exportLogToolStripMenuItem
            // 
            this.exportLogToolStripMenuItem.Name = "exportLogToolStripMenuItem";
            this.exportLogToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.exportLogToolStripMenuItem.Text = "Export Log";
            this.exportLogToolStripMenuItem.Click += new System.EventHandler(this.exportLogToolStripMenuItem_Click);
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openTemplateFolderToolStripMenuItem,
            this.runDebugLureFinderToolStripMenuItem});
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            this.configurationToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.configurationToolStripMenuItem.Text = "Configuration";
            // 
            // openTemplateFolderToolStripMenuItem
            // 
            this.openTemplateFolderToolStripMenuItem.Name = "openTemplateFolderToolStripMenuItem";
            this.openTemplateFolderToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.openTemplateFolderToolStripMenuItem.Text = "Open template folder";
            this.openTemplateFolderToolStripMenuItem.Click += new System.EventHandler(this.openTemplateFolderToolStripMenuItem_Click);
            // 
            // runDebugLureFinderToolStripMenuItem
            // 
            this.runDebugLureFinderToolStripMenuItem.Name = "runDebugLureFinderToolStripMenuItem";
            this.runDebugLureFinderToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.runDebugLureFinderToolStripMenuItem.Text = "Run debug lure finder";
            this.runDebugLureFinderToolStripMenuItem.Click += new System.EventHandler(this.runDebugLureFinderToolStripMenuItem_Click);
            // 
            // OffsetXnumericUpDown
            // 
            this.OffsetXnumericUpDown.Location = new System.Drawing.Point(598, 351);
            this.OffsetXnumericUpDown.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.OffsetXnumericUpDown.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.OffsetXnumericUpDown.Name = "OffsetXnumericUpDown";
            this.OffsetXnumericUpDown.Size = new System.Drawing.Size(59, 20);
            this.OffsetXnumericUpDown.TabIndex = 34;
            this.OffsetXnumericUpDown.ValueChanged += new System.EventHandler(this.OffestXnumericUpDown_ValueChanged);
            // 
            // OffsetYnumericUpDown
            // 
            this.OffsetYnumericUpDown.Location = new System.Drawing.Point(663, 351);
            this.OffsetYnumericUpDown.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.OffsetYnumericUpDown.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.OffsetYnumericUpDown.Name = "OffsetYnumericUpDown";
            this.OffsetYnumericUpDown.Size = new System.Drawing.Size(59, 20);
            this.OffsetYnumericUpDown.TabIndex = 35;
            this.OffsetYnumericUpDown.ValueChanged += new System.EventHandler(this.OffestYnumericUpDown_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(607, 319);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 13);
            this.label7.TabIndex = 36;
            this.label7.Text = "X and Y click Offsets";
            // 
            // templateFolderComboBox
            // 
            this.templateFolderComboBox.FormattingEnabled = true;
            this.templateFolderComboBox.Location = new System.Drawing.Point(12, 403);
            this.templateFolderComboBox.Name = "templateFolderComboBox";
            this.templateFolderComboBox.Size = new System.Drawing.Size(145, 21);
            this.templateFolderComboBox.TabIndex = 37;
            this.templateFolderComboBox.SelectedIndexChanged += new System.EventHandler(this.templateFolderComboBox_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(34, 384);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 38;
            this.label8.Text = "Template Folder";
            // 
            // refreshTemplatesButton
            // 
            this.refreshTemplatesButton.Location = new System.Drawing.Point(163, 401);
            this.refreshTemplatesButton.Name = "refreshTemplatesButton";
            this.refreshTemplatesButton.Size = new System.Drawing.Size(24, 23);
            this.refreshTemplatesButton.TabIndex = 39;
            this.refreshTemplatesButton.Text = "R";
            this.refreshTemplatesButton.UseVisualStyleBackColor = true;
            this.refreshTemplatesButton.Click += new System.EventHandler(this.refreshTemplatesButton_Click);
            // 
            // FileInfoLabel
            // 
            this.FileInfoLabel.AutoSize = true;
            this.FileInfoLabel.Location = new System.Drawing.Point(9, 427);
            this.FileInfoLabel.Name = "FileInfoLabel";
            this.FileInfoLabel.Size = new System.Drawing.Size(0, 13);
            this.FileInfoLabel.TabIndex = 40;
            // 
            // FishingBotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 461);
            this.Controls.Add(this.FileInfoLabel);
            this.Controls.Add(this.refreshTemplatesButton);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.templateFolderComboBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.OffsetYnumericUpDown);
            this.Controls.Add(this.OffsetXnumericUpDown);
            this.Controls.Add(this.clearExecutionInfoButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.FindingPrecisionNumericUpDown);
            this.Controls.Add(this.ForceLureFindingCheckBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.saveConfigurationButton);
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
            this.Controls.Add(this.menuStrip1);
            this.Icon = global::FishingBotFoffosEdition.Properties.Resources.Icon;
            this.Name = "FishingBotForm";
            this.Text = "Foffos Fishing Bot Wow 3.3.5 Beta 0.2.2";
            ((System.ComponentModel.ISupportInitialize)(this.VolumeTresholdNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExecutionNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FindingPrecisionNumericUpDown)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OffsetXnumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OffsetYnumericUpDown)).EndInit();
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
        private System.Windows.Forms.Button saveConfigurationButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox ForceLureFindingCheckBox;
        private System.Windows.Forms.NumericUpDown FindingPrecisionNumericUpDown;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button clearExecutionInfoButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem OptionsStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteTempFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openTemplateFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runDebugLureFinderToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown OffsetXnumericUpDown;
        private System.Windows.Forms.NumericUpDown OffsetYnumericUpDown;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox templateFolderComboBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button refreshTemplatesButton;
        private System.Windows.Forms.Label FileInfoLabel;
    }
}

