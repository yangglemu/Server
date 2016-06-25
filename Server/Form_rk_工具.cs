using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Server
{
    public partial class Form_rk_工具 : Server.Form_rk
    {
        public Form_rk_工具()
        {            
            InitializeComponent();
            this.Icon = Properties.Resources.yuan; 
        }

        override protected string GetDatabaseName()
        {
            return "rk_temp";
        }        
    }
}
