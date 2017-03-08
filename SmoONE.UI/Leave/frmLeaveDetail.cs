using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.Application ;
using SmoONE.Domain;
using SmoONE.CommLib;
using SmoONE.DTOs;
namespace SmoONE.UI.Leave
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 1.0
    // Copyright  (c)  2016-2017 Smobiler 
    // ����ʱ�䣺 2016/11
    // ��Ҫ���ݣ�  ����������
    // ******************************************************************
    partial class frmLeaveDetail : Smobiler.Core.MobileForm
    {
        #region "definition"
        public string lID;//��ٱ��
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
        /// <summary>
        /// ��ʼ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLeaveDef_Load(object sender, EventArgs e)
        {
            GetLeave();
        }
        /// <summary>
        /// ������ٱ�Ż�ȡ�������
        /// </summary>
        private void GetLeave()
        {
            try
            {
                LeaveDetailDto leave = AutofacConfig.leaveService.GetByID(lID);
                if (Convert.IsDBNull(leave) == false)
                {
                    UserDetails userDetails = new UserDetails();
                    UserDetailDto userInfo = userDetails.getUser(leave.L_CreateUser);
                    if (userInfo !=null )
                    {
                        TitleText = userInfo.U_Name + "�����";
                        lblUserName.Text = userInfo.U_Name;
                        imgPortrait.ResourceID = userInfo.U_Portrait;
                    }
                    //UserDetailDto userInfo = AutofacConfig.userService.GetUserByUserID(leave.L_CreateUser);
                    //TitleText = userInfo.U_Name + "�����";
                    //lblUserName.Text = userInfo.U_Name;
                    //if (string.IsNullOrEmpty(userInfo.U_Portrait) == true)
                    //{
                    //    switch (userInfo.U_Sex)
                    //    {
                    //        case (int)Sex.��:
                    //            imgPortrait.ResourceID = "boy";
                    //            break;
                    //        case (int)Sex.Ů:
                    //            imgPortrait.ResourceID = "girl";
                    //            break;
                    //    }
                    //}
                    //else
                    //{
                    //    imgPortrait.ResourceID = userInfo.U_Portrait;
                    //}
                    switch (leave.L_Status)
                    {
                        case (int)L_Status.�½�:
                            string[] CheckUsers = leave.L_CheckUsers.Split(',');
                            lblStateNote.Text = "�ȴ�����";
                            bool iscuserCheck = false;//��ǰ�û��Ƿ�������Ȩ��
                            //�����ǰ�û��������û�����ʾ����Ȩ�ޣ�ͬ�⡢�ܾ���ť��������ʾ
                            if (CheckUsers.Contains(Client.Session["U_ID"].ToString()) == true)
                            {
                                FooterBarLayoutData.Items["btnAgreed"].Visible = true;
                                FooterBarLayoutData.Items["btnRefuse"].Visible = true;
                                iscuserCheck = true;
                            }
                            else
                            {
                                FooterBarLayoutData.Items["btnAgreed"].Visible = false;
                                FooterBarLayoutData.Items["btnRefuse"].Visible = false;
                                iscuserCheck = false;
                            }
                            //����ǵ�ǰ�û���������Ĵ����û�������ʾ�༭��ť��������ʾ��
                            if (Client.Session["U_ID"].ToString().Equals(leave.L_CreateUser))
                            {
                                FooterBarLayoutData.Items["btnEdit"].Visible = true;
                                if (iscuserCheck == true)
                                {
                                    FooterBarLayoutData.Items["btnAgreed"].Width = 85;
                                    FooterBarLayoutData.Items["btnAgreed"].Left = 10;
                                    FooterBarLayoutData.Items["btnRefuse"].Width = 85;
                                    FooterBarLayoutData.Items["btnRefuse"].Left = 108;
                                    FooterBarLayoutData.Items["btnEdit"].Width = 85;
                                    FooterBarLayoutData.Items["btnEdit"].Left = 205;
                                }
                                else
                                {
                                    FooterBarLayoutData.Items["btnEdit"].Width = 280;
                                    FooterBarLayoutData.Items["btnEdit"].Left = 10;
                                }
                            }
                            else
                            {
                                FooterBarLayoutData.Items["btnEdit"].Visible = false;
                                if (iscuserCheck == true)
                                {
                                    FooterBarLayoutData.Items["btnAgreed"].Width = 134;
                                    FooterBarLayoutData.Items["btnAgreed"].Left = 10;
                                    FooterBarLayoutData.Items["btnRefuse"].Width = 134;
                                    FooterBarLayoutData.Items["btnRefuse"].Left = 156;
                                }
                                else
                                {
                                    FooterBarLayoutData.Items["Line1"].Visible = false;
                                    FooterBarLayoutForm.Close();
                                }
                            }

                            break;
                        case (int)L_Status.������:
                            lblStateNote.Text = "����������ɣ�";
                            FooterBarLayoutData.Items["btnAgreed"].Visible = false;
                            FooterBarLayoutData.Items["btnRefuse"].Visible = false;
                            FooterBarLayoutData.Items["btnEdit"].Visible = false;
                            FooterBarLayoutData.Items["Line1"].Visible = false;

                            break;
                        case (int)L_Status.�Ѿܾ�:
                            lblStateNote.Text = "���������ܾ���";

                            //����ǵ�ǰ�û���������Ĵ����û�������ʾ�༭��ť��������ʾ��
                            if (Client.Session["U_ID"].ToString().Equals(leave.L_CreateUser))
                            {
                                FooterBarLayoutData.Items["btnAgreed"].Visible = false;
                                FooterBarLayoutData.Items["btnRefuse"].Visible = false;
                                FooterBarLayoutData.Items["btnEdit"].Visible = true;
                                FooterBarLayoutData.Items["btnEdit"].Width = 280;
                                FooterBarLayoutData.Items["btnEdit"].Left = 10;
                            }
                            {
                                FooterBarLayoutData.Items["Line1"].Visible = false;
                            }
                            break;
                    }
                    lblLId.Text = lID;
                    lblLType.Text = leave.L_TypeName;
                    lblStartDate.Text = leave.L_StartDate.ToString();
                    lblEndDate.Text = leave.L_EndDate.ToString();
                    lblLDay.Text = leave.L_LDay.ToString() + "��";
                    lblReson.Text = leave.L_Reason;
                    if (string.IsNullOrWhiteSpace(leave.L_Img1) == false)
                    {
                        imgL.ResourceID = leave.L_Img1;
                    }
                    //��ȡ����nodeview����
                    getNodeItemDate(leave);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    
     /// <summary>
        /// ��ӽڵ�����
        /// </summary>
        /// <param name="leave"></param>
        private void getNodeItemDate(LeaveDetailDto leave)
        {

           //������нڵ�
            nodeStateDate.Items.Clear();
            //��ӽڵ�����
            NodeViewItem nodeItem = new NodeViewItem();
            nodeItem.Icon = "tijiao";
            UserDetails userDetails = new UserDetails();
            UserDetailDto createUser = userDetails.getUser(leave.L_CreateUser);
            if (createUser != null)
            {
                nodeItem.Image = createUser.U_Portrait;
                if (Client.Session["U_ID"].Equals(leave.L_CreateUser))
                {
                    nodeItem.Text = "��";
                }
                else
                {
                    nodeItem.Text = createUser.U_Name;
                }
                nodeItem.SubText = "��������";
                nodeItem.Date = leave.L_CreateDate;
                nodeItem.TextColor = System.Drawing.Color.FromArgb(45, 45, 45);
                nodeItem.SubTextColor = System.Drawing.Color.FromArgb(236, 163, 56);
                nodeItem.DateColor = System.Drawing.Color.FromArgb(145, 145, 145);
                nodeStateDate.Items.Add(nodeItem);//��̬��ӽڵ�
            }
            switch (leave.L_Status)
            {
                case (int)L_Status.�½�:
                    if (string.IsNullOrEmpty(leave.L_CheckUsers) == false)
                    {
                        string[] CheckUsers = leave.L_CheckUsers.Split(',');
                        foreach (string cUser in CheckUsers)
                        {
                            NodeViewItem nodeItem2 = new NodeViewItem();
                            nodeItem2.Icon = "jinxingzhong";
                            UserDetailDto checkUser = userDetails.getUser(cUser);
                            if (checkUser != null)
                              {
                                  nodeItem2.Image = checkUser.U_Portrait;
                                  if (Client.Session["U_ID"].Equals(cUser))
                                  {
                                      nodeItem2.Text = "��";
                                  }
                                  else
                                  {
                                      nodeItem2.Text = checkUser.U_Name;
                                  }
                                  nodeItem2.SubText = "������";
                                  nodeItem2.TextColor = System.Drawing.Color.FromArgb(45, 45, 45);
                                  nodeItem2.SubTextColor = System.Drawing.Color.FromArgb(236, 163, 56);
                                  nodeStateDate.Items.Add(nodeItem2);
                              }
                        }
                    }
                    break;
                case (int)L_Status.������:
                    NodeViewItem nodeItem3 = new NodeViewItem();
                    nodeItem3.Icon = "wancheng";
                    UserDetailDto checkUser1 = userDetails.getUser(leave.L_CurrantCheck);
                    if (checkUser1 != null)
                    {
                            nodeItem3.Image = checkUser1.U_Portrait;
                            if (Client.Session["U_ID"].Equals(leave.L_CurrantCheck))
                            {
                                nodeItem3.Text = "��";
                            }
                            else
                            {
                                nodeItem3.Text = checkUser1.U_Name;
                            }
                            nodeItem3.SubText = "��ͬ��";
                            nodeItem3.Date = leave.L_UpdateDate;
                            nodeItem3.TextColor = System.Drawing.Color.FromArgb(45, 45, 45);
                            nodeItem3.SubTextColor = System.Drawing.Color.FromArgb(143, 187, 78);
                            nodeItem3.DateColor = System.Drawing.Color.FromArgb(145, 145, 145);
                            nodeStateDate.Items.Add(nodeItem3);
                        }
                     
                  
                    break;
                case (int)L_Status.�Ѿܾ�:
                    NodeViewItem nodeItem4 = new NodeViewItem();
                    nodeItem4.Icon = "jujue";
                    UserDetailDto checkUser2 = userDetails.getUser(leave.L_CurrantCheck);
                    if (checkUser2 != null)
                    {
                        nodeItem4.Image = checkUser2.U_Portrait;
                        if (Client.Session["U_ID"].Equals(leave.L_CurrantCheck))
                        {
                            nodeItem4.Text = "��";
                        }
                        else
                        {
                            nodeItem4.Text = checkUser2.U_Name;
                        }
                        nodeItem4.SubText = "�Ѿܾ�";
                        nodeItem4.Date = leave.L_UpdateDate;
                        nodeItem4.TextColor = System.Drawing.Color.FromArgb(45, 45, 45);
                        nodeItem4.SubTextColor = System.Drawing.Color.FromArgb(244, 64, 69);
                        nodeItem4.DateColor = System.Drawing.Color.FromArgb(145, 145, 145);
                        nodeStateDate.Items.Add(nodeItem4);
                     }
                    break;
            }
        }
        ///// <summary>
        ///// ��ӽڵ�����
        ///// </summary>
        ///// <param name="leave"></param>
        //private void getNodeItemDate(LeaveDetailDto leave)
        //{

        //   //������нڵ�
        //    nodeStateDate.Items.Clear();
        //    //��ӽڵ�����
        //    NodeViewItem nodeItem = new NodeViewItem();
        //    nodeItem.Icon = "tijiao";
        //    UserDetailDto createUser = AutofacConfig.userService.GetUserByUserID(leave.L_CreateUser);
        //    if (string.IsNullOrEmpty(createUser.U_Portrait) == true)
        //    {
        //        switch (createUser.U_Sex)
        //        {
        //            case (int)Sex.��:
        //                nodeItem.Image = "boy";
        //                break;
        //            case (int)Sex.Ů:
        //                nodeItem.Image = "girl";
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        nodeItem.Image = createUser.U_Portrait;
        //    }
        //    if (Client.Session["U_ID"].Equals(leave.L_CreateUser))
        //    {
        //        nodeItem.Text = "��";
        //    }
        //    else
        //    {
        //        nodeItem.Text = createUser.U_Name;
        //    }          
        //    nodeItem.SubText = "��������";
        //    nodeItem.Date = leave.L_CreateDate;
        //    nodeItem.TextColor = System.Drawing.Color.FromArgb(45,45,45);
        //    nodeItem.SubTextColor = System.Drawing.Color.FromArgb(236, 163, 56);
        //    nodeItem.DateColor = System.Drawing.Color.FromArgb(145, 145, 145);
        //    nodeStateDate.Items.Add(nodeItem);//��̬��ӽڵ�
        //    switch (leave.L_Status)
        //    {
        //        case (int)L_Status.�½�:
        //            if (string.IsNullOrEmpty(leave.L_CheckUsers) == false)
        //            {
        //                string[] CheckUsers = leave.L_CheckUsers.Split(',');
        //                foreach (string cUser in CheckUsers)
        //                {
        //                    NodeViewItem nodeItem2 = new NodeViewItem();
        //                    nodeItem2.Icon = "jinxingzhong";
        //                    UserDetailDto checkUser = AutofacConfig.userService.GetUserByUserID(cUser);
        //                    if (string.IsNullOrEmpty(checkUser.U_Portrait) == true)
        //                    {
        //                        switch (checkUser.U_Sex)
        //                        {
        //                            case (int)Sex.��:
        //                                nodeItem2.Image = "boy";
        //                                break;
        //                            case (int)Sex.Ů:
        //                                nodeItem2.Image = "girl";
        //                                break;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        nodeItem2.Image = checkUser.U_Portrait;
        //                    }

        //                    if (Client.Session["U_ID"].Equals(cUser))
        //                    {
        //                        nodeItem2.Text = "��";
        //                    }
        //                    else
        //                    {
        //                        nodeItem2.Text = checkUser.U_Name;
        //                    }
        //                    nodeItem2.SubText = "������";
        //                    nodeItem2.TextColor = System.Drawing.Color.FromArgb(45, 45, 45);
        //                    nodeItem2.SubTextColor = System.Drawing.Color.FromArgb(236, 163, 56);
        //                    nodeStateDate.Items.Add(nodeItem2);
        //                }
        //            }
        //            break;
        //        case (int)L_Status.������:
        //            NodeViewItem nodeItem3 = new NodeViewItem();
        //            nodeItem3.Icon = "wancheng";
        //            UserDetailDto checkUser1 = AutofacConfig.userService.GetUserByUserID(leave.L_CurrantCheck);
        //            if (string.IsNullOrEmpty(checkUser1.U_Portrait) == true)
        //                {
        //                    switch (checkUser1.U_Sex)
        //                    {
        //                        case (int)Sex.��:
        //                            nodeItem3.Image = "boy";
        //                            break;
        //                        case (int)Sex.Ů:
        //                            nodeItem3.Image = "girl";
        //                            break;
        //                    }
        //                }
        //                else
        //                {
        //                    nodeItem3.Image = checkUser1.U_Portrait;
        //                }
                     
        //            if (Client.Session["U_ID"].Equals(leave.L_CurrantCheck))
        //            {
        //                nodeItem3.Text = "��";
        //            }
        //            else
        //            {
        //                nodeItem3.Text = checkUser1.U_Name;
        //            }
        //            nodeItem3.SubText = "��ͬ��";
        //            nodeItem3.Date = leave.L_UpdateDate;
        //            nodeItem3.TextColor = System.Drawing.Color.FromArgb(45, 45, 45);
        //            nodeItem3.SubTextColor = System.Drawing.Color.FromArgb(143, 187, 78);
        //             nodeItem3.DateColor  = System.Drawing.Color.FromArgb(145, 145, 145);
        //            nodeStateDate.Items.Add(nodeItem3);
        //            break;
        //        case (int)L_Status.�Ѿܾ�:
        //            NodeViewItem nodeItem4 = new NodeViewItem();
        //            nodeItem4.Icon = "jujue";
        //            UserDetailDto checkUser2 = AutofacConfig.userService.GetUserByUserID(leave.L_CurrantCheck);
        //           if (string.IsNullOrEmpty(checkUser2.U_Portrait) == true)
        //                {
        //                    switch (checkUser2.U_Sex)
        //                    {
        //                        case (int)Sex.��:
        //                            nodeItem4.Image = "boy";
        //                            break;
        //                        case (int)Sex.Ů:
        //                            nodeItem4.Image = "girl";
        //                            break;
        //                    }
        //                }
        //                else
        //                {
        //                    nodeItem4.Image = checkUser2.U_Portrait;
        //                }
                     
        //            if (Client.Session["U_ID"].Equals(leave.L_CurrantCheck))
        //            {
        //                nodeItem4.Text = "��";
        //            }
        //            else
        //            {
        //                nodeItem4.Text = checkUser2.U_Name;
        //            }
        //            nodeItem4.SubText = "�Ѿܾ�";
        //            nodeItem4.Date = leave.L_UpdateDate;
        //            nodeItem4.TextColor = System.Drawing.Color.FromArgb(45, 45, 45);
        //            nodeItem4.SubTextColor = System.Drawing.Color.FromArgb(244, 64, 69);
        //             nodeItem4.DateColor  = System.Drawing.Color.FromArgb(145, 145, 145);
        //            nodeStateDate.Items.Add(nodeItem4);
        //            break;
        //    }
        //}

        /// <summary>
        /// �ֻ��Դ����˰�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLeaveDef_KeyDown(object sender, KeyDownEventArgs e)
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
        private void frmLeaveDef_TitleImageClick(object sender, EventArgs e)
        {
            Close();
        }
      
        /// <summary>
        /// ���������FooterBarDialog����¼���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLeaveDetail_FooterBarDialogLayoutItemClick(object sender, MobileFormLayoutItemEventArgs e)
        {
            try
            {
                switch (e.CellItem.Name)
                {
                    case "btnCancel":
                        CloseFooterBar();
                        break;
                    case "btnOK":
                        if (FooterBarDialogData.Items["txtReason"].Text.Trim().Length <= 0)
                        {
                            throw new Exception("������ܾ����ɣ�");
                        }
                        //�����˲������
                        ReturnInfo result = AutofacConfig.leaveService.UpdateLeaveStatus(lID, L_Status.�Ѿܾ�, Client.Session["U_ID"].ToString(), FooterBarDialogData.Items["txtReason"].Text.Trim());
                      //�������true�������ɹ�������ʧ�ܲ��׳�����
                        if (result.IsSuccess == true)
                      {
                          GetLeave();
                          CloseFooterBar();
                          ShowResult = ShowResult.Yes;
                          Toast("�Ѿܾ���٣�", ToastLength.SHORT);
                      }
                      else
                      {
                          throw new Exception (result.ErrorInfo);
                      }
                        break;
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message, ToastLength.SHORT);
            }
        }
        /// <summary>
        /// FooterBar����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLeaveDetail_FooterBarLayoutItemClick(object sender, MobileFormLayoutItemEventArgs e)
        {
            if (e.CellItem .Name  == "btnAgreed")
            {
                //MessageBox.Show("�Ƿ�ȷ��ͬ��������", "�������", MessageBoxButtons.YesNo, (Object s, MessageBoxHandlerArgs args) =>
                //{
                //    if (args.Result == Smobiler.Core.ShowResult.Yes)
                //    {
                        //������ͬ�����
                        ReturnInfo result = AutofacConfig.leaveService.UpdateLeaveStatus(lID, L_Status.������, Client.Session["U_ID"].ToString(), "");
                        //�������true�������ɹ�������ʧ�ܲ��׳�����
                        if (result.IsSuccess == true)
                        {
                            GetLeave();
                            ShowResult = ShowResult.Yes;
                            Toast("��ͬ�����������", ToastLength.SHORT);
                        }
                        else
                        {
                            Toast(result.ErrorInfo, ToastLength.SHORT);
                        }
                //    }
                //}
                //      );

            }
            if (e.CellItem.Name == "btnRefuse")
            {  //�����ܾ������������
                ShowFooterBar("frmRefuseLayout");
            }
            if (e.CellItem.Name == "btnEdit")
            {
                //��ת���༭����
                Leave.frmLeaveCreate frmLeaveCreate = new Leave.frmLeaveCreate();
                frmLeaveCreate.LID = lID;
                Redirect(frmLeaveCreate, (MobileForm form, object args) =>
                {
                    if (frmLeaveCreate.ShowResult == ShowResult.Yes)
                    {
                        ShowResult = ShowResult.Yes;
                        GetLeave();
                    }
                });
            }
        }
    }
}