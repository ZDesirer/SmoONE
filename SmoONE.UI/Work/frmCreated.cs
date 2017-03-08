using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.Application;
using SmoONE.DTOs;
using SmoONE.Domain;

namespace SmoONE.UI
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 1.0
    // Copyright  (c)  2016-2017 Smobiler 
    // ����ʱ�䣺 2016/11
    // ��Ҫ���ݣ�  �Ҵ������б����
    // ******************************************************************
    partial class frmCreated : Smobiler.Core.MobileForm
    {
        #region "definition"
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
        /// <summary>
        /// ��ȡ��ʼ������
        /// </summary>
        private void Bind()
        {
            try
            {
                //��ȡ��ǰ�û��������������
                List<LeaveDto> listLeaveDto = AutofacConfig.leaveService.GetByCreateUsers(Client.Session["U_ID"].ToString());
                //��ȡ��ǰ�û������ı�������
                List<ReimbursementDto> listRBDto = AutofacConfig.rBService.GetByCreateUsers(Client.Session["U_ID"].ToString());
                List<DataGridview> listCreated = new List<DataGridview>();//�ҷ��������
                UserDetails userDetails = new UserDetails();
                //������������������0������ӵ��ҷ��������
                if (listLeaveDto.Count > 0)
                {
                    foreach (LeaveDto leave in listLeaveDto)
                    {
                        DataGridview dataGItem = new DataGridview();
                        dataGItem.ID = leave.L_ID;
                        if (string.IsNullOrEmpty(leave.U_Portrait) == true)
                        {
                            UserDetailDto user = userDetails.getUser(leave.U_ID);
                            if (user != null)
                            {
                                dataGItem.U_Portrait = user.U_Portrait;
                            }
                            //UserDetailDto user = AutofacConfig.userService.GetUserByUserID(leave.U_ID);
                            //switch (user.U_Sex)
                            //{
                            //    case (int)Sex.��:
                            //        dataGItem.U_Portrait = "boy";
                            //        break;
                            //    case (int)Sex.Ů:
                            //        dataGItem.U_Portrait = "girl";
                            //        break;
                            //}
                        }
                        else
                        {
                            dataGItem.U_Portrait = leave.U_Portrait;
                        }
                       
                        dataGItem.Name = leave.U_Name + "��" + DataGridviewType.���;
                        dataGItem.Type = ((int )Enum.Parse(typeof(DataGridviewType), DataGridviewType.���.ToString())).ToString();
                        dataGItem.CreateDate = leave.L_CreateDate.ToString("yyyy/MM/dd");
                        switch (leave.L_Status)
                        {
                            case (int)L_Status.�½�:
                                dataGItem.L_StatusDesc = "�ȴ�����";
                                break;
                            case (int)L_Status.������:
                                dataGItem.L_StatusDesc = "����������ɣ�";
                                break;
                            case (int)L_Status.�Ѿܾ�:
                                dataGItem.L_StatusDesc = "���������ܾ���";
                                break;
                        }
                        listCreated.Add(dataGItem);
                    }
                }

                //�������������������0������ӵ��ҷ��������
                if (listRBDto.Count > 0)
                {
                    foreach (ReimbursementDto reimbursement in listRBDto)
                    {
                        DataGridview dataGItem = new DataGridview();
                        dataGItem.ID = reimbursement.RB_ID;
                        if (string.IsNullOrEmpty(reimbursement.U_Portrait) == true)
                        {
                            UserDetailDto user = userDetails.getUser(reimbursement.U_ID);
                            if (user != null)
                            {
                                dataGItem.U_Portrait = user.U_Portrait;
                            }
                            //UserDetailDto user = AutofacConfig.userService.GetUserByUserID(reimbursement.U_ID);
                            //switch (user.U_Sex)
                            //{
                            //    case (int)Sex.��:
                            //        dataGItem.U_Portrait = "boy";
                            //        break;
                            //    case (int)Sex.Ů:
                            //        dataGItem.U_Portrait = "girl";
                            //        break;
                            //}
                        }
                        else
                        {
                            dataGItem.U_Portrait = reimbursement.U_Portrait;
                        }
                        dataGItem.Name = reimbursement.U_Name + "��" + DataGridviewType.����;
                        dataGItem.Type = ((int )Enum.Parse(typeof(DataGridviewType), DataGridviewType.����.ToString())).ToString();
                        dataGItem.CreateDate = reimbursement.RB_CreateDate.ToString("yyyy/MM/dd");
                        switch (reimbursement.RB_Status)
                        {
                            case (int)RB_Status.�½�:
                                dataGItem.L_StatusDesc = "�ȴ�����������";
                                break;
                            case (int)RB_Status.����������:
                                dataGItem.L_StatusDesc = "�ȴ���������";
                                break;
                            case (int)RB_Status.��������:
                                dataGItem.L_StatusDesc = "�ȴ���������";
                                break;
                            case (int)RB_Status.��������:
                                dataGItem.L_StatusDesc = "����������";
                                break;
                            case (int)RB_Status.�Ѿܾ�:
                                dataGItem.L_StatusDesc = "���������ܾ���";
                                break;
                        }
                        listCreated.Add(dataGItem);
                    }
                }
                gridCrateData.Rows.Clear();//����ҷ�����б�����
                if (listCreated.Count > 0)
                {
                    gridCrateData.DataSource = listCreated;//��gridView����
                    gridCrateData.DataBind();
                }
               
            }
            catch (Exception ex)
            {
                Toast(ex.Message, ToastLength.SHORT);
            }
        }

        /// <summary>
        /// �ֻ��Դ����˰�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCreated_KeyDown(object sender, KeyDownEventArgs e)
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
        private void frmCreated_TitleImageClick(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// gridCrateData����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridCrateData_CellClick(object sender, GridViewCellEventArgs e)
        {
            string ID = e.Cell.Items["lblId"].Value.ToString();
            switch (Convert .ToInt32(e.Cell.Items["lblType"].Value))
            {
                //��ת�������ϸ����
                case (int)DataGridviewType.���:
                    Leave.frmLeaveDetail frmLeaveDetail = new Leave.frmLeaveDetail();
                    frmLeaveDetail.lID = ID;
                    Redirect(frmLeaveDetail, (MobileForm form, object args) =>
                    {
                        if (frmLeaveDetail.ShowResult == ShowResult.Yes)
                        {
                            Bind();
                        }
                    });

                    break;
                //��ת��������ϸ����
                case (int)DataGridviewType.����:
                    RB.frmRBDetail frmRBDetail = new RB.frmRBDetail();
                    frmRBDetail.ID = ID;
                    Redirect(frmRBDetail, (MobileForm form, object args) =>
                    {
                        if (frmRBDetail.ShowResult == ShowResult.Yes)
                        {
                            Bind();
                        }
                    });
                    break;
            }
         
        }
        /// <summary>
        /// ��ʼ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCreated_Load(object sender, EventArgs e)
        {
            Bind();
        }
    }

}