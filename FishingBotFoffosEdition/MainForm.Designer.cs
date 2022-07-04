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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.RunButton = new System.Windows.Forms.Button();
            this.xLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.BindFishingKey = new System.Windows.Forms.Button();
            this.keyBindingFishingLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(32, 26);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(659, 20);
            this.textBox1.TabIndex = 0;
            // 
            // RunButton
            // 
            this.RunButton.Location = new System.Drawing.Point(621, 330);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(151, 98);
            this.RunButton.TabIndex = 1;
            this.RunButton.Text = "Run";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // xLabel
            // 
            this.xLabel.AutoSize = true;
            this.xLabel.Location = new System.Drawing.Point(621, 227);
            this.xLabel.Name = "xLabel";
            this.xLabel.Size = new System.Drawing.Size(0, 13);
            this.xLabel.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // BindFishingKey
            // 
            this.BindFishingKey.Location = new System.Drawing.Point(621, 92);
            this.BindFishingKey.Name = "BindFishingKey";
            this.BindFishingKey.Size = new System.Drawing.Size(133, 23);
            this.BindFishingKey.TabIndex = 4;
            this.BindFishingKey.Text = "Bind Fishing Key";
            this.BindFishingKey.UseVisualStyleBackColor = true;
            this.BindFishingKey.Click += new System.EventHandler(this.BindFishingKey_Click);
            // 
            // label1
            // 
            this.keyBindingFishingLabel.AutoSize = true;
            this.keyBindingFishingLabel.Location = new System.Drawing.Point(671, 118);
            this.keyBindingFishingLabel.Name = "KeyBindingFishingLabel";
            this.keyBindingFishingLabel.Size = new System.Drawing.Size(35, 13);
            this.keyBindingFishingLabel.TabIndex = 5;
            this.keyBindingFishingLabel.Text = "no key set";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.keyBindingFishingLabel);
            this.Controls.Add(this.BindFishingKey);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.xLabel);
            this.Controls.Add(this.RunButton);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button RunButton;
        private System.Windows.Forms.Label xLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button BindFishingKey;
        private System.Windows.Forms.Label keyBindingFishingLabel;
    }
}

