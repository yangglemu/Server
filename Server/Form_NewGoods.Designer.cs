namespace Server
{
    partial class Form_NewGoods
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_NewGoods));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_tm = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_pm = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_jj = new System.Windows.Forms.TextBox();
            this.textBox_sj = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_ptzq = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_hyzq = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox小类 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBox中类 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBox大类 = new System.Windows.Forms.ComboBox();
            this.textBox_ghs = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(338, 158);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "条码：";
            // 
            // textBox_tm
            // 
            this.textBox_tm.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_tm.Location = new System.Drawing.Point(381, 149);
            this.textBox_tm.Name = "textBox_tm";
            this.textBox_tm.ReadOnly = true;
            this.textBox_tm.Size = new System.Drawing.Size(215, 29);
            this.textBox_tm.TabIndex = 1;
            this.textBox_tm.Text = "XXXXXXXXX";
            this.textBox_tm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 159);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "品名：";
            // 
            // textBox_pm
            // 
            this.textBox_pm.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_pm.Location = new System.Drawing.Point(53, 151);
            this.textBox_pm.Name = "textBox_pm";
            this.textBox_pm.Size = new System.Drawing.Size(246, 29);
            this.textBox_pm.TabIndex = 2;
            this.textBox_pm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_pm_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "进价：";
            // 
            // textBox_jj
            // 
            this.textBox_jj.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_jj.Location = new System.Drawing.Point(53, 78);
            this.textBox_jj.Name = "textBox_jj";
            this.textBox_jj.Size = new System.Drawing.Size(61, 29);
            this.textBox_jj.TabIndex = 3;
            this.textBox_jj.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_jj_KeyDown);
            // 
            // textBox_sj
            // 
            this.textBox_sj.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_sj.Location = new System.Drawing.Point(163, 78);
            this.textBox_sj.Name = "textBox_sj";
            this.textBox_sj.Size = new System.Drawing.Size(61, 29);
            this.textBox_sj.TabIndex = 4;
            this.textBox_sj.TextChanged += new System.EventHandler(this.textBox_sj_TextChanged);
            this.textBox_sj.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_sj_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(127, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "售价：";
            // 
            // textBox_ptzq
            // 
            this.textBox_ptzq.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_ptzq.Location = new System.Drawing.Point(299, 78);
            this.textBox_ptzq.Name = "textBox_ptzq";
            this.textBox_ptzq.Size = new System.Drawing.Size(49, 29);
            this.textBox_ptzq.TabIndex = 5;
            this.textBox_ptzq.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_ptzq_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(238, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "普通折扣：";
            // 
            // textBox_hyzq
            // 
            this.textBox_hyzq.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_hyzq.Location = new System.Drawing.Point(424, 78);
            this.textBox_hyzq.Name = "textBox_hyzq";
            this.textBox_hyzq.Size = new System.Drawing.Size(49, 29);
            this.textBox_hyzq.TabIndex = 6;
            this.textBox_hyzq.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_hyzq_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(362, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "会员折扣：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(484, 87);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "供货商：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(279, 225);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 39);
            this.button1.TabIndex = 8;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox小类);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.textBox_pm);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.textBox_tm);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox_hyzq);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.textBox_ptzq);
            this.groupBox1.Controls.Add(this.textBox_sj);
            this.groupBox1.Controls.Add(this.comboBox中类);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.comboBox大类);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox_ghs);
            this.groupBox1.Controls.Add(this.textBox_jj);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(23, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(616, 196);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // comboBox小类
            // 
            this.comboBox小类.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox小类.FormattingEnabled = true;
            this.comboBox小类.Location = new System.Drawing.Point(437, 20);
            this.comboBox小类.MaxDropDownItems = 100;
            this.comboBox小类.Name = "comboBox小类";
            this.comboBox小类.Size = new System.Drawing.Size(159, 20);
            this.comboBox小类.TabIndex = 6;
            this.comboBox小类.SelectedIndexChanged += new System.EventHandler(this.comboBox小类_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(395, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 7;
            this.label10.Text = "小类：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(206, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 5;
            this.label9.Text = "中类：";
            // 
            // comboBox中类
            // 
            this.comboBox中类.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox中类.FormattingEnabled = true;
            this.comboBox中类.Location = new System.Drawing.Point(249, 20);
            this.comboBox中类.MaxDropDownItems = 100;
            this.comboBox中类.Name = "comboBox中类";
            this.comboBox中类.Size = new System.Drawing.Size(121, 20);
            this.comboBox中类.TabIndex = 4;
            this.comboBox中类.SelectedIndexChanged += new System.EventHandler(this.comboBox中类_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 3;
            this.label8.Text = "大类：";
            // 
            // comboBox大类
            // 
            this.comboBox大类.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox大类.FormattingEnabled = true;
            this.comboBox大类.Location = new System.Drawing.Point(53, 20);
            this.comboBox大类.MaxDropDownItems = 100;
            this.comboBox大类.Name = "comboBox大类";
            this.comboBox大类.Size = new System.Drawing.Size(121, 20);
            this.comboBox大类.TabIndex = 2;
            this.comboBox大类.SelectedIndexChanged += new System.EventHandler(this.comboBox大类_SelectedIndexChanged);
            // 
            // textBox_ghs
            // 
            this.textBox_ghs.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_ghs.FormattingEnabled = true;
            this.textBox_ghs.ItemHeight = 14;
            this.textBox_ghs.Location = new System.Drawing.Point(540, 82);
            this.textBox_ghs.Name = "textBox_ghs";
            this.textBox_ghs.Size = new System.Drawing.Size(58, 22);
            this.textBox_ghs.TabIndex = 1;
            this.textBox_ghs.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_ghs_KeyDown);
            // 
            // Form_NewGoods
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 283);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_NewGoods";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新增商品";
            this.Load += new System.EventHandler(this.Form_NewGoods_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_tm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_pm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_jj;
        private System.Windows.Forms.TextBox textBox_sj;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_ptzq;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_hyzq;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox textBox_ghs;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBox小类;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBox中类;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBox大类;
    }
}