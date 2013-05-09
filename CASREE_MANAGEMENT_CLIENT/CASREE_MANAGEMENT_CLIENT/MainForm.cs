using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CASREE_MANAGEMENT_CLIENT
{
    delegate void SetTextCallback(string str);   //定义委托
    public partial class MainForm : Form
    {
        string solutionName = string.Empty;
        public MainForm()
        {
            InitializeComponent();
            //Program.cb.//搜索工程列表
            //this.prjlistcomboBox.Items.Add();//手动添加工程
        }

        /// <summary>
        /// 单击添加用户按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void adduserbutton_Click(object sender, EventArgs e)
        {
            resultlabel.Text = "";
            string username = this.addusernametextBox.Text;
            string pw1 = this.adduserpwtextBox.Text;
            string pw2 = this.adduserpwtextBox2.Text;
            string groupid=this.groupidtextBox.Text;
            if (pw1.Equals(pw2))
            {
                int groupnum = 0;
                if (int.TryParse(groupid, out groupnum))//检查类型转换是否正确
                {
                    ///下面版本在服务器端未实现对应功能
                    //string resultmessage = Program.cb.AddUser(username, pw1, Convert.ToInt32(groupid));
                    //if (resultmessage.Equals("succeed"))
                    //    MessageBox.Show("用户添加成功！");
                    //else if (resultmessage.Equals("existed"))
                    //    resultlabel.Text = "用户已存在！";
                    //else
                    //    resultlabel.Text = "用户添加失败！";
                    Boolean resultmessage = Program.cb.AddUser(username, pw1, Convert.ToInt32(groupid));
                    if (resultmessage)
                        MessageBox.Show("用户添加成功！");
                    else
                        resultlabel.Text = "用户添加失败！";
                }
                else
                    resultlabel.Text = "用户组必须为正整数！";
            }
            else
            {
                resultlabel.Text = "两次输入密码不一致！";
            }
        }
        /// <summary>
        /// 单击搜索用户按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchuserbutton_Click(object sender, EventArgs e)
        {
            searchresultlabel.Text = "";
            string username = this.searchusernametextBox.Text;
            string resultmessage= Program.cb.SearchUser(username);
            if (resultmessage.Equals("fail"))
            {
                searchresultlabel.Text = "查询失败！";
            }
            else if(resultmessage.Equals("notexisted"))
            {
                searchresultlabel.Text = "用户不存在";
            }
            else
            {
                string username2 = resultmessage.Split(':')[0];
                string passwd = resultmessage.Split(':')[1];
                string groupid = resultmessage.Split(':')[2];
                pwlabel.Text = passwd;
                groupidlabel.Text = groupid;
            }
        }
        /// <summary>
        /// 单击删除用户按钮，待完善
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deletebutton_Click(object sender, EventArgs e)
        {
            pwlabel.Text = "";
            groupidlabel.Text = "";
            MessageBox.Show("功能尚未添加！");
        }
        /// <summary>
        /// 单击添加权限按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addpermissionbutton_Click(object sender, EventArgs e)
        {
            addpmresultlabel.Text = "";
            string username = this.addpmusertextBox.Text;
            string projectid = this.addpmprjidtextBox.Text;
            string permissionlevel = this.addpmleveltextBox.Text;
            int level = 0;
            if (username.Equals(string.Empty) || projectid.Equals(string.Empty) || permissionlevel.Equals(string.Empty))
            {
                addpmresultlabel.Text = "请补全权限信息！";
            }
            else
            {
                if (int.TryParse(permissionlevel, out level))//检查类型转换是否正确
                {
                    string resultmessage= Program.cb.AddPermission(username, projectid, level);
                    if (resultmessage.Equals("fail"))
                        addpmresultlabel.Text = "权限添加失败";
                    else if (resultmessage.Equals("existed"))
                        addpmresultlabel.Text = "权限已经存在";
                    else if (resultmessage.Equals("notexisted"))
                        addpmresultlabel.Text = "用户不存在";
                    else
                    {
                        addpmresultlabel.Text = "权限添加成功！";
                    }
                }
                else
                    addpmresultlabel.Text = "权限级别不合法！";
            }
        }
        /// <summary>
        /// 单击搜索权限按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchpermissionbutton_Click(object sender, EventArgs e)
        {
            searchpmresultlabel.Text = "";//清空权限查询结果标签
            string username = this.searchpmusertextBox.Text;//待查询用户名
            if (username.Equals(string.Empty))//用户名为空
            {
                searchpmresultlabel.Text="用户名不能为空！";
            }
            else//用户名不为空
            {
                string resultmessage = Program.cb.SearchPermission(username);//搜索，返回结果
                if(resultmessage.Equals("notexisted"))//用户不存在
                {
                    searchpmresultlabel.Text = "用户不存在！";
                }
                else if (resultmessage.Equals("fail"))//查询失败
                {
                    searchpmresultlabel.Text = "权限查询失败！";
                }
                else//权限查询成功，处理查询结果
                {
                    int permissionnum = Convert.ToInt32(resultmessage.Split(':')[0]);
                    List<string> projectidlist = new List<string>();
                    List<string> levellist = new List<string>();
                    for (int i = 0; i < permissionnum * 2; i = i + 2)
                    {
                        projectidlist.Add(resultmessage.Split(':')[i + 1]);
                        levellist.Add(resultmessage.Split(':')[i + 2]);
                    }
                    for (int i = 0; i < permissionnum; i++)
                        searchpmdataGridView.Rows.Add(projectidlist[i], levellist[i]);
                        //searchpmresultlabel.Text += "工程名称：" + projectidlist[i] + " 权限级别:" + levellist[i] + "\n";
                }
            }
        }

        private void searchsolutionbutton_Click(object sender, EventArgs e)
        {
            searchsolutionresultlabel.Text = "";//清空权限查询结果标签
            solutiondataGridView.Rows.Clear();//清空工程列表
            sourcelistBox.Items.Clear();
            destinationlistBox.Items.Clear();
            pushresultlabel.Text = "";
            string solutionname = this.solutiontextBox.Text;//待查询用户名
            this.solutionName = solutionname;
            if (solutionname.Equals(string.Empty))//项目名为空
            {
                searchpmresultlabel.Text = "项目名称不能为空！";
            }
            else//用户名不为空
            {
                string resultmessage = Program.cb.SearchProjectsBySolution(solutionname);//搜索，返回结果
                if (resultmessage.Equals("notexisted"))//用户不存在
                {
                    this.searchsolutionresultlabel.Text = "项目不存在！";
                }
                else if (resultmessage.Equals("fail"))//查询失败
                {
                    this.searchsolutionresultlabel.Text = "项目查询失败！";
                }
                else//权限查询成功，处理查询结果
                {
                    int permissionnum = Convert.ToInt32(resultmessage.Split(':')[0]);
                    List<string> tooltypelist = new List<string>();//工具类型
                    List<string> projectidlist = new List<string>();//工程id
                    List<string> statuslist = new List<string>();//在线状态
                    for (int i = 0; i < permissionnum * 3; i = i + 3)
                    {
                        tooltypelist.Add(resultmessage.Split(':')[i + 1]);
                        projectidlist.Add(resultmessage.Split(':')[i + 2]);
                        statuslist.Add(resultmessage.Split(':')[i + 3]);
                    }
                    if (tooltypelist.Count == 0)
                        this.searchsolutionresultlabel.Text = "项目下不存在任何工程";
                    for (int i = 0; i < permissionnum; i++)
                    {
                        solutiondataGridView.Rows.Add(tooltypelist[i], projectidlist[i], statuslist[i]);

                        //设置推送listbox
                        sourcelistBox.Items.Add(projectidlist[i]);//显示全部工程
                        if(statuslist[i].Equals("在线"))
                            destinationlistBox.Items.Add(projectidlist[i]); //仅显示在线工程
                    }
                    //searchpmresultlabel.Text += "工程名称：" + projectidlist[i] + " 权限级别:" + levellist[i] + "\n";

                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string sourceprjid=string.Empty;
            string destinationprjid = string.Empty;
            if (this.solutionName != string.Empty && sourcelistBox.SelectedItem != null && destinationlistBox.SelectedItem != null)
            {
                sourceprjid = sourcelistBox.SelectedItem.ToString();
                destinationprjid = destinationlistBox.SelectedItem.ToString();
                pushresultlabel.Text=Program.cb.PushXML(sourceprjid, destinationprjid, this.solutionName);
                Program.cb.StatusChange = this.refreshPushResultLabel;
            }

        }

        private void refreshPushResultLabel(string result)
        {
            if (pushresultlabel.InvokeRequired)  //控件是否跨线程？如果是，则执行括号里代码
            {
                SetTextCallback setListCallback = new SetTextCallback(refreshPushResultLabel);   //实例化委托对象
                pushresultlabel.Invoke(setListCallback, result);   //重新调用SetListBox函数
            }
            else  //否则，即是本线程的控件，控件直接操作
            {
                pushresultlabel.Text = result;
            }
        }
    }
}
