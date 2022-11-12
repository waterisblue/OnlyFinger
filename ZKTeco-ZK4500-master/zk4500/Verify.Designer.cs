namespace zk4500
{
    partial class Verify
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
            this.checkMission = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkMission
            // 
            this.checkMission.Location = new System.Drawing.Point(490, 199);
            this.checkMission.Name = "checkMission";
            this.checkMission.Size = new System.Drawing.Size(75, 23);
            this.checkMission.TabIndex = 0;
            this.checkMission.Text = "选择任务";
            this.checkMission.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkMission.UseVisualStyleBackColor = true;
            // 
            // Verify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.checkMission);
            this.Name = "Verify";
            this.Text = "Verify";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button checkMission;
    }
}