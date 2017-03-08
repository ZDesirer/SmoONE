using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.Domain;


namespace SmoONE.UI
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 1.0
    // Copyright  (c)  2016-2017 Smobiler 
    // ����ʱ�䣺 2016/11
    // ��Ҫ���ݣ�  ��������
    // ******************************************************************
    partial class frmWork : Smobiler.Core.MobileForm
    {
        #region "definition"
        /// <summary>
        /// �˵����ֵ�
        /// </summary>
        /// <remarks></remarks>
        private Dictionary<string, IconMenuViewGroup> MenuGroupDict;//�����˵�
        private DateTime toasttime;//toastʱ��
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
        /// <summary>
        /// TabBar����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageTabBar1_ItemClick(object sender, TabBarItemClickEventArgs e)
        {
            switch (imageTabBar1.SelectItemIndex)
            {
                case 0:
                    frmCheck frmCheck = new frmCheck();
                    Redirect(frmCheck);
                        break;
                case 1:
                        frmCreated frmCreated = new frmCreated();
                        Redirect(frmCreated);
                        break;
                case 2:
                        frmCCTo frmCCTo = new frmCCTo();
                        Redirect(frmCCTo);
                        break;
            }
        }
        /// <summary>
        /// iconMenuData����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iconMenuData_MenuItemClick(object sender, IconMenuItemEventArgs e)
        {
            MenuItem(e.Item.ID);
        }

        /// <summary>
        /// Toolbar����
        /// </summary>
        /// <param name="toolbarItemName"></param>
        private void ProcessToolbarFormName(string toolbarItemName)
        {
            try
            {
                switch (toolbarItemName)
                {
                    case "":
                        this.Close();
                        break;
                    case "Me":
                        frmUser frm = new frmUser();
                        this.Redirect(frm, (MobileForm sender1, object args) => ProcessToolbarFormName(frm.toolbarItemName));
                        break;
                }
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
        private void frmWork_ToolbarItemClick(object sender, ToolbarClickEventArgs e)
        {
            ProcessToolbarFormName(e.Name);
        }
        /// <summary>
        /// ��ʼ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmWork_Load(object sender, EventArgs e)
        {
            MenuGroupDict = new Dictionary<string, IconMenuViewGroup>();
            //��ȡ�˵�
            MenuGroup();
        }
        /// <summary>
        ///��ȡ�˵�
        /// </summary>
        private void MenuGroup()
        {
            try
            {
                List<Menu> listmenu = AutofacConfig.userService.GetMenuByUserID(Client.Session["U_ID"].ToString());
                this.iconMenuData.Groups.Clear();
                MenuGroupDict.Clear();
                IconMenuViewGroup grp = new IconMenuViewGroup("Default", "");
                //��ȡ���в˵���
                foreach (Menu menu in listmenu)
                {
                    if (string.IsNullOrWhiteSpace(menu.M_ParentID) == true)
                    {
                        //���һ���˵�
                        grp.Items.Add(new IconMenuViewItem(menu.M_MenuID, menu.M_Description, menu.M_Portrait, menu.M_MenuID));
                        //��Ӷ����˵�
                        List<Menu> listsecondMenu = AutofacConfig.userService.GetSubMenuByUserID(Client.Session["U_ID"].ToString(), menu.M_MenuID);
                        if (listsecondMenu.Count > 0)
                        {
                            Menu menuItem = AutofacConfig.userService.GetMenuByMenuID(menu.M_MenuID);
                            IconMenuViewGroup mvGroupItem = new IconMenuViewGroup(menuItem.M_MenuID, menuItem.M_Description);
                            foreach (Menu secondMenu in listsecondMenu)
                            {
                                mvGroupItem.Items.Add(new IconMenuViewItem(secondMenu.M_MenuID, secondMenu.M_Description, secondMenu.M_Portrait, secondMenu.M_MenuID));
                                if (MenuGroupDict.ContainsKey(menu.M_MenuID) == false)
                                    MenuGroupDict.Add(menu.M_MenuID, mvGroupItem);
                            }

                        }

                    }
                }
                this.iconMenuData.Groups.Add(grp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// �˵�����¼�����
        /// </summary>
        /// <param name="id"></param>
        private void MenuItem(string id)
        {
	        if (MenuGroupDict.ContainsKey(id) == true) 
            {
                //��ʾ��ǰ�˵��Ķ����˵�
		        this.iconMenuData.ShowDialogMenu(MenuGroupDict[id]);
	        } 
            else
            {
                switch (id)
                {
                        //�������
                    case "Leave":
                            Leave.frmLeaveCreate frmLeaveCreate = new Leave.frmLeaveCreate();
                            Redirect(frmLeaveCreate);
                            break;
                        //��������
                    case "Reimbursement":
                            RB.frmRBCreate frmRBCreate = new RB.frmRBCreate();
                            Redirect(frmRBCreate);
                            break;
                        //�������Ѽ�¼
                    case "RB_Rows":
                            RB.frmRBRows frmRBRows = new RB.frmRBRows();
                            Redirect(frmRBRows);
                            break;
                    //�������Ѽ�¼ģ��
                    case "RB_RType_Template":
                            RB.frmRTypeTemplate frmRTypeTemplate = new RB.frmRTypeTemplate();
                            Redirect(frmRTypeTemplate);
                            break;
                    //��������
                    case "Department":
                            Department.frmDepartment frmDepartment = new Department.frmDepartment();
                            Redirect(frmDepartment);
                            break;
                    //�����ɱ�����
                    case "CostCenter":
                            CostCenter.frmCostCenter frmCostCenter = new CostCenter.frmCostCenter();
                            Redirect(frmCostCenter);
                            break;
                    //�����ɱ�����ģ��
                    case "CC_Type_Template":
                            CostCenter.frmCostTemplet frmCostTemplet = new CostCenter.frmCostTemplet();
                            Redirect(frmCostTemplet);
                       
                            break;
                    //���ڹ���ģ��
                    case "AttendanceManagement":
                            Attendance.frmAttendanceManager frmAttendanceManager = new Attendance.frmAttendanceManager();
                            Redirect(frmAttendanceManager);
                            break;
                    //����
                    case "AttendanceInfo":
                            Attendance.frmAttendanceMain frmAttendanceMain = new Attendance.frmAttendanceMain();
                            frmAttendanceMain.enter = (int)Enum.Parse(typeof(ATMainState), ATMainState.����ǩ��.ToString());
                            Redirect(frmAttendanceMain);
                            break;
                    //�ҵĿ�����ʷ
                    case "MyAttendanceHistory":
                            Attendance.frmAttendanceStatSelfDay frmAttendanceStatSelfDay = new Attendance.frmAttendanceStatSelfDay();
                            Redirect(frmAttendanceStatSelfDay);
                            break;
                    //����ͳ��
                    case "AttendanceStatistics":
                            Attendance.frmAttendanceStatistics frmAttendanceStatistics = new Attendance.frmAttendanceStatistics();
                            Redirect(frmAttendanceStatistics);
                            break;
                 
                }
		    }
        }
        /// <summary>
        /// �ֻ��Դ����˰�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmWork_KeyDown(object sender, KeyDownEventArgs e)
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

                this.Client.Exit();
            }
            else
            {
                toasttime = DateTime.Now;
                this.Toast("�ٰ�һ���˳�ϵͳ", ToastLength.SHORT);
            }
        }
    }
}