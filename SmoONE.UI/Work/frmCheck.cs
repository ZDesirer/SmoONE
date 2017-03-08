using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.Application;
using SmoONE.DTOs;
using SmoONE.Domain;
using System.Data;

namespace SmoONE.UI
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 1.0
    // Copyright  (c)  2016-2017 Smobiler 
    // ����ʱ�䣺 2016/11
    // ��Ҫ���ݣ�  ���������б����
    // ******************************************************************
    partial class frmCheck : Smobiler.Core.MobileForm
    {
        #region "definition"
        private string type="";//����
        private string state = "";//״̬
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion

        /// <summary>
        /// �ֻ��Դ����˰�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCheck_KeyDown(object sender, KeyDownEventArgs e)
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
        private void frmCheck_TitleImageClick(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// tabBar����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textTabBar1_ItemClick(object sender, TabBarItemClickEventArgs e)
        {
            type = "";
            state = "";
            Bind ();
        }

       /// <summary>
       /// ��ȡ��ʼ������
       /// </summary>
        private void Bind()
        {
            try
            {
               
                List<LeaveDto> listLeaveDto = new List<LeaveDto>();
                List<ReimbursementDto> listRBDto = new List<ReimbursementDto>();
                switch (textTabBar1.SelectItemIndex)
                {
                    case 0:
                        //��ȡ��ǰ�û����������������
                        listLeaveDto = AutofacConfig.leaveService.GetNewByCheckUsers(Client.Session["U_ID"].ToString());
                        //��ȡ��ǰ�û��������ı�������
                        listRBDto = AutofacConfig.rBService.GetNewByCheckUsers(Client.Session["U_ID"].ToString());
                        break;
                    case 1:
                        //��ȡ��ǰ�û����������������
                        listLeaveDto = AutofacConfig.leaveService.GetCheckedByCheckUsers(Client.Session["U_ID"].ToString());
                        //��ȡ��ǰ�û��������ı�������
                        listRBDto = AutofacConfig.rBService.GetCheckedByCheckUsers(Client.Session["U_ID"].ToString());
                        break;
                }
                List<DataGridview> listCheck = new List<DataGridview>();//��������
               UserDetails userDetails = new UserDetails();
                //������������������������������������0������ӵ���������                
                if (listLeaveDto.Count > 0)
                {
                    foreach (LeaveDto leave in listLeaveDto)
                    {
                        DataGridview dataGItem = new DataGridview();
                        dataGItem.ID = leave.L_ID;
                        if (string.IsNullOrEmpty(leave.U_Portrait) == true)
                        {
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
                           
                            UserDetailDto user = userDetails.getUser(leave.U_ID);
                            if (user != null)
                            {
                                dataGItem.U_Portrait = user.U_Portrait;
                            }
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
                                if (string.IsNullOrEmpty(leave.L_CheckUsers) == false)
                                {
                                    dataGItem.L_StatusDesc = "�ȴ�������";
                                }
                                else
                                {
                                    dataGItem.L_StatusDesc = "�ȴ�������";
                                }
                                break;
                            case (int)L_Status.������:
                                dataGItem.L_StatusDesc = "����������ɣ�";
                                break;
                            case (int)L_Status.�Ѿܾ�:
                                dataGItem.L_StatusDesc = "���������ܾ���";
                                break;
                        }
                        listCheck.Add(dataGItem);
                    }
                }

                //��������������������ı���������������0������ӵ���������     
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
                                switch (textTabBar1.SelectItemIndex)
                                {
                                    case 0:
                                            dataGItem.L_StatusDesc = "�ȴ�����������";
                                        break;
                                }
                                break;
                            case (int)RB_Status.����������:
                                switch (textTabBar1.SelectItemIndex)
                                {
                                    case 0:
                                        dataGItem.L_StatusDesc = "�ȴ���������";
                                        break;
                                    case 1:
                                        dataGItem.L_StatusDesc = "������������";
                                        break;
                                }
                                break;
                            case (int)RB_Status.��������:
                                switch (textTabBar1.SelectItemIndex)
                                {
                                    case 0:
                                        dataGItem.L_StatusDesc = "�ȴ���������";
                                        break;
                                    case 1:
                                        dataGItem.L_StatusDesc = "����������";
                                        break;
                                }
                                break;
                            case (int)RB_Status.��������:
                                switch (textTabBar1.SelectItemIndex)
                                {
                                    case 1:
                                        dataGItem.L_StatusDesc = "����������";
                                        break;
                                }
                                break;
                            case (int)RB_Status.�Ѿܾ�:
                                dataGItem.L_StatusDesc = "���������ܾ���";
                                break;
                        }
                        listCheck.Add(dataGItem);
                    }
                }

                gridCheckData.Rows.Clear();//����������б�����
                if (listCheck.Count > 0)
                {
                    gridCheckData.DataSource = listCheck;//��gridview����
                    gridCheckData.DataBind();
                }
              
            }
            catch (Exception ex)
            {
                Toast(ex.Message, ToastLength.SHORT);
            }
        }

        /// <summary>
        /// ��ȡɸѡ����
        /// </summary>
        private void GetCheckData()
        {
            List<LeaveDto> listLeaveDto = new List<LeaveDto>();
            List<ReimbursementDto> listRBDto = new List<ReimbursementDto>();
            if (string.IsNullOrWhiteSpace(type) == false & string.IsNullOrWhiteSpace(state) == false)
            {
                switch (Convert.ToInt32(type))
                {
                    case (int)DataGridviewType.���:
                        switch (textTabBar1.SelectItemIndex)
                        {
                            case 0:
                                //��ȡ��ǰ�û����������������
                                listLeaveDto = AutofacConfig.leaveService.GetNewByCheckUsers(Client.Session["U_ID"].ToString());
                                break;
                            case 1:
                                //�������״̬��ȡ��ǰ�û����������������
                               listLeaveDto = AutofacConfig.leaveService.QueryCheckedByCheckUsers(Client.Session["U_ID"].ToString(), Convert.ToInt32(state));
                               break;
                        }
                        break;
                    case (int)DataGridviewType.����:
                        switch (textTabBar1.SelectItemIndex)
                        {
                            case 0:
                                //���ݱ���״̬��ȡ��ǰ�û��������ı�������
                                listRBDto = AutofacConfig.rBService.QueryNewByCheckUsers(Client.Session["U_ID"].ToString(), Convert.ToInt32(state));
                                break;
                            case 1:
                                //���ݱ���״̬��ȡ��ǰ�û��������ı�������
                                listRBDto = AutofacConfig.rBService.QueryCheckedByCheckUsers(Client.Session["U_ID"].ToString(), Convert.ToInt32(state));
                                break;
                        }
                       
                        break;
                }
            }
            List<DataGridview> listCheck = new List<DataGridview>();//��������

            //������������������������������������0������ӵ���������     
            if (listLeaveDto.Count > 0)
            {
                foreach (LeaveDto leave in listLeaveDto)
                {
                    DataGridview dataGItem = new DataGridview();
                    dataGItem.ID = leave.L_ID;
                    if (string.IsNullOrEmpty(leave.U_Portrait) == true)
                    {
                        UserDetailDto user = AutofacConfig.userService.GetUserByUserID(leave.U_ID);
                        switch (user.U_Sex)
                        {
                            case (int)Sex.��:
                                dataGItem.U_Portrait = "boy";
                                break;
                            case (int)Sex.Ů:
                                dataGItem.U_Portrait = "girl";
                                break;
                        }
                    }
                    else
                    {
                        dataGItem.U_Portrait = leave.U_Portrait;
                    }
                    dataGItem.Name = leave.U_Name + "��" + DataGridviewType.���;
                    dataGItem.Type = ((int )Enum.Parse(typeof(DataGridviewType), DataGridviewType.���.ToString())).ToString();
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
                    listCheck.Add(dataGItem);
                }
            }

            //��������������������ı���������������0������ӵ���������     
            if (listRBDto.Count > 0)
            {
                foreach (ReimbursementDto reimbursement in listRBDto)
                {
                    DataGridview dataGItem = new DataGridview();
                    dataGItem.ID = reimbursement.RB_ID;
                    if (string.IsNullOrEmpty(reimbursement.U_Portrait) == true)
                    {
                        UserDetailDto user = AutofacConfig.userService.GetUserByUserID(reimbursement.U_ID);
                        switch (user.U_Sex)
                        {
                            case (int)Sex.��:
                                dataGItem.U_Portrait = "boy";
                                break;
                            case (int)Sex.Ů:
                                dataGItem.U_Portrait = "girl";
                                break;
                        }
                    }
                    else
                    {
                        dataGItem.U_Portrait = reimbursement.U_Portrait;
                    }
                    dataGItem.Name = reimbursement.U_Name + "��" + DataGridviewType.����;
                    dataGItem.Type = ((int )Enum.Parse(typeof(DataGridviewType), DataGridviewType.����.ToString())).ToString();
                    switch (reimbursement.RB_Status)
                    {
                        case (int)RB_Status.�½�:
                            switch (textTabBar1.SelectItemIndex)
                            {
                                case 0:
                                    dataGItem.L_StatusDesc = "�ȴ�����������";
                                    break;
                            }
                            break;
                        case (int)RB_Status.����������:
                            switch (textTabBar1.SelectItemIndex)
                            {
                                case 0:
                                    dataGItem.L_StatusDesc = "�ȴ���������";
                                    break;
                                case 1:
                                    dataGItem.L_StatusDesc = "������������";
                                    break;
                            }
                            break;
                        case (int)RB_Status.��������:
                            switch (textTabBar1.SelectItemIndex)
                            {
                                case 0:
                                    dataGItem.L_StatusDesc = "�ȴ���������";
                                    break;
                                case 1:
                                    dataGItem.L_StatusDesc = "����������";
                                    break;
                            }
                            break;
                        case (int)RB_Status.��������:
                            switch (textTabBar1.SelectItemIndex)
                            {
                                case 1:
                                    dataGItem.L_StatusDesc = "����������";
                                    break;
                            }
                            break;
                        case (int)RB_Status.�Ѿܾ�:
                            dataGItem.L_StatusDesc = "���������ܾ���";
                            break;
                    }
                    listCheck.Add(dataGItem);
                }
            }
            gridCheckData.Rows.Clear();//����б�����
            if (listCheck.Count > 0)
            {
                gridCheckData.DataSource = listCheck;
                gridCheckData.DataBind();
            }
           
        }
        /// <summary>
        /// ��ʼ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCheck_Load(object sender, EventArgs e)
        {
            type = "";
            state = "";
            Bind();
        }

        /// <summary>
        /// gridCheckData����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridCheckData_CellClick(object sender, GridViewCellEventArgs e)
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
                            type = "";
                            state = "";
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
                            type = "";
                            state = "";
                        }
                    });
                    break;
            }
           
        }
        /// <summary>
        /// ɸѡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgbtnSearch_Click(object sender, EventArgs e)
        {
            popList1.Groups.Clear();
            DataCheckPoplist poplist =new  DataCheckPoplist();
            DataTable grouptable = poplist.GetPopGroup();
              if ( grouptable.Rows.Count > 0)
              {
                  foreach (DataRow  Grow in grouptable.Rows)
                  {
                        PopListGroup poli = new PopListGroup();
                        popList1.Groups.Add(poli);
                        poli.Text = Grow["GroupName"].ToString ();
                        DataTable table=new DataTable ();
                         switch (textTabBar1.SelectItemIndex)
                            {
                                case 0:
                                   table  = poplist.GetPendingCheckPopItem();
                                    break;
                                 case 1:
                                   table  = poplist.GetCheckPopItem();
                                     break ;
                            }
                      if (table .Rows .Count >0)
                      {
                           foreach (DataRow  rowli in table.Rows)
                            {
                                if (rowli["ParentID"].ToString().Equals(Grow["GroupID"].ToString()))
                               {
                                   poli.Items.Add(rowli["PopItemName"].ToString(), rowli["ParentID"].ToString() + "-" + rowli["Status"].ToString());
                                   if (type.Trim().Length > 0 & state .Trim ().Length >0)
                                   {
                                       if ((type.Trim() + "-" + state.Trim()).Equals(rowli["ParentID"].ToString() + "-" + rowli["Status"].ToString()))
                                       {
                                           popList1.SetSelections(poli.Items[(poli.Items.Count - 1)]);
                                       }
                                   }
                               }
                             }
                      }
                  }
              }
            popList1.Show();
        }
        
        /// <summary>
        /// ɸѡ��ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popList1_Selected(object sender, EventArgs e)
        {
            if (popList1.Selection != null)
            {
                foreach (PopListItem poitem in popList1.Selections)
                {

                    type = (popList1.Selection.Value.ToString()).Split('-')[0].ToString();
                    state = (popList1.Selection.Value.ToString()).Split('-')[1].ToString();          
                }
                GetCheckData();
            }
        }
    }
}