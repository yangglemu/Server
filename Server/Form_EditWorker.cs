using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Server
{
    public partial class Form_EditWorker : Form
    {
        MySqlCommand command;
        public Form_EditWorker()
        {
            InitializeComponent();
            command = Form_main.Command;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //检查所有文本框内容是否符合要求
            if (this.textBox_bh.TextLength != 4)
            {
                MessageBox.Show("编号为四位字符（字符或数字）");
                this.textBox_bh.Select();
                this.textBox_bh.SelectAll();
                return;
            }
            if (this.textBox_xm.TextLength < 1)
            {
                this.textBox_xm.Select();
                this.textBox_xm.SelectAll();
                return;
            }
            if (this.textBox_mm.TextLength != 6)
            {
                MessageBox.Show("密码长度为6位！");
                this.textBox_mm.Select();
                this.textBox_mm.SelectAll();
                return;
            }
            if (this.textBox_dh.TextLength < 7)
            {
                MessageBox.Show("请输入电话号码！");
                this.textBox_dh.Select();
                this.textBox_dh.SelectAll();
                return;
            }
            if (this.comboBox1.Text != "低" && this.comboBox1.Text != "中" && this.comboBox1.Text != "高")
            {
                MessageBox.Show("检查权限设置是否正确！");
                this.comboBox1.Select();
                return;
            }

            //要修改的是否是已登录的当前用户，并且表中存在此记录，才接受修改请求           

            Form_main mf = this.Owner as Form_main;
            if (mf.worker.bh == this.textBox_bh.Text.Trim() || mf.worker.qx=="中" || mf.worker.qx == "高")
            {
                string s;
                s = string.Format("update worker set xm='{0}',mm='{1}',dh='{2}',qx='{3}' where bh='{4}'",
                    this.textBox_xm.Text,
                    this.textBox_mm.Text,
                    this.textBox_dh.Text,
                    this.comboBox1.Text,
                    this.textBox_bh.Text);
                command.CommandText = s;
                command.ExecuteNonQuery();
                MessageBox.Show("修改成功！");
                this.Close();
                return;
            }
            else
            {
                MessageBox.Show("修改员工资料时，请先使用该员工帐号登录，或者当前操作员具有中级以上权限！");
                this.textBox_bh.SelectAll();
            }
        }

        private void Form_editWorker_Load(object sender, EventArgs e)
        {
            Form_main mf = this.Owner as Form_main;
            if (mf.worker.qx == "高")
                this.comboBox1.Items.AddRange(new string[] { "低", "中", "高" });
            else if (mf.worker.qx == "中")
                this.comboBox1.Items.AddRange(new string[] { "中", "低" });
            else if (mf.worker.qx == "低")
                this.comboBox1.Items.Add("低");
            this.comboBox1.SelectedIndex = 0;

            string s = string.Format("select xm,mm,qx,dh from worker where(bh='{0}')", mf.worker.bh);
            command.CommandText = s;
            MySqlDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                this.textBox_bh.Text = mf.worker.bh;
                this.textBox_bh.ReadOnly = true;
                this.textBox_xm.Text = dr.GetString(0);
                this.textBox_mm.Text = dr.GetString(1);
                this.comboBox1.Text = dr.GetString(2);
                this.textBox_dh.Text = dr.GetString(3);
            }
            else
            {
                MessageBox.Show("未知错误!");
            }
            dr.Close();
        }
    }
}
