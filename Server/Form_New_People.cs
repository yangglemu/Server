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
    public partial class Form_New_People : Form
    {
        MySqlCommand command;
        public Form_New_People()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.yuan;
            command = Form_main.Command;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                textBox_xm.Select();
            else if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                textBox_sj.Select();
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                button1.Select();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox_bh.Text.Trim().Length < 1 && textBox_bh.Text.Trim().Length > 12)
            {
                MessageBox.Show("会员编号为12位以下数字!");
                textBox_bh.Select();
                textBox_bh.SelectAll();
                return;
            }
            if (textBox_xm.Text.Trim().Length < 2)
            {
                MessageBox.Show("姓名必须2个字以上");
                textBox_xm.Select();
                textBox_xm.SelectAll();
                return;
            }

            string s = "select ifnull(count(*),0) from people where bh='";
            s += textBox_bh.Text + "'";
            command.CommandText = s;
            int count = int.Parse(command.ExecuteScalar().ToString());

            if (count != 0)
            {
                MessageBox.Show("此会员已存在！");
                textBox_bh.Select();
                textBox_bh.SelectAll();
                return;
            }

            if (this.textBox_sj.Text.Trim().Length != 11)
            {
                MessageBox.Show("手机号为11位");
                textBox_sj.Select();
                textBox_sj.SelectAll();
                return;
            }
            foreach (char c in textBox_sj.Text)
            {
                if (!char.IsNumber(c))
                {
                    MessageBox.Show("手机号，输入了非数字!");
                    textBox_sj.Select();
                    textBox_sj.SelectAll();
                    return;
                }
            }

            Form_main main = this.Owner as Form_main;
            s = "insert into people(bh,xm,dh,rq) values('";
            s += textBox_bh.Text.Trim() + "','";
            s += textBox_xm.Text.Trim() + "','";
            s += textBox_sj.Text.Trim() + "','";
            s += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            //MessageBox.Show(s);
            command.CommandText = s;
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception se)
            {
                MessageBox.Show("添加新会员出错,请检查输入是否正确！\r\n" + se.Message);
                return;
            }
            MessageBox.Show("添加新会员成功！");
            this.textBox_bh.Clear();
            this.textBox_xm.Clear();
            this.textBox_sj.Clear();
            this.textBox_bh.Select();
        }
    }
}
