namespace YoutubeWallpapers
{
    partial class Form4
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
            this.metroButton = new MetroFramework.Controls.MetroButton();
            this.label_Message = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // metroButton
            // 
            this.metroButton.Highlight = true;
            this.metroButton.Location = new System.Drawing.Point(427, 114);
            this.metroButton.Name = "metroButton";
            this.metroButton.Size = new System.Drawing.Size(50, 23);
            this.metroButton.Style = MetroFramework.MetroColorStyle.Yellow;
            this.metroButton.TabIndex = 1;
            this.metroButton.Text = "OK";
            this.metroButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroButton.UseSelectable = true;
            this.metroButton.Click += new System.EventHandler(this.metroButton_Click);
            // 
            // label_Message
            // 
            this.label_Message.AutoSize = true;
            this.label_Message.BackColor = System.Drawing.Color.Transparent;
            this.label_Message.Font = new System.Drawing.Font("나눔고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Message.ForeColor = System.Drawing.Color.White;
            this.label_Message.Location = new System.Drawing.Point(23, 75);
            this.label_Message.Name = "label_Message";
            this.label_Message.Size = new System.Drawing.Size(76, 19);
            this.label_Message.TabIndex = 0;
            this.label_Message.Text = "Message";
            // 
            // Form4
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(500, 160);
            this.ControlBox = false;
            this.Controls.Add(this.metroButton);
            this.Controls.Add(this.label_Message);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form4";
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Yellow;
            this.Text = "Title";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form4_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton metroButton;
        private System.Windows.Forms.Label label_Message;
    }
}