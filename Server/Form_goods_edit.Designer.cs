namespace Server
{
    partial class Form_goods_edit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_goods_edit));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_tm = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox_sj = new System.Windows.Forms.TextBox();
            this.comboBox_ghs = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_jj = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_pm = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "条码：";
            // 
            // textBox_tm
            // 
            this.textBox_tm.Location = new System.Drawing.Point(58, 25);
            this.textBox_tm.Name = "textBox_tm";
            this.textBox_tm.Size = new System.Drawing.Size(214, 21);
            this.textBox_tm.TabIndex = 1;
            this.textBox_tm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_tm_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox_sj);
            this.groupBox1.Controls.Add(this.comboBox_ghs);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox_jj);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox_pm);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox_tm);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(290, 184);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // textBox_sj
            // 
            this.textBox_sj.Location = new System.Drawing.Point(200, 105);
            this.textBox_sj.Name = "textBox_sj";
            this.textBox_sj.Size = new System.Drawing.Size(72, 21);
            this.textBox_sj.TabIndex = 4;
            this.textBox_sj.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_sj_KeyDown);
            // 
            // comboBox_ghs
            // 
            this.comboBox_ghs.FormattingEnabled = true;
            this.comboBox_ghs.Location = new System.Drawing.Point(58, 145);
            this.comboBox_ghs.Name = "comboBox_ghs";
            this.comboBox_ghs.Size = new System.Drawing.Size(214, 20);
            this.comboBox_ghs.TabIndex = 5;
            this.comboBox_ghs.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_ghs_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "供货商：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(160, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "售价：";
            // 
            // textBox_jj
            // 
            this.textBox_jj.Location = new System.Drawing.Point(58, 105);
            this.textBox_jj.Name = "textBox_jj";
            this.textBox_jj.Size = new System.Drawing.Size(72, 21);
            this.textBox_jj.TabIndex = 3;
            this.textBox_jj.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_jj_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "进价：";
            // 
            // textBox_pm
            // 
            this.textBox_pm.Location = new System.Drawing.Point(58, 65);
            this.textBox_pm.Name = "textBox_pm";
            this.textBox_pm.Size = new System.Drawing.Size(214, 21);
            this.textBox_pm.TabIndex = 2;
            this.textBox_pm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_pm_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "品名：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(120, 211);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 31);
            this.button1.TabIndex = 6;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form_goods_edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 258);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_goods_edit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "修改商品资料";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_tm;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox_sj;
        private System.Windows.Forms.ComboBox comboBox_ghs;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_jj;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_pm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
    }
}