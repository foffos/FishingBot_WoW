namespace FishingBotFoffosEdition
{
    partial class ImageViewerForm
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
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.imageInfoRichTextBox = new System.Windows.Forms.RichTextBox();
			this.nextButton = new System.Windows.Forms.Button();
			this.previewPictureBox = new System.Windows.Forms.PictureBox();
			this.widthNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.heigtNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.startPicLureButton = new System.Windows.Forms.Button();
			this.templateNametextBox = new System.Windows.Forms.TextBox();
			this.saveTemplateButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.saveInfoLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.previewPictureBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.widthNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.heigtNumericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox
			// 
			this.pictureBox.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.pictureBox.Location = new System.Drawing.Point(12, 12);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(960, 540);
			this.pictureBox.TabIndex = 3;
			this.pictureBox.TabStop = false;
			this.pictureBox.Click += new System.EventHandler(this.pictureBox_Click);
			this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
			// 
			// imageInfoRichTextBox
			// 
			this.imageInfoRichTextBox.Location = new System.Drawing.Point(979, 13);
			this.imageInfoRichTextBox.Name = "imageInfoRichTextBox";
			this.imageInfoRichTextBox.Size = new System.Drawing.Size(360, 80);
			this.imageInfoRichTextBox.TabIndex = 4;
			this.imageInfoRichTextBox.Text = "";
			// 
			// nextButton
			// 
			this.nextButton.Location = new System.Drawing.Point(979, 99);
			this.nextButton.Name = "nextButton";
			this.nextButton.Size = new System.Drawing.Size(100, 33);
			this.nextButton.TabIndex = 5;
			this.nextButton.Text = "Next";
			this.nextButton.UseVisualStyleBackColor = true;
			this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
			// 
			// previewPictureBox
			// 
			this.previewPictureBox.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.previewPictureBox.Location = new System.Drawing.Point(979, 328);
			this.previewPictureBox.Name = "previewPictureBox";
			this.previewPictureBox.Size = new System.Drawing.Size(200, 200);
			this.previewPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.previewPictureBox.TabIndex = 6;
			this.previewPictureBox.TabStop = false;
			// 
			// widthNumericUpDown
			// 
			this.widthNumericUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.widthNumericUpDown.Location = new System.Drawing.Point(979, 302);
			this.widthNumericUpDown.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
			this.widthNumericUpDown.Name = "widthNumericUpDown";
			this.widthNumericUpDown.Size = new System.Drawing.Size(62, 20);
			this.widthNumericUpDown.TabIndex = 7;
			this.widthNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			// 
			// heigtNumericUpDown
			// 
			this.heigtNumericUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.heigtNumericUpDown.Location = new System.Drawing.Point(1047, 302);
			this.heigtNumericUpDown.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
			this.heigtNumericUpDown.Name = "heigtNumericUpDown";
			this.heigtNumericUpDown.Size = new System.Drawing.Size(62, 20);
			this.heigtNumericUpDown.TabIndex = 8;
			this.heigtNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			// 
			// startPicLureButton
			// 
			this.startPicLureButton.Location = new System.Drawing.Point(979, 272);
			this.startPicLureButton.Name = "startPicLureButton";
			this.startPicLureButton.Size = new System.Drawing.Size(100, 24);
			this.startPicLureButton.TabIndex = 9;
			this.startPicLureButton.Text = "Start";
			this.startPicLureButton.UseVisualStyleBackColor = true;
			this.startPicLureButton.Click += new System.EventHandler(this.startPicLureButton_Click);
			// 
			// templateNametextBox
			// 
			this.templateNametextBox.Enabled = false;
			this.templateNametextBox.Location = new System.Drawing.Point(1185, 328);
			this.templateNametextBox.Name = "templateNametextBox";
			this.templateNametextBox.Size = new System.Drawing.Size(176, 20);
			this.templateNametextBox.TabIndex = 10;
			// 
			// saveTemplateButton
			// 
			this.saveTemplateButton.Enabled = false;
			this.saveTemplateButton.Location = new System.Drawing.Point(1186, 355);
			this.saveTemplateButton.Name = "saveTemplateButton";
			this.saveTemplateButton.Size = new System.Drawing.Size(113, 23);
			this.saveTemplateButton.TabIndex = 11;
			this.saveTemplateButton.Text = "Save New Template";
			this.saveTemplateButton.UseVisualStyleBackColor = true;
			this.saveTemplateButton.Click += new System.EventHandler(this.saveTemplateButton_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(1186, 309);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(82, 13);
			this.label1.TabIndex = 12;
			this.label1.Text = "Template Name";
			// 
			// saveInfoLabel
			// 
			this.saveInfoLabel.AutoSize = true;
			this.saveInfoLabel.Location = new System.Drawing.Point(1186, 390);
			this.saveInfoLabel.Name = "saveInfoLabel";
			this.saveInfoLabel.Size = new System.Drawing.Size(0, 13);
			this.saveInfoLabel.TabIndex = 13;
			// 
			// ImageViewerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1373, 562);
			this.Controls.Add(this.saveInfoLabel);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.saveTemplateButton);
			this.Controls.Add(this.templateNametextBox);
			this.Controls.Add(this.startPicLureButton);
			this.Controls.Add(this.heigtNumericUpDown);
			this.Controls.Add(this.widthNumericUpDown);
			this.Controls.Add(this.previewPictureBox);
			this.Controls.Add(this.nextButton);
			this.Controls.Add(this.imageInfoRichTextBox);
			this.Controls.Add(this.pictureBox);
			this.Name = "ImageViewerForm";
			this.Text = "ImageViewerForm";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.previewPictureBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.widthNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.heigtNumericUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.RichTextBox imageInfoRichTextBox;
        private System.Windows.Forms.Button nextButton;
		private System.Windows.Forms.PictureBox previewPictureBox;
		private System.Windows.Forms.NumericUpDown widthNumericUpDown;
		private System.Windows.Forms.NumericUpDown heigtNumericUpDown;
		private System.Windows.Forms.Button startPicLureButton;
		private System.Windows.Forms.TextBox templateNametextBox;
		private System.Windows.Forms.Button saveTemplateButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label saveInfoLabel;
	}
}