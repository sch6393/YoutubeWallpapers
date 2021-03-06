﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing.Text;
using System.Runtime.InteropServices;

using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Forms;

using libFont;

namespace YoutubeWallpapers
{
    public partial class Form4 : MetroForm
    {
        /// <summary>
        /// 폰트 선언
        /// </summary>
        protected LibFont m_libFont = new LibFont();

        protected static Form4 m_form4;

        public Form4()
        {
            InitializeComponent();

            m_libFont.FontCollection();
            FontSet();

            StyleManager = m_metroStyleManager;

            StyleMode();
        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            //// 메모리, 리소스 해제
            //this.Close();
            //this.Dispose();
        }

        #region Font

        /// <summary>
        /// 폰트 설정
        /// </summary>
        protected void FontSet()
        {
            m_libFont.FontSet(label_Message, 10f, FontStyle.Regular);
        }

        #endregion

        public static DialogResult DialogCustom(string strTitle, string strMessage)
        {
            m_form4 = new Form4();
            m_form4.Text = strTitle;
            m_form4.label_Message.Text = strMessage;
            m_form4.ShowDialog();

            return DialogResult.Yes;
        }

        private void metroButton_Click(object sender, EventArgs e)
        {
            m_form4.Close();
        }

        /// <summary>
        /// Style Mode
        /// </summary>
        protected void StyleMode()
        {
            MetroThemeStyle metroThemeStyle = Form1.m_bStyle ? MetroThemeStyle.Dark : MetroThemeStyle.Light;

            m_metroStyleManager.Theme = metroThemeStyle;
            m_metroStyleManager.Style = MetroColorStyle.Yellow;

            foreach (Control control in Controls)
            {
                if (typeof(MetroButton) == control.GetType())
                {
                    (control as MetroButton).Theme = metroThemeStyle;
                }
                else if (typeof(Label) == control.GetType())
                {
                    (control as Label).ForeColor = (metroThemeStyle == MetroThemeStyle.Light) ? Color.Black : Color.White;
                }
            }

            // 오브젝트가 자동으로 업데이트 되지 않음
            Refresh();
        }
    }
}
