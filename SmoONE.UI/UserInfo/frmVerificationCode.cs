using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.Domain;
using SmoONE.UI.UserInfo;
using SmoONE.CommLib;

namespace SmoONE.UI
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 1.0
    // Copyright  (c)  2016-2017 Smobiler 
    // ����ʱ�䣺 2016/11
    // ��Ҫ���ݣ�  �ֻ���֤����
    // ******************************************************************
    partial class frmVerificationCode : Smobiler.Core.MobileForm
    {
        #region "definition"
        public string Tel;//�ֻ�����
        public bool isVerifyLogon;//�Ƿ�����֤��¼
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
        /// <summary>
        /// ��ʼ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmVerificationCode_Load(object sender, EventArgs e)
        {
            Bind();
          
        }
        /// <summary>
        /// ��ʼ������
        /// </summary>
        private void Bind()
        {
            try
            {
                if (Tel != null)
                {              
                    lblTel.Text = "��֤���ѷ������ֻ���" + Tel;
                    if (isVerifyLogon==true )
                    {
                        lblHint.Visible = true;
                        lblHint.Top = txtVcode1.Top + txtVcode1.Height;
                        btnSave.Top = lblHint.Top + lblHint.Height;
                        //ģ�ⷢ�Ͷ�����֤��
                        ReturnInfo result = AutofacConfig.userService.SimulateSendVCode(Tel);
                        if (result.IsSuccess == false)
                        {
                            throw new Exception(result.ErrorInfo);
                        }
                    }
                    else 
                    {
                        lblHint.Visible = false ;
                        btnSave.Top = txtVcode1.Top + txtVcode1.Height+10;
                        //�����ֻ���֤�룬����true����ʾ���ͳɹ���������ʧ�ܣ����׳�����
                        ReturnInfo result = AutofacConfig.userService.SendVCode(Tel,Client .DeviceID);
                        if (result .IsSuccess ==false )
                        {
                            throw new Exception(result.ErrorInfo);
                        }
                    }
                }
                else
                {
                    throw new Exception("�������ֻ�����");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show (ex.Message);
            }

        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            string code = txtVcode1.Text.Trim();
             if (code .Length >0)
             {
                if (isVerifyLogon == true)
                {
                //�ж���֤���Ƿ�������ȷ��������ֵΪtrueʱ����ʾ��֤�ɹ�������ʧ�ܣ���������
                ReturnInfo result=    AutofacConfig.userService.LoginByVCode(Tel, code);
                if (result.IsSuccess == true)
                {
                    //��ȡ��ǰ�û���ɫ
                    List<Role> role = AutofacConfig.userService.GetRoleByUserID(Tel);
                    Client.Session["U_ID"] = Tel;
                    Client.Session["Roler"] = role;
                    Close();
                    frmWork frmWork = new frmWork();
                    Redirect(frmWork);
                }
                else
                {
                    Toast(result.ErrorInfo,ToastLength.SHORT);
                }
                }
                else
                {
                    Close();
                    frmRegister frmRegister = new frmRegister();
                    frmRegister.Tel = Tel;
                    frmRegister.VCode = code;
                    Redirect(frmRegister);
                }
             }
            else 
             {
                 Toast("�������ֻ���֤�룡",ToastLength.SHORT);
             }
        }
        /// <summary>
        /// ������ͼƬ��ť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmVerificationCode_TitleImageClick(object sender, EventArgs e)
         {
            Close();
        }
      
         
        /// <summary>
        /// �ֻ��Դ����˰�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmVerificationCode_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                Close();         //�رյ�ǰҳ��
            }
        }
    }
}