using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Security.Principal;
using System.Threading;
using Microsoft.Win32;

using MetroFramework.Forms;

using libFont;

namespace YoutubeWallpapers
{
    public partial class Form1 : MetroForm
    {
        protected const string constStrApplication = "YoutubeWallpapers";

        /// <summary>
        /// Program 주 진입점 선언
        /// </summary>
        public Program g_program;

        /// <summary>
        /// 폰트 선언
        /// </summary>
        protected LibFont m_libFont = new LibFont();

        /// <summary>
        /// Setting 선언
        /// </summary>
        /// protected readonly string m_strSettingFile = Path.Combine(Application.StartupPath, constStrApplication + ".dat");
        protected string m_strSettingFile = Path.Combine(Application.StartupPath, constStrApplication + ".dat");
        protected Setting m_setting = new Setting();

        /// <summary>
        /// Form2가 필요로 하는 데이터 변수 (Youtube 변환 주소)
        /// </summary>
        public static string m_strConvertAddress = "";

        /// <summary>
        /// Youtube Address & ID
        /// </summary>
        protected string m_strAddress = "";

        /// <summary>
        /// true면 단일, false면 리스트
        /// </summary>
        protected bool m_bCheck;

        /// <summary>
        /// 밝기 값
        /// </summary>
        public static int m_iBrightness = 50;

        /// <summary>
        /// 볼륨 값
        /// </summary>
        protected int m_iVolume = 0;

        /// <summary>
        /// Background 변수들
        /// (각 폼에서 개별적으로 선언)
        /// </summary>
        // public static bool m_bFixed = false;
        // public static int  m_iMonitor = 0;

        /// <summary>
        /// 생성자
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            #region Event Handler Register

            /// <summary>
            /// ThreadException 이벤트 핸들러 등록
            /// </summary>
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);

            /// <summary>
            /// UnhandledException 이벤트 핸들러 등록
            /// </summary>
            Thread.GetDomain().UnhandledException += new UnhandledExceptionEventHandler(Application_UnhandledException);

            #endregion

            m_libFont.FontCollection();
            FontSet();
        }

        /// <summary>
        /// 폼 로딩 시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadSetting();

            // 아이콘 생성
            m_NotifyIcon.Visible = true;

            // 밝기 값
            m_iBrightness = m_setting.iBrightness;

            // 밝기 조절
            SetBrightness(m_iBrightness);

            // 볼륨 상태 표시 (CefSharp로 변경해서 미사용)
            // GetVolume();

            // 모니터 출력
            if (m_setting.iMonitor != 0)
            {
                // Form4.DialogCustom("Caution!", "Monitor Index is not 0!");

                for (int i = 0; i < m_setting.iMonitor; i++)
                {
                    metroButton_Monitor_Click(null, e);
                }
            }

            // 시작프로그램 등록 여부
            using (var varKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
            {
                metroCheckBox_StartupPrograms.Checked = (varKey.GetValue(constStrApplication) != null);
            }

            // 시작프로그램으로 등록되어있을 경우
            if (metroCheckBox_StartupPrograms.Checked)
            {
                Task.Factory.StartNew(new Action(() =>
                {
                    Invoke(new Action(() =>
                    {
                        HideWindow();
                    }));
                }));

                Thread.Sleep(100);

                label_StartOn.Visible = true;
                label_StartOff.Visible = false;
            }
            else
            {
                label_StartOn.Visible = false;
                label_StartOff.Visible = true;
            }

            Play();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 메모리, 리소스 해제 및 메세지 처리 후 종료
            g_program.ExitThread();

            Dispose();
            Application.Exit();
        }


        #region Event Handler

        /// <summary>
        /// 미처리 예외를 캐치 하는 이벤트 핸들러
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ShowErrorMessage(e.Exception, "Application_ThreadException.");
        }

        /// <summary>
        /// 미처리 예외를 캐치 하는 이벤트 핸들러
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected static void Application_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;

            if (ex != null)
            {
                ShowErrorMessage(ex, "An Unhandled Exception Occurred!");
            }
        }

        /// <summary>
        /// 에러 메시지
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="extraMessage"></param>
        protected static void ShowErrorMessage(Exception ex, string extraMessage)
        {
            Form4.DialogCustom("Unexpected Error Occurred!", "A Problem With The File or Assembly!");

            //MessageBox.Show(extraMessage
            //    + "\n\n"
            //    + "예기치 않은 오류가 발생하였습니다."
            //    + "\n\n"
            //    + ex.Message
            //    + "\n\n"
            // );

            Environment.Exit(0);
        }

        #endregion

        #region Font

        /// <summary>
        /// 폰트 설정
        /// </summary>
        protected void FontSet()
        {
            m_libFont.FontSet(label_Help, 10f, FontStyle.Regular);
            m_libFont.FontSet(label_Address, 10f, FontStyle.Regular);
            m_libFont.FontSet(label_Type, 10f, FontStyle.Regular);
            m_libFont.FontSet(label_TypeList, 10f, FontStyle.Regular);
            m_libFont.FontSet(label_TypeSingle, 10f, FontStyle.Regular);
            m_libFont.FontSet(label_VQ, 10f, FontStyle.Regular);
            m_libFont.FontSet(label_Volume, 10f, FontStyle.Regular);
            m_libFont.FontSet(label_VolumeOn, 10f, FontStyle.Regular);
            m_libFont.FontSet(label_VolumeOff, 10f, FontStyle.Regular);
            m_libFont.FontSet(label_VolumeError, 10f, FontStyle.Regular);
            m_libFont.FontSet(label_Brightness, 10f, FontStyle.Regular);
            m_libFont.FontSet(label_Buttons, 10f, FontStyle.Regular);
            m_libFont.FontSet(label_Monitor, 10f, FontStyle.Regular);
            m_libFont.FontSet(label_StartupProgram, 10f, FontStyle.Regular);
            m_libFont.FontSet(label_StartOn, 10f, FontStyle.Regular);
            m_libFont.FontSet(label_StartOff, 10f, FontStyle.Regular);
            m_libFont.FontSet(label_Help, 10f, FontStyle.Regular);
            m_libFont.FontSet(label_Help, 10f, FontStyle.Regular);

            m_libFont.FontSet(metroTextBox_Address, 10f, FontStyle.Regular);
            m_libFont.FontSet(metroTextBox_Number, 10f, FontStyle.Regular);
        }

        #endregion

        #region Setting File

        /// <summary>
        /// Setting 파일 로드
        /// </summary>
        protected void LoadSetting()
        {
            if (File.Exists(m_strSettingFile))
            {
                m_setting.LoadFromFile(m_strSettingFile);
            }

            ApplySetting();
        }

        /// <summary>
        /// Setting 파일 저장
        /// </summary>
        protected void SaveSetting()
        {
            if (label_TypeSingle.Visible)
            {
                m_setting.enumIdType = Setting.IDType.Single;
            }
            else if (label_TypeList.Visible)
            {
                m_setting.enumIdType = Setting.IDType.List;
            }

            if (metroRadioButton_Auto.Checked)
            {
                m_setting.enumVideoQuality = Setting.VideoQuality.Auto;
            }
            else if (metroRadioButton_720.Checked)
            {
                m_setting.enumVideoQuality = Setting.VideoQuality.P720;
            }
            else if (metroRadioButton_1080.Checked)
            {
                m_setting.enumVideoQuality = Setting.VideoQuality.P1080;
            }
            else if (metroRadioButton_1440.Checked)
            {
                m_setting.enumVideoQuality = Setting.VideoQuality.P1440;
            }

            m_setting.strAddress = metroTextBox_Address.Text;
            m_setting.strNumber = metroTextBox_Number.Text;
            m_setting.iBrightness = metroTrackBar_Brightness.Value;
            m_setting.iVolume = metroTrackBar_Volume.Value;

            m_setting.SaveToFile(m_strSettingFile);
        }

        /// <summary>
        /// Setting 파일 적용
        /// </summary>
        protected void ApplySetting()
        {
            switch (m_setting.enumIdType)
            {
                case Setting.IDType.Single:
                    label_TypeList.Visible = false;
                    label_TypeSingle.Visible = true;
                    metroTextBox_Number.Visible = false;
                    m_bCheck = true;
                    break;

                case Setting.IDType.List:
                    label_TypeList.Visible = true;
                    label_TypeSingle.Visible = false;
                    metroTextBox_Number.Visible = true;
                    m_bCheck = false;
                    break;
            }

            switch (m_setting.enumVideoQuality)
            {
                case Setting.VideoQuality.Auto:
                    metroRadioButton_Auto.Checked = true;
                    break;

                case Setting.VideoQuality.P720:
                    metroRadioButton_720.Checked = true;
                    break;

                case Setting.VideoQuality.P1080:
                    metroRadioButton_1080.Checked = true;
                    break;

                case Setting.VideoQuality.P1440:
                    metroRadioButton_1440.Checked = true;
                    break;
            }

            m_strAddress = m_setting.strAddress;
            metroTextBox_Address.Text = m_strAddress;

            metroTextBox_Number.Text = m_setting.strNumber;

            m_iBrightness = m_setting.iBrightness;
            metroTrackBar_Brightness.Value = m_setting.iBrightness;

            m_iVolume = m_setting.iVolume;
            metroTrackBar_Volume.Value = m_setting.iVolume;
        }

        #endregion

        #region Function

        /// <summary>
        /// 재생
        /// </summary>
        protected void Play()
        {
            SaveSetting();

            // Youtube Player Parameters
            // https://developers.google.com/youtube/player_parameters

            Stop();

            string strID = VideoDivision(metroTextBox_Address.Text);

            StringBuilder stringBuilder = new StringBuilder(@"https://www.youtube.com/");

            // 리스트
            if (!m_bCheck)
            {
                label_TypeList.Visible = true;
                label_TypeSingle.Visible = false;
                metroTextBox_Number.Visible = true;
                metroButton_Prev.Enabled = true;
                metroButton_Next.Enabled = true;

                stringBuilder.Append(@"embed?listType=playlist&list=");

                stringBuilder.Append(strID);

                Parameter(!m_bCheck, stringBuilder);

                string strNum = metroTextBox_Number.Text;

                if (!string.IsNullOrEmpty(strNum))
                {
                    int itmp = Convert.ToInt32(strNum);

                    stringBuilder.Append("&index=");

                    stringBuilder.Append(itmp - 1);
                }
            }
            // 단일
            else
            {
                label_TypeList.Visible = false;
                label_TypeSingle.Visible = true;
                metroTextBox_Number.Visible = false;
                metroButton_Prev.Enabled = false;
                metroButton_Next.Enabled = false;

                stringBuilder.Append(@"embed/");

                stringBuilder.Append(strID);

                Parameter(m_bCheck, stringBuilder);

                // 반복 재생
                stringBuilder.Append("&loop=1");

                // 단일 동영상을 반복 재생하기 위함
                stringBuilder.Append("&playlist=");

                stringBuilder.Append(strID);
            }

            // Test
            // stringBuilder.Append("&start=5");
            // stringBuilder.Append("&end=10");

            g_program.m_form2.BrowserURL(stringBuilder.ToString());
        }

        /// <summary>
        /// 파라미터
        /// </summary>
        /// <param name="bFlag"></param>
        /// <param name="stringBuilder"></param>
        protected void Parameter(bool bFlag, StringBuilder stringBuilder)
        {
            if (!m_bCheck)
            {
                // AS3 Version
                stringBuilder.Append("&version=3");
            }
            else
            {
                // AS3 Version
                stringBuilder.Append("?version=3");
            }

            // 플레이어 컨트롤러 자동 숨기기
            stringBuilder.Append("&autohide=1");

            // 자동 재생
            stringBuilder.Append("&autoplay=1");

            // 동영상 정보 미표시
            stringBuilder.Append("&showinfo=0");

            // 동영상 특수효과 끄기
            stringBuilder.Append("&iv_load_policy=3");

            // 자막 끄기
            stringBuilder.Append("&cc_load_policy=0");

            // 플레이어 컨트롤러 미표시
            stringBuilder.Append("&controls=0");

            // 키보드 컨트롤 사용 중지
            stringBuilder.Append("&disablekb=1");

            // Youtube 로고 미표시
            stringBuilder.Append("&modestbranding=1");

            // 전체화면
            stringBuilder.Append("&playsinline=0");

            // 화질
            //  240p | small
            //  360p | medium
            //  480p | large
            //  720p | hd720
            // 1080p | hd1080
            // 1440p | hd1440

            stringBuilder.Append("&vq=");

            string strVideoQuality = "";

            switch (m_setting.enumVideoQuality)
            {
                case Setting.VideoQuality.Auto:
                    strVideoQuality = "highres";
                    break;

                case Setting.VideoQuality.P720:
                    strVideoQuality = "hd720";
                    break;

                case Setting.VideoQuality.P1080:
                    strVideoQuality = "hd1080";
                    break;

                case Setting.VideoQuality.P1440:
                    strVideoQuality = "hd1440";
                    break;
            }

            stringBuilder.Append(strVideoQuality);
        }

        /// <summary>
        /// 정지
        /// </summary>
        protected void Stop()
        {
            g_program.m_form2.Stop();
        }

        /// <summary>
        /// 밝기 조절
        /// Form3의 기본 Opacity 값은 50%으로 설정
        /// </summary>
        protected void SetBrightness(int itmp)
        {
            float ftmp = itmp * 0.01f;

            if (ftmp < 0.5f)
            {
                g_program.m_form3.Opacity = 1 - (2 * ftmp);
                g_program.m_form3.BackColor = Color.Black;
            }
            else
            {
                g_program.m_form3.Opacity = 2 * (ftmp - 0.5f);
                g_program.m_form3.BackColor = Color.White;
            }
        }

        /// <summary>
        /// Youtube Address에서 ID 가져오기
        /// </summary>
        /// <param name="strURL"></param>
        /// <param name="strTypeName"></param>
        /// <returns></returns>
        protected string GetID(string strURL, string strTypeName)
        {
            int iLength = strURL.IndexOf(strTypeName);

            if (iLength >= 0)
            {
                int iStart = iLength + strTypeName.Length;

                int iEnd = strURL.IndexOf('&', iStart);

                if (iEnd >= 0)
                    return strURL.Substring(iStart, iEnd - iStart);
                else
                    return strURL.Substring(iStart);
            }

            return string.Empty;
        }

        /// <summary>
        /// 동영상 구분 후 ID 적용
        /// </summary>
        /// <param name="strURL"></param>
        /// <returns></returns>
        protected string VideoDivision(string strURL)
        {
            string strtmp;

            strtmp = GetID(strURL, "list=");

            if (!string.IsNullOrEmpty(strtmp))
            {
                m_bCheck = false;

                label_TypeList.Visible = true;
                label_TypeSingle.Visible = false;
                metroTextBox_Number.Visible = true;

                // metroTextBox_Number.Text = "1";
            }
            else
            {
                strtmp = GetID(strURL, "v=");

                if (!string.IsNullOrEmpty(strtmp))
                {
                    m_bCheck = true;

                    label_TypeList.Visible = false;
                    label_TypeSingle.Visible = true;
                    metroTextBox_Number.Visible = false;
                }
                else
                    return "";
            }

            return strtmp;
        }

        /// <summary>
        /// Get Volume (CefSharp로 변경해서 미사용)
        /// </summary>
        protected void GetVolume()
        {
            // 레지스트리를 건드리므로 불안정할 수 있음 → 테스트 완료
            WindowsIdentity windowsIdentity = WindowsIdentity.GetCurrent();
            string strPath = string.Format("{0}\\Software\\Microsoft\\Internet Explorer\\Main", windowsIdentity.User.Value);
            RegistryKey registryKey = Registry.Users.OpenSubKey(strPath, true);

            if (registryKey == null)
                throw new Exception("Error! Registry not found!");

            object objtmp = registryKey.GetValue("Play_Background_Sounds", "");

            if (objtmp.ToString() == "yes")
            {
                label_VolumeOn.Visible = true;
                label_VolumeOff.Visible = false;
                label_VolumeError.Visible = false;

                metroCheckBox_Volume.Enabled = true;
                metroCheckBox_Volume.Checked = true;
            }
            else if (objtmp.ToString() == "no")
            {
                label_VolumeOn.Visible = false;
                label_VolumeOff.Visible = true;
                label_VolumeError.Visible = false;

                metroCheckBox_Volume.Enabled = true;
                metroCheckBox_Volume.Checked = false;
            }
            else
            {
                label_VolumeOn.Visible = false;
                label_VolumeOff.Visible = false;
                label_VolumeError.Visible = true;

                metroCheckBox_Volume.Enabled = false;
            }
        }

        /// <summary>
        /// Set Volume (CefSharp로 변경해서 미사용)
        /// </summary>
        /// <param name="bFlag"></param>
        protected void SetVolume(bool bFlag)
        {
            // 레지스트리를 건드리므로 불안정할 수 있음 → 테스트 완료
            WindowsIdentity windowsIdentity = WindowsIdentity.GetCurrent();
            string strPath = string.Format("{0}\\Software\\Microsoft\\Internet Explorer\\Main", windowsIdentity.User.Value);
            RegistryKey registryKey = Registry.Users.OpenSubKey(strPath, true);

            if (registryKey == null)
                throw new Exception("Error! Registry not found!");

            string strValue = bFlag ? "yes" : "no";

            registryKey.SetValue("Play_Background_Sounds", strValue, RegistryValueKind.String);
        }

        /// <summary>
        /// 트레이로 숨기기
        /// </summary>
        protected void HideWindow()
        {
            Hide();

            m_NotifyIcon.ShowBalloonTip(1000, constStrApplication, "Click to Open!", ToolTipIcon.None);
        }

        /// <summary>
        /// 재활성화
        /// </summary>
        protected void ShowWindow()
        {
            Visible = true;

            if (WindowState == FormWindowState.Minimized)
            {
                // 최소화 멈춤
                WindowState = FormWindowState.Normal;
            }

            Activate();
        }

        #endregion

        #region NotifyIcon Event

        /// <summary>
        /// 트레이 아이콘 더블 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowWindow();
        }

        /// <summary>
        /// 트레이 아이콘 팁 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_NotifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            ShowWindow();
        }

        #endregion

        #region TrackBar Event

        /// <summary>
        /// 밝기 조절
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metroTrackBar_Brightness_ValueChanged(object sender, EventArgs e)
        {
            m_iBrightness = metroTrackBar_Brightness.Value;
            // metroProgressBar_Brightness.Value = Convert.ToInt32(metroTrackBar_Brightness.Value * 0.98) + 2;

            SetBrightness(m_iBrightness);

            m_setting.iBrightness = metroTrackBar_Brightness.Value;
            m_setting.SaveToFile(m_strSettingFile);
        }

        private void MetroTrackBar_Volume_ValueChanged(object sender, EventArgs e)
        {
            Form2.iVolume = metroTrackBar_Volume.Value;

            m_setting.iVolume = metroTrackBar_Volume.Value;
            m_setting.SaveToFile(m_strSettingFile);
        }

        #endregion

        #region CheckBox Event

        /// <summary>
        /// 시작 프로그램 등록
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metroCheckBox_StartupPrograms_Click(object sender, EventArgs e)
        {
            using (var varKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
            {
                if (metroCheckBox_StartupPrograms.Checked)
                {
                    varKey.SetValue(constStrApplication, Application.ExecutablePath.ToString());
                    label_StartOn.Visible = true;
                    label_StartOff.Visible = false;
                    // MessageBox.Show("등록 성공!", "시작 프로그램 등록", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    varKey.DeleteValue(constStrApplication, false);
                    label_StartOn.Visible = false;
                    label_StartOff.Visible = true;
                    // MessageBox.Show("해제 성공!", "시작 프로그램 해제", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// 볼륨 설정 (CefSharp로 변경해서 미사용)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metroCheckBox_Volume_Click(object sender, EventArgs e)
        {
            if (label_VolumeOn.Visible)
            {
                // SetVolume(false);
            }
            else if (label_VolumeOff.Visible)
            {
                // SetVolume(true);
            }

            // Form4.DialogCustom("Volume Setup is Complete!", "Restart Program!");

            // 재시작을 해야 볼륨 설정이 적용
            // Application.Restart();
        }

        #endregion

        #region Button Event

        /// <summary>
        /// 도움말 보기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metroButton_Help_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.ShowDialog();
        }

        private void metroButton_Start_Click(object sender, EventArgs e)
        {
            Play();
        }

        /// <summary>
        /// 정지
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metroButton_Stop_Click(object sender, EventArgs e)
        {
            Stop();
        }

        /// <summary>
        /// 이전 목록 재생
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metroButton_Prev_Click(object sender, EventArgs e)
        {
            int itmp = Convert.ToInt32(metroTextBox_Number.Text);

            if (itmp > 0)
            {
                itmp--;
            }

            metroTextBox_Number.Text = itmp.ToString();

            Play();
        }

        /// <summary>
        /// 다음 목록 재생
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metroButton_Next_Click(object sender, EventArgs e)
        {
            int itmp = Convert.ToInt32(metroTextBox_Number.Text);

            if (itmp < 999)
            {
                itmp++;
            }

            metroTextBox_Number.Text = itmp.ToString();

            Play();
        }

        /// <summary>
        /// 트레이로 숨기기 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metroButton_Hide_Click(object sender, EventArgs e)
        {
            HideWindow();
        }

        /// <summary>
        /// 출력 모니터 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metroButton_Monitor_Click(object sender, EventArgs e)
        {
            // 화면 전환
            g_program.m_form2.Monitor++;
            g_program.m_form3.Monitor++;

            // 전환에 성공하였으면 설정을 저장하고 그렇지 않으면 정지
            if (g_program.m_form2.Fixed && g_program.m_form3.Fixed)
            {
                m_setting.iMonitor = g_program.m_form2.Monitor;
                m_setting.SaveToFile(m_strSettingFile);
            }
            else
            {
                // MessageBox.Show("출력 모니터를 변경할 수 없습니다.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Form4.DialogCustom("Error!", "Can not Change Output Monitor!");

                Stop();
            }
        }

        #endregion

        #region TextBox Event

        /// <summary>
        /// 동영상 구분
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metroTextBox_Address_TextChanged(object sender, EventArgs e)
        {
            VideoDivision(metroTextBox_Address.Text);

            metroTextBox_Number.Text = "1";
        }

        /// <summary>
        /// 숫자, 백스페이스, 엔터
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metroTextBox_Number_KeyPress(object sender, KeyPressEventArgs e)
        {
            // BackSpace
            if (!(char.IsDigit(e.KeyChar)) && e.KeyChar != 8 && e.KeyChar != 13)
            {
                e.Handled = true;
            }
            // Enter
            else if (e.KeyChar == 13)
            {
                metroButton_Start_Click(null, e);
            }
        }

        private void metroTextBox_Number_Leave(object sender, EventArgs e)
        {
            return;
        }

        /// <summary>
        /// 엔터
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MetroTextBox_Address_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Enter
            if (e.KeyChar == 13)
            {
                metroButton_Start_Click(null, e);
            }
        }

        #endregion

    }
}
