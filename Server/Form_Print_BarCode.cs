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
using Seagull.BarTender.Print;

namespace Server
{
    public partial class Form_Print_BarCode : Form
    {
        private MySqlCommand command;
        string tm;
        string pm;
        string dj;
        string hh;
        int fs;

        public Form_Print_BarCode()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            this.Icon = Properties.Resources.yuan;
            this.textBox1_tm.Select();
            command = Form_main.Command;
            this.ShowInTaskbar = false;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)//条码
        {
            if (e.KeyCode == Keys.Return)
            {
                tm = this.textBox1_tm.Text.Trim();///////tm
                if (tm.Length < 8 && tm.Length > 15)
                    return;
                string sql = string.Format("select pm,sj from goods where tm='{0}'", tm);
                command.CommandText = sql;
                MySqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    this.pm = this.textBox_pm.Text = dr.GetString(0);//pm
                    this.dj = dr.GetFloat(1).ToString("N2");/////////////dj
                    this.dj = this.textBox3_dj.Text = "￥" + dj;
                }
                dr.Close();
                if (this.textBox_pm.TextLength == 0)
                {
                    MessageBox.Show("不存在的条码！");
                    this.textBox1_tm.Select();
                    this.textBox1_tm.SelectAll();
                    return;
                }
                this.textBox4_fs.Select();
                this.textBox4_fs.SelectAll();
            }
        }

        private void textBox4_fs_KeyDown(object sender, KeyEventArgs e)
        {
            /*
            if (!CheckFS())
            {
                this.textBox4_fs.Select();
                this.textBox4_fs.SelectAll();
                return;
            }
            if (e!=null && e.KeyCode == Keys.Return)
            {                
                this.textBox1_hh.Select();
            }
             */
            if (!CheckFS())
            {
                return;
            }
            if (e != null && e.KeyCode == Keys.Return)
            {
                this.textBox1_hh.Select();
            }
        }
        private bool CheckFS()
        {
            /*
            if (!int.TryParse(this.textBox4_fs.Text.Trim(), out fs))//fs
            {
                MessageBox.Show("输入打印份数出错！");
                return false;
            }
            if (fs < 1)
            {
                MessageBox.Show("打印份数必须大于等于1");
                return false;
            }
            if (fs > 9)
            {
                if (MessageBox.Show("打印副本过多，是否继续？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                    return false;
            }
            return true;
             * */
            int m = 0;
            int.TryParse(this.textBox4_fs.Text.Trim(), out m);
            if (m < 1)
            {
                this.textBox4_fs.Select();
                return false;
            }
            if (m > 9)
            {
                if (MessageBox.Show("打印副本过多，是否继续？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                {
                    this.textBox4_fs.Clear();
                    this.textBox4_fs.Select();
                    this.textBox4_fs.SelectAll();
                    return false;
                }
            }
            this.fs = m;
            return true;
        }
        private void button1_print_Click(object sender, EventArgs e)
        {
            if (!CheckFS())
                return;
            this.hh = this.textBox1_hh.Text.Trim();
            LabelFormatDocument doc;
            if (hh.Length < 1)
                doc = Form_main.labeldoc;
            else
                doc = Form_main.labeldoc2;
            doc.SubStrings["tm"].Value = this.tm;
            doc.SubStrings["pm"].Value = this.pm;
            doc.SubStrings["sj"].Value = this.dj;
            doc.SubStrings["fs"].Value = fs.ToString();
            if (hh.Length > 0)
                doc.SubStrings["hh"].Value = hh;
            doc.Print();
            this.textBox_pm.Clear();
            this.textBox3_dj.Clear();
            this.textBox1_tm.Clear();
            this.textBox4_fs.Clear();
            this.textBox1_tm.Select();
            this.textBox1_hh.Clear();
            this.textBox1_tm.SelectAll();
            this.tm = this.pm = this.dj = this.hh = "";
            this.fs = 1;
        }

        private void Form_Print_BarCode_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void textBox1_hh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                this.button1_print.Select();
        }
    }
}
