using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using Smobiler.Core.Controls.Native;

namespace SmoONE.UI.Attendance
{
    partial class frmATGPSEdit : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        //����
        public float Longitude;
        //γ��
        public float Latitude;
        public string addressInfo;
         #endregion

        /// <summary>
        /// �ص�΢��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mapView1_TagClick(object sender, MapViewTagClickEventArgs e)
        {
            if ((!e.Tag.Longitude.Equals(Longitude)) & (!e.Tag.Latitude.Equals(Latitude)))
            {
                MessageBox.Show("�Ƿ�ȷ�����ĵص㣿", "�ص�΢��", MessageBoxButtons.YesNo, (Object s, MessageBoxHandlerArgs args) =>
                {
                    if (args.Result == Smobiler.Core.Controls.ShowResult.Yes)
                    {
                        Longitude = e.Tag.Longitude;
                        Latitude = e.Tag.Latitude;
                        addressInfo = e.Tag.Description;
                        ShowResult = ShowResult.Yes;
                        
                    }
                });
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSignGPSEdit_Load(object sender, EventArgs e)
        {
            mapTrimView1.Longitude = Longitude;
            mapTrimView1.Latitude = Latitude;
            mapTrimView1.Description = addressInfo;
        }
        /// <summary>
        /// �ֻ��Դ����ؼ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSignGPSEdit_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                Close();
            }
        }
    }
}