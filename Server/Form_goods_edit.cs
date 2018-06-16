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
    public partial class Form_goods_edit : Form
    {
        MySqlCommand command;
        public Form_goods_edit()
        {
            InitializeComponent();

            command = Form_main.Command;

            Ghs zm = new Ghs("1001", "专卖");
            Ghs zy = new Ghs("1002", "自营");
            this.comboBox_ghs.Items.AddRange(new Ghs[] { zm, zy });
            this.comboBox_ghs.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!CheckTM())
                return;
            if (!CheckPM())
                return;
            if (!CheckJJ())
                return;
            if (!CheckSJ())
                return;
            if (!CheckGHS())
                return;
            if (this.comboBox_ghs.SelectedIndex < 0 || this.comboBox_ghs.SelectedIndex > 1)
            {
                MessageBox.Show("请从下拉框中选择供货商");
                this.comboBox_ghs.Select();
                return;
            }

            float jj = float.Parse(this.textBox_jj.Text.Trim());
            float sj = float.Parse(this.textBox_sj.Text.Trim());
            if (sj < jj)
            {
                DialogResult dr = MessageBox.Show("售价小于进价，这样会亏本甩卖，确定吗？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dr != DialogResult.OK)
                {
                    this.textBox_sj.Select();
                    this.textBox_sj.SelectAll();
                    return;
                }
            }
            string s = string.Format("update goods set pm='{0}',jj={1},sj={2},ghs='{3}' where tm='{4}'",
                this.textBox_pm.Text,
                this.textBox_jj.Text,
                this.textBox_sj.Text,
                (this.comboBox_ghs.Items[comboBox_ghs.SelectedIndex] as Ghs).bh,
                this.textBox_tm.Text);
            command.CommandText = s;
            command.ExecuteNonQuery();
            this.textBox_tm.ReadOnly = false;

            this.textBox_tm.Clear();
            this.textBox_pm.Clear();
            this.textBox_jj.Clear();
            this.textBox_sj.Clear();
        }


        private bool CheckTM()
        {
            if (this.textBox_tm.ReadOnly)
                return true;
            string s = this.textBox_tm.Text.Trim();
            if (s.Length > 0 && s.Length < 15)
            {
                command.CommandText = "select pm,jj,sj,ghs from goods where(tm='" + this.textBox_tm.Text + "')";
                MySqlDataReader dr = command.ExecuteReader();
                if (dr.Read())
                {
                    this.textBox_pm.Text = dr.GetString(0);
                    this.textBox_jj.Text = dr.GetString(1);
                    this.textBox_sj.Text = dr.GetString(2);
                    string ghs = dr.GetString(3);
                    if (ghs == "1001")
                        this.comboBox_ghs.SelectedIndex = 0;
                    else if (ghs == "1002")
                        this.comboBox_ghs.SelectedIndex = 1;
                    else
                        throw new Exception("1001/1002不可预知错误！");
                    this.textBox_tm.ReadOnly = true;
                    dr.Close();
                    return true;
                }
                else
                {
                    MessageBox.Show("无此商品！");
                    textBox_tm.Select();
                    textBox_tm.SelectAll();
                    dr.Close();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("条码输入应为一位以上数字！");
            }
            this.textBox_tm.Select();
            this.textBox_tm.SelectAll();
            return false;
        }
        private bool CheckPM()
        {
            string s = this.textBox_pm.Text.Trim();
            if (s.Length > 0)
                return true;
            MessageBox.Show("品名不能为空！");
            this.textBox_pm.Select();
            this.textBox_pm.SelectAll();
            return false;
        }
        private bool CheckJJ()
        {
            string s = this.textBox_jj.Text.Trim();
            if (s.Length > 0)
            {
                float f;
                if (float.TryParse(s, out f))
                {
                    if (f > 0)
                        return true;
                }
            }
            MessageBox.Show("进价输入应为大于 0 的数值！");
            this.textBox_jj.Select();
            this.textBox_jj.SelectAll();
            return false;
        }
        private bool CheckSJ()
        {
            string s = this.textBox_sj.Text.Trim();
            float f;
            if (float.TryParse(s, out f))
            {
                if (f > 0)
                    return true;
            }
            MessageBox.Show("售价输入应为大于 0 的数值！");
            this.textBox_sj.Select();
            this.textBox_sj.SelectAll();
            return false;
        }
        private bool CheckGHS()
        {
            if (this.comboBox_ghs.SelectedIndex < 0 || this.comboBox_ghs.SelectedIndex > 1)
            {
                MessageBox.Show("供货商只能在列表框中选择“专卖”和“自营”二者之一！");
                this.comboBox_ghs.Select();
                this.comboBox_ghs.SelectAll();
                return false;
            }
            return true;
        }
        private void textBox_tm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (CheckTM())
                {
                    this.textBox_pm.Select();
                }
            }
        }

        private void textBox_pm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    if (CheckPM())
                    {
                        this.textBox_jj.Select();
                        this.textBox_jj.SelectAll();
                    }
                    break;
                default:
                    break;
            }
        }

        private void textBox_jj_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    if (CheckJJ())
                    {
                        this.textBox_sj.Select();
                        this.textBox_sj.SelectAll();
                    }
                    break;
                default:
                    break;
            }
        }

        private void textBox_sj_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    if (CheckSJ())
                    {
                        this.comboBox_ghs.Select();
                        this.comboBox_ghs.SelectAll();
                    }
                    break;

                default:
                    break;
            }
        }

        private void comboBox_ghs_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    if (CheckGHS())
                    {
                        this.button1.Select();
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
