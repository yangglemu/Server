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
    public partial class Form_Edit_People : Form
    {

        MySqlConnection connection;
        MySqlCommand command;
        string xm;
        string sj;
        public Form_Edit_People()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.yuan;
            connection = Form_main.Connection;
            command = new MySqlCommand();
            command.Connection = connection;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.Return:
                    if (textBox_bh.Text.Trim().Length != 7)
                    {
                        MessageBox.Show("会员编号为7位");
                        textBox_bh.Select();
                        textBox_bh.SelectAll();
                        return;
                    }
                    string s = "select xm, dh from people where bh='";
                    s += textBox_bh.Text + "'";
                    command.CommandText = s;
                    MySqlDataReader dr = command.ExecuteReader();
                    if (dr.Read())
                    {
                        this.xm = this.textBox_xm.Text = dr.GetString(0);
                        this.sj = this.textBox_sj.Text = dr.GetString(1);
                        this.textBox_bh.ReadOnly = true;
                        this.textBox_xm.Select();
                    }
                    else
                    {
                        MessageBox.Show("无此会员！");
                        this.textBox_bh.Select();
                        this.textBox_bh.SelectAll();
                    }
                    dr.Close();
                    break;
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)//xm
        {
            if (e.KeyCode == Keys.Return)
            {
                if (this.textBox_xm.Text.Trim().Length > 1)
                    textBox_sj.Select();
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)//dh
        {
            if (e.KeyCode == Keys.Return)
                if (this.textBox_sj.Text.Trim().Length == 11)
                    button1.Select();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox_xm.Text.Trim().Length < 2)
            {
                MessageBox.Show("姓名至少2个字！");
                this.textBox_xm.Select();
                this.textBox_xm.SelectAll();
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
            string s;
            s = "update people set xm='";
            s += textBox_xm.Text.Trim() + "', dh='";
            s += textBox_sj.Text.Trim() + "' where bh='";
            s += this.textBox_bh.Text + "'";
            command.CommandText = s;
            try
            {
                command.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("修改会员出错,请检查输入是否正确！");
                return;
            }
            MessageBox.Show("修改会员成功！");
            this.textBox_bh.Clear();
            this.textBox_xm.Clear();
            this.textBox_sj.Clear();
            this.textBox_bh.ReadOnly = false;
            this.textBox_bh.Select();
        }

        private void textBox_xm_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
