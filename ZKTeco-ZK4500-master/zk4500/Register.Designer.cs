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
            this.manRadio = new System.Windows.Forms.RadioButton();
            this.womanRadio = new System.Windows.Forms.RadioButton();
            this.descInput = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.customInstaller1 = new MySql.Data.MySqlClient.CustomInstaller();
            this.taskId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
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
            this.nameinput.Location = new System.Drawing.Point(486, 44);
            this.nameinput.Name = "nameinput";
            this.nameinput.Size = new System.Drawing.Size(207, 25);
            this.nameinput.TabIndex = 2;
            this.nameinput.TextChanged += new System.EventHandler(this.nameinput_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(486, 202);
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
            this.label1.Location = new System.Drawing.Point(415, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "姓名：";
            // 
            // verify
            // 
            this.verify.Location = new System.Drawing.Point(486, 359);
            this.verify.Name = "verify";
            this.verify.Size = new System.Drawing.Size(207, 23);
            this.verify.TabIndex = 10;
            this.verify.Text = "签到";
            this.verify.UseVisualStyleBackColor = true;
            this.verify.Click += new System.EventHandler(this.verify_Click);
            // 
            // manRadio
            // 
            this.manRadio.AutoSize = true;
            this.manRadio.Location = new System.Drawing.Point(486, 149);
            this.manRadio.Name = "manRadio";
            this.manRadio.Size = new System.Drawing.Size(43, 19);
            this.manRadio.TabIndex = 11;
            this.manRadio.TabStop = true;
            this.manRadio.Text = "男";
            this.manRadio.UseVisualStyleBackColor = true;
            this.manRadio.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // womanRadio
            // 
            this.womanRadio.AutoSize = true;
            this.womanRadio.Location = new System.Drawing.Point(565, 149);
            this.womanRadio.Name = "womanRadio";
            this.womanRadio.Size = new System.Drawing.Size(43, 19);
            this.womanRadio.TabIndex = 12;
            this.womanRadio.TabStop = true;
            this.womanRadio.Text = "女";
            this.womanRadio.UseVisualStyleBackColor = true;
            // 
            // descInput
            // 
            this.descInput.Location = new System.Drawing.Point(486, 101);
            this.descInput.Name = "descInput";
            this.descInput.Size = new System.Drawing.Size(207, 25);
            this.descInput.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(415, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "描述：";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // taskId
            // 
            this.taskId.Location = new System.Drawing.Point(486, 288);
            this.taskId.Name = "taskId";
            this.taskId.Size = new System.Drawing.Size(207, 25);
            this.taskId.TabIndex = 15;
            this.taskId.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(415, 291);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 15);
            this.label3.TabIndex = 16;
            this.label3.Text = "任务id：";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.taskId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.descInput);
            this.Controls.Add(this.womanRadio);
            this.Controls.Add(this.manRadio);
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
        private System.Windows.Forms.RadioButton manRadio;
        private System.Windows.Forms.RadioButton womanRadio;
        private System.Windows.Forms.TextBox descInput;
        private System.Windows.Forms.Label label2;
        private MySql.Data.MySqlClient.CustomInstaller customInstaller1;
        private System.Windows.Forms.TextBox taskId;
        private System.Windows.Forms.Label label3;
    }
}