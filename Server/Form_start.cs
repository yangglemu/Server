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
    public partial class Form_start : Form
    {
        MySqlConnection conn;
        MySqlCommand comm;
        
        public Form_start()
        {
            InitializeComponent();
        }

        private void Form_start_Load(object sender, EventArgs e)
        {
            conn = Form_main.Connection;
            comm = new MySqlCommand();
            comm.Connection = conn;
            comm.CommandText = "select bh,xm,mm,qx from worker where `show`";
            MySqlDataReader dr = comm.ExecuteReader();
            while(dr.Read())
            {
                Worker w = new Worker();
                w.bh = dr.GetString(0);
                w.xm = dr.GetString(1);
                w.mm = dr.GetString(2);
                w.qx = dr.GetString(3);
                this.comboBox1.Items.Add(w);
            }
            dr.Close();
            if (this.comboBox1.Items.Count > 0)
                this.comboBox1.SelectedIndex = 0;
            else
            {
                MessageBox.Show("无管理员权限!!","警告",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                Application.Exit();
            }
            this.textBox1.Select();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Return:
                    if(this.comboBox1.SelectedIndex < 0)
                    {
                        MessageBox.Show("请从列表框中选择用户。");
                        this.textBox1.Clear();
                        this.comboBox1.SelectedIndex = 0;
                        this.comboBox1.Select();
                        return;
                    }                    

                    if (this.textBox1.Text.Trim().Length == 6)
                    {
                        Worker w = comboBox1.Items[comboBox1.SelectedIndex] as Worker;
                        if (w.mm == textBox1.Text.Trim())
                        {
                            if(w.qx != "低" && w.qx != "中" &&　w.qx != "高")
                            {
                                MessageBox.Show("权限错误！请从列表框中选择用户。");
                                return;
                            }
                            Form_main f = this.Owner as Form_main;
                            f.worker = w;
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }                    
                    this.textBox1.SelectAll();
                    break;

                case Keys.Escape:
                    this.DialogResult = DialogResult.Cancel;
                    Close();
                    break;

                default:
                    break;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.textBox1.Select();
        }
    }
}
