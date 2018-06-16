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
    public partial class Form_AddWorkerl : Form
    {
        MySqlCommand command;
        public Form_AddWorkerl()
        {
            InitializeComponent();
            command = Form_main.Command;
        }

        private void button1_Click(object sender, EventArgs e)
        {
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

            if(this.comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("请从列表框选择权限");
                this.comboBox1.Select();
                this.comboBox1.SelectedIndex = 0;
                return;
            }

            if (this.comboBox1.Text != "低" && this.comboBox1.Text != "中" && this.comboBox1.Text != "高")
            {
                MessageBox.Show("检查权限设置是否正确！");
                this.comboBox1.Select();
                return;
            }

            string s = "select count(*) from worker where bh='" + this.textBox_bh.Text.Trim() + "'";
            command.CommandText = s;
            int count = int.Parse(command.ExecuteScalar().ToString());
            if (count == 1)
            {               
                MessageBox.Show("此编号已使用，要修改该帐号资料，请先使用该帐号登录！");
                this.textBox_bh.SelectAll();
                return;
            }
            Form_main main = this.Owner as Form_main;
            s = string.Format("insert into worker(bh,xm,mm,qx,dh,rq) values('{0}','{1}','{2}','{3}','{4}','{5}')",
                this.textBox_bh.Text,
                this.textBox_xm.Text,
                this.textBox_mm.Text,
                this.comboBox1.Text,
                this.textBox_dh.Text,
                DateTime.Now.ToString());
            command.CommandText = s;
            command.ExecuteNonQuery();
            MessageBox.Show("添加新员工成功！");
            this.textBox_bh.Clear();
            this.textBox_xm.Clear();
            this.textBox_mm.Clear();
            this.textBox_dh.Clear();
            this.textBox_bh.Select();
        }

        private void Form_yggl_Shown(object sender, EventArgs e)
        {
            Form_main mf = this.Owner as Form_main;
            if (mf.worker.qx == "高")
                this.comboBox1.Items.AddRange(new string[] { "低", "中" });
            else if (mf.worker.qx == "中")
                this.comboBox1.Items.AddRange(new string[] { "低" });
            this.comboBox1.SelectedIndex = 0;
        }
    }
}
