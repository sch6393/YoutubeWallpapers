using System;
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

using MetroFramework.Forms;

using SCHLibFont;

namespace YoutubeWallpapers
{
    public partial class Form4 : MetroForm
    {
        /// <summary>
        /// 폰트 선언
        /// </summary>
        protected SCHFont m_SCHFont = new SCHFont();

        protected static Form4 m_form4;

        public Form4()
        {
            InitializeComponent();

            m_SCHFont.FontCollection();
            FontSet();
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
            m_SCHFont.FontSet(label_Message, 10f, FontStyle.Regular);
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
    }
}
