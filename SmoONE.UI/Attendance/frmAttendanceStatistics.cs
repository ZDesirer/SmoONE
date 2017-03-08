using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.DTOs;

namespace SmoONE.UI.Attendance
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 1.1
    // Copyright  (c)  2016-2017 Smobiler
    // ����ʱ�䣺 2017/2
    // ��Ҫ���ݣ� ����ͳ�Ʋ鿴����
    // ******************************************************************
    partial class frmAttendanceStatistics : Smobiler.Core.MobileForm
    {       
        #region "definition"
        private int btnMode;             //ѡ����ʾģ��
        internal string UserID;          //�û�ID
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
        /// <summary>
        /// �ֻ��Դ����˰�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceStatistics_KeyDown(object sender, KeyDownEventArgs e)
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
        private void frmAttendanceStatistics_TitleImageClick(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// ������
        /// </summary>
        private void Bind()
        {
            try
            {
                string[] Year = this.btnYear.Text.Split('��');           //���
                string[] Month = this.btnMonth.Text.Split('��');         //�·�             
                DataTable table = new DataTable();
                switch (btnMode)
                {
                    case 1:
                        this.gridATdata.Layout = "frmAttendanceStatisticsLayout";
                        //��ȡĳ�¿����û�����
                        List<string> Users = AutofacConfig.attendanceService.GetUserNameByPeriod(Convert.ToDateTime(Year[0] + Month[0]).AddDays(-DateTime.Now.Day + 1), Convert.ToDateTime(Year[0] + Month[0]).AddDays(1 - DateTime.Now.Day).AddMonths(1));
                        if (Users.Count > 0)
                        {
                            table.Columns.Add("ID");                //�û�ID
                            table.Columns.Add("Pict");              //�û�ͷ��
                            table.Columns.Add("Name");              //�û�����
                            table.Columns.Add("Total");             //Ӧǩ������
                            table.Columns.Add("Al");                //׼ʱǩ������
                            table.Columns.Add("Late");              //�ٵ�����
                            table.Columns.Add("Early");             //���˴���
                            table.Columns.Add("No");                //δǩ����
                            foreach (string Row in Users)
                            {
                                MonthlyStatisticsDto Stat = AutofacConfig.attendanceService.GetUserMonthlyStatistics(Row, Convert.ToDateTime(Year[0] + Month[0]));
                                UserDetails UserDetail = new UserDetails();
                                UserDetailDto User = UserDetail.getUser(Row);
                                table.Rows.Add(User.U_ID, User.U_Portrait, User.U_Name, Stat.MS_AllCount, Stat.MS_InTimeCount, Stat.MS_ComeLateCount, Stat.MS_LeaveEarlyCount, Stat.MS_NoSignInCount + Stat.MS_NoSignOutCount);
                            }
                        }
                        break;
                    case 2:
                        this.gridATdata.Layout = "frmAttendanceStatDayLayout";
                        //��ȡĳ�¿�������
                        List<DateTime> listDate = AutofacConfig.attendanceService.GetDayOfMonthlyStatistics(UserID, Convert.ToDateTime(Year[0] + Month[0]));
                        if (listDate.Count > 0)
                        {
                            table.Columns.Add("Day");         //��Ҫǩ��������
                            foreach (DateTime Row in listDate)
                            {
                                string Time = Row.ToString("yyyy��M��d��    dddd", new System.Globalization.CultureInfo("zh-CN"));
                                table.Rows.Add(Time);
                            }
                        }
                        break;
                }
                gridATdata.Rows.Clear();    //�������ͳ���б�����
                if (table.Rows.Count > 0)
                {
                    this.gridATdata.DataSource = table;
                    this.gridATdata.DataBind();
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ҳ����أ���ʼ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceStatistics_Load(object sender, EventArgs e)
        {
            try
            {
                UserID = Client.Session["U_ID"].ToString();
                this.btnYear.Text = DateTime.Now.Year.ToString() + "�ꨋ";              //���
                this.btnMonth.Text = DateTime.Now.Month.ToString() + "�¨�";            //�·�
                for (int i = DateTime.Now.Year; DateTime.Now.Year - i < 10; i--)        //������ѡ��Χ
                {
                    PopListItem YearItem = new PopListItem();
                    YearItem.Text = i.ToString();
                    popListYear.Groups.FindByText("��ѡ�����").AddListItem(YearItem);
                    if (i == DateTime.Now.Year)
                    {
                        popListYear.SetSelections(YearItem);
                    }
                }
                popListMonth.SetSelections(popListMonth.Groups[0].Items[(DateTime.Now.Month - 1)]);
                btnMode = 1;
                 
                Bind();
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ������Ӧѡ����ҳ������ʾ��Ӧ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridATdata_CellClick(object sender, GridViewCellEventArgs e)
        {
            try
            {
                switch (btnMode)                    //ҳ����ʾģʽ
                {
                    case 1:
                        string[] Year = this.btnYear.Text.Split('��');      //���
                        string[] Month = this.btnMonth.Text.Split('��');       //�·�
                        frmAttendanceStatDay frmDay = new frmAttendanceStatDay();
                        frmDay.UserID = e.Cell.Items["image1"].Value.ToString();
                        frmDay.DayTime = Year[0] + Month[0];         //��������Ϣ���ݵ��¸�ҳ��
                        this.Redirect(frmDay);
                        break;
                    case 2:
                        frmAttendanceStatMan frmMan = new frmAttendanceStatMan();
                        frmMan.DayTime = e.Cell.Items["lblDay"].Text;    //��ѡ�����ڣ����ݵ��¸�ҳ��
                        this.Redirect(frmMan);
                        break;
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ģʽѡ����������GradView��ʾģ�������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textTabBar1_ItemClick(object sender, TabBarItemClickEventArgs e)
        {
            try
            {
                this.btnYear.Text = DateTime.Now.Year.ToString() + "�ꨋ";          //���
                this.btnMonth.Text = DateTime.Now.Month.ToString() + "�¨�";        //�·�
                popListYear.ClearSelections();
                popListYear.SetSelections(popListYear.Groups[0].Items[0]);
                popListMonth.ClearSelections();
                popListMonth.SetSelections(popListMonth.Groups[0].Items[(DateTime.Now.Month - 1)]);
                switch (e.Item.Value)
                {
                    case "Men":
                        btnMode = 1;
                        this.btnCheck.Text = "";
                        btnCheck.Enabled = false;
                        break;
                    case "Day":
                        btnMode = 2;
                        this.btnCheck.Text = ">";
                        btnCheck.Enabled = true;
                        break;
                }
                Bind();
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ѡ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popListYear_Selected(object sender, EventArgs e)
        {
            try
            {
                if (popListYear.Selection != null)       //���ѡ�������
                {
                    this.btnYear.Text = popListYear.Selection.Text + "�ꨋ";
                    Bind();         //���¼�������
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ѡ���·�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popListMonth_Selected(object sender, EventArgs e)
        {
            try
            {
                if (popListMonth.Selection != null)     //���ѡ�����·�
                {
                    this.btnMonth.Text = popListMonth.Selection.Text + "�¨�";
                    Bind();
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ����������ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButYear_Click(object sender, EventArgs e)
        {
            popListYear.Show();              //��ʾ���ѡ���б�
        }
        /// <summary>
        /// ��������·�ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButMonth_Click(object sender, EventArgs e)
        {
            popListMonth.Show();           //��ʾ�·�ѡ���б�
        }
        /// <summary>
        /// �鿴ѡ���·ݵ�ǩ��ͳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButCheck_Click(object sender, EventArgs e)
        {
            try
            {
                frmAttendanceStatMonth frmMonth = new frmAttendanceStatMonth();
                string[] Year = this.btnYear.Text.Split('��');
                frmMonth.Year = Year[0];                 //���
                string[] Month = this.btnMonth.Text.Split('��');
                frmMonth.month = Month[0];          //�·�
                this.Redirect(frmMonth);
            }
            catch(Exception ex)
            {
                Toast(ex.Message);
            }
        }
    }
}