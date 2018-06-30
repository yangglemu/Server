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
    public partial class Form_Input : Form
    {
        public string Input;
        public Form_Input()
        {
            InitializeComponent();
            this.textBox1.Select();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.TextLength < 1) return;
            this.Input = this.textBox1.Text.Trim().Replace("\r\n", "");
            this.DialogResult = DialogResult.OK;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape) this.button2_Click(null, null);
        }
    }
}
