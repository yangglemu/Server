namespace Server
{
    partial class Form_jfcz
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_jfcz));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_hybh = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox_zj = new System.Windows.Forms.ComboBox();
            this.textBox_xm = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_czyy = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_jfzj = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_xyjf = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "会员编号：";
            // 
            // textBox_hybh
            // 
            this.textBox_hybh.Location = new System.Drawing.Point(78, 28);
            this.textBox_hybh.Name = "textBox_hybh";
            this.textBox_hybh.Size = new System.Drawing.Size(139, 21);
            this.textBox_hybh.TabIndex = 1;
            this.textBox_hybh.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_hybh_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox_zj);
            this.groupBox1.Controls.Add(this.textBox_xm);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBox_czyy);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox_jfzj);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox_xyjf);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox_hybh);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(24, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(237, 218);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // comboBox_zj
            // 
            this.comboBox_zj.FormattingEnabled = true;
            this.comboBox_zj.Items.AddRange(new object[] {
            "-",
            "+"});
            this.comboBox_zj.Location = new System.Drawing.Point(81, 139);
            this.comboBox_zj.Name = "comboBox_zj";
            this.comboBox_zj.Size = new System.Drawing.Size(34, 20);
            this.comboBox_zj.TabIndex = 10;
            // 
            // textBox_xm
            // 
            this.textBox_xm.Location = new System.Drawing.Point(78, 65);
            this.textBox_xm.Name = "textBox_xm";
            this.textBox_xm.ReadOnly = true;
            this.textBox_xm.Size = new System.Drawing.Size(139, 21);
            this.textBox_xm.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "会员姓名：";
            // 
            // textBox_czyy
            // 
            this.textBox_czyy.Location = new System.Drawing.Point(78, 176);
            this.textBox_czyy.Name = "textBox_czyy";
            this.textBox_czyy.Size = new System.Drawing.Size(139, 21);
            this.textBox_czyy.TabIndex = 7;
            this.textBox_czyy.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_czyy_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 180);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "操作原因：";
            // 
            // textBox_jfzj
            // 
            this.textBox_jfzj.Location = new System.Drawing.Point(121, 139);
            this.textBox_jfzj.Name = "textBox_jfzj";
            this.textBox_jfzj.Size = new System.Drawing.Size(96, 21);
            this.textBox_jfzj.TabIndex = 5;
            this.textBox_jfzj.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_jfzj_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "积分增减：";
            // 
            // textBox_xyjf
            // 
            this.textBox_xyjf.Location = new System.Drawing.Point(78, 102);
            this.textBox_xyjf.Name = "textBox_xyjf";
            this.textBox_xyjf.ReadOnly = true;
            this.textBox_xyjf.Size = new System.Drawing.Size(139, 21);
            this.textBox_xyjf.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "现有积分：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(105, 247);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 28);
            this.button1.TabIndex = 1;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form_jfcz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 291);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_jfcz";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "积分操作";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form_jfcz_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_hybh;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox_xyjf;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_czyy;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_jfzj;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox_xm;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox_zj;
    }
}