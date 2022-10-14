namespace DoenaSoft.IsbnConverter
{
   internal partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.IsbnTextBox = new System.Windows.Forms.TextBox();
            this.EanTextBox = new System.Windows.Forms.TextBox();
            this.ConvertToEanButton = new System.Windows.Forms.Button();
            this.ConvertToIsbnButton = new System.Windows.Forms.Button();
            this.IsbnLabel = new System.Windows.Forms.Label();
            this.EanLabel = new System.Windows.Forms.Label();
            this.IsbnChecksumTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.EanChecksumTextBox = new System.Windows.Forms.TextBox();
            this.CalculateIsbnChecksumButton = new System.Windows.Forms.Button();
            this.CalculateEanChecksumButton = new System.Windows.Forms.Button();
            this.CheckIsbnChecksumButton = new System.Windows.Forms.Button();
            this.CheckEanChecksumButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // IsbnTextBox
            // 
            this.IsbnTextBox.Location = new System.Drawing.Point(68, 25);
            this.IsbnTextBox.Name = "IsbnTextBox";
            this.IsbnTextBox.Size = new System.Drawing.Size(128, 20);
            this.IsbnTextBox.TabIndex = 2;
            // 
            // EanTextBox
            // 
            this.EanTextBox.Location = new System.Drawing.Point(68, 54);
            this.EanTextBox.Name = "EanTextBox";
            this.EanTextBox.Size = new System.Drawing.Size(128, 20);
            this.EanTextBox.TabIndex = 8;
            // 
            // ConvertToEanButton
            // 
            this.ConvertToEanButton.Location = new System.Drawing.Point(202, 23);
            this.ConvertToEanButton.Name = "ConvertToEanButton";
            this.ConvertToEanButton.Size = new System.Drawing.Size(123, 23);
            this.ConvertToEanButton.TabIndex = 3;
            this.ConvertToEanButton.Text = "Convert to EAN";
            this.ConvertToEanButton.UseVisualStyleBackColor = true;
            this.ConvertToEanButton.Click += new System.EventHandler(this.OnConvertToEanButtonClick);
            // 
            // ConvertToIsbnButton
            // 
            this.ConvertToIsbnButton.Location = new System.Drawing.Point(202, 52);
            this.ConvertToIsbnButton.Name = "ConvertToIsbnButton";
            this.ConvertToIsbnButton.Size = new System.Drawing.Size(123, 23);
            this.ConvertToIsbnButton.TabIndex = 9;
            this.ConvertToIsbnButton.Text = "Convert to ISBN";
            this.ConvertToIsbnButton.UseVisualStyleBackColor = true;
            this.ConvertToIsbnButton.Click += new System.EventHandler(this.OnConvertToIsbnButtonClick);
            // 
            // IsbnLabel
            // 
            this.IsbnLabel.AutoSize = true;
            this.IsbnLabel.Location = new System.Drawing.Point(12, 28);
            this.IsbnLabel.Name = "IsbnLabel";
            this.IsbnLabel.Size = new System.Drawing.Size(35, 13);
            this.IsbnLabel.TabIndex = 1;
            this.IsbnLabel.Text = "ISBN:";
            // 
            // EanLabel
            // 
            this.EanLabel.AutoSize = true;
            this.EanLabel.Location = new System.Drawing.Point(12, 57);
            this.EanLabel.Name = "EanLabel";
            this.EanLabel.Size = new System.Drawing.Size(32, 13);
            this.EanLabel.TabIndex = 7;
            this.EanLabel.Text = "EAN:";
            // 
            // IsbnChecksumTextBox
            // 
            this.IsbnChecksumTextBox.Location = new System.Drawing.Point(331, 25);
            this.IsbnChecksumTextBox.Name = "IsbnChecksumTextBox";
            this.IsbnChecksumTextBox.Size = new System.Drawing.Size(54, 20);
            this.IsbnChecksumTextBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(328, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Checksum";
            // 
            // EanChecksumTextBox
            // 
            this.EanChecksumTextBox.Location = new System.Drawing.Point(331, 54);
            this.EanChecksumTextBox.Name = "EanChecksumTextBox";
            this.EanChecksumTextBox.Size = new System.Drawing.Size(54, 20);
            this.EanChecksumTextBox.TabIndex = 10;
            // 
            // CalculateIsbnChecksumButton
            // 
            this.CalculateIsbnChecksumButton.Location = new System.Drawing.Point(391, 23);
            this.CalculateIsbnChecksumButton.Name = "CalculateIsbnChecksumButton";
            this.CalculateIsbnChecksumButton.Size = new System.Drawing.Size(123, 23);
            this.CalculateIsbnChecksumButton.TabIndex = 5;
            this.CalculateIsbnChecksumButton.Text = "Calculate Checksum";
            this.CalculateIsbnChecksumButton.UseVisualStyleBackColor = true;
            this.CalculateIsbnChecksumButton.Click += new System.EventHandler(this.OnCalculateIsbnChecksumButtonClick);
            // 
            // CalculateEanChecksumButton
            // 
            this.CalculateEanChecksumButton.Location = new System.Drawing.Point(391, 52);
            this.CalculateEanChecksumButton.Name = "CalculateEanChecksumButton";
            this.CalculateEanChecksumButton.Size = new System.Drawing.Size(123, 23);
            this.CalculateEanChecksumButton.TabIndex = 11;
            this.CalculateEanChecksumButton.Text = "Calculate Checksum";
            this.CalculateEanChecksumButton.UseVisualStyleBackColor = true;
            this.CalculateEanChecksumButton.Click += new System.EventHandler(this.OnCalculateEanChecksumButtonClick);
            // 
            // CheckIsbnChecksumButton
            // 
            this.CheckIsbnChecksumButton.Location = new System.Drawing.Point(520, 23);
            this.CheckIsbnChecksumButton.Name = "CheckIsbnChecksumButton";
            this.CheckIsbnChecksumButton.Size = new System.Drawing.Size(123, 23);
            this.CheckIsbnChecksumButton.TabIndex = 6;
            this.CheckIsbnChecksumButton.Text = "Verify Checksum";
            this.CheckIsbnChecksumButton.UseVisualStyleBackColor = true;
            this.CheckIsbnChecksumButton.Click += new System.EventHandler(this.OnCheckIsbnChecksumButtonClick);
            // 
            // CheckEanChecksumButton
            // 
            this.CheckEanChecksumButton.Location = new System.Drawing.Point(520, 52);
            this.CheckEanChecksumButton.Name = "CheckEanChecksumButton";
            this.CheckEanChecksumButton.Size = new System.Drawing.Size(123, 23);
            this.CheckEanChecksumButton.TabIndex = 12;
            this.CheckEanChecksumButton.Text = "Verify Checksum";
            this.CheckEanChecksumButton.UseVisualStyleBackColor = true;
            this.CheckEanChecksumButton.Click += new System.EventHandler(this.OnCheckEanChecksumButtonClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 111);
            this.Controls.Add(this.CheckEanChecksumButton);
            this.Controls.Add(this.CheckIsbnChecksumButton);
            this.Controls.Add(this.CalculateEanChecksumButton);
            this.Controls.Add(this.CalculateIsbnChecksumButton);
            this.Controls.Add(this.EanChecksumTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.IsbnChecksumTextBox);
            this.Controls.Add(this.EanLabel);
            this.Controls.Add(this.IsbnLabel);
            this.Controls.Add(this.ConvertToIsbnButton);
            this.Controls.Add(this.ConvertToEanButton);
            this.Controls.Add(this.EanTextBox);
            this.Controls.Add(this.IsbnTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 150);
            this.Name = "MainForm";
            this.Text = "ISBN <-> EAN";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox IsbnTextBox;
        private System.Windows.Forms.TextBox EanTextBox;
        private System.Windows.Forms.Button ConvertToEanButton;
        private System.Windows.Forms.Button ConvertToIsbnButton;
        private System.Windows.Forms.Label IsbnLabel;
        private System.Windows.Forms.Label EanLabel;
        private System.Windows.Forms.TextBox IsbnChecksumTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox EanChecksumTextBox;
        private System.Windows.Forms.Button CalculateIsbnChecksumButton;
        private System.Windows.Forms.Button CalculateEanChecksumButton;
        private System.Windows.Forms.Button CheckIsbnChecksumButton;
        private System.Windows.Forms.Button CheckEanChecksumButton;
    }
}