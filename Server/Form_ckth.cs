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
    public partial class Form_ckth : Form
    {
        MySqlCommand command;
        string yy;
        string tm;

        public Form_ckth()
        {
            InitializeComponent();
            command = Form_main.Command;
            yy = this.radioButton_th.Text;
        }

        private void textBox_tm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    if (CheckTM())
                    {
                        this.textBox_sl.Select();
                    }
                    break;

                default:
                    break;
            }
        }

        private void textBox_sl_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    if (CheckSL())
                    {
                        this.button1.Select();
                    }
                    break;

                default:
                    break;
            }
        }

        private bool CheckTM()
        {
            if (this.textBox_tm.ReadOnly)
            {
                return true;
            }
            string s = this.textBox_tm.Text.Trim();
            if (s.Length < 4)
            {
                int x;
                if (Int32.TryParse(s, out x))
                {
                    s = "010101" + x.ToString("000");
                }
                else
                {
                    return false;
                }
            }
            if (s.Length > 0 && s.Length < 15)
            {
                command.CommandText = "select pm,jj,sj from goods where tm='" + s + "'";
                MySqlDataReader dr = command.ExecuteReader();
                if (dr.Read())
                {
                    this.textBox_pm.Text = dr.GetString(0);
                    this.textBox_jj.Text = dr.GetFloat(1).ToString("N2");
                    this.textBox_sj.Text = dr.GetFloat(2).ToString("N2");
                    tm = s;
                    this.textBox_tm.Text = tm;
                    this.textBox_tm.ReadOnly = true;
                    this.textBox_sl.Select();
                    dr.Close();
                    return true;
                }
                else
                {
                    dr.Close();
                    tm = "";
                    MessageBox.Show("无此商品！检查条码是否正确", "友情提示");
                    this.textBox_tm.SelectAll();
                    return false;
                }
            }
            MessageBox.Show("条码输入有误！其值为9位字符串");
            this.textBox_tm.Select();
            this.textBox_tm.SelectAll();
            return false;
        }
        private bool CheckSL()
        {
            string s = this.textBox_sl.Text.Trim();
            int f;
            if (int.TryParse(s, out f))
            {
                if (f > 0)
                    return true;
            }
            MessageBox.Show("数量应输入大于 0 的数值！");
            this.textBox_sl.Select();
            this.textBox_sl.SelectAll();
            return false;
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
            if (!CheckSL())
                return;

            if (this.textBox_pm.TextLength < 1)
                return;

            Form_main f = this.Owner as Form_main;

            string s = string.Format("update goods set kc=kc-{0} where tm='{1}'",
                this.textBox_sl.Text, tm);

            command.CommandText = s;
            MySqlTransaction tr = command.Connection.BeginTransaction();
            try
            {
                command.ExecuteNonQuery();
                s = string.Format("insert into ck(rq,tm,czy,sl,bz) values('{0}','{1}','{2}',{3},'{4}')",
                    DateTime.Now.ToString(), tm, f.worker.bh, this.textBox_sl.Text, this.yy);
                command.CommandText = s;
                command.ExecuteNonQuery();
                tr.Commit();
            }
            catch (Exception se)
            {
                tr.Rollback();
                MessageBox.Show(se.Message, "出错提示");
                return;
            }
            this.tm = "";
            this.textBox_tm.Clear();
            this.textBox_pm.Clear();
            this.textBox_jj.Clear();
            this.textBox_sj.Clear();
            this.textBox_sl.Clear();
            this.textBox_tm.ReadOnly = false;
            this.textBox_tm.Select();
        }

        private void radioButton_zy_Click(object sender, EventArgs e)
        {
            this.yy = this.radioButton_zy.Text;
        }

        private void radioButton_bs_Click(object sender, EventArgs e)
        {
            this.yy = this.radioButton_bs.Text;
        }

        private void radioButton_th_CheckedChanged(object sender, EventArgs e)
        {
            this.yy = this.radioButton_th.Text;
        }
    }
}
