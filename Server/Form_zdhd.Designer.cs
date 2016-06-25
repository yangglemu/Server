namespace Server
{
    partial class Form_zdhd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_zdhd));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_kc = new System.Windows.Forms.TextBox();
            this.button_kc = new System.Windows.Forms.Button();
            this.button_pd = new System.Windows.Forms.Button();
            this.textBox_pd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.button_start_hd = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "库存文件";
            // 
            // textBox_kc
            // 
            this.textBox_kc.Location = new System.Drawing.Point(102, 35);
            this.textBox_kc.Name = "textBox_kc";
            this.textBox_kc.ReadOnly = true;
            this.textBox_kc.Size = new System.Drawing.Size(385, 21);
            this.textBox_kc.TabIndex = 1;
            // 
            // button_kc
            // 
            this.button_kc.Location = new System.Drawing.Point(502, 34);
            this.button_kc.Name = "button_kc";
            this.button_kc.Size = new System.Drawing.Size(67, 23);
            this.button_kc.TabIndex = 2;
            this.button_kc.Text = "打开";
            this.button_kc.UseVisualStyleBackColor = true;
            this.button_kc.Click += new System.EventHandler(this.button_kc_Click);
            // 
            // button_pd
            // 
            this.button_pd.Location = new System.Drawing.Point(502, 75);
            this.button_pd.Name = "button_pd";
            this.button_pd.Size = new System.Drawing.Size(67, 23);
            this.button_pd.TabIndex = 8;
            this.button_pd.Text = "打开";
            this.button_pd.UseVisualStyleBackColor = true;
            this.button_pd.Click += new System.EventHandler(this.button_pd_Click);
            // 
            // textBox_pd
            // 
            this.textBox_pd.Location = new System.Drawing.Point(102, 76);
            this.textBox_pd.Name = "textBox_pd";
            this.textBox_pd.ReadOnly = true;
            this.textBox_pd.Size = new System.Drawing.Size(385, 21);
            this.textBox_pd.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "盘点文件";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(13, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(577, 104);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 178);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(601, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Margin = new System.Windows.Forms.Padding(9, 3, 0, 2);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(546, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "就绪";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button_start_hd
            // 
            this.button_start_hd.Location = new System.Drawing.Point(263, 135);
            this.button_start_hd.Name = "button_start_hd";
            this.button_start_hd.Size = new System.Drawing.Size(75, 23);
            this.button_start_hd.TabIndex = 11;
            this.button_start_hd.Text = "开始核对";
            this.button_start_hd.UseVisualStyleBackColor = true;
            this.button_start_hd.Click += new System.EventHandler(this.button_start_hd_Click);
            // 
            // Form_zdhd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 200);
            this.Controls.Add(this.button_start_hd);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button_pd);
            this.Controls.Add(this.textBox_pd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button_kc);
            this.Controls.Add(this.textBox_kc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_zdhd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "自动核对";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_kc;
        private System.Windows.Forms.Button button_kc;
        private System.Windows.Forms.Button button_pd;
        private System.Windows.Forms.TextBox textBox_pd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button button_start_hd;
    }
}