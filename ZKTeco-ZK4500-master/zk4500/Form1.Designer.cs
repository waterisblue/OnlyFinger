﻿namespace zk4500
{
    partial class Form1
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
            this.btnVerify = new System.Windows.Forms.Button();
            this.prompt = new System.Windows.Forms.Label();
            this.fpicture = new System.Windows.Forms.PictureBox();
            this.btnRegister = new System.Windows.Forms.Button();
            this.deviceSerial = new System.Windows.Forms.Label();
            this.txtTemplate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.fpicture)).BeginInit();
            this.SuspendLayout();
            // 
            // btnVerify
            // 
            this.btnVerify.Location = new System.Drawing.Point(759, 78);
            this.btnVerify.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(127, 44);
            this.btnVerify.TabIndex = 2;
            this.btnVerify.Text = "&Verify";
            this.btnVerify.UseVisualStyleBackColor = true;
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // prompt
            // 
            this.prompt.AutoSize = true;
            this.prompt.Location = new System.Drawing.Point(921, 77);
            this.prompt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.prompt.Name = "prompt";
            this.prompt.Size = new System.Drawing.Size(79, 15);
            this.prompt.TabIndex = 3;
            this.prompt.Text = "Ready ...";
            // 
            // fpicture
            // 
            this.fpicture.BackColor = System.Drawing.SystemColors.Window;
            this.fpicture.Location = new System.Drawing.Point(189, 65);
            this.fpicture.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.fpicture.Name = "fpicture";
            this.fpicture.Size = new System.Drawing.Size(268, 283);
            this.fpicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.fpicture.TabIndex = 21;
            this.fpicture.TabStop = false;
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(624, 77);
            this.btnRegister.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(127, 45);
            this.btnRegister.TabIndex = 22;
            this.btnRegister.Text = "&Register";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // deviceSerial
            // 
            this.deviceSerial.AutoSize = true;
            this.deviceSerial.Location = new System.Drawing.Point(620, 203);
            this.deviceSerial.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.deviceSerial.Name = "deviceSerial";
            this.deviceSerial.Size = new System.Drawing.Size(127, 15);
            this.deviceSerial.TabIndex = 3;
            this.deviceSerial.Text = "Device Serial: ";
            // 
            // txtTemplate
            // 
            this.txtTemplate.Location = new System.Drawing.Point(624, 247);
            this.txtTemplate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtTemplate.Multiline = true;
            this.txtTemplate.Name = "txtTemplate";
            this.txtTemplate.Size = new System.Drawing.Size(564, 94);
            this.txtTemplate.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(620, 228);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Template: ";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(415, 367);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(132, 24);
            this.btnClear.TabIndex = 22;
            this.btnClear.Text = "&Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(624, 140);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(162, 44);
            this.button1.TabIndex = 24;
            this.button1.Text = "Test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1248, 479);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtTemplate);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.fpicture);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.deviceSerial);
            this.Controls.Add(this.prompt);
            this.Controls.Add(this.btnVerify);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Form1";
            this.Text = "ZK4500";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fpicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.Label prompt;
        private System.Windows.Forms.PictureBox fpicture;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Label deviceSerial;
        private System.Windows.Forms.TextBox txtTemplate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button button1;
    }
}

