namespace FishingBotFoffosEdition
{
    partial class MainForm
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
            this.BindFishingKey = new System.Windows.Forms.Button();
            this.keyBindingFishingLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.logTextBox = new System.Windows.Forms.RichTextBox();
            this.outputVolumeTrackerLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.VolumeTresholdNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.statsRichTextBox = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.VolumeTresholdNumericUpDown)).BeginInit();
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
            this.xLabel.Location = new System.Drawing.Point(12, 380);
            this.xLabel.Name = "xLabel";
            this.xLabel.Size = new System.Drawing.Size(131, 13);
            this.xLabel.TabIndex = 2;
            this.xLabel.Text = "MOUSE POS TEMPLATE";
            // 
            // BindFishingKey
            // 
            this.BindFishingKey.Location = new System.Drawing.Point(32, 208);
            this.BindFishingKey.Name = "BindFishingKey";
            this.BindFishingKey.Size = new System.Drawing.Size(133, 23);
            this.BindFishingKey.TabIndex = 4;
            this.BindFishingKey.Text = "Bind Fishing Key";
            this.BindFishingKey.UseVisualStyleBackColor = true;
            this.BindFishingKey.Click += new System.EventHandler(this.BindFishingKey_Click);
            // 
            // keyBindingFishingLabel
            // 
            this.keyBindingFishingLabel.AutoSize = true;
            this.keyBindingFishingLabel.Location = new System.Drawing.Point(29, 247);
            this.keyBindingFishingLabel.Name = "keyBindingFishingLabel";
            this.keyBindingFishingLabel.Size = new System.Drawing.Size(56, 13);
            this.keyBindingFishingLabel.TabIndex = 5;
            this.keyBindingFishingLabel.Text = "no key set";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(32, 157);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "TEST BUTTON";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // stopButton
            // 
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
            this.logTextBox.Size = new System.Drawing.Size(372, 366);
            this.logTextBox.TabIndex = 9;
            this.logTextBox.Text = "";
            // 
            // outputVolumeTrackerLabel
            // 
            this.outputVolumeTrackerLabel.AutoSize = true;
            this.outputVolumeTrackerLabel.Location = new System.Drawing.Point(29, 287);
            this.outputVolumeTrackerLabel.Name = "outputVolumeTrackerLabel";
            this.outputVolumeTrackerLabel.Size = new System.Drawing.Size(135, 13);
            this.outputVolumeTrackerLabel.TabIndex = 11;
            this.outputVolumeTrackerLabel.Text = "outputVolumeTrackerLabel";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 324);
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
            this.VolumeTresholdNumericUpDown.Location = new System.Drawing.Point(32, 340);
            this.VolumeTresholdNumericUpDown.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.VolumeTresholdNumericUpDown.Name = "VolumeTresholdNumericUpDown";
            this.VolumeTresholdNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.VolumeTresholdNumericUpDown.TabIndex = 14;
            this.VolumeTresholdNumericUpDown.ValueChanged += new System.EventHandler(this.VolumeTresholdNumericUpDown_valueChanged);
            // 
            // statsRichTextBox
            // 
            this.statsRichTextBox.Location = new System.Drawing.Point(600, 27);
            this.statsRichTextBox.Name = "statsRichTextBox";
            this.statsRichTextBox.Size = new System.Drawing.Size(148, 140);
            this.statsRichTextBox.TabIndex = 15;
            this.statsRichTextBox.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 405);
            this.Controls.Add(this.statsRichTextBox);
            this.Controls.Add(this.VolumeTresholdNumericUpDown);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.outputVolumeTrackerLabel);
            this.Controls.Add(this.logTextBox);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.keyBindingFishingLabel);
            this.Controls.Add(this.BindFishingKey);
            this.Controls.Add(this.xLabel);
            this.Controls.Add(this.RunButton);
            this.Name = "MainForm";
            this.Text = "Foffos Fishing Bot Wow 3.3.5 Beta 0.1.2";
            ((System.ComponentModel.ISupportInitialize)(this.VolumeTresholdNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button RunButton;
        private System.Windows.Forms.Label xLabel;
        private System.Windows.Forms.Button BindFishingKey;
        private System.Windows.Forms.Label keyBindingFishingLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.RichTextBox logTextBox;
        private System.Windows.Forms.Label outputVolumeTrackerLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown VolumeTresholdNumericUpDown;
        private System.Windows.Forms.RichTextBox statsRichTextBox;
    }
}

