namespace Server
{
    partial class Form_zp_ck
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_zp_ck));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox_sj = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_jj = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton_bs = new System.Windows.Forms.RadioButton();
            this.radioButton_zy = new System.Windows.Forms.RadioButton();
            this.radioButton_th = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox_sl = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_pm = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_tm = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox_sj);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox_jj);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.textBox_sl);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBox_pm);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox_tm);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(319, 283);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // textBox_sj
            // 
            this.textBox_sj.Location = new System.Drawing.Point(208, 102);
            this.textBox_sj.Name = "textBox_sj";
            this.textBox_sj.ReadOnly = true;
            this.textBox_sj.Size = new System.Drawing.Size(91, 21);
            this.textBox_sj.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(169, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 17;
            this.label4.Text = "售价：";
            // 
            // textBox_jj
            // 
            this.textBox_jj.Location = new System.Drawing.Point(56, 102);
            this.textBox_jj.Name = "textBox_jj";
            this.textBox_jj.ReadOnly = true;
            this.textBox_jj.Size = new System.Drawing.Size(91, 21);
            this.textBox_jj.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "进价：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton_bs);
            this.groupBox2.Controls.Add(this.radioButton_zy);
            this.groupBox2.Controls.Add(this.radioButton_th);
            this.groupBox2.Location = new System.Drawing.Point(19, 145);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(280, 71);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "退货原因";
            // 
            // radioButton_bs
            // 
            this.radioButton_bs.AutoSize = true;
            this.radioButton_bs.Location = new System.Drawing.Point(203, 33);
            this.radioButton_bs.Name = "radioButton_bs";
            this.radioButton_bs.Size = new System.Drawing.Size(47, 16);
            this.radioButton_bs.TabIndex = 2;
            this.radioButton_bs.Text = "报损";
            this.radioButton_bs.UseVisualStyleBackColor = true;
            this.radioButton_bs.Click += new System.EventHandler(this.radioButton_bs_Click);
            // 
            // radioButton_zy
            // 
            this.radioButton_zy.AutoSize = true;
            this.radioButton_zy.Location = new System.Drawing.Point(111, 33);
            this.radioButton_zy.Name = "radioButton_zy";
            this.radioButton_zy.Size = new System.Drawing.Size(47, 16);
            this.radioButton_zy.TabIndex = 1;
            this.radioButton_zy.Text = "退货";
            this.radioButton_zy.UseVisualStyleBackColor = true;
            this.radioButton_zy.Click += new System.EventHandler(this.radioButton_zy_Click);
            // 
            // radioButton_th
            // 
            this.radioButton_th.AutoSize = true;
            this.radioButton_th.Checked = true;
            this.radioButton_th.Location = new System.Drawing.Point(21, 33);
            this.radioButton_th.Name = "radioButton_th";
            this.radioButton_th.Size = new System.Drawing.Size(47, 16);
            this.radioButton_th.TabIndex = 0;
            this.radioButton_th.TabStop = true;
            this.radioButton_th.Text = "赠送";
            this.radioButton_th.UseVisualStyleBackColor = true;
            this.radioButton_th.CheckedChanged += new System.EventHandler(this.radioButton_th_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(213, 235);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 29);
            this.button1.TabIndex = 13;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox_sl
            // 
            this.textBox_sl.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_sl.Location = new System.Drawing.Point(82, 235);
            this.textBox_sl.Name = "textBox_sl";
            this.textBox_sl.Size = new System.Drawing.Size(94, 29);
            this.textBox_sl.TabIndex = 11;
            this.textBox_sl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_sl_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 243);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "退货数量：";
            // 
            // textBox_pm
            // 
            this.textBox_pm.Location = new System.Drawing.Point(56, 64);
            this.textBox_pm.Name = "textBox_pm";
            this.textBox_pm.ReadOnly = true;
            this.textBox_pm.Size = new System.Drawing.Size(243, 21);
            this.textBox_pm.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "品名：";
            // 
            // textBox_tm
            // 
            this.textBox_tm.Location = new System.Drawing.Point(56, 25);
            this.textBox_tm.Name = "textBox_tm";
            this.textBox_tm.Size = new System.Drawing.Size(243, 21);
            this.textBox_tm.TabIndex = 3;
            this.textBox_tm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_tm_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "条码：";
            // 
            // Form_zp_ck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 307);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_zp_ck";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "赠品出库";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox_sl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_pm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_tm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButton_bs;
        private System.Windows.Forms.RadioButton radioButton_zy;
        private System.Windows.Forms.RadioButton radioButton_th;
        private System.Windows.Forms.TextBox textBox_sj;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_jj;
        private System.Windows.Forms.Label label3;
    }
}