using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.Win32;

using libWallpaper;

using CefSharp;
using CefSharp.WinForms;

namespace YoutubeWallpapers
{
    /// <summary>
    /// v1.1 : 기본 웹브라우저를 쓰면 인터넷 속도가 잘 나옴에도 불구하고 자동으로 화질을 떨어뜨리는 문제가 발생해서 CefSharp로 변경함
    /// v1.2 : CefSharp를 사용하면 H.264 코덱이 미포함되어있어 해당 코덱 영상이나 스트리밍 영상이 재생되지 않는 문제가 발생
    ///        WebBrowser는 H.264 코덱에 영향을 받지 않으므로 따로 체크 옵션을 두어 해당 코덱 영상이나 스트리밍 영상을 재생할 때는 WebBrowser를 이용
    ///        CefSharp, WebBrowser 따로 작동시키려니 로직상 미리 정의해놓은 CefSharp로 인해 프로그램 종료가 제대로 되지 않는 문제가 발생함 -> 추후 방법 모색
    ///        일단 조치사항으로 H.264가 체크되어 있지 않다면 CefSharp만 사용
    ///        체크되있을 때는 브라우저 2개를 동시에 사용하고 CefSharp는 Unvisible 상태에 볼륨 값을 0으로 변경
    ///        어차피 H.264 코덱 영상이나 스트리밍 영상은 CefSharp에선 정상 작동하지 않으므로 인터넷 속도에 영향은 없음
    ///        다만 체크된 상태에서 H.264 코덱 영상이나 스트리밍 영상이 아닌 다른 영상을 재생할 경우 CefSharp에서도 정상 작동되므로 인터넷 속도에 주의할 필요가 있음
    ///        v1.1에서 발생했던 WebBrowser의 화질 저하 문제는 익스플로러 버전 값을 11001로 설정하면 해결됨
    ///        (인터넷 속도에 문제가 없고 처음에 설정한 화질이 나오더라도 동영상이 진행될 수록 화질을 한 단계씩 계속 내리는 현상)
    ///        하지만 11001로 설정한 이후 화질이 한 단계씩 계속 내려가는 현상은 없어졌으나 처음 설정한 화질이 설정되지 못하는 현상이 가끔 발생함 -> 추후 방법 모색
    /// </summary>
    public partial class Form2 : Form
    {
        [DllImport("winmm.dll")]
        public static extern int waveOutGetVolume(IntPtr h, out uint dwVolume);

        [DllImport("winmm.dll")]
        public static extern int waveOutSetVolume(IntPtr h, uint dwVolume);

        /// <summary>
        /// Background 변수들
        /// </summary>
        private bool m_bFixed = false;
        private int m_iMonitor = 0;

        /// <summary>
        /// CefSharp
        /// </summary>
        protected ChromiumWebBrowser m_chromiumWebBrowser;

        /// <summary>
        /// 볼륨 값
        /// </summary>
        public static int iVolume
        {
            get
            {
                uint uitmp = 0;

                waveOutGetVolume(IntPtr.Zero, out uitmp);

                return (int)((double)(uitmp & 0xFFFF) * 100 / 0xFFFF);
            }
            set
            {
                uint uitmp = (uint)((double)0xFFFF * value / 100) & 0xFFFF;

                waveOutSetVolume(IntPtr.Zero, (uitmp << 16) | uitmp);
            }
        }

        public Form2()
        {
            // CefSharp로 변경해서 사용하지 않음 -> 다시 사용 (Form2 주석 참고)
            // 웹브라우저 컨트롤 생성 전에 등록
            SetBrowserFeatureControl();

            InitializeComponent();

            Monitor = m_iMonitor;

            Background();

            CefSharpBrowser();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // DPI의 영향을 받지 않는 해상도 값
            Utility.Initialize();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!Form1.m_bH264)
            {
                m_timer.Stop();
            }                

            Cef.Shutdown();
        }

        /// <summary>
        /// CefSharp Browser
        /// </summary>
        protected void CefSharpBrowser()
        {
            CultureInfo cultureInfo = CultureInfo.InstalledUICulture;

            CefSettings cefSettings = new CefSettings();

            // 소리
            // cefSettings.CefCommandLineArgs.Add("mute-audio", "true");

            // 자동 실행 정책
            cefSettings.CefCommandLineArgs["autoplay-policy"] = "no-user-gesture-required";

            // 지역 설정
            cefSettings.Locale = cultureInfo.TwoLetterISOLanguageName; // cultureInfo.ThreeLetterISOLanguageName;

            Cef.Initialize(cefSettings);

            m_chromiumWebBrowser = new ChromiumWebBrowser("about:blank");
            m_chromiumWebBrowser.Dock = DockStyle.Fill;
            m_chromiumWebBrowser.FrameLoadEnd += M_chromiumWebBrowser_FrameLoadEnd;
            Controls.Add(m_chromiumWebBrowser);
        }

        /// <summary>
        /// WebBrowser Start
        /// </summary>
        /// <param name="url"></param>
        public void BrowserURL(string url)
        {
            webBrowser.Visible = Form1.m_bH264;
            m_chromiumWebBrowser.Visible = !Form1.m_bH264;

            if (Form1.m_bH264)
            {
                webBrowser.Navigate(url);

                iVolume = 0;
            }

            m_chromiumWebBrowser.Load(url);
        }

        /// <summary>
        /// Stop
        /// </summary>
        public void Stop()
        {
            if (Form1.m_bH264)
            {
                webBrowser.Navigate("");
                m_chromiumWebBrowser.Load("about:blank");
            }
            else
            {
                m_chromiumWebBrowser.Load("about:blank");
            }
        }

        /// <summary>
        /// 브라우저 로딩이 끝난 후 (CefSharp)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void M_chromiumWebBrowser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (e.Frame.IsMain)
            {
                // m_chromiumWebBrowser.ViewSource();
                m_chromiumWebBrowser.GetSourceAsync().ContinueWith(taskHtml =>
                {
                    Form1.m_strHtml = taskHtml.Result;
                });

                m_timer.Start();
            }
        }

        /// <summary>
        /// 정주기 (2초)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void M_timer_Tick(object sender, EventArgs e)
        {
            HtmlSource();
        }

        /// <summary>
        /// Html Source
        /// </summary>
        protected void HtmlSource()
        {
            // WebBrowser의 경우 Html Source가 동적으로 읽어지지 않음
            // 어차피 CefSharp 동시 실행이므로 관계 없음 -> Form2 주석 참고
            m_chromiumWebBrowser.GetSourceAsync().ContinueWith(taskHtml =>
            {
                Form1.m_strHtml = taskHtml.Result;
            });

            if (Form1.m_bCheck)
            {
                m_timer.Stop();
            }
        }

        #region Background

        protected bool Background()
        {
            m_bFixed = Wallpaper.Background(this.Handle);

            if (m_bFixed)
            {
                Utility.FillMonitor(this, MonitorInfo);
            }

            return m_bFixed;
        }

        public WinApi.MONITORINFO MonitorInfo
        {
            get
            {
                if (Monitor < Utility.g_staticMONITORINFO.Length)
                    return Utility.g_staticMONITORINFO[Monitor];

                return new WinApi.MONITORINFO()
                {
                    rcMonitor = Screen.PrimaryScreen.Bounds,
                    rcWork = Screen.PrimaryScreen.WorkingArea,
                };
            }
        }

        public bool Fixed
        {
            get
            {
                return m_bFixed;
            }
        }

        public int Monitor
        {
            get
            {
                return m_iMonitor;
            }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                else if (value >= Screen.AllScreens.Length)
                {
                    value = 0;
                }

                if (m_iMonitor != value)
                {
                    m_iMonitor = value;

                    Background();
                }
            }
        }

        #endregion

        #region IE Feature Control (CefSharp로 변경해서 미사용) -> 다시 사용 (Form2 주석 참고)

        // https://stackoverflow.com/questions/18333459/c-sharp-webbrowser-ajax-call

        private void SetBrowserFeatureControlKey(string feature, string appName, uint value)
        {
            using (var key = Registry.CurrentUser.CreateSubKey(
                string.Concat(@"Software\Microsoft\Internet Explorer\Main\FeatureControl\", feature),
                RegistryKeyPermissionCheck.ReadWriteSubTree))
            {
                key.SetValue(appName, value, RegistryValueKind.DWord);
            }
        }

        private void SetBrowserFeatureControl()
        {
            // http://msdn.microsoft.com/en-us/library/ee330720(v=vs.85).aspx

            // FeatureControl settings are per-process
            var fileName = System.IO.Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);

            // make the control is not running inside Visual Studio Designer
            if (string.Compare(fileName, "devenv.exe", true) == 0 || string.Compare(fileName, "XDesProc.exe", true) == 0)
                return;

            // Webpages containing standards-based !DOCTYPE directives are displayed in IE10 Standards mode.
            SetBrowserFeatureControlKey("FEATURE_BROWSER_EMULATION", fileName, GetBrowserEmulationMode());
            SetBrowserFeatureControlKey("FEATURE_AJAX_CONNECTIONEVENTS", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_ENABLE_CLIPCHILDREN_OPTIMIZATION", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_MANAGE_SCRIPT_CIRCULAR_REFS", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_DOMSTORAGE ", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_GPU_RENDERING ", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_IVIEWOBJECTDRAW_DMLT9_WITH_GDI  ", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_DISABLE_LEGACY_COMPRESSION", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_LOCALMACHINE_LOCKDOWN", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_BLOCK_LMZ_OBJECT", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_BLOCK_LMZ_SCRIPT", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_DISABLE_NAVIGATION_SOUNDS", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_SCRIPTURL_MITIGATION", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_SPELLCHECKING", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_STATUS_BAR_THROTTLING", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_TABBED_BROWSING", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_VALIDATE_NAVIGATE_URL", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_WEBOC_DOCUMENT_ZOOM", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_WEBOC_POPUPMANAGEMENT", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_WEBOC_MOVESIZECHILD", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_ADDON_MANAGEMENT", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_WEBSOCKET", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_WINDOW_RESTRICTIONS ", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_XMLHTTP", fileName, 1);
        }

        private uint GetBrowserEmulationMode()
        {
            int browserVersion = 11;

            using (var ieKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer",
                RegistryKeyPermissionCheck.ReadSubTree,
                System.Security.AccessControl.RegistryRights.QueryValues))
            {
                var version = ieKey.GetValue("svcVersion");

                if (null == version)
                {
                    version = ieKey.GetValue("Version");

                    if (null == version)
                        throw new ApplicationException("Microsoft Internet Explorer is required!");
                }

                int.TryParse(version.ToString().Split('.')[0], out browserVersion);
            }

            // 11001 : Internet Explorer 11. Webpages are displayed in IE11 edge mode, regardless of the !DOCTYPE directive.
            uint mode = 11001;

            switch (browserVersion)
            {
                // Webpages containing standards-based !DOCTYPE directives are displayed in IE7 Standards mode. Default value for applications hosting the WebBrowser Control.
                case 7:
                    mode = 7000;
                    break;

                // Webpages containing standards-based !DOCTYPE directives are displayed in IE8 mode. Default value for Internet Explorer 8
                case 8:
                    mode = 8000;
                    break;

                // Internet Explorer 9. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode. Default value for Internet Explorer 9.
                case 9:
                    mode = 9000;
                    break;

                // Internet Explorer 10. Webpages containing standards-based !DOCTYPE directives are displayed in IE10 mode. Default value for Internet Explorer 10.
                case 10:
                    mode = 10000;
                    break;

                // 11000 : Internet Explorer 11. Webpages containing standards-based !DOCTYPE directives are displayed in IE11 Standards mode. Default value for Internet Explorer 11.
                case 11:
                    // mode = 11000;
                    mode = 11001;
                    break;

                default:
                    break;
            }

            return mode;
        }

        #endregion

        #region Alt + Tab Unvisible

        protected override CreateParams CreateParams
        {
            get
            {
                // Turn on WS_EX_TOOLWINDOW style bit
                CreateParams createParams = base.CreateParams;

                createParams.ExStyle |= 0x80;

                return createParams;
            }
        }


        #endregion

    }
}
