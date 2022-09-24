namespace zk4500
{
    partial class Register
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
            this.components = new System.ComponentModel.Container();
            this.fingerImage = new System.Windows.Forms.PictureBox();
            this.nameinput = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.prompt = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.verify = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.fingerImage)).BeginInit();
            this.SuspendLayout();
            // 
            // fingerImage
            // 
            this.fingerImage.Location = new System.Drawing.Point(34, 52);
            this.fingerImage.Name = "fingerImage";
            this.fingerImage.Size = new System.Drawing.Size(290, 321);
            this.fingerImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.fingerImage.TabIndex = 1;
            this.fingerImage.TabStop = false;
            // 
            // nameinput
            // 
            this.nameinput.Location = new System.Drawing.Point(486, 172);
            this.nameinput.Name = "nameinput";
            this.nameinput.Size = new System.Drawing.Size(207, 25);
            this.nameinput.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(486, 293);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(207, 26);
            this.button1.TabIndex = 7;
            this.button1.Text = "注册";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // prompt
            // 
            this.prompt.AutoSize = true;
            this.prompt.Location = new System.Drawing.Point(45, 400);
            this.prompt.Name = "prompt";
            this.prompt.Size = new System.Drawing.Size(87, 15);
            this.prompt.TabIndex = 8;
            this.prompt.Text = "connect...";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(419, 182);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "姓名：";
            // 
            // verify
            // 
            this.verify.Location = new System.Drawing.Point(486, 350);
            this.verify.Name = "verify";
            this.verify.Size = new System.Drawing.Size(207, 23);
            this.verify.TabIndex = 10;
            this.verify.Text = "验证";
            this.verify.UseVisualStyleBackColor = true;
            this.verify.Click += new System.EventHandler(this.verify_Click);
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.verify);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.prompt);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.nameinput);
            this.Controls.Add(this.fingerImage);
            this.Name = "Register";
            this.Text = "Register";
            this.Load += new System.EventHandler(this.RegisterLoad);
            ((System.ComponentModel.ISupportInitialize)(this.fingerImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox fingerImage;
        private System.Windows.Forms.TextBox nameinput;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label prompt;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button verify;
    }
}