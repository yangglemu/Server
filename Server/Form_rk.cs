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
    public partial class Form_rk : Form
    {
        protected MySqlCommand command;

        public Form_rk()
        {
            InitializeComponent();
            command = Form_main.Command;
            this.textBox_tm.Select();
        }

        protected bool CheckTM(string s)
        {
            if (this.textBox_tm.ReadOnly)
            {
                return true;
            }
            if (s.Length < 4 && s.Length > 0)
            {
                int i;
                if (int.TryParse(s, out i))
                {
                    s = "010101" + i.ToString("000");
                    this.textBox_tm.Text = s;
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
                    this.textBox_tm.ReadOnly = true;
                    this.textBox_sl.Select();
                    dr.Close();
                    return true;
                }
                else
                {
                    dr.Close();
                    MessageBox.Show("无此商品！检查条码是否正确", "提示");
                    this.textBox_tm.SelectAll();
                    return false;
                }
            }
            MessageBox.Show("条码输入有误！其值为9位字符串");
            this.textBox_tm.Select();
            this.textBox_tm.SelectAll();
            return false;
        }

        protected bool CheckSL()
        {
            string s = this.textBox_sl.Text.Trim();
            if (s.Length > 0)
            {
                int sl;
                if (int.TryParse(s, out sl))
                {
                    if (sl > 0)
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("数量不能为负数或零！");
                        this.textBox_sl.Select();
                        this.textBox_sl.SelectAll();
                        return false;
                    }
                }
            }
            MessageBox.Show("数量输入有错！");
            this.textBox_sl.Select();
            this.textBox_sl.SelectAll();
            return false;
        }
        virtual protected string GetDatabaseName()
        {
            return "rk";
        }
        virtual protected void button1_Click(object sender, EventArgs e)
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
            MySqlTransaction tr = Form_main.Connection.BeginTransaction();
            string db = GetDatabaseName();
            string s;
            try
            {
                Form_main f = this.Owner as Form_main;
                if (db == "rk")//区分rk与rk_temp表
                {
                    s = string.Format("update goods set kc=kc+{0} where tm='{1}'",
                        this.textBox_sl.Text.Trim(), this.textBox_tm.Text.Trim());
                    command.CommandText = s;
                    command.ExecuteNonQuery();//更新库存
                }
                s = string.Format("insert into {0}(rq,tm,czy,sl) values('{1}','{2}','{3}',{4})",
                    db, DateTime.Now.ToString(), this.textBox_tm.Text, f.worker.bh, this.textBox_sl.Text);
                command.CommandText = s;
                command.ExecuteNonQuery();//添加入库操作记录
                tr.Commit();
            }
            catch (MySqlException se)
            {
                tr.Rollback();
                MessageBox.Show(se.Message, "出错提示");
                return;
            }
            this.textBox_tm.Clear();
            this.textBox_pm.Clear();
            this.textBox_jj.Clear();
            this.textBox_sj.Clear();
            this.textBox_sl.Clear();
            this.textBox_tm.ReadOnly = false;
            this.textBox_tm.Select();
        }

        private void textBox_tm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    if (CheckTM(textBox_tm.Text.Trim()))
                        this.textBox_sl.Select();
                    break;
                case Keys.Escape:
                    this.Close();
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
                        this.button1.Select();
                    break;
                case Keys.Escape:
                    Close();
                    break;

                default:
                    break;
            }
        }

        public bool add_rk_gj(string tm)
        {
            if (!CheckTM(tm))
            {
                MessageBox.Show("条码不存在！");
                this.Close();
                return false;
            }
            this.textBox_tm.Text = tm;
            return true;
        }
    }
}
