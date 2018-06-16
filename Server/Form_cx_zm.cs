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
    public partial class Form_cx_zm : Form
    {
        MySqlCommand command;

        public Form_cx_zm()
        {
            InitializeComponent();
            command = Form_main.Command;
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
        private void textBox_xzq_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    if(this.CheckSZ(this.textBox_xzq.Text.Trim()))
                    {
                        this.textBox_xhyzq.Select();
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
            if (!CheckSZ(this.textBox_xzq.Text.Trim()))
                return;
            if (!CheckSZ(this.textBox_xhyzq.Text.Trim()))
                return;
            string s = string.Format("update goods set zq={0},hyzq={1} where ghs='{2}'",
                this.textBox_xzq.Text.Trim(), this.textBox_xhyzq.Text.Trim(), "1001");
            command.CommandText = s;
            try
            {
                if (command.ExecuteNonQuery() > 0)
                    MessageBox.Show("操作成功！");
                this.textBox_xzq.Clear();
                this.textBox_xhyzq.Clear();
                this.textBox_xzq.Select();
            }
            catch
            {
                MessageBox.Show("设置促销商品折扣时出错");
                this.textBox_xzq.Select();
                this.textBox_xzq.SelectAll();
            }
        }
    }
}
