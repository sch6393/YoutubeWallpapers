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
using System.Net;
using System.Security.Principal;
using System.Threading;
using Microsoft.Win32;

using MetroFramework;
using MetroFramework.Controls;
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
        /// Youtube Address & ID
        /// </summary>
        protected string m_strAddress = "";

        /// <summary>
        /// true면 단일, false면 리스트
        /// </summary>
        public static bool m_bCheck;

        /// <summary>
        /// 밝기 값
        /// </summary>
        protected int m_iBrightness = 50;

        /// <summary>
        /// 볼륨 값
        /// </summary>
        // protected int m_iVolume = 0;

        /// <summary>
        /// Background 변수들
        /// (각 폼에서 개별적으로 선언)
        /// </summary>
        // public static bool m_bFixed = false;
        // public static int  m_iMonitor = 0;

        /// <summary>
        /// Style Mode
        /// false : Light, true : Dark
        /// </summary>
        public static bool m_bStyle = false;

        /// <summary>
        /// Html Source
        /// </summary>
        public static string m_strHtml = "";

        /// <summary>
        /// Youtube Video Name
        /// </summary>
        protected string m_strNameOld = "";
        protected string m_strNameNew = "";

        /// <summary>
        /// Playlist Number
        /// </summary>
        protected int m_iPlaylistNumber;

        /// <summary>
        /// Prev, Next 구분
        /// 0 : 초기화, 1 : Prev, 2 : Next, 3 : 기본 값
        /// </summary>
        protected int m_iPrevNext = 0;

        /// <summary>
        /// H264 Flag
        /// false : CefSharp, true : WebBrowser
        /// </summary>
        public static bool m_bH264 = false;

        /// <summary>
        /// 투명 라벨
        /// </summary>
        protected TransparentLabel m_transparentLabel = new TransparentLabel();
        // protected TransparentLabelTop m_transparentLabelTop = new TransparentLabelTop();

        /// <summary>
        /// true : 증가, false : 감소
        /// </summary>
        protected bool m_bTransparent = false;
        public static int m_iTransparent = 255;

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

            // StyleManager
            StyleManager = m_metroStyleManager;
        }

        /// <summary>
        /// 폼 로딩 시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadSetting();

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

        /// <summary>
        /// 폼 로딩이 끝난 후
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Shown(object sender, EventArgs e)
        {
            VersionCheck();
        }

        /// <summary>
        /// 폼 종료 시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ProgramExit();
        }

        /// <summary>
        /// 정주기 (2초)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void M_timer_Tick(object sender, EventArgs e)
        {
            Parsing();
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
            m_libFont.FontSet(label_StyleMode, 10f, FontStyle.Regular);

            m_libFont.FontSet(metroTextBox_Address, 10f, FontStyle.Regular);
            m_libFont.FontSet(metroTextBox_Number, 10f, FontStyle.Regular);

            m_libFont.FontSet(m_metroContextMenu, 10f, FontStyle.Regular);

            m_libFont.FontSet(label_H264, 10f, FontStyle.Regular);
            m_libFont.FontSet(label_H264On, 10f, FontStyle.Regular);
            m_libFont.FontSet(label_H264Off, 10f, FontStyle.Regular);

            m_libFont.FontSet(label_Name, 10f, FontStyle.Regular);
            m_libFont.FontSet(label_VideoName, 10f, FontStyle.Regular);
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
            m_setting.bStyle = m_bStyle;
            m_setting.bH264 = m_bH264;

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
                    autoToolStripMenuItem.Checked = true;
                    break;

                case Setting.VideoQuality.P720:
                    metroRadioButton_720.Checked = true;
                    video720ToolStripMenuItem.Checked = true;
                    break;

                case Setting.VideoQuality.P1080:
                    metroRadioButton_1080.Checked = true;
                    video1080ToolStripMenuItem.Checked = true;
                    break;

                case Setting.VideoQuality.P1440:
                    metroRadioButton_1440.Checked = true;
                    video1440ToolStripMenuItem.Checked = true;
                    break;
            }

            m_strAddress = m_setting.strAddress;
            metroTextBox_Address.Text = m_strAddress;

            metroTextBox_Number.Text = m_setting.strNumber;
            m_iPlaylistNumber = int.Parse(m_setting.strNumber);

            m_iBrightness = m_setting.iBrightness;
            metroTrackBar_Brightness.Value = m_setting.iBrightness;

            Form2.iVolume = m_setting.iVolume;
            metroTrackBar_Volume.Value = m_setting.iVolume;

            m_bStyle = m_setting.bStyle;

            m_bH264 = m_setting.bH264;

            // 아이콘 생성
            m_NotifyIcon.Visible = true;

            // 밝기 조절
            SetBrightness(m_iBrightness);

            // 볼륨 상태 표시 (CefSharp로 변경해서 미사용)
            // GetVolume();

            // Style Mode
            StyleMode();

            if (m_bH264)
            {
                label_H264On.Visible = true;
                label_VolumeError.Visible = true;
                label_VolumeOff.Visible = true;
                label_VolumeOn.Visible = true;
                metroCheckBox_Volume.Visible = true;
                metroCheckBox_H264.Checked = true;

                GetVolume();
            }
            else
            {
                label_H264Off.Visible = true;
                metroTrackBar_Volume.Visible = true;
                metroCheckBox_H264.Checked = false;
            }
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
                prevToolStripMenuItem.Enabled = true;
                nextToolStripMenuItem.Enabled = true;

                stringBuilder.Append(@"embed?listType=playlist&list=");

                stringBuilder.Append(strID);

                Parameter(!m_bCheck, stringBuilder);

                string strNum = metroTextBox_Number.Text;

                if (!string.IsNullOrEmpty(strNum))
                {
                    int itmp = Convert.ToInt32(strNum);
                    m_iPlaylistNumber = itmp;

                    stringBuilder.Append("&index=");

                    if (m_iPrevNext == 1)
                    {
                        itmp--;
                    }
                    else if (m_iPrevNext == 2)
                    {
                        itmp++;
                    }
                    else
                    {

                    }

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
                prevToolStripMenuItem.Enabled = false;
                nextToolStripMenuItem.Enabled = false;

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

            m_timer.Start();
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
            m_timer.Stop();

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
        /// Get Volume (CefSharp로 변경해서 미사용) -> 다시 사용 (Form2 주석 참고)
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
        /// Set Volume (CefSharp로 변경해서 미사용) -> 다시 사용 (Form2 주석 참고)
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

        /// <summary>
        /// 프로그램 종료
        /// </summary>
        protected void ProgramExit()
        {
            m_NotifyIcon.Visible = false;

            // 메모리, 리소스 해제 및 메세지 처리 후 종료
            g_program.ExitThread();

            Dispose();

            Application.Exit();
        }

        /// <summary>
        /// Style Mode
        /// </summary>
        protected void StyleMode()
        {
            MetroThemeStyle metroThemeStyle = m_bStyle ? MetroThemeStyle.Dark : MetroThemeStyle.Light;

            m_metroStyleManager.Theme = metroThemeStyle;

            // Metro 오브젝트가 StyleManager로 변경이 안됨 (???)
            // Label의 경우 Metro에 속해있지 않아서 직접 색깔을 변경해줘야 함
            foreach (Control control in Controls)
            {
                if (typeof(MetroButton) == control.GetType())
                {
                    (control as MetroButton).Theme = metroThemeStyle;
                }
                else if (typeof(MetroCheckBox) == control.GetType())
                {
                    (control as MetroCheckBox).Theme = metroThemeStyle;
                }
                else if (typeof(MetroTrackBar) == control.GetType())
                {
                    (control as MetroTrackBar).Theme = metroThemeStyle;
                }
                else if (typeof(MetroRadioButton) == control.GetType())
                {
                    (control as MetroRadioButton).Theme = metroThemeStyle;
                }
                else if (typeof(MetroTextBox) == control.GetType())
                {
                    (control as MetroTextBox).Theme = metroThemeStyle;
                }
                // ContextMenuStrip 적용 -> StyleManager의 기본 값을 Default로 맞춰야 함
                else if (typeof(MetroContextMenu) == control.GetType())
                {
                    (control as MetroContextMenu).Theme = metroThemeStyle;
                }
                else if (typeof(Label) == control.GetType())
                {
                    if (control.Name == label_TypeSingle.Name ||
                        control.Name == label_TypeList.Name ||
                        control.Name == label_StartOn.Name ||
                        control.Name == label_VolumeOn.Name ||
                        control.Name == label_VolumeError.Name ||
                        control.Name == label_H264On.Name)
                        continue;
                    else
                    {
                        (control as Label).ForeColor = (metroThemeStyle == MetroThemeStyle.Light) ? Color.Black : Color.White;
                    }
                }
            }

            m_setting.bStyle = m_bStyle;
            m_setting.SaveToFile(m_strSettingFile);

            // 오브젝트가 자동으로 업데이트 되지 않음
            Refresh();
        }

        /// <summary>
        /// 이전 목록 재생
        /// </summary>
        protected void Prev()
        {
            int itmp = int.Parse(metroTextBox_Number.Text);

            if (itmp > 1)
            {
                m_iPrevNext = 1;

                Play();
            }
        }

        /// <summary>
        /// 다음 목록 재생
        /// </summary>
        protected void Next()
        {
            int itmp = int.Parse(metroTextBox_Number.Text);

            if (itmp <= 999)
            {
                m_iPrevNext = 2;

                Play();
            }
        }

        /// <summary>
        /// 출력 모니터 변경
        /// </summary>
        protected void MultiMonitor()
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

        /// <summary>
        /// 시작 프로그램 등록
        /// </summary>
        protected void StartupPrograms(object objSender)
        {
            using (var varKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
            {
                if (objSender.Equals(metroCheckBox_StartupPrograms))
                {
                    if (metroCheckBox_StartupPrograms.Checked)
                    {
                        varKey.SetValue(constStrApplication, Application.ExecutablePath.ToString());
                        label_StartOn.Visible = true;
                        label_StartOff.Visible = false;
                        startupProgramToolStripMenuItem.Checked = true;

                        // MessageBox.Show("등록 성공!", "시작 프로그램 등록", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        varKey.DeleteValue(constStrApplication, false);
                        label_StartOn.Visible = false;
                        label_StartOff.Visible = true;
                        startupProgramToolStripMenuItem.Checked = false;

                        // MessageBox.Show("해제 성공!", "시작 프로그램 해제", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (startupProgramToolStripMenuItem.Checked)
                    {
                        varKey.SetValue(constStrApplication, Application.ExecutablePath.ToString());
                        label_StartOn.Visible = true;
                        label_StartOff.Visible = false;
                        metroCheckBox_StartupPrograms.Checked = true;

                        // MessageBox.Show("등록 성공!", "시작 프로그램 등록", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        varKey.DeleteValue(constStrApplication, false);
                        label_StartOn.Visible = false;
                        label_StartOff.Visible = true;
                        metroCheckBox_StartupPrograms.Checked = false;

                        // MessageBox.Show("해제 성공!", "시작 프로그램 해제", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        // <summary>
        /// 도움말
        /// </summary>
        protected void Help()
        {
            Form5 form5 = new Form5();
            form5.ShowDialog();
        }

        /// <summary>
        /// 데이터 파싱
        /// </summary>
        protected void Parsing()
        {
            try
            {
                // </a><div class="ytp-title-subtext">
                // feature=player-title
                if (m_strHtml != null)
                {
                    m_strNameOld = m_strNameNew;

                    int iStart = m_strHtml.IndexOf("feature=player-title");
                    iStart = m_strHtml.IndexOf(">", iStart);
                    int iEnd = m_strHtml.IndexOf("<", iStart);

                    m_strNameNew = m_strHtml.Substring(iStart + 1, iEnd - iStart - 1);

                    label_VideoName.Text = m_strNameNew;

                    if (m_strNameOld != m_strNameNew)
                    {
                        if (m_iPrevNext == 1)
                        {
                            m_iPlaylistNumber--;
                        }
                        else if (m_iPrevNext == 2)
                        {
                            m_iPlaylistNumber++;
                        }
                        else if (m_iPrevNext == 3)
                        {
                            m_iPlaylistNumber++;
                        }
                        else
                        {

                        }

                        metroTextBox_Number.Text = m_iPlaylistNumber.ToString();

                        m_iPrevNext = 3;

                        m_setting.strNumber = metroTextBox_Number.Text;
                        m_setting.SaveToFile(m_strSettingFile);

                        // 단일 동영상의 경우 타이머를 계속 돌릴 필요가 없음
                        if (m_bCheck)
                        {
                            m_timer.Stop();
                        }
                    }
                }
            }
            catch
            {
                
            }
        }

        /// <summary>
        /// 버전 체크
        /// </summary>
        protected async void VersionCheck()
        {
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    string strtmp = await webClient.DownloadStringTaskAsync(@"https://raw.githubusercontent.com/sch6393/YoutubeWallpapers/master/YoutubeWallpapers/Properties/AssemblyInfo.cs");

                    int iStart = strtmp.LastIndexOf("AssemblyVersion");

                    iStart = strtmp.IndexOf('\"', iStart) + 1;
                    int iEnd = strtmp.IndexOf('\"', iStart);

                    string strVersion = strtmp.Substring(iStart, iEnd - iStart);

                    if (Version.Parse(strVersion) > Version.Parse(Application.ProductVersion))
                    {
                        TransparentLabel();

                        TransparentLabelTop transparentLabelTop = new TransparentLabelTop();
                        transparentLabelTop.Height = 16;
                        transparentLabelTop.Width = 151;
                        transparentLabelTop.Top = 40;
                        transparentLabelTop.Left = 400;
                        transparentLabelTop.Text = "TOPTOPTOPTOPTOP";
                        // transparentLabelTop.Font = new Font(m_fontFamily, 10, FontStyle.Bold);
                        m_libFont.FontSet(transparentLabelTop, 10f, FontStyle.Bold);
                        this.Controls.Add(transparentLabelTop);
                        transparentLabelTop.Visible = true;
                        transparentLabelTop.BringToFront();
                        transparentLabelTop.Cursor = Cursors.Hand;
                        transparentLabelTop.Click += TransparentLabelTop_Click;

                        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                        timer.Interval = 100;
                        timer.Tick += Timer_Tick;
                        timer.Enabled = true;
                    }
                }
                catch (WebException)//webEx)
                {
                    Form4.DialogCustom("Caution!", "Unable to Check Version Without Internet Connection!");
                }
                catch (Exception)//ex)
                {
                    Form4.DialogCustom("Error!", "Failed to Version Check!");
                }
            }
        }

        /// <summary>
        /// 정주기 (100ms)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (m_bTransparent)
            {
                m_iTransparent += 20;
            }
            else
            {
                m_iTransparent -= 20;
            }

            if (m_iTransparent > 235)
            {
                m_bTransparent = false;
            }
            else if (m_iTransparent < 235 && m_iTransparent > 25)
            {

            }
            else if (m_iTransparent < 25)
            {
                m_bTransparent = true;
            }

            TransparentLabel();
        }

        /// <summary>
        /// New Version Available 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TransparentLabelTop_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"https://github.com/sch6393/YoutubeWallpapers/releases");
        }

        /// <summary>
        /// 투명 라벨 소멸 & 생성
        /// </summary>
        protected void TransparentLabel()
        {
            m_transparentLabel.Dispose();

            m_transparentLabel = new TransparentLabel();
            m_transparentLabel.Height = 16;
            m_transparentLabel.Width = 151;
            m_transparentLabel.Top = 40;
            m_transparentLabel.Left = 400;
            m_transparentLabel.Text = "New Version Available";
            // m_transparentLabel.Font = new Font(m_fontFamily, 10, FontStyle.Bold);
            m_libFont.FontSet(m_transparentLabel, 10f, FontStyle.Bold);
            m_transparentLabel.ForeColor = Color.Crimson;
            this.Controls.Add(m_transparentLabel);
            m_transparentLabel.Visible = true;
            // m_transparentLabel.BringToFront();
            // m_transparentLabel.SendToBack();
            // m_transparentLabel.Cursor = Cursors.Hand;
            // m_transparentLabel.Click += M_transparentLabel_Click;
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

        /// <summary>
        /// 아이콘 메뉴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void M_NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            // 임시 제한
            //// 우클릭시
            //if (e.Button == MouseButtons.Right)
            //{
            //    m_NotifyIcon.ContextMenuStrip = m_metroContextMenu;

            //    MethodInfo methodInfo = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
            //    methodInfo.Invoke(m_NotifyIcon, null);

            //    //videoNameToolStripMenuItem.Text = label_VideoName.Text;
            //    //volumeSetToolStripMenuItem.Text = metroTrackBar_Volume.Value.ToString();
            //    //brightnessSetToolStripMenuItem.Text = (metroTrackBar_Brightness.Value * 2).ToString();

            //    //volumeSetToolStripMenuItem.Theme = m_bStyle ? MetroThemeStyle.Dark : MetroThemeStyle.Light;

            //    //volumeSetToolStripMenuItem.Value = Form2.iVolume;
            //    //brightnessSetToolStripMenuItem.Value = m_iBrightness;

            //    nameToolStripMenuItem.Text = m_strNameNew;
            //}
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

        /// <summary>
        /// 볼륨 조절
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            StartupPrograms(sender);
        }

        /// <summary>
        /// 볼륨 설정 (CefSharp로 변경해서 미사용) -> 다시 사용 (Form2 주석 참고)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metroCheckBox_Volume_Click(object sender, EventArgs e)
        {
            if (label_VolumeOn.Visible)
            {
                SetVolume(false);
            }
            else if (label_VolumeOff.Visible)
            {
                SetVolume(true);
            }

            Form4.DialogCustom("Volume Setup is Complete!", "Restart Program!");

            // 재시작을 해야 볼륨 설정이 적용
            Application.Restart();
        }

        /// <summary>
        /// H264
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MetroCheckBox_H264_Click(object sender, EventArgs e)
        {
            if (metroCheckBox_H264.Checked)
            {
                m_bH264 = true;
            }
            else
            {
                m_bH264 = false;
            }

            m_setting.bH264 = m_bH264;
            m_setting.SaveToFile(m_strSettingFile);

            Form4.DialogCustom("H.264 Setup is Complete!", "Restart Program!");

            // 재시작을 해야 안전하게 설정이 적용
            Application.Restart();
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
            Help();
        }

        /// <summary>
        /// 시작
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metroButton_Start_Click(object sender, EventArgs e)
        {
            m_iPrevNext = 0;

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
            Prev();
        }

        /// <summary>
        /// 다음 목록 재생
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metroButton_Next_Click(object sender, EventArgs e)
        {
            Next();
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
            MultiMonitor();
        }

        /// <summary>
        /// Light
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MetroButton_Light_Click(object sender, EventArgs e)
        {
            m_bStyle = false;

            StyleMode();
        }

        /// <summary>
        /// Dark
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MetroButton_Dark_Click(object sender, EventArgs e)
        {
            m_bStyle = true;

            StyleMode();
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

        #region RadioButton Event

        /// <summary>
        /// Radio Button Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MetroRadioButton_Click(object sender, EventArgs e)
        {
            metroButton_Start_Click(null, e);
        }

        #endregion

        #region ContextMenuStrip Event

        /// <summary>
        /// 재활성화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowWindow();
        }

        /// <summary>
        /// Video Quality Auto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            autoToolStripMenuItem.Checked = true;
            video720ToolStripMenuItem.Checked = false;
            video1080ToolStripMenuItem.Checked = false;
            video1440ToolStripMenuItem.Checked = false;

            metroRadioButton_Auto.Checked = true;
            metroRadioButton_720.Checked = false;
            metroRadioButton_1080.Checked = false;
            metroRadioButton_1440.Checked = false;

            MetroRadioButton_Click(null, e);
        }

        /// <summary>
        /// Video Quality 720
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Video720ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            autoToolStripMenuItem.Checked = false;
            video720ToolStripMenuItem.Checked = true;
            video1080ToolStripMenuItem.Checked = false;
            video1440ToolStripMenuItem.Checked = false;

            metroRadioButton_Auto.Checked = false;
            metroRadioButton_720.Checked = true;
            metroRadioButton_1080.Checked = false;
            metroRadioButton_1440.Checked = false;

            MetroRadioButton_Click(null, e);
        }

        /// <summary>
        /// Video Quality 1080
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Video1080ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            autoToolStripMenuItem.Checked = false;
            video720ToolStripMenuItem.Checked = false;
            video1080ToolStripMenuItem.Checked = true;
            video1440ToolStripMenuItem.Checked = false;

            metroRadioButton_Auto.Checked = false;
            metroRadioButton_720.Checked = false;
            metroRadioButton_1080.Checked = true;
            metroRadioButton_1440.Checked = false;

            MetroRadioButton_Click(null, e);
        }

        /// <summary>
        /// Video Quality 1440
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Video1440ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            autoToolStripMenuItem.Checked = false;
            video720ToolStripMenuItem.Checked = false;
            video1080ToolStripMenuItem.Checked = false;
            video1440ToolStripMenuItem.Checked = true;

            metroRadioButton_Auto.Checked = false;
            metroRadioButton_720.Checked = false;
            metroRadioButton_1080.Checked = false;
            metroRadioButton_1440.Checked = true;

            MetroRadioButton_Click(null, e);
        }

        /// <summary>
        /// 시작
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Play();
        }

        /// <summary>
        /// 정지
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stop();
        }

        /// <summary>
        /// 이전 목록 재생
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrevToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Prev();
        }

        /// <summary>
        /// 다음 목록 재생
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Next();
        }

        /// <summary>
        /// 출력 모니터 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextMonitorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MultiMonitor();
        }

        /// <summary>
        /// 시작 프로그램 등록
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartupProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartupPrograms(sender);
        }

        /// <summary>
        /// 프로그램 종료
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProgramExit();
        }

        #endregion

    }

    #region Transparent Label Class

    /// <summary>
    /// 투명 라벨
    /// </summary>
    public class TransparentLabel : Control
    {

        public TransparentLabel()
        {
            TabStop = false;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= 0x20;

                return createParams;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(Form1.m_iTransparent, 220, 20, 60)))
            {
                e.Graphics.DrawString(Text, Font, solidBrush, -1, 0);
            }
        }
    }

    /// <summary>
    /// 투명 라벨 Top
    /// </summary>
    public class TransparentLabelTop : Control
    {

        public TransparentLabelTop()
        {
            TabStop = false;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= 0x20;

                return createParams;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(0, 0, 0, 0)))
            {
                e.Graphics.DrawString(Text, Font, solidBrush, -1, 0);
            }
        }
    }

    #endregion

}
