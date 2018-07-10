using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Form_SelectDate : Form
    {
        public Form_SelectDate()
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
    }
}
