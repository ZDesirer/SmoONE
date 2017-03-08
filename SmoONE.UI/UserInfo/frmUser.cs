using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.Application;
using SmoONE.CommLib;
using System.Threading.Tasks;
using SmoONE.Domain;
using System.Data;
using SmoONE.DTOs;

namespace SmoONE.UI
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 1.0
    // Copyright  (c)  2016-2017 Smobiler 
    // ����ʱ�䣺 2016/11
    // ��Ҫ���ݣ�  ��ǰ�û��������
    // ******************************************************************
    partial class frmUser : Smobiler.Core.MobileForm
    {
        #region "definition"
        internal string toolbarItemName = "";
        private int eInfo;
        private Sex sex;//�Ա�
        private DateTime toasttime;//toastʱ��
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion

        /// <summary>
        /// �޸���Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ldEditUserInfo_ItemClick(object sender, MobileFormLayoutItemEventArgs e)
        {
            try
            {
                switch (e.CellItem.Name)
                {
                    case "btnCancel":
                        ldEditUserInfo.Close();
                        break;
                    case "btnOK":
                        string errinfo;
                        if (eInfo == (int)EuserInfo.�޸ĵ�¼����)
                        {
                            errinfo = "ԭ����";
                        }
                        else
                        {
                            errinfo = ((EuserInfo)Enum.ToObject(typeof(EuserInfo), eInfo)).ToString();
                        }
                        if (e.CellItem.Text.Trim().Length <= 0)
                        {
                            throw new Exception("������" + errinfo);
                        }

                        if (eInfo == (int)EuserInfo.�޸ĵ�¼����)
                        {
                            //�ж��û������Ƿ���ȷ
                            string Pwd = e.Cell.Items["txtEditInfo"].Text.Trim();
                            string encryptPwd = AutofacConfig.userService.Encrypt(Pwd);
                            bool  result = AutofacConfig.userService.IsPwd(Client.Session["U_ID"].ToString(), encryptPwd);
                            if (result== false)
                            {
                                e.Cell.Items["txtEditInfo"].Text="";
                                throw new Exception("�������ԭ���벻��ȷ�����������룡");
                            }
                            else
                            {
                                frmChangePWD frmChangePWD = new frmChangePWD();
                                frmChangePWD.oldPwd = encryptPwd;
                                Redirect(frmChangePWD);
                            }
                        }
                        else
                        {
                           
                                UserInputDto upuser = new UserInputDto();
                                upuser.U_ID = Client.Session["U_ID"].ToString();
                                switch (eInfo)
                                {
                                    case (int)EuserInfo.�޸��ǳ�:
                                        upuser.U_Name = e.Cell.Items["txtEditInfo"].Text.Trim();
                                        break;
                                    case (int)EuserInfo.�޸��ʼ�:
                                        upuser.U_Email = e.Cell.Items["txtEditInfo"].Text.Trim();
                                        break;
                                }
                               
                                ReturnInfo result = AutofacConfig.userService.UpdateUser(upuser);

                                if (result.IsSuccess == false)
                                {
                                    throw new Exception(result.ErrorInfo);
                                }
                                else
                                {
                                    ldEditUserInfo.Close();
                                    GetUser();
                                }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message , ToastLength.SHORT);
            }
        }
        /// <summary>
        /// �޸��ǳ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnName_Click(object sender, EventArgs e)
        {
            eInfo =(int) EuserInfo.�޸��ǳ�;
            ShowlayoutDialog();
        }
        /// <summary>
        /// ��ʾlayoutDialog
        /// </summary>
        private void ShowlayoutDialog()

        {
            string editLbltxt;
            if (eInfo == (int)EuserInfo.�޸ĵ�¼����)
            {
                editLbltxt = "�޸�����ǰ����дԭ����";
            }
            else
            {
                editLbltxt = ((EuserInfo)Enum.ToObject(typeof(EuserInfo), eInfo)).ToString();
            }
            ldEditUserInfo.LayoutData.Items["lblEditInfo"].DefaultValue = editLbltxt;
            switch (eInfo)
            {
                case (int)EuserInfo.�޸��ǳ�:
                    if (btnName.Text.Trim().Length > 0)
                    {
                        ldEditUserInfo.LayoutData.Items["txtEditInfo"].DefaultValue = btnName.Text.Trim();

                    }
                    else
                    {
                        ldEditUserInfo.LayoutData.Items["txtEditInfo"].DefaultValue = "";
                    }
                    break;
                case (int)EuserInfo.�޸��ʼ�:
                    if (btnEmail.Text.Trim().Length > 0)
                    {
                        ldEditUserInfo.LayoutData.Items["txtEditInfo"].DefaultValue = btnEmail.Text.Trim();
                    }
                    else
                    {
                        ldEditUserInfo.LayoutData.Items["txtEditInfo"].DefaultValue = "";
                    }
                    break;
                case (int)EuserInfo.�޸ĵ�¼����:
                    ldEditUserInfo.LayoutData.Items["txtEditInfo"].DefaultValue = "";
                    break;

            }
            ldEditUserInfo.Show();
        }
       
        /// <summary>
        /// �޸�Email
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEmail_Click(object sender, EventArgs e)
        {
            eInfo = (int)EuserInfo.�޸��ʼ�;
            ShowlayoutDialog();
        }
        /// <summary>
        /// �޸ĵ�¼����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPwd_Click(object sender, EventArgs e)
        {
            eInfo = (int)EuserInfo.�޸ĵ�¼���� ;
            ShowlayoutDialog();
        }
        /// <summary>
        /// �޸ĳ�������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dpkBirthday_DatePicked(object sender, DatePickerEventArgs e)
        {
             UserDetailDto user = AutofacConfig.userService.GetUserByUserID(Client.Session["U_ID"].ToString());
             if (user != null)
             {
                 UserInputDto upuser = new UserInputDto();
                 upuser.U_ID = Client.Session["U_ID"].ToString();
                 upuser.U_Birthday = dpkBirthday.CurrentDate;
                 ReturnInfo result = AutofacConfig.userService.UpdateUser(upuser);
                 //���ؽ�����Ϊfalse�����޸�ʧ��
                 if (result.IsSuccess == false)
                 {
                     dpkBirthday.CurrentDate = (DateTime )upuser.U_Birthday;
                     Toast(result.ErrorInfo, ToastLength.SHORT);
                 }
             }
        }
        /// <summary>
        /// �޸��û�ͷ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cameraPortrait_ImageCaptured(object sender, BinaryData e)
        {
            if (e.IsError == false)
            {
                     UserInputDto upuser = new UserInputDto();
                     upuser.U_ID = Client.Session["U_ID"].ToString();
                     upuser.U_Portrait = e.ResourceID;
                     ReturnInfo result = AutofacConfig.userService.UpdateUser(upuser);
                     if (result.IsSuccess == false)
                     {
                         Toast(result.ErrorInfo, ToastLength.SHORT);
                     }
                     else
                     {
                         e.SaveFile(e.ResourceID + ".png");
                         imgPortrait.ResourceID = e.ResourceID;
                         imgPortrait.Refresh();
                     }
            }
        }
        /// <summary>
        /// �޸��Ա�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popSex_Selected(object sender, EventArgs e)
        {
            if (popSex.Selection != null)
            {
                    UserInputDto upuser = new UserInputDto();
                    upuser.U_ID = Client.Session["U_ID"].ToString();
                    upuser.U_Sex = (Sex)Convert.ToInt32(popSex.Selection.Value);
                    ReturnInfo result = AutofacConfig.userService.UpdateUser(upuser);
                    if (result.IsSuccess == true)
                    {
                        sex =(Sex) Convert.ToInt32(popSex.Selection.Value);
                        btnSex.Text = popSex.Selection.Text;
                    }
                    else
                    {
                        Toast(result.ErrorInfo, ToastLength.SHORT);
                    }
            }
        }
        /// <summary>
        /// ��ʼ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmUser_Load(object sender, EventArgs e)
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
                    UserDetails userDetails = new UserDetails();
                    UserDetailDto user = userDetails.getUser(Client.Session["U_ID"].ToString());
                    if (user !=null )
                     {
                        imgPortrait.ResourceID = user.U_Portrait;
                        btnName.Text = user.U_Name;
                        sex = (Sex)user.U_Sex;
                        switch (sex)
                        {
                            case Sex.��:
                                btnSex.Text = "��";
                                break;
                            case Sex.Ů:
                                btnSex.Text = "Ů";
                                break;

                        }
                        dpkBirthday.CurrentDate = user.U_Birthday;
                        btnEmail.Text = user.U_Email;
                }
                else
                {
                    throw new Exception("�û�" + Client.Session["U_ID"].ToString() + "�����ڣ����飡");
                }
                //UserDetailDto user = AutofacConfig.userService.GetUserByUserID(Client.Session["U_ID"].ToString());
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
                //    btnName.Text = user.U_Name;
                //    sex = (Sex)user.U_Sex;
                //    switch (sex)
                //    {
                //        case Sex.��:
                //            btnSex.Text = "��";
                //            break;
                //        case Sex.Ů:
                //            btnSex.Text = "Ů";
                //            break;

                //    }

                //    dpkBirthday.CurrentDate = user.U_Birthday;
                //    btnEmail.Text = user.U_Email;
                //}
                //else
                //{
                //    throw new Exception("�û�" + Client.Session["U_ID"].ToString() + "�����ڣ����飡");
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Toolbar����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmUser_ToolbarItemClick(object sender, ToolbarClickEventArgs e)
        {
            if (!e.Name.Equals( Me))
            {
                toolbarItemName = e.Name;
                Close();
            }
        }
        /// <summary>
        /// �ֻ��Դ����˰�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmUser_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                HandleToast();
            }
        }
        /// <summary>
        /// Toast
        /// </summary>
        private void HandleToast()
        {
            if (toasttime.AddSeconds(3) >= DateTime.Now)
            {
                //�˳��ͻ���
                this.Client.Exit();
            }
            else
            {
                toasttime = DateTime.Now;
                this.Toast("�ٰ�һ���˳�ϵͳ", ToastLength.SHORT);
            }
        }
        /// <summary>
        /// �ϴ�ͷ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUp_Click(object sender, EventArgs e)
        {
            cameraPortrait.GetPhoto();
        }
        /// <summary>
        /// ѡ���Ա�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSex_Click(object sender, EventArgs e)
        {
            popSex.Groups.Clear();
            PopListGroup poli = new PopListGroup();
            popSex.Groups.Add(poli);
            poli.Text = "�Ա�ѡ��";
            UserSex UserSex = new UserSex();
            DataTable table = UserSex.GetSex();
            foreach (DataRow  row in table.Rows)
            {
                poli.Items.Add(row["SexName"].ToString(), row["SexID"].ToString());
                    if (((int)sex).Equals(row["SexID"]))
                    {
                        popSex.SetSelections(poli.Items[(poli.Items.Count - 1)]);
                    }
   
            }
            popSex.ShowDialog();
    }
        /// <summary>
        /// �˳���¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            MessageBox.Show("�Ƿ��˳���ǰϵͳ��", MessageBoxButtons.YesNo, (object o, MessageBoxHandlerArgs args) =>
            {
                try
                {
                    if (args.Result == Smobiler.Core.ShowResult.Yes)
                    {

                        this.Close();
                        frmLogon frmLogon = new frmLogon();
                       Redirect(frmLogon);
                        //�˳��ͻ���
                        //this.Client.Exit();
                    }
                }
                catch (Exception ex)
                {
                    Toast(ex.Message, ToastLength.SHORT);
                }
            });
        }
       
    }
    
}