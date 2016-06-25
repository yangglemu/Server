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
    public partial class Form_cx_one : Form
    {
        MySqlConnection connection;
        MySqlCommand command;

        public Form_cx_one()
        {
            InitializeComponent();
            connection = Form_main.Connection;
            command = new MySqlCommand();
            command.Connection = connection;

            this.Icon = Properties.Resources.yuan;
        }

        private bool CheckTM()
        {
            if (this.textBox_tm.ReadOnly)
            {
                return true;
            }
            string s = this.textBox_tm.Text.Trim();
            if (s.Length > 0 && s.Length < 15)
            {
                command.CommandText = string.Format("select pm,zq,hyzq from goods where(tm='{0}')",
                    this.textBox_tm.Text);
                MySqlDataReader dr = command.ExecuteReader();
                if (dr.Read())
                {
                    this.textBox_pm.Text = dr.GetString(0);
                    this.textBox_yzq.Text = dr.GetFloat(1).ToString("N2");
                    this.textBox_yhyzq.Text = dr.GetFloat(2).ToString("N2");
                    this.textBox_tm.ReadOnly = true;
                    dr.Close();
                    return true;
                }
                else
                {
                    dr.Close();
                    MessageBox.Show("无此商品！检查条码是否正确", "提示");
                    this.textBox_tm.Select();
                    this.textBox_tm.SelectAll();
                    return false;
                }
            }
            MessageBox.Show("条码输入有误！其值为9位字符串");
            this.textBox_tm.Select();
            this.textBox_tm.SelectAll();
            return false;
        }

        private bool CheckSZ(string sz)
        {
            float f = 0;
            if (float.TryParse(sz, out f) && f != 0)
            {
                if (f >= 0.1f && f <= 1.0f)
                {
                    return true;
                }
            }
            MessageBox.Show("折扣值在 0.1 和 1.0 之间！");
            return false;
        }
        private void textBox_tm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    if (CheckTM())
                        this.textBox_xzq.Select();
                    break;
            }
        }

        private void textBox_xzq_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    if(CheckSZ(this.textBox_xzq.Text.Trim()))
                    {
                        this.textBox_xhyzq.Select();
                        this.textBox_xhyzq.SelectAll();
                    }
                    else
                    {
                        this.textBox_xzq.Select();
                        this.textBox_xzq.SelectAll();
                    }
                    break;

                default:
                    break;
            }
        }

        private void textBox_xhyzq_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    if(CheckSZ(this.textBox_xhyzq.Text.Trim()))
                    {
                        this.button1.Select();
                    }
                    else
                    {
                        this.textBox_xhyzq.Select();
                        this.textBox_xhyzq.SelectAll();
                    }
                    break;

                default:
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!this.textBox_tm.ReadOnly)
            {
                MessageBox.Show("条码输入完毕后请按回车键，以便系统检索商品是否存在！");
                this.textBox_tm.Select();
                this.textBox_tm.SelectAll();
                return;
            }
            if (!CheckSZ(this.textBox_xzq.Text.Trim()))
                return;
            if (!CheckSZ(this.textBox_xhyzq.Text.Trim()))
                return;

            string s = string.Format("update goods set zq='{0}',hyzq='{1}' where tm='{2}'",
                this.textBox_xzq.Text, this.textBox_xhyzq.Text, this.textBox_tm.Text);
            command.CommandText = s;
            try
            {
                command.ExecuteNonQuery();
                this.textBox_tm.Clear();
                this.textBox_pm.Clear();
                this.textBox_xzq.Clear();
                this.textBox_xhyzq.Clear();
                this.textBox_yzq.Clear();
                this.textBox_yhyzq.Clear();
                this.textBox_tm.ReadOnly = false;
                this.textBox_tm.Select();
            }
            catch
            {
                MessageBox.Show("设置促销商品折扣时出错");
                this.textBox_tm.Select();
                this.textBox_tm.SelectAll();
            }
        }
    }
}
