﻿namespace StandUpDeveloperPicker.Console.WinFormsApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblResult = new Label();
            pbResult = new PictureBox();
            btnPrevious = new Button();
            btnNext = new Button();
            ((System.ComponentModel.ISupportInitialize)pbResult).BeginInit();
            SuspendLayout();
            // 
            // lblResult
            // 
            lblResult.Dock = DockStyle.Top;
            lblResult.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblResult.Location = new Point(0, 0);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(800, 83);
            lblResult.TabIndex = 0;
            lblResult.Text = "Results...";
            lblResult.TextAlign = ContentAlignment.TopCenter;
            // 
            // pbResult
            // 
            pbResult.Dock = DockStyle.Fill;
            pbResult.Location = new Point(0, 83);
            pbResult.Name = "pbResult";
            pbResult.Size = new Size(800, 367);
            pbResult.SizeMode = PictureBoxSizeMode.CenterImage;
            pbResult.TabIndex = 1;
            pbResult.TabStop = false;
            // 
            // btnPrevious
            // 
            btnPrevious.Location = new Point(636, 222);
            btnPrevious.Name = "btnPrevious";
            btnPrevious.Size = new Size(75, 23);
            btnPrevious.TabIndex = 2;
            btnPrevious.Text = "Previous";
            btnPrevious.UseVisualStyleBackColor = true;
            btnPrevious.Click += btnPrevious_Click;
            // 
            // btnNext
            // 
            btnNext.Location = new Point(636, 264);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(75, 23);
            btnNext.TabIndex = 3;
            btnNext.Text = "Next";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnNext);
            Controls.Add(btnPrevious);
            Controls.Add(pbResult);
            Controls.Add(lblResult);
            Name = "MainForm";
            Text = "Stand Up Developer Picker";
            ((System.ComponentModel.ISupportInitialize)pbResult).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label lblResult;
        private PictureBox pbResult;
        private Button btnPrevious;
        private Button btnNext;
    }
}