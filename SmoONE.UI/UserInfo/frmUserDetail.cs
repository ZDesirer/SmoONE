using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.DTOs;

namespace SmoONE.UI.UserInfo
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 1.0
    // Copyright  (c)  2016-2017 Smobiler 
    // ����ʱ�䣺 2016/11
    // ��Ҫ���ݣ�  �û��������
    // ******************************************************************
    partial class frmUserDetail : Smobiler.Core.MobileForm
    {
        #region "definition"
        internal string U_ID;//�û����
        private Sex sex;//�Ա�
        private string email = "";//�ʼ�
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
        private void frmUserDetail_Load(object sender, EventArgs e)
        {
            GetUser();
        }
        /// <summary>
        /// ��ȡ�û���Ϣ
        /// </summary>
        private void GetUser()
        {
            try
            {
                if (string.IsNullOrEmpty(U_ID) == true)
                {
                    throw new Exception("������绰���룡");

                }
                UserDetails userDetails = new UserDetails();
                UserDetailDto user = userDetails.getUser(U_ID);
                if (user != null)
                {
                    imgPortrait.ResourceID = user.U_Portrait;
                    string name = user.U_Name;
                    sex = (Sex)user.U_Sex;
                    switch (sex)
                    {
                        case Sex.��:
                            lblName.Text = name + "  ��";
                            break;
                        case Sex.Ů:
                            lblName.Text = name + "  Ů";
                            break;

                    }
                    lblTel.Text = U_ID;
                    lblBirthday.Text = user.U_Birthday.ToString("yyyy/MM/dd");
                    email = user.U_Email;
                    lblEmail.Text = user.U_Email;
                }
                else
                {
                    throw new Exception("�û�" + U_ID + "�����ڣ����飡");
                }
                //UserDetailDto user = AutofacConfig.userService.GetUserByUserID(U_ID);
                //if (user != null)
                //{
                //    if (string.IsNullOrEmpty(user.U_Portrait) == true)
                //    {
                //        switch (user.U_Sex)
                //        {
                //            case (int)Sex.��:
                //                imgPortrait.ResourceID = "boy";
                //                break;
                //            case (int)Sex.Ů:
                //                imgPortrait.ResourceID = "girl";
                //                break;
                //        }
                //    }
                //    else
                //    {
                //        imgPortrait.ResourceID = user.U_Portrait;
                //    }
                //    string name = user.U_Name;
                //    sex = (Sex)user.U_Sex;
                //    switch (sex)
                //    {
                //        case Sex.��:
                //            lblName.Text = name+"  ��";
                //            break;
                //        case Sex.Ů:
                //            lblName.Text = name + "  Ů";
                //            break;

                //    }
                //    lblTel.Text = U_ID;
                //    lblBirthday.Text = user.U_Birthday.ToString ("yyyy/MM/dd");
                //    email = user.U_Email;
                //    lblEmail.Text = user.U_Email;
                //}
                //else
                //{
                //    throw new Exception("�û�" + U_ID + "�����ڣ����飡");
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// ��绰
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTel_Click(object sender, EventArgs e)
        {
            Client.TelCall(U_ID);
        }
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMes_Click(object sender, EventArgs e)
        {
            Client.SendSMS("", U_ID);
        }
        /// <summary>
        /// ���ʼ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEmail_Click(object sender, EventArgs e)
        {
            Client.SendEmail("","",email );
        }
        /// <summary>
        ///  �ֻ��Դ����˰�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmUserDetail_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                Close();         //�رյ�ǰҳ��
            }
        }
        /// <summary>
        /// ������ͼƬ��ť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmUserDetail_TitleImageClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}