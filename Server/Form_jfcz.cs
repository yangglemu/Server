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
    public partial class Form_jfcz : Form
    {
        int xyjf;
        MySqlCommand command;
        public Form_jfcz()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.yuan;
            command = Form_main.Command;
        }

        private bool CheckSL()
        {
            string s = this.textBox_jfzj.Text.Trim();
            int f;
            if (int.TryParse(s, out f))
            {
                if (f > 0)
                    return true;
            }
            MessageBox.Show("数量应输入大于 0 的数值！");
            this.textBox_jfzj.Select();
            this.textBox_jfzj.SelectAll();
            return false;
        }
        private void textBox_hybh_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;

                case Keys.Return:
                    string tm = this.textBox_hybh.Text.Trim();
                    if (tm.Length == 7 || tm.Length == 8)
                    {
                        string s = "select xm,jf from people where bh='";
                        s += tm + "'";
                        command.CommandText = s;
                        MySqlDataReader dr = command.ExecuteReader();
                        if (dr.Read())
                        {
                            this.textBox_xm.Text = dr.GetString(0);
                            this.textBox_xyjf.Text = dr.GetString(1);
                            this.xyjf = dr.GetInt32(1);
                            this.textBox_hybh.ReadOnly = true;
                            this.textBox_jfzj.Select();
                            dr.Close();
                            return;
                        }
                        else
                        {
                            MessageBox.Show("无此会员！");
                            this.textBox_hybh.Select();
                            this.textBox_hybh.SelectAll();
                        }
                        dr.Close();
                        this.textBox_hybh.Select();
                        this.textBox_hybh.SelectAll();
                    }
                    else
                    {
                        MessageBox.Show("会员卡号为7或8位数字");
                        this.textBox_hybh.SelectAll();
                    }
                    break;

                default:
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!this.textBox_hybh.ReadOnly)
            {
                MessageBox.Show("输入会员卡号后直接按回车键，以便检查会员是否存在！", "提示");
                return;
            }
            if (!CheckSL())
                return;

            int zj = 0;
            string strjfzf = this.textBox_jfzj.Text.Trim();
            if (!int.TryParse(strjfzf, out zj) || zj == 0)
            {
                MessageBox.Show("积分增减需输入大于零的数字！");
                textBox_jfzj.Select();
                textBox_jfzj.SelectAll();
                return;
            }

            if (this.comboBox_zj.SelectedIndex < 0 || this.comboBox_zj.SelectedIndex > 1)
            {
                MessageBox.Show("增减处必须选择+号或者减号之一！");
                return;
            }

            if (this.textBox_czyy.Text.Trim().Length < 1)
            {
                MessageBox.Show("操作原因不能为空！");
                this.textBox_czyy.Select();
                return;
            }

            string strZJ = this.comboBox_zj.Items[this.comboBox_zj.SelectedIndex].ToString();
            if (strZJ != "+" && strZJ != "-")
            {
                MessageBox.Show("增减操作只能是+或者-！");
                return;
            }

            if (this.comboBox_zj.SelectedIndex == 0)
            {
                zj = -zj;
                if (int.Parse(this.textBox_xyjf.Text) + zj < 0)
                {
                    MessageBox.Show("这样操作的结果导致剩余积分为负数！", "出错");
                    this.textBox_jfzj.Select();
                    this.textBox_jfzj.SelectAll();
                    return;
                }
            }

            int syjf = this.xyjf + zj;

            MySqlTransaction tr = command.Connection.BeginTransaction();
            string s = "insert into jfcz(bh,cz,czjf,syjf,czyy,rq,czy) values('";
            s += textBox_hybh.Text + "', '";
            s += strZJ + "', '";
            s += zj.ToString() + "', '";
            s += syjf.ToString() + "', '";
            s += this.textBox_czyy.Text.Trim() + "', '";
            s += DateTime.Now.ToString() + "', '";
            s += (this.Owner as Form_main).worker.bh + "')";
            command.CommandText = s;
            try
            {

                command.ExecuteNonQuery();
                s = "update people set jf='";
                s += syjf.ToString() + "' ";
                s += "where bh='";
                s += this.textBox_hybh.Text + "'";
                command.CommandText = s;
                command.ExecuteNonQuery();
                tr.Commit();

                MessageBox.Show("积分操作成功！");
                this.textBox_hybh.Clear();
                this.textBox_xm.Clear();
                this.textBox_xyjf.Clear();
                this.textBox_czyy.Clear();
                this.textBox_jfzj.Clear();
                this.textBox_hybh.ReadOnly = false;
                this.comboBox_zj.SelectedIndex = 0;
                this.textBox_hybh.Select();
            }
            catch
            {
                tr.Rollback();
                MessageBox.Show("积分操作数据库操作失败！");
                return;
            }
        }

        private void textBox_czyy_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    if (this.textBox_czyy.Text.Trim().Length > 0)
                    {
                        this.button1.Select();
                    }
                    break;
            }
        }

        private void textBox_jfzj_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    if (this.textBox_jfzj.Text.Trim().Length > 0)
                        textBox_czyy.Select();
                    break;
            }
        }

        private void Form_jfcz_Load(object sender, EventArgs e)
        {
            this.comboBox_zj.SelectedIndex = 0;
        }
    }
}
