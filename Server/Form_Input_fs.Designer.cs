namespace Server
{
    partial class Form_Input_fs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Input_fs));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1_副本份数 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBo_条码 = new System.Windows.Forms.TextBox();
            this.textBox1_售价 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_品名 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2_货号 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "份数：";
            // 
            // textBox1_副本份数
            // 
            this.textBox1_副本份数.Location = new System.Drawing.Point(74, 143);
            this.textBox1_副本份数.Name = "textBox1_副本份数";
            this.textBox1_副本份数.Size = new System.Drawing.Size(100, 21);
            this.textBox1_副本份数.TabIndex = 1;
            this.textBox1_副本份数.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBo_条码);
            this.groupBox1.Controls.Add(this.textBox1_售价);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox_品名);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox2_货号);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(14, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(177, 214);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // textBo_条码
            // 
            this.textBo_条码.Location = new System.Drawing.Point(60, 22);
            this.textBo_条码.Name = "textBo_条码";
            this.textBo_条码.ReadOnly = true;
            this.textBo_条码.Size = new System.Drawing.Size(100, 21);
            this.textBo_条码.TabIndex = 1;
            // 
            // textBox1_售价
            // 
            this.textBox1_售价.Location = new System.Drawing.Point(60, 99);
            this.textBox1_售价.Name = "textBox1_售价";
            this.textBox1_售价.ReadOnly = true;
            this.textBox1_售价.Size = new System.Drawing.Size(100, 21);
            this.textBox1_售价.TabIndex = 1;
            this.textBox1_售价.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "售价：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "条码：";
            // 
            // textBox_品名
            // 
            this.textBox_品名.Location = new System.Drawing.Point(60, 60);
            this.textBox_品名.Name = "textBox_品名";
            this.textBox_品名.ReadOnly = true;
            this.textBox_品名.Size = new System.Drawing.Size(100, 21);
            this.textBox_品名.TabIndex = 1;
            this.textBox_品名.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_货号_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "品名：";
            // 
            // textBox2_货号
            // 
            this.textBox2_货号.Location = new System.Drawing.Point(60, 177);
            this.textBox2_货号.Name = "textBox2_货号";
            this.textBox2_货号.Size = new System.Drawing.Size(100, 21);
            this.textBox2_货号.TabIndex = 1;
            this.textBox2_货号.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_货号_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 181);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "货号：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(65, 233);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 33);
            this.button1.TabIndex = 3;
            this.button1.Text = "打印";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form_Input_fs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(205, 281);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1_副本份数);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_Input_fs";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "打印条码";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1_副本份数;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox2_货号;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBo_条码;
        private System.Windows.Forms.TextBox textBox1_售价;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_品名;
        private System.Windows.Forms.Label label3;
    }
}