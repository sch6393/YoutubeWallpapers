namespace YoutubeWallpapers
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.metroTrackBar_Brightness = new MetroFramework.Controls.MetroTrackBar();
            this.metroButton_Help = new MetroFramework.Controls.MetroButton();
            this.label_Help = new System.Windows.Forms.Label();
            this.label_Address = new System.Windows.Forms.Label();
            this.label_Brightness = new System.Windows.Forms.Label();
            this.label_Volume = new System.Windows.Forms.Label();
            this.label_Type = new System.Windows.Forms.Label();
            this.label_VQ = new System.Windows.Forms.Label();
            this.metroTextBox_Address = new MetroFramework.Controls.MetroTextBox();
            this.label_TypeList = new System.Windows.Forms.Label();
            this.label_TypeSingle = new System.Windows.Forms.Label();
            this.metroTextBox_Number = new MetroFramework.Controls.MetroTextBox();
            this.metroRadioButton_Auto = new MetroFramework.Controls.MetroRadioButton();
            this.metroRadioButton_720 = new MetroFramework.Controls.MetroRadioButton();
            this.metroRadioButton_1080 = new MetroFramework.Controls.MetroRadioButton();
            this.metroRadioButton_1440 = new MetroFramework.Controls.MetroRadioButton();
            this.label_VolumeOn = new System.Windows.Forms.Label();
            this.label_VolumeOff = new System.Windows.Forms.Label();
            this.metroButton_Start = new MetroFramework.Controls.MetroButton();
            this.label_StartupProgram = new System.Windows.Forms.Label();
            this.label_Monitor = new System.Windows.Forms.Label();
            this.label_Buttons = new System.Windows.Forms.Label();
            this.metroButton_Stop = new MetroFramework.Controls.MetroButton();
            this.metroButton_Hide = new MetroFramework.Controls.MetroButton();
            this.metroButton_Monitor = new MetroFramework.Controls.MetroButton();
            this.label_StartOn = new System.Windows.Forms.Label();
            this.label_StartOff = new System.Windows.Forms.Label();
            this.metroCheckBox_StartupPrograms = new MetroFramework.Controls.MetroCheckBox();
            this.m_NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.label_VolumeError = new System.Windows.Forms.Label();
            this.metroCheckBox_Volume = new MetroFramework.Controls.MetroCheckBox();
            this.metroButton_Next = new MetroFramework.Controls.MetroButton();
            this.metroButton_Prev = new MetroFramework.Controls.MetroButton();
            this.metroTrackBar_Volume = new MetroFramework.Controls.MetroTrackBar();
            this.SuspendLayout();
            // 
            // metroTrackBar_Brightness
            // 
            this.metroTrackBar_Brightness.BackColor = System.Drawing.Color.Transparent;
            this.metroTrackBar_Brightness.LargeChange = 10;
            this.metroTrackBar_Brightness.Location = new System.Drawing.Point(223, 190);
            this.metroTrackBar_Brightness.Maximum = 50;
            this.metroTrackBar_Brightness.Minimum = 5;
            this.metroTrackBar_Brightness.MouseWheelBarPartitions = 18;
            this.metroTrackBar_Brightness.Name = "metroTrackBar_Brightness";
            this.metroTrackBar_Brightness.Size = new System.Drawing.Size(330, 23);
            this.metroTrackBar_Brightness.SmallChange = 5;
            this.metroTrackBar_Brightness.Style = MetroFramework.MetroColorStyle.Red;
            this.metroTrackBar_Brightness.TabIndex = 8;
            this.metroTrackBar_Brightness.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroTrackBar_Brightness.ValueChanged += new System.EventHandler(this.metroTrackBar_Brightness_ValueChanged);
            // 
            // metroButton_Help
            // 
            this.metroButton_Help.ForeColor = System.Drawing.Color.White;
            this.metroButton_Help.Highlight = true;
            this.metroButton_Help.Location = new System.Drawing.Point(223, 310);
            this.metroButton_Help.Name = "metroButton_Help";
            this.metroButton_Help.Size = new System.Drawing.Size(330, 23);
            this.metroButton_Help.Style = MetroFramework.MetroColorStyle.Red;
            this.metroButton_Help.TabIndex = 16;
            this.metroButton_Help.Text = "Help";
            this.metroButton_Help.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroButton_Help.UseCustomForeColor = true;
            this.metroButton_Help.UseSelectable = true;
            this.metroButton_Help.Click += new System.EventHandler(this.metroButton_Help_Click);
            // 
            // label_Help
            // 
            this.label_Help.AutoSize = true;
            this.label_Help.BackColor = System.Drawing.Color.Transparent;
            this.label_Help.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Help.ForeColor = System.Drawing.Color.White;
            this.label_Help.Location = new System.Drawing.Point(23, 315);
            this.label_Help.Name = "label_Help";
            this.label_Help.Size = new System.Drawing.Size(34, 15);
            this.label_Help.TabIndex = 0;
            this.label_Help.Text = "Help";
            // 
            // label_Address
            // 
            this.label_Address.AutoSize = true;
            this.label_Address.BackColor = System.Drawing.Color.Transparent;
            this.label_Address.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Address.ForeColor = System.Drawing.Color.White;
            this.label_Address.Location = new System.Drawing.Point(23, 75);
            this.label_Address.Name = "label_Address";
            this.label_Address.Size = new System.Drawing.Size(113, 15);
            this.label_Address.TabIndex = 0;
            this.label_Address.Text = "Youtube Address";
            // 
            // label_Brightness
            // 
            this.label_Brightness.AutoSize = true;
            this.label_Brightness.BackColor = System.Drawing.Color.Transparent;
            this.label_Brightness.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Brightness.ForeColor = System.Drawing.Color.White;
            this.label_Brightness.Location = new System.Drawing.Point(23, 195);
            this.label_Brightness.Name = "label_Brightness";
            this.label_Brightness.Size = new System.Drawing.Size(121, 15);
            this.label_Brightness.TabIndex = 0;
            this.label_Brightness.Text = "Brightness Control";
            // 
            // label_Volume
            // 
            this.label_Volume.AutoSize = true;
            this.label_Volume.BackColor = System.Drawing.Color.Transparent;
            this.label_Volume.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Volume.ForeColor = System.Drawing.Color.White;
            this.label_Volume.Location = new System.Drawing.Point(23, 165);
            this.label_Volume.Name = "label_Volume";
            this.label_Volume.Size = new System.Drawing.Size(104, 15);
            this.label_Volume.TabIndex = 0;
            this.label_Volume.Text = "Volume Control";
            // 
            // label_Type
            // 
            this.label_Type.AutoSize = true;
            this.label_Type.BackColor = System.Drawing.Color.Transparent;
            this.label_Type.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Type.ForeColor = System.Drawing.Color.White;
            this.label_Type.Location = new System.Drawing.Point(23, 105);
            this.label_Type.Name = "label_Type";
            this.label_Type.Size = new System.Drawing.Size(120, 15);
            this.label_Type.TabIndex = 0;
            this.label_Type.Text = "Distinction of Type";
            // 
            // label_VQ
            // 
            this.label_VQ.AutoSize = true;
            this.label_VQ.BackColor = System.Drawing.Color.Transparent;
            this.label_VQ.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_VQ.ForeColor = System.Drawing.Color.White;
            this.label_VQ.Location = new System.Drawing.Point(23, 135);
            this.label_VQ.Name = "label_VQ";
            this.label_VQ.Size = new System.Drawing.Size(88, 15);
            this.label_VQ.TabIndex = 0;
            this.label_VQ.Text = "Video Quality";
            // 
            // metroTextBox_Address
            // 
            // 
            // 
            // 
            this.metroTextBox_Address.CustomButton.Image = null;
            this.metroTextBox_Address.CustomButton.Location = new System.Drawing.Point(308, 1);
            this.metroTextBox_Address.CustomButton.Name = "";
            this.metroTextBox_Address.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.metroTextBox_Address.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox_Address.CustomButton.TabIndex = 1;
            this.metroTextBox_Address.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox_Address.CustomButton.UseSelectable = true;
            this.metroTextBox_Address.CustomButton.Visible = false;
            this.metroTextBox_Address.ForeColor = System.Drawing.Color.White;
            this.metroTextBox_Address.Lines = new string[] {
        "Youtube Address"};
            this.metroTextBox_Address.Location = new System.Drawing.Point(223, 70);
            this.metroTextBox_Address.MaxLength = 32767;
            this.metroTextBox_Address.Name = "metroTextBox_Address";
            this.metroTextBox_Address.PasswordChar = '\0';
            this.metroTextBox_Address.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox_Address.SelectedText = "";
            this.metroTextBox_Address.SelectionLength = 0;
            this.metroTextBox_Address.SelectionStart = 0;
            this.metroTextBox_Address.ShortcutsEnabled = true;
            this.metroTextBox_Address.Size = new System.Drawing.Size(330, 23);
            this.metroTextBox_Address.Style = MetroFramework.MetroColorStyle.Red;
            this.metroTextBox_Address.TabIndex = 1;
            this.metroTextBox_Address.Text = "Youtube Address";
            this.metroTextBox_Address.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroTextBox_Address.UseCustomForeColor = true;
            this.metroTextBox_Address.UseSelectable = true;
            this.metroTextBox_Address.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox_Address.WaterMarkFont = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.metroTextBox_Address.TextChanged += new System.EventHandler(this.metroTextBox_Address_TextChanged);
            this.metroTextBox_Address.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MetroTextBox_Address_KeyPress);
            // 
            // label_TypeList
            // 
            this.label_TypeList.AutoSize = true;
            this.label_TypeList.BackColor = System.Drawing.Color.Transparent;
            this.label_TypeList.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_TypeList.ForeColor = System.Drawing.Color.Crimson;
            this.label_TypeList.Location = new System.Drawing.Point(220, 105);
            this.label_TypeList.Name = "label_TypeList";
            this.label_TypeList.Size = new System.Drawing.Size(146, 15);
            this.label_TypeList.TabIndex = 0;
            this.label_TypeList.Text = "Playlist -> Start to        ";
            this.label_TypeList.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_TypeSingle
            // 
            this.label_TypeSingle.AutoSize = true;
            this.label_TypeSingle.BackColor = System.Drawing.Color.Transparent;
            this.label_TypeSingle.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_TypeSingle.ForeColor = System.Drawing.Color.Crimson;
            this.label_TypeSingle.Location = new System.Drawing.Point(220, 105);
            this.label_TypeSingle.Name = "label_TypeSingle";
            this.label_TypeSingle.Size = new System.Drawing.Size(126, 15);
            this.label_TypeSingle.TabIndex = 0;
            this.label_TypeSingle.Text = "Single or Streaming";
            this.label_TypeSingle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // metroTextBox_Number
            // 
            // 
            // 
            // 
            this.metroTextBox_Number.CustomButton.Image = null;
            this.metroTextBox_Number.CustomButton.Location = new System.Drawing.Point(11, 1);
            this.metroTextBox_Number.CustomButton.Name = "";
            this.metroTextBox_Number.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.metroTextBox_Number.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox_Number.CustomButton.TabIndex = 1;
            this.metroTextBox_Number.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox_Number.CustomButton.UseSelectable = true;
            this.metroTextBox_Number.CustomButton.Visible = false;
            this.metroTextBox_Number.ForeColor = System.Drawing.Color.White;
            this.metroTextBox_Number.Lines = new string[] {
        "999"};
            this.metroTextBox_Number.Location = new System.Drawing.Point(340, 100);
            this.metroTextBox_Number.MaxLength = 3;
            this.metroTextBox_Number.Name = "metroTextBox_Number";
            this.metroTextBox_Number.PasswordChar = '\0';
            this.metroTextBox_Number.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox_Number.SelectedText = "";
            this.metroTextBox_Number.SelectionLength = 0;
            this.metroTextBox_Number.SelectionStart = 0;
            this.metroTextBox_Number.ShortcutsEnabled = true;
            this.metroTextBox_Number.Size = new System.Drawing.Size(33, 23);
            this.metroTextBox_Number.Style = MetroFramework.MetroColorStyle.Red;
            this.metroTextBox_Number.TabIndex = 2;
            this.metroTextBox_Number.Text = "999";
            this.metroTextBox_Number.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.metroTextBox_Number.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroTextBox_Number.UseCustomForeColor = true;
            this.metroTextBox_Number.UseSelectable = true;
            this.metroTextBox_Number.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox_Number.WaterMarkFont = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.metroTextBox_Number.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.metroTextBox_Number_KeyPress);
            this.metroTextBox_Number.Leave += new System.EventHandler(this.metroTextBox_Number_Leave);
            // 
            // metroRadioButton_Auto
            // 
            this.metroRadioButton_Auto.Checked = true;
            this.metroRadioButton_Auto.ForeColor = System.Drawing.Color.White;
            this.metroRadioButton_Auto.Location = new System.Drawing.Point(223, 130);
            this.metroRadioButton_Auto.Name = "metroRadioButton_Auto";
            this.metroRadioButton_Auto.Size = new System.Drawing.Size(50, 25);
            this.metroRadioButton_Auto.Style = MetroFramework.MetroColorStyle.Red;
            this.metroRadioButton_Auto.TabIndex = 3;
            this.metroRadioButton_Auto.TabStop = true;
            this.metroRadioButton_Auto.Text = "Auto";
            this.metroRadioButton_Auto.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroRadioButton_Auto.UseCustomForeColor = true;
            this.metroRadioButton_Auto.UseSelectable = true;
            // 
            // metroRadioButton_720
            // 
            this.metroRadioButton_720.ForeColor = System.Drawing.Color.White;
            this.metroRadioButton_720.Location = new System.Drawing.Point(317, 130);
            this.metroRadioButton_720.Name = "metroRadioButton_720";
            this.metroRadioButton_720.Size = new System.Drawing.Size(50, 25);
            this.metroRadioButton_720.Style = MetroFramework.MetroColorStyle.Red;
            this.metroRadioButton_720.TabIndex = 4;
            this.metroRadioButton_720.Text = "720";
            this.metroRadioButton_720.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroRadioButton_720.UseCustomForeColor = true;
            this.metroRadioButton_720.UseSelectable = true;
            // 
            // metroRadioButton_1080
            // 
            this.metroRadioButton_1080.ForeColor = System.Drawing.Color.White;
            this.metroRadioButton_1080.Location = new System.Drawing.Point(411, 130);
            this.metroRadioButton_1080.Name = "metroRadioButton_1080";
            this.metroRadioButton_1080.Size = new System.Drawing.Size(50, 25);
            this.metroRadioButton_1080.Style = MetroFramework.MetroColorStyle.Red;
            this.metroRadioButton_1080.TabIndex = 5;
            this.metroRadioButton_1080.Text = "1080";
            this.metroRadioButton_1080.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroRadioButton_1080.UseCustomForeColor = true;
            this.metroRadioButton_1080.UseSelectable = true;
            // 
            // metroRadioButton_1440
            // 
            this.metroRadioButton_1440.ForeColor = System.Drawing.Color.White;
            this.metroRadioButton_1440.Location = new System.Drawing.Point(505, 130);
            this.metroRadioButton_1440.Name = "metroRadioButton_1440";
            this.metroRadioButton_1440.Size = new System.Drawing.Size(50, 25);
            this.metroRadioButton_1440.Style = MetroFramework.MetroColorStyle.Red;
            this.metroRadioButton_1440.TabIndex = 6;
            this.metroRadioButton_1440.Text = "1440";
            this.metroRadioButton_1440.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroRadioButton_1440.UseCustomForeColor = true;
            this.metroRadioButton_1440.UseSelectable = true;
            // 
            // label_VolumeOn
            // 
            this.label_VolumeOn.AutoSize = true;
            this.label_VolumeOn.BackColor = System.Drawing.Color.Transparent;
            this.label_VolumeOn.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_VolumeOn.ForeColor = System.Drawing.Color.Crimson;
            this.label_VolumeOn.Location = new System.Drawing.Point(185, 165);
            this.label_VolumeOn.Name = "label_VolumeOn";
            this.label_VolumeOn.Size = new System.Drawing.Size(25, 15);
            this.label_VolumeOn.TabIndex = 0;
            this.label_VolumeOn.Text = "On";
            this.label_VolumeOn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label_VolumeOn.Visible = false;
            // 
            // label_VolumeOff
            // 
            this.label_VolumeOff.AutoSize = true;
            this.label_VolumeOff.BackColor = System.Drawing.Color.Transparent;
            this.label_VolumeOff.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_VolumeOff.ForeColor = System.Drawing.Color.Gray;
            this.label_VolumeOff.Location = new System.Drawing.Point(185, 165);
            this.label_VolumeOff.Name = "label_VolumeOff";
            this.label_VolumeOff.Size = new System.Drawing.Size(27, 15);
            this.label_VolumeOff.TabIndex = 0;
            this.label_VolumeOff.Text = "Off";
            this.label_VolumeOff.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label_VolumeOff.Visible = false;
            // 
            // metroButton_Start
            // 
            this.metroButton_Start.ForeColor = System.Drawing.Color.White;
            this.metroButton_Start.Highlight = true;
            this.metroButton_Start.Location = new System.Drawing.Point(223, 220);
            this.metroButton_Start.Name = "metroButton_Start";
            this.metroButton_Start.Size = new System.Drawing.Size(61, 23);
            this.metroButton_Start.Style = MetroFramework.MetroColorStyle.Red;
            this.metroButton_Start.TabIndex = 9;
            this.metroButton_Start.Text = "Start";
            this.metroButton_Start.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroButton_Start.UseCustomForeColor = true;
            this.metroButton_Start.UseSelectable = true;
            this.metroButton_Start.Click += new System.EventHandler(this.metroButton_Start_Click);
            // 
            // label_StartupProgram
            // 
            this.label_StartupProgram.AutoSize = true;
            this.label_StartupProgram.BackColor = System.Drawing.Color.Transparent;
            this.label_StartupProgram.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_StartupProgram.ForeColor = System.Drawing.Color.White;
            this.label_StartupProgram.Location = new System.Drawing.Point(23, 285);
            this.label_StartupProgram.Name = "label_StartupProgram";
            this.label_StartupProgram.Size = new System.Drawing.Size(186, 15);
            this.label_StartupProgram.TabIndex = 0;
            this.label_StartupProgram.Text = "Startup Program Registration";
            // 
            // label_Monitor
            // 
            this.label_Monitor.AutoSize = true;
            this.label_Monitor.BackColor = System.Drawing.Color.Transparent;
            this.label_Monitor.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Monitor.ForeColor = System.Drawing.Color.White;
            this.label_Monitor.Location = new System.Drawing.Point(23, 255);
            this.label_Monitor.Name = "label_Monitor";
            this.label_Monitor.Size = new System.Drawing.Size(93, 15);
            this.label_Monitor.TabIndex = 0;
            this.label_Monitor.Text = "Multi Monitor";
            // 
            // label_Buttons
            // 
            this.label_Buttons.AutoSize = true;
            this.label_Buttons.BackColor = System.Drawing.Color.Transparent;
            this.label_Buttons.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Buttons.ForeColor = System.Drawing.Color.White;
            this.label_Buttons.Location = new System.Drawing.Point(23, 225);
            this.label_Buttons.Name = "label_Buttons";
            this.label_Buttons.Size = new System.Drawing.Size(98, 15);
            this.label_Buttons.TabIndex = 0;
            this.label_Buttons.Text = "Action Buttons";
            // 
            // metroButton_Stop
            // 
            this.metroButton_Stop.ForeColor = System.Drawing.Color.White;
            this.metroButton_Stop.Highlight = true;
            this.metroButton_Stop.Location = new System.Drawing.Point(290, 220);
            this.metroButton_Stop.Name = "metroButton_Stop";
            this.metroButton_Stop.Size = new System.Drawing.Size(61, 23);
            this.metroButton_Stop.Style = MetroFramework.MetroColorStyle.Red;
            this.metroButton_Stop.TabIndex = 10;
            this.metroButton_Stop.Text = "Stop";
            this.metroButton_Stop.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroButton_Stop.UseCustomForeColor = true;
            this.metroButton_Stop.UseSelectable = true;
            this.metroButton_Stop.Click += new System.EventHandler(this.metroButton_Stop_Click);
            // 
            // metroButton_Hide
            // 
            this.metroButton_Hide.ForeColor = System.Drawing.Color.White;
            this.metroButton_Hide.Highlight = true;
            this.metroButton_Hide.Location = new System.Drawing.Point(491, 220);
            this.metroButton_Hide.Name = "metroButton_Hide";
            this.metroButton_Hide.Size = new System.Drawing.Size(61, 23);
            this.metroButton_Hide.Style = MetroFramework.MetroColorStyle.Red;
            this.metroButton_Hide.TabIndex = 13;
            this.metroButton_Hide.Text = "Hide";
            this.metroButton_Hide.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroButton_Hide.UseCustomForeColor = true;
            this.metroButton_Hide.UseSelectable = true;
            this.metroButton_Hide.Click += new System.EventHandler(this.metroButton_Hide_Click);
            // 
            // metroButton_Monitor
            // 
            this.metroButton_Monitor.ForeColor = System.Drawing.Color.White;
            this.metroButton_Monitor.Highlight = true;
            this.metroButton_Monitor.Location = new System.Drawing.Point(223, 250);
            this.metroButton_Monitor.Name = "metroButton_Monitor";
            this.metroButton_Monitor.Size = new System.Drawing.Size(330, 23);
            this.metroButton_Monitor.Style = MetroFramework.MetroColorStyle.Red;
            this.metroButton_Monitor.TabIndex = 14;
            this.metroButton_Monitor.Text = "Next Monitor";
            this.metroButton_Monitor.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroButton_Monitor.UseCustomForeColor = true;
            this.metroButton_Monitor.UseSelectable = true;
            this.metroButton_Monitor.Click += new System.EventHandler(this.metroButton_Monitor_Click);
            // 
            // label_StartOn
            // 
            this.label_StartOn.AutoSize = true;
            this.label_StartOn.BackColor = System.Drawing.Color.Transparent;
            this.label_StartOn.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_StartOn.ForeColor = System.Drawing.Color.Crimson;
            this.label_StartOn.Location = new System.Drawing.Point(243, 285);
            this.label_StartOn.Name = "label_StartOn";
            this.label_StartOn.Size = new System.Drawing.Size(25, 15);
            this.label_StartOn.TabIndex = 0;
            this.label_StartOn.Text = "On";
            this.label_StartOn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_StartOff
            // 
            this.label_StartOff.AutoSize = true;
            this.label_StartOff.BackColor = System.Drawing.Color.Transparent;
            this.label_StartOff.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_StartOff.ForeColor = System.Drawing.Color.Gray;
            this.label_StartOff.Location = new System.Drawing.Point(243, 285);
            this.label_StartOff.Name = "label_StartOff";
            this.label_StartOff.Size = new System.Drawing.Size(27, 15);
            this.label_StartOff.TabIndex = 0;
            this.label_StartOff.Text = "Off";
            this.label_StartOff.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // metroCheckBox_StartupPrograms
            // 
            this.metroCheckBox_StartupPrograms.Location = new System.Drawing.Point(223, 280);
            this.metroCheckBox_StartupPrograms.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.metroCheckBox_StartupPrograms.Name = "metroCheckBox_StartupPrograms";
            this.metroCheckBox_StartupPrograms.Size = new System.Drawing.Size(23, 25);
            this.metroCheckBox_StartupPrograms.Style = MetroFramework.MetroColorStyle.Red;
            this.metroCheckBox_StartupPrograms.TabIndex = 15;
            this.metroCheckBox_StartupPrograms.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroCheckBox_StartupPrograms.UseSelectable = true;
            this.metroCheckBox_StartupPrograms.Click += new System.EventHandler(this.metroCheckBox_StartupPrograms_Click);
            // 
            // m_NotifyIcon
            // 
            this.m_NotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("m_NotifyIcon.Icon")));
            this.m_NotifyIcon.Text = "YoutubeWallpapers";
            this.m_NotifyIcon.Visible = true;
            this.m_NotifyIcon.BalloonTipClicked += new System.EventHandler(this.m_NotifyIcon_BalloonTipClicked);
            this.m_NotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.m_NotifyIcon_MouseDoubleClick);
            // 
            // label_VolumeError
            // 
            this.label_VolumeError.AutoSize = true;
            this.label_VolumeError.BackColor = System.Drawing.Color.Transparent;
            this.label_VolumeError.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_VolumeError.ForeColor = System.Drawing.Color.Yellow;
            this.label_VolumeError.Location = new System.Drawing.Point(185, 165);
            this.label_VolumeError.Name = "label_VolumeError";
            this.label_VolumeError.Size = new System.Drawing.Size(37, 15);
            this.label_VolumeError.TabIndex = 0;
            this.label_VolumeError.Text = "Error";
            this.label_VolumeError.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label_VolumeError.Visible = false;
            // 
            // metroCheckBox_Volume
            // 
            this.metroCheckBox_Volume.Location = new System.Drawing.Point(165, 160);
            this.metroCheckBox_Volume.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.metroCheckBox_Volume.Name = "metroCheckBox_Volume";
            this.metroCheckBox_Volume.Size = new System.Drawing.Size(23, 25);
            this.metroCheckBox_Volume.Style = MetroFramework.MetroColorStyle.Red;
            this.metroCheckBox_Volume.TabIndex = 7;
            this.metroCheckBox_Volume.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroCheckBox_Volume.UseSelectable = true;
            this.metroCheckBox_Volume.Visible = false;
            this.metroCheckBox_Volume.Click += new System.EventHandler(this.metroCheckBox_Volume_Click);
            // 
            // metroButton_Next
            // 
            this.metroButton_Next.ForeColor = System.Drawing.Color.White;
            this.metroButton_Next.Highlight = true;
            this.metroButton_Next.Location = new System.Drawing.Point(424, 220);
            this.metroButton_Next.Name = "metroButton_Next";
            this.metroButton_Next.Size = new System.Drawing.Size(61, 23);
            this.metroButton_Next.Style = MetroFramework.MetroColorStyle.Red;
            this.metroButton_Next.TabIndex = 12;
            this.metroButton_Next.Text = "Next";
            this.metroButton_Next.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroButton_Next.UseCustomForeColor = true;
            this.metroButton_Next.UseSelectable = true;
            this.metroButton_Next.Click += new System.EventHandler(this.metroButton_Next_Click);
            // 
            // metroButton_Prev
            // 
            this.metroButton_Prev.ForeColor = System.Drawing.Color.White;
            this.metroButton_Prev.Highlight = true;
            this.metroButton_Prev.Location = new System.Drawing.Point(357, 220);
            this.metroButton_Prev.Name = "metroButton_Prev";
            this.metroButton_Prev.Size = new System.Drawing.Size(61, 23);
            this.metroButton_Prev.Style = MetroFramework.MetroColorStyle.Red;
            this.metroButton_Prev.TabIndex = 11;
            this.metroButton_Prev.Text = "Prev";
            this.metroButton_Prev.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroButton_Prev.UseCustomForeColor = true;
            this.metroButton_Prev.UseSelectable = true;
            this.metroButton_Prev.Click += new System.EventHandler(this.metroButton_Prev_Click);
            // 
            // metroTrackBar_Volume
            // 
            this.metroTrackBar_Volume.BackColor = System.Drawing.Color.Transparent;
            this.metroTrackBar_Volume.Location = new System.Drawing.Point(223, 160);
            this.metroTrackBar_Volume.Name = "metroTrackBar_Volume";
            this.metroTrackBar_Volume.Size = new System.Drawing.Size(330, 23);
            this.metroTrackBar_Volume.Style = MetroFramework.MetroColorStyle.Red;
            this.metroTrackBar_Volume.TabIndex = 7;
            this.metroTrackBar_Volume.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroTrackBar_Volume.Value = 100;
            this.metroTrackBar_Volume.ValueChanged += new System.EventHandler(this.MetroTrackBar_Volume_ValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(580, 350);
            this.Controls.Add(this.metroTrackBar_Volume);
            this.Controls.Add(this.metroButton_Next);
            this.Controls.Add(this.metroButton_Prev);
            this.Controls.Add(this.metroCheckBox_Volume);
            this.Controls.Add(this.label_VolumeError);
            this.Controls.Add(this.label_StartOn);
            this.Controls.Add(this.label_StartOff);
            this.Controls.Add(this.label_VolumeOn);
            this.Controls.Add(this.label_VolumeOff);
            this.Controls.Add(this.metroCheckBox_StartupPrograms);
            this.Controls.Add(this.metroButton_Monitor);
            this.Controls.Add(this.metroButton_Hide);
            this.Controls.Add(this.metroButton_Stop);
            this.Controls.Add(this.label_StartupProgram);
            this.Controls.Add(this.label_Monitor);
            this.Controls.Add(this.label_Buttons);
            this.Controls.Add(this.metroButton_Start);
            this.Controls.Add(this.metroRadioButton_1440);
            this.Controls.Add(this.metroRadioButton_1080);
            this.Controls.Add(this.metroRadioButton_720);
            this.Controls.Add(this.metroRadioButton_Auto);
            this.Controls.Add(this.metroTextBox_Number);
            this.Controls.Add(this.metroTextBox_Address);
            this.Controls.Add(this.label_VQ);
            this.Controls.Add(this.label_Type);
            this.Controls.Add(this.label_Volume);
            this.Controls.Add(this.label_Brightness);
            this.Controls.Add(this.label_Address);
            this.Controls.Add(this.metroButton_Help);
            this.Controls.Add(this.label_Help);
            this.Controls.Add(this.metroTrackBar_Brightness);
            this.Controls.Add(this.label_TypeSingle);
            this.Controls.Add(this.label_TypeList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "YoutubeWallpapers";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTrackBar metroTrackBar_Brightness;
        private MetroFramework.Controls.MetroButton metroButton_Help;
        private System.Windows.Forms.Label label_Help;
        private System.Windows.Forms.Label label_Address;
        private System.Windows.Forms.Label label_Brightness;
        private System.Windows.Forms.Label label_Volume;
        private System.Windows.Forms.Label label_Type;
        private System.Windows.Forms.Label label_VQ;
        private MetroFramework.Controls.MetroTextBox metroTextBox_Address;
        private System.Windows.Forms.Label label_TypeList;
        private System.Windows.Forms.Label label_TypeSingle;
        private MetroFramework.Controls.MetroTextBox metroTextBox_Number;
        private MetroFramework.Controls.MetroRadioButton metroRadioButton_Auto;
        private MetroFramework.Controls.MetroRadioButton metroRadioButton_720;
        private MetroFramework.Controls.MetroRadioButton metroRadioButton_1080;
        private MetroFramework.Controls.MetroRadioButton metroRadioButton_1440;
        private System.Windows.Forms.Label label_VolumeOn;
        private System.Windows.Forms.Label label_VolumeOff;
        private MetroFramework.Controls.MetroButton metroButton_Start;
        private System.Windows.Forms.Label label_StartupProgram;
        private System.Windows.Forms.Label label_Monitor;
        private System.Windows.Forms.Label label_Buttons;
        private MetroFramework.Controls.MetroButton metroButton_Stop;
        private MetroFramework.Controls.MetroButton metroButton_Hide;
        private MetroFramework.Controls.MetroButton metroButton_Monitor;
        private System.Windows.Forms.Label label_StartOn;
        private System.Windows.Forms.Label label_StartOff;
        private MetroFramework.Controls.MetroCheckBox metroCheckBox_StartupPrograms;
        private System.Windows.Forms.NotifyIcon m_NotifyIcon;
        private System.Windows.Forms.Label label_VolumeError;
        private MetroFramework.Controls.MetroCheckBox metroCheckBox_Volume;
        private MetroFramework.Controls.MetroButton metroButton_Next;
        private MetroFramework.Controls.MetroButton metroButton_Prev;
        private MetroFramework.Controls.MetroTrackBar metroTrackBar_Volume;
    }
}

