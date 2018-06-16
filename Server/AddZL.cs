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
    public partial class AddZL : Form
    {
        int bh;
        protected MySqlCommand command;
        public AddZL()
        {
            InitializeComponent();
            this.command = Form_main.Command;
        }

        protected void AddZL_Load(object sender, EventArgs e)
        {
            //bh编号(max)、pm品名、dnm店内码(2位大类、4位中类、6位小类)
            command.CommandText = "select bh,pm,dnm from fl where char_length(dnm)=2 order by pm asc";
            MySqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                GoodsClass gc = new GoodsClass(dr.GetInt32(0),
                    dr.GetString(1),
                    dr.GetString(2));
                this.comboBox1.Items.Add(gc);
            }
            dr.Close();
            if (this.comboBox1.Items.Count == 0)
                return;
            this.comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = string.Format("insert into fl(bh,pm,dnm) values({0},'{1}','{2}')",
                this.bh,
                this.textBox2品名.Text.Trim(),
                this.textBox1中类.Text);
            command.CommandText = sql;
            if (command.ExecuteNonQuery() != 1)
            {
                MessageBox.Show("Error,AddZL.cs 54 line.");
                return;
            }
            MessageBox.Show("添加中类成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.comboBox1_SelectedIndexChanged(null, null);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox1.Items.Count == 0)
                return;
            GoodsClass g = this.comboBox1.SelectedItem as GoodsClass;
            command.CommandText = string.Format("select ifnull(max(bh),0)+1 from fl where char_length(dnm)=4 and substring(dnm,1,2)='{0}'",
                g.dnm);
            bh = int.Parse(command.ExecuteScalar().ToString());
            this.textBox1中类.Text = g.dnm + bh.ToString("00");
            this.textBox2品名.Text = g.pm;
            this.textBox2品名.Select();
            this.textBox2品名.SelectionStart = this.textBox2品名.TextLength;
        }

        private void textBox2品名_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Return)
            {
                if (this.textBox2品名.TextLength > 0)
                    this.button1.Select();
            }
        }
    }
}
