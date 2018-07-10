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
    public partial class Form_cx_qb : Form
    {
        MySqlCommand command;
        public Form_cx_qb()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.yuan;
            command = Form_main.Command;
        }

        private void textBox_xzq_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    if (this.textBox_xzq.TextLength < 1)
                        return;

                    float m = 0.0f;
                    try
                    {
                        m = float.Parse(this.textBox_xzq.Text);
                        this.textBox_xhyzq.Select();
                    }
                    catch
                    {
                        MessageBox.Show("输入是否数字？");
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
                    if (this.textBox_xhyzq.TextLength < 1)
                        return;

                    float m = 0.0f;
                    try
                    {
                        m = float.Parse(this.textBox_xhyzq.Text);
                        this.button1.Select();
                    }
                    catch
                    {
                        MessageBox.Show("输入是否数字？");
                        this.textBox_xhyzq.SelectAll();
                    }
                    break;

                default:
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = string.Format("update goods set zq={0},hyzq={1}",
                this.textBox_xzq.Text, this.textBox_xhyzq.Text);
            command.CommandText = s;
            try
            {
                command.ExecuteNonQuery();
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
