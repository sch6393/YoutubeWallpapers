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

using libFont;

namespace YoutubeWallpapers
{
    public partial class Form5 : MetroForm
    {
        /// <summary>
        /// 폰트 선언
        /// </summary>
        protected LibFont m_libFont = new LibFont();

        public Form5()
        {
            InitializeComponent();

            m_libFont.FontCollection();
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
            m_libFont.FontSet(label1, 10f, FontStyle.Bold);
            m_libFont.FontSet(label2, 10f, FontStyle.Regular);
            m_libFont.FontSet(label3, 10f, FontStyle.Bold);
            m_libFont.FontSet(label4, 10f, FontStyle.Regular);
            m_libFont.FontSet(label5, 10f, FontStyle.Bold);
            m_libFont.FontSet(label6, 10f, FontStyle.Bold);

            m_libFont.FontSet(linkLabel1, 10f, FontStyle.Regular);
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/sch6393");
        }
    }
}
