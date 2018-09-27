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
            this.textBox1_tm.Select();
            command = Form_main.Command;
        }

        public bool CheckTM()
        {
            tm = this.textBox1_tm.Text.Trim();///////tm
            if (tm.Length < 1 || tm.Length > 15)
                return false;
            if (tm.Length < 4 && tm.Length > 0)
            {
                int i;
                if (int.TryParse(tm, out i))
                {
                    tm = "010101" + i.ToString("000");
                    this.textBox1_tm.Text = tm;
                }
            }
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
                MessageBox.Show("不存在的条码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1_tm.Select();
                this.textBox1_tm.SelectAll();
                return false;
            }
            this.textBox1_tm.ReadOnly = true;
            this.textBox4_fs.Select();
            this.textBox4_fs.SelectAll();
            return true;
        }
        public void textBox1_KeyDown(object sender, KeyEventArgs e)//条码
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    CheckTM();
                    break;
                case Keys.Escape:
                    this.Close();
                    break;
                default:
                    break;
            }
        }

        private void textBox4_fs_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
                case Keys.Return:
                    if (CheckFS()) this.textBox1_hh.Select();
                    break;
                default:
                    break;
            }
        }
        private bool CheckFS()
        {
            int m = 0;
            int.TryParse(this.textBox4_fs.Text.Trim(), out m);
            if (m < 1)
            {
                this.textBox4_fs.Select();
                return false;
            }
            if (m > 29)
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
            doc.SubStrings["shop"].Value = Form_main.shop;
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
            this.textBox1_tm.ReadOnly = false;
        }

        private void Form_Print_BarCode_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void textBox1_hh_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    this.button1_print.Select();
                    break;
                case Keys.Escape:
                    Close();
                    break;
                default:
                    break;
            }
        }


        public bool add_print(string tm)
        {
            this.textBox1_tm.Text = tm;
            return CheckTM();
        }

        private void button1_print_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
                default:
                    break;
            }
        }
    }
}
