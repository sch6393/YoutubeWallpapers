﻿using System;
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
    /// 기본 웹브라우저를 쓰면 인터넷 속도가 잘 나옴에도 불구하고 자동으로 화질을 떨어뜨리는 문제가 발생해서 CefSharp로 변경함
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
            // CefSharp로 변경해서 사용하지 않음
            // 웹브라우저 컨트롤 생성 전에 등록
            // SetBrowserFeatureControl();

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
            Controls.Add(m_chromiumWebBrowser);
        }

        /// <summary>
        /// WebBrowser Start
        /// </summary>
        public void BrowserURL(string url)
        {
            m_chromiumWebBrowser.Load(url);

            // webBrowser.Navigate(url);
        }

        /// <summary>
        /// Stop
        /// </summary>
        public void Stop()
        {
            m_chromiumWebBrowser.Load("about:blank");

            // webBrowser.Navigate("about:blank");
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

        #region IE Feature Control (CefSharp로 변경해서 미사용)

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
            int browserVersion = 7;

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
                    mode = 11000;
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