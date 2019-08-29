using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MetroFramework.Forms;

using SCHLibFont;

namespace YoutubeWallpapers
{
    public partial class Form5 : MetroForm
    {
        /// <summary>
        /// 폰트 선언
        /// </summary>
        protected SCHFont m_SCHFont = new SCHFont();

        public Form5()
        {
            InitializeComponent();

            m_SCHFont.FontCollection();
            FontSet();

            label6.Text = "Version " + Application.ProductVersion.Substring(0, 3);
        }

        private void Form5_FormClosed(object sender, FormClosedEventArgs e)
        {
            //// 메모리, 리소스 해제
            //this.Close();
            //this.Dispose();
        }

        /// <summary>
        /// 폰트 설정
        /// </summary>
        protected void FontSet()
        {
            m_SCHFont.FontSet(label1, 10f, FontStyle.Bold);
            m_SCHFont.FontSet(label2, 10f, FontStyle.Regular);
            m_SCHFont.FontSet(label3, 10f, FontStyle.Bold);
            m_SCHFont.FontSet(label4, 10f, FontStyle.Regular);
            m_SCHFont.FontSet(label5, 10f, FontStyle.Bold);
            m_SCHFont.FontSet(label6, 10f, FontStyle.Bold);

            m_SCHFont.FontSet(linkLabel1, 10f, FontStyle.Regular);
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/sch6393");
        }
    }
}
