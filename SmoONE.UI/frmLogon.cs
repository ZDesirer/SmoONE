using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.CommLib;
using SmoONE.Domain;
using SmoONE.UI.UserInfo;

namespace SmoONE.UI
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 1.0
    // Copyright  (c)  2016-2017 Smobiler 
    // ����ʱ�䣺 2016/11
    // ��Ҫ���ݣ�  ��¼����
    // ******************************************************************
    partial class frmLogon : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
        /// <summary>
        /// ��¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogon_Click(object sender, EventArgs e)
        {
            try
            {
                
                string userID = txtTel.Text.Trim();
                string pwd = txtPwd.Text.Trim();
                if (userID.Length <= 0)
                {
                    throw new Exception("�������ֻ����룡");
                }
                if (pwd.Length <= 0)
                {
                    throw new Exception("���������룡");
                }
                if (pwd.Length < 6 || pwd.Length > 12)
                {
                    throw new Exception("�������Ϊ6-12λ��");
                }
               
                //���봦��,��������
                string encryptPwd = AutofacConfig.userService.Encrypt(DateTime.Now.ToString("yyyyMMddHHmmss") + pwd);
                ReturnInfo result = AutofacConfig.userService.Login(userID, encryptPwd);

                if (result.IsSuccess == true)
                {
                    List<Role> role = AutofacConfig.userService.GetRoleByUserID(userID);
                    Client.Session["U_ID"] = userID;
                    Client.Session["Roler"] = role;
                    SmoONE.UI.Work.frmWork frmWork = new SmoONE.UI.Work.frmWork();
                    Show (frmWork);
                }
                else
                {
                    throw new Exception(result.ErrorInfo);
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message, ToastLength.SHORT);
            }
        }
        /// <summary>
        /// �˳��ͻ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLogon_KeyDown(object sender, KeyDownEventArgs e)
        {
            Client.Exit();
        }

        /// <summary>
        /// ��ת��ע�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegister_Click(object sender, EventArgs e)
        {
            frmRegisterTel frmRegister = new frmRegisterTel();
            Show (frmRegister);
        }
        /// <summary>
        /// ��֤��¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVerify_Click(object sender, EventArgs e)
        {
            try
            {
                string userID = txtTel.Text.Trim();
                if (userID.Length <= 0)
                {
                    throw new Exception("�������ֻ����룡");
                }

                frmVerificationCode frmVerificationCode = new frmVerificationCode();
                frmVerificationCode.Tel = userID;
                Show(frmVerificationCode);
            }
            catch (Exception ex)
            {
                Toast(ex.Message, ToastLength.SHORT);
            }
        }


    }
}