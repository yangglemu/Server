namespace Server
{
    partial class AddLB
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
            this.textBox1条码 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2品名 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button添加 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1条码
            // 
            this.textBox1条码.Location = new System.Drawing.Point(75, 29);
            this.textBox1条码.Name = "textBox1条码";
            this.textBox1条码.ReadOnly = true;
            this.textBox1条码.Size = new System.Drawing.Size(218, 21);
            this.textBox1条码.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "编码：";
            // 
            // textBox2品名
            // 
            this.textBox2品名.Location = new System.Drawing.Point(75, 76);
            this.textBox2品名.Name = "textBox2品名";
            this.textBox2品名.Size = new System.Drawing.Size(218, 21);
            this.textBox2品名.TabIndex = 0;
            this.textBox2品名.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2品名_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "品名：";
            // 
            // button添加
            // 
            this.button添加.Location = new System.Drawing.Point(129, 150);
            this.button添加.Name = "button添加";
            this.button添加.Size = new System.Drawing.Size(75, 32);
            this.button添加.TabIndex = 2;
            this.button添加.Text = "添加";
            this.button添加.UseVisualStyleBackColor = true;
            this.button添加.Click += new System.EventHandler(this.button添加_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(308, 118);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // AddLB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 207);
            this.Controls.Add(this.textBox2品名);
            this.Controls.Add(this.textBox1条码);
            this.Controls.Add(this.button添加);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddLB";
            this.ShowInTaskbar = false;
            this.Text = "添加类别";
            this.Load += new System.EventHandler(this.AddLB_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.TextBox textBox1条码;
        protected System.Windows.Forms.Label label1;
        protected System.Windows.Forms.TextBox textBox2品名;
        protected System.Windows.Forms.Label label2;
        protected System.Windows.Forms.Button button添加;
        protected System.Windows.Forms.GroupBox groupBox1;
    }
}