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
    public partial class Link_Form : Form
    {
        public Link_Form()
        {
            InitializeComponent();
            this.label1.Parent = this.pictureBox1;
        }        
    }
}
