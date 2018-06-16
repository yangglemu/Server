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
    public partial class Form_rk_auto : Form
    {
        protected MySqlCommand command;

        public Form_rk_auto()
        {
            InitializeComponent();
            command = Form_main.Command;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridView1.ColumnHeadersHeight = 31;
            dataGridView1.Columns.Add("tm", "条码");
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns.Add("pm","品名");
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns.Add("sj","售价");
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns.Add("sl","数量");
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns.Add("zt","状态");
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ScrollBars = ScrollBars.None;
            this.textBox_tm.Select();

        }

        protected bool CheckTM()
        {
            string s = this.textBox_tm.Text.Trim();
            if (s.Length > 0 && s.Length < 15)
            {
                command.CommandText = "select pm,jj,sj from goods where tm='" + s + "'";
                MySqlDataReader dr = command.ExecuteReader();
                if (dr.Read())
                {
                    this.textBox_pm.Text = dr.GetString(0);
                    this.textBox_jj.Text = dr.GetFloat(1).ToString("N2");
                    this.textBox_sj.Text = dr.GetFloat(2).ToString("N2");
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
            MessageBox.Show("条码输入有误！其值应为9位字符串");
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
        protected void button1_Click(object sender, EventArgs e)
        {
            if (!CheckTM())
            {
                this.ShowError();
                return;
            }

            if (!CheckSL())
            {
                this.ShowError();
                return;
            }

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
                    this.GetDatabaseName(),
                    DateTime.Now.ToString(), this.textBox_tm.Text, f.worker.bh, this.textBox_sl.Text);
                command.CommandText = s;
                command.ExecuteNonQuery();//添加入库操作记录
                tr.Commit();
            }
            catch (Exception se)
            {
                tr.Rollback();
                MessageBox.Show(se.Message, "出错提示");
                this.ShowError();
                return;
            }
            string label = string.Format("本次：【{0}】,【{1}】,【{2}】件)。",
                this.textBox_tm.Text.Trim(),
                this.textBox_pm.Text.Trim(),
                this.textBox_sl.Text.Trim());
            this.ShowMessage();
            this.textBox_tm.Clear();
            this.textBox_pm.Clear();
            this.textBox_jj.Clear();
            this.textBox_sj.Clear();
            this.textBox_sl.Text = "1";
            this.textBox_tm.Select();
        }

        private void textBox_tm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                //this.textBox_sl.Select();
                //this.textBox_sl.SelectAll();
                this.button1_Click(null, null);
            }
            else if(e.KeyCode==Keys.Add)
            {
                int m;
                if(int.TryParse(this.textBox_sl.Text.Trim(),out m))
                {
                    ++m;
                    this.textBox_sl.Text = (m).ToString();
                }
                e.SuppressKeyPress = true;
            }
            else if(e.KeyCode==Keys.Subtract)
            {
                int m;
                if (int.TryParse(this.textBox_sl.Text.Trim(), out m))
                {
                    --m;
                    if (m < 1)
                        m = 1;
                    this.textBox_sl.Text = (m).ToString();
                }
                e.SuppressKeyPress = true;
            }
        }

        private void textBox_sl_KeyDown(object sender, KeyEventArgs e)
        {
            /*
            switch (e.KeyCode)
            {
                case Keys.Return:
                    if (CheckSL())
                        this.button1.Select();
                    break;

                default:
                    break;
            }
             * */
        }

        private void Form_rk_auto_Load(object sender, EventArgs e)
        {
            this.textBox_sl.Text = "1";
        }
        private void ShowError()
        {
            if (this.dataGridView1.Rows.Count > 5)
            {
                this.dataGridView1.Rows.Remove(dataGridView1.Rows[0]);
            }
            int index = dataGridView1.Rows.Add();
            DataGridViewRow row = dataGridView1.Rows[index];
            row.DefaultCellStyle.ForeColor = Color.Red;
            row.Cells[0].Value = this.textBox_tm.Text.Trim();
            row.Cells[1].Value = this.textBox_pm.Text;
            row.Cells[2].Value = this.textBox_sj.Text;
            row.Cells[3].Value = this.textBox_sl.Text.Trim();
            row.Cells[4].Value = "Err";
            dataGridView1.ClearSelection();
            this.textBox_tm.Select();
        }
        private void ShowMessage()
        {
            if (this.dataGridView1.Rows.Count > 5)
            {
                this.dataGridView1.Rows.Remove(dataGridView1.Rows[0]);
            }
            int index = dataGridView1.Rows.Add();
            DataGridViewRow row = dataGridView1.Rows[index];
            row.Cells[0].Value = this.textBox_tm.Text.Trim();
            row.Cells[1].Value = this.textBox_pm.Text;
            row.Cells[2].Value = this.textBox_sj.Text;
            row.Cells[3].Value = this.textBox_sl.Text.Trim();
            row.Cells[4].Value = "OK";
            dataGridView1.ClearSelection();
            this.textBox_tm.Select();
        }
    }
}
