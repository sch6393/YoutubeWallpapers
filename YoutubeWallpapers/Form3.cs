using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using libWallpaper;

namespace YoutubeWallpapers
{
    public partial class Form3 : Form
    {
        /// <summary>
        /// Program 주 진입점 선언
        /// </summary>
        public Program g_program;

        private int m_iMonitor = 0;

        public Form3()
        {
            InitializeComponent();

            Monitor = m_iMonitor;

            Background();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // DPI의 영향을 받지 않는 해상도 값
            Utility.Initialize();
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            g_program.ExitThread();
        }

        #region Background

        protected bool Background()
        {
            Fixed = Wallpaper.Background(this.Handle);

            if (Fixed)
            {
                Utility.FillMonitor(this, MonitorInfo);
            }

            return Fixed;
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

        public bool Fixed { get; private set; } = false;

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

        #region Brightness Control

        protected override void WndProc(ref Message m)
        {
            // WM_NCHITTEST
            if (m.Msg == 0x0084)
            {
                // HTTRANSPARENT
                m.Result = (IntPtr)(-1);
            }
            else
            {
                base.WndProc(ref m);
            }
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
