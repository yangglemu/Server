namespace Server
{
    partial class Form_Print_BarCode
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Print_BarCode));
            this.button1_print = new System.Windows.Forms.Button();
            this.textBox1_tm = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1_hh = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox4_fs = new System.Windows.Forms.TextBox();
            this.textBox3_dj = new System.Windows.Forms.TextBox();
            this.textBox_pm = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1_print
            // 
            this.button1_print.Location = new System.Drawing.Point(105, 238);
            this.button1_print.Name = "button1_print";
            this.button1_print.Size = new System.Drawing.Size(75, 32);
            this.button1_print.TabIndex = 0;
            this.button1_print.Text = "打印";
            this.button1_print.UseVisualStyleBackColor = true;
            this.button1_print.Click += new System.EventHandler(this.button1_print_Click);
            this.button1_print.KeyDown += new System.Windows.Forms.KeyEventHandler(this.button1_print_KeyDown);
            // 
            // textBox1_tm
            // 
            this.textBox1_tm.Location = new System.Drawing.Point(84, 33);
            this.textBox1_tm.Name = "textBox1_tm";
            this.textBox1_tm.Size = new System.Drawing.Size(163, 21);
            this.textBox1_tm.TabIndex = 1;
            this.textBox1_tm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "条码：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox1_hh);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox4_fs);
            this.groupBox1.Controls.Add(this.textBox3_dj);
            this.groupBox1.Controls.Add(this.textBox_pm);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(259, 211);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "货号：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "份数：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "售价：";
            // 
            // textBox1_hh
            // 
            this.textBox1_hh.Location = new System.Drawing.Point(71, 173);
            this.textBox1_hh.Name = "textBox1_hh";
            this.textBox1_hh.Size = new System.Drawing.Size(163, 21);
            this.textBox1_hh.TabIndex = 1;
            this.textBox1_hh.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_hh_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "品名：";
            // 
            // textBox4_fs
            // 
            this.textBox4_fs.Location = new System.Drawing.Point(71, 134);
            this.textBox4_fs.Name = "textBox4_fs";
            this.textBox4_fs.Size = new System.Drawing.Size(163, 21);
            this.textBox4_fs.TabIndex = 1;
            this.textBox4_fs.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox4_fs_KeyDown);
            // 
            // textBox3_dj
            // 
            this.textBox3_dj.Location = new System.Drawing.Point(71, 97);
            this.textBox3_dj.Name = "textBox3_dj";
            this.textBox3_dj.ReadOnly = true;
            this.textBox3_dj.Size = new System.Drawing.Size(163, 21);
            this.textBox3_dj.TabIndex = 1;
            // 
            // textBox_pm
            // 
            this.textBox_pm.Location = new System.Drawing.Point(71, 59);
            this.textBox_pm.Name = "textBox_pm";
            this.textBox_pm.ReadOnly = true;
            this.textBox_pm.Size = new System.Drawing.Size(163, 21);
            this.textBox_pm.TabIndex = 1;
            // 
            // Form_Print_BarCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 286);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1_tm);
            this.Controls.Add(this.button1_print);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_Print_BarCode";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "打印条码";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Print_BarCode_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1_print;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox4_fs;
        private System.Windows.Forms.TextBox textBox3_dj;
        private System.Windows.Forms.TextBox textBox_pm;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox1_hh;
        public System.Windows.Forms.TextBox textBox1_tm;
    }
}