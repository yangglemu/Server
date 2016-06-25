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
    public partial class Form_DatePicker : Form
    {
        MySqlConnection conn;
        MySqlCommand comm;
        Worker _worker;
        string _start;
        string _end;
        public string start
        {
            get { return _start; }
            set { _start = value; }
        }


        public string end
        {
            get { return _end; }
            set { _end = value; }
        }

        public Worker worker
        {
            get { return _worker; }
            set { _worker = value; }
        }

        public Form_DatePicker()
        {
            InitializeComponent();

            this.Icon = Properties.Resources.yuan;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.dateTimePicker2.Value.Date >= this.dateTimePicker1.Value.Date)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("截止日期要大于等于起始日期");
            }
        }

        private void Form_ygjl_Load(object sender, EventArgs e)
        {
            conn = Form_main.Connection;
            comm = new MySqlCommand();
            comm.Connection = conn;
            comm.CommandText = "select bh,xm from worker";
            MySqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                Worker w = new Worker();
                w.bh = dr.GetString(0);
                w.xm = dr.GetString(1);
                this.comboBox1.Items.Add(w);
            }
            dr.Close();
            this.comboBox1.SelectedIndex = 0;
            this._start = this.dateTimePicker1.Value.ToShortDateString();
            this._end = this.dateTimePicker2.Value.ToShortDateString();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            this._start = this.dateTimePicker1.Value.ToShortDateString();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            this._end = this.dateTimePicker2.Value.ToShortDateString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._worker = this.comboBox1.Items[this.comboBox1.SelectedIndex] as Worker;
        }
    }
}
