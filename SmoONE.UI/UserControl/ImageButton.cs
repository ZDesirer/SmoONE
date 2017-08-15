using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using System.ComponentModel;
using System.Drawing;

namespace SmoONE.UI.UserControl
{
    [ToolboxItem(true), DefaultEvent("Press")]
    partial class ImageButton : Smobiler.Core.Controls.MobileUserControl
    {
        /// <summary>
        /// ͼ����Դ����
        /// </summary>
        [Browsable(true), Category("Appearance"), DefaultValue(""), Description("ͼ����Դ����")]
        public string ResourceID
        {
            get
            {
                return this.imageEx1.ResourceID;
            }
            set
            {
                this.imageEx1.ResourceID = value;
            }
        }
        /// <summary>
        /// ͼƬ���
        /// </summary>
        [Browsable(true), Category("����"), DefaultValue(""), Description("����ͼƬ�Ŀ�Ⱥ͸߶�")]
        public Size iamgeSize
        {
            get
            {
                return this.imageEx1.Size;
            }
            set
            {
                this.panel1.Size = value;
                this.imageEx1.Size = value;
            }
        }
        /// <summary>
        /// ͼ������
        /// </summary>
        [Browsable(true), Category("Appearance"), DefaultValue(""), Description("ͼ������")]
        public ImageEx.ImageStyle ImageType
        {
            get
            {
                return this.imageEx1.ImageType;
            }
            set
            {
                this.imageEx1.ImageType = value;
            }
        }
    }
}