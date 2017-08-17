using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using System.ComponentModel;

namespace SmoONE.UI.Layout
{
    [System.ComponentModel.ToolboxItem(true)]
     public partial class Title : Smobiler.Core.Controls.MobileUserControl
    {
        /// <summary>
        /// ��������
        /// </summary>
        [Browsable(true),Category("Appearance"),DefaultValue(""),Description("����")]
        public string TitleText
        {
            get
            {
                return this.label1.Text;
            }
            set
            {
                this.label1.Text = value;

            }
        }
        /// <summary>
        /// ���������С
        /// </summary>
        [Browsable(true), Category("Appearance"), DefaultValue(""), Description("���������С")]
        public float FontSize
        {
            get
            {
                return this.label1.FontSize;
            }
            set
            {
                this.label1.FontSize = value;

            }
        }
        /// <summary>
        /// ����������ɫ
        /// </summary>
        [Browsable(true), Category("Appearance"), DefaultValue(""), Description("�ı���ɫ")]
        public System.Drawing.Color ForeColr
        {
            get
            {
                return this.label1.ForeColor;
            }
            set
            {
                this.label1.ForeColor = value;

            }
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel1_Press(object sender, EventArgs e)
        {
            this.Form.Close();
        }

      
    }
}