using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Server
{
    public partial class AddLB : Form
    {
        /// <summary>
        /// 商品分类：1=大类，2=中类，3=小类。
        /// </summary>        
        protected int lb;
        protected MySqlCommand command;

        public AddLB()
        {

        }
        public AddLB(int lb)
        {
            this.lb = lb;           
            this.command = Form_main.Command;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Icon = Properties.Resources.yuan;
            InitializeComponent();
            switch (lb)
            {
                case 1:
                    this.Text = "添加大类";
                    break;
                case 2:
                    this.Text = "添加中类";
                    break;
                case 3:
                    this.Text = "添加小类";
                    break;
                default:
                    break;
            }
            this.Icon = Properties.Resources.yuan;
        }

        public void Add()
        {
            string sql = string.Format("insert into fl(bh,pm,dnm) values({0},'{1}','{2}')",
                textBox1条码.Text,
                textBox2品名.Text.Trim(),
                this.textBox1条码.Text);
            command.CommandText = sql;
            if (command.ExecuteNonQuery() != 1)
            {
                MessageBox.Show("Error,AddLB.cs line 32");
                return;
            }
            MessageBox.Show(this, "添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.AddLB_Load(null, null);
        }

        private void button添加_Click(object sender, System.EventArgs e)
        {
            this.Add();
        }

        private void AddLB_Load(object sender, System.EventArgs e)
        {
            string sql = string.Format("select ifnull(max(bh),0)+1 from fl where char_length(dnm)={0}", lb * 2);
            command.CommandText = sql;
            int bh = int.Parse(command.ExecuteScalar().ToString());
            this.textBox1条码.Text = bh.ToString("00");
            this.textBox2品名.Clear();
            this.textBox2品名.Select();
        }

        private void textBox2品名_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                if (this.textBox2品名.TextLength > 0)
                    this.button添加.Select();
            }
        }
    }
}
