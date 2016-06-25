using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Form_Edit_rk_temp : Form
    {
        DataGridViewRow row;
        public string tm;
        public int sl;
        public Form_Edit_rk_temp(DataGridViewRow row)
        {
            InitializeComponent();
            this.row = row;
            this.textBox1.Text = row.Cells["条码"].Value.ToString();
            this.textBox2.Text = row.Cells["数量"].Value.ToString();
            this.textBox1.Select();
            this.textBox1.SelectAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!this.textBox1.ReadOnly)
                return;

            if (this.textBox2.TextLength < 1 || this.textBox1.TextLength < 1)
            {
                this.textBox1.ReadOnly = false;
                MessageBox.Show("输入错误！\r\n本次操作失败！");
                this.textBox1.Clear();
                this.textBox2.Clear();
                this.textBox1.Select();
                return;
            }

            tm = this.textBox1.Text.Trim();
            if (!int.TryParse(this.textBox2.Text.Trim(), out sl))
            {
                MessageBox.Show("输入数量错误！");
                this.textBox2.Select();
                this.textBox2.SelectAll();
                return;
            }
            string sql = string.Format("update rk_temp set tm='{0}',sl='{1}' where rq='{2}'", tm, sl, row.Cells["日期"].Value);
            Form_main.Command.CommandText = sql;
            int ret = Form_main.Command.ExecuteNonQuery();
            if(ret !=1 )
            {
                MessageBox.Show("更新数据时出错！");
                    return;
            }
            this.row.Cells["条码"].Value = tm;
            this.row.Cells["数量"].Value = sl;
            this.Close();
            this.textBox1.ReadOnly = false;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string sql = string.Format("select count(*) from goods where tm='{0}'", this.textBox1.Text);
                Form_main.Command.CommandText = sql;
                int ret = int.Parse(Form_main.Command.ExecuteScalar().ToString());
                if (ret != 1)
                {
                    MessageBox.Show("条码出错！");
                    return;
                }
                this.textBox1.ReadOnly = true;
                this.textBox2.Select();
                this.textBox2.SelectAll();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.textBox2.TextLength < 1)
                    return;
                this.button1.Select();
            }
        }
    }
}
