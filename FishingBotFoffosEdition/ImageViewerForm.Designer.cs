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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(12, 12);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(960, 540);
            this.pictureBox.TabIndex = 3;
            this.pictureBox.TabStop = false;
            // 
            // imageInfoRichTextBox
            // 
            this.imageInfoRichTextBox.Location = new System.Drawing.Point(979, 13);
            this.imageInfoRichTextBox.Name = "imageInfoRichTextBox";
            this.imageInfoRichTextBox.Size = new System.Drawing.Size(360, 154);
            this.imageInfoRichTextBox.TabIndex = 4;
            this.imageInfoRichTextBox.Text = "";
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(988, 187);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(123, 67);
            this.nextButton.TabIndex = 5;
            this.nextButton.Text = "Next";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // ImageViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1373, 562);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.imageInfoRichTextBox);
            this.Controls.Add(this.pictureBox);
            this.Name = "ImageViewerForm";
            this.Text = "ImageViewerForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.RichTextBox imageInfoRichTextBox;
        private System.Windows.Forms.Button nextButton;
    }
}