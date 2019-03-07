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
    public partial class Form_NewGoods : Form
    {
        MySqlCommand command;
        string db;
        public Form_NewGoods(string db)
        {
            InitializeComponent();
            this.db = db;

            this.textBox_ptzq.Text = "1.0";
            this.textBox_hyzq.Text = "1.0";

            command = Form_main.Command;

            // Ghs zm = new Ghs("1001", "专卖");
            Ghs zy = new Ghs("1001", "自营");
            this.textBox_ghs.Items.AddRange(new Ghs[] { zy });
            this.textBox_ghs.SelectedIndex = 0;
        }

        private void textBox_pm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    if (CheckPM())
                    {
                        this.button1.Select();
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
                case Keys.R:
                    if (e.Control)
                    {
                        if (!CheckJJ()) break;
                        textBox_sj.Text = textBox_jj.Text;
                        textBox_sj_TextChanged(null, null);
                        var rk_gj = new Form_rk_工具();
                        if (rk_gj.add_rk_gj(textBox_tm.Text))
                            rk_gj.ShowDialog(this.Owner);
                        this.textBox_jj.Clear();
                        this.textBox_sj.Clear();
                        /*
                        var tm = this.textBox_tm.Text;
                        tm = tm.Remove(6);
                        tm += "XXX";
                        this.textBox_tm.Text = tm;
                         * */
                        rk_gj.Close();
                    }
                    break;
                case Keys.P:
                    if (!e.Control) break;
                    if (!CheckJJ()) break;
                    textBox_sj.Text = textBox_jj.Text;
                    textBox_sj_TextChanged(null, null);
                    var print = new Form_Print_BarCode();
                    if (print.add_print(textBox_tm.Text))
                        print.ShowDialog(this.Owner);
                    this.textBox_jj.Clear();
                    this.textBox_sj.Clear();
                    /*
                    var tm2 = this.textBox_tm.Text;
                    tm2 = tm2.Remove(6);
                    tm2 += "XXX";
                    this.textBox_tm.Text = tm2;
                     * */
                    print.Close();
                    break;
                case Keys.Escape:
                    Close();
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
                        //this.textBox_ptzq.Select();
                        //this.textBox_ptzq.SelectAll();
                        this.button1.Select();
                    }
                    break;
                case Keys.Escape:
                    this.Close();
                    break;

                default:
                    break;
            }
        }

        private void textBox_ptzq_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    if (CheckPTZQ())
                    {
                        this.textBox_hyzq.Select();
                        this.textBox_hyzq.SelectAll();
                    }
                    break;

                default:
                    break;
            }
        }

        private void textBox_hyzq_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    if (CheckHYZQ())
                    {
                        this.textBox_ghs.Select();
                        this.textBox_ghs.SelectAll();
                    }
                    break;

                default:
                    break;
            }
        }

        private void textBox_ghs_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    if (CheckGHS())
                    {
                        this.textBox_pm.Select();
                    }
                    break;

                default:
                    break;
            }
        }
        private bool CheckTM()
        {
            string s = this.textBox_tm.Text.Trim();
            if (s.Length >= 1 && s.Length <= 10)
            {
                foreach (char c in s)
                {
                    if (!char.IsNumber(c))
                    {
                        MessageBox.Show("条码不要输入0~9之外的字符！");
                        return false;
                    }
                }
                string ss = string.Format("select count(*) from {0} where tm='{1}'", db, s);
                command.CommandText = ss;
                int count = int.Parse(command.ExecuteScalar().ToString());
                if (count == 0)
                {
                    return true;
                }
                else if (count == 1)
                {
                    MessageBox.Show("此商品已存在！");
                    this.textBox_jj.Select();
                    this.textBox_jj.SelectAll();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("条码输入应为长度1-10位数字串！");
            }
            this.textBox_jj.Select();
            this.textBox_jj.SelectAll();
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
            if (s.Length < 1 || s.Length > 4)
            {
                MessageBox.Show("应输入1-4位数值！");
                return false;
            }
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
        private bool CheckPTZQ()
        {
            string s = this.textBox_ptzq.Text.Trim();
            float f;
            if (float.TryParse(s, out f))
            {
                if (f <= 1.0f && f >= 0.1f)
                {
                    return true;
                }
            }
            MessageBox.Show("折扣应为 0.1 到 1.0 之间的小数！");
            this.textBox_ptzq.Select();
            this.textBox_ptzq.SelectAll();
            return false;
        }
        private bool CheckHYZQ()
        {
            string s = this.textBox_hyzq.Text.Trim();
            float f;
            if (float.TryParse(s, out f))
            {
                if (f <= 1.0f && f >= 0.1f)
                {
                    return true;
                }
            }
            MessageBox.Show("会员折扣应为 0.1 到 1.0 之间的小数！");
            this.textBox_hyzq.Select();
            this.textBox_hyzq.SelectAll();
            return false;
        }

        private bool CheckGHS()
        {
            if (this.textBox_ghs.SelectedIndex < 0 || this.textBox_ghs.SelectedIndex > 1)
            {
                MessageBox.Show("供货商只能在列表框中选择“专卖”和“自营”二者之一！");
                this.textBox_ghs.Select();
                this.textBox_ghs.SelectAll();
                return false;
            }
            return true;
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
            if (!CheckPTZQ())
                return;
            if (!CheckHYZQ())
                return;
            if (!CheckGHS())
                return;

            string str = string.Format("insert into {0}(tm,pm,jj,sj,zq,hyzq,ghs) values(", db);
            str += "'" + this.textBox_tm.Text.Trim() + "',";//tm
            str += "'" + this.textBox_pm.Text.Trim() + "',";//pm
            str += this.textBox_jj.Text.Trim() + ",";//jj
            str += this.textBox_sj.Text.Trim() + ",";//sj
            str += this.textBox_ptzq.Text.Trim() + ",";//ptzq;
            str += this.textBox_hyzq.Text.Trim() + ",'";//hyzq
            Ghs ghs = this.textBox_ghs.SelectedItem as Ghs;
            str += ghs.bh + "')";//ghs
            command.CommandText = str;
            try
            {
                command.ExecuteNonQuery();
            }
            catch (MySqlException se)
            {
                MessageBox.Show("新增商品时发生错误，检查输入数据是否正确\r\n" + se.Message, "出错");
            }
            //this.textBox_tm.Clear();
            //this.textBox_pm.Clear();
            this.textBox_jj.Clear();
            this.textBox_sj.Clear();
            this.textBox_jj.Select();
        }

        private void Form_NewGoods_Load(object sender, EventArgs e)
        {
            this.SetCombox1();
        }

        private void SetCombox1()
        {
            string s = "select bh,pm,dnm from fl where char_length(dnm)=2 order by dnm asc";
            command.CommandText = s;
            MySqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                GoodsClass gc = new GoodsClass(dr.GetInt32(0), dr.GetString(1), dr.GetString(2));
                this.comboBox大类.Items.Add(gc);
            }
            dr.Close();
            if (this.comboBox大类.Items.Count > 0)
                this.comboBox大类.SelectedIndex = 0;
        }

        private void comboBox小类_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tm = this.textBox_tm.Text;
            tm = tm.Remove(0, 6);
            tm = (this.comboBox小类.SelectedItem as GoodsClass).dnm + tm;
            this.textBox_tm.Text = tm;
            this.textBox_jj.Select();
            GoodsClass g = this.comboBox小类.SelectedItem as GoodsClass;
            if (g == null)
                return;
            this.textBox_pm.Text = g.pm;
        }

        private void comboBox中类_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetCombox3();
        }
        private void comboBox大类_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetCombox2();
        }

        private void SetCombox2()
        {
            GoodsClass g = (this.comboBox大类.SelectedItem as GoodsClass);
            if (g == null)
                return;
            string s = string.Format("select bh,pm,dnm from fl where char_length(dnm)=4 and substring(dnm,1,2)='{0}' order by dnm asc",
                g.dnm);
            command.CommandText = s;
            MySqlDataReader dr = command.ExecuteReader();
            this.comboBox中类.Items.Clear();
            while (dr.Read())
            {
                GoodsClass gc = new GoodsClass(dr.GetInt32(0), dr.GetString(1), dr.GetString(2));
                this.comboBox中类.Items.Add(gc);
            }
            dr.Close();
            if (this.comboBox中类.Items.Count > 0)
                this.comboBox中类.SelectedIndex = 0;
        }

        private void SetCombox3()
        {
            GoodsClass g = (this.comboBox中类.SelectedItem as GoodsClass);
            if (g == null)
                return;
            string s = string.Format("select bh,pm,dnm from fl where char_length(dnm)=6 and substring(dnm,1,4)='{0}' order by dnm asc",
                g.dnm);
            command.CommandText = s;
            MySqlDataReader dr = command.ExecuteReader();
            this.comboBox小类.Items.Clear();
            while (dr.Read())
            {
                GoodsClass gc = new GoodsClass(dr.GetInt32(0), dr.GetString(1), dr.GetString(2));
                this.comboBox小类.Items.Add(gc);
            }
            dr.Close();
            if (this.comboBox小类.Items.Count > 0)
                this.comboBox小类.SelectedIndex = 0;
        }

        private void textBox_sj_TextChanged(object sender, EventArgs e)
        {
            /*
            if (this.textBox_tm.TextLength < 6)
                return;

            float f;
            string tm = this.textBox_tm.Text;
            tm = tm.Remove(6);
            if (float.TryParse(this.textBox_sj.Text.Trim(), out f))
            {
                tm += f.ToString("000");
            }
            else
            {
                tm += "XXX";
            }*/
            this.textBox_tm.Text = this.textBox_sj.Text.Trim();
        }
    }
}
