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
    public partial class Form_Input_fs : Form
    {
        public class PrintData
        {
            public string tm;
            public string pm;
            public string sj;
            public string hh;
            public string fs;
        }
        private PrintData _printdata;

        public PrintData Printdata
        {
            get { return _printdata; }
            set { _printdata = value; }
        }
        public Form_Input_fs(string tm, string pm, string sj)
        {
            InitializeComponent();
            this.textBox1_副本份数.Select();
            this.textBox1_副本份数.SelectAll();
            _printdata = new PrintData();
            _printdata.tm = this.textBo_条码.Text = tm;
            _printdata.pm = this.textBox_品名.Text = pm;
            _printdata.sj = this.textBox1_售价.Text = sj;
            _printdata.fs = "1";
            _printdata.hh = "";
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!CheckFS())
            {
                return;
            }
            if (e != null && e.KeyCode == Keys.Return)
            {
                this.textBox2_货号.Select();
            }
        }

        private bool CheckFS()
        {
            int m = 0;
            int.TryParse(this.textBox1_副本份数.Text.Trim(), out m);
            if (m < 1)
            {
                this.textBox1_副本份数.Select();
                return false;
            }
            if (m > 29)
            {
                if (MessageBox.Show("打印副本过多，是否继续？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                {
                    this.textBox1_副本份数.Clear();
                    this.textBox1_副本份数.Select();
                    this.textBox1_副本份数.SelectAll();
                    return false;
                }
            }
            Printdata.fs = m.ToString();
            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!CheckFS())
                return;
            this.Printdata.hh = this.textBox2_货号.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void textBox2_货号_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                this.button1.Select();
        }
    }
}
