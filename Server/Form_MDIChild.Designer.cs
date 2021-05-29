namespace Server
{
    partial class Form_MDIChild
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_MDIChild));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this._toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除本行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1_print = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_删除此行 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_编辑此行 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_print = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_打印整张表 = new System.Windows.Forms.ToolStripMenuItem();
            this.Delete撤回此入库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.撤回此出库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_备注 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打印此单ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1_print.SuspendLayout();
            this.contextMenuStrip_备注.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 340);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(834, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // _toolStripStatusLabel1
            // 
            this._toolStripStatusLabel1.Margin = new System.Windows.Forms.Padding(10, 3, 0, 2);
            this._toolStripStatusLabel1.Name = "_toolStripStatusLabel1";
            this._toolStripStatusLabel1.Size = new System.Drawing.Size(809, 17);
            this._toolStripStatusLabel1.Spring = true;
            this._toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            this._toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除本行ToolStripMenuItem,
            this.打印此单ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(169, 102);
            // 
            // 删除本行ToolStripMenuItem
            // 
            this.删除本行ToolStripMenuItem.Image = global::Server.Properties.Resources.Delete;
            this.删除本行ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.删除本行ToolStripMenuItem.Name = "删除本行ToolStripMenuItem";
            this.删除本行ToolStripMenuItem.Size = new System.Drawing.Size(168, 38);
            this.删除本行ToolStripMenuItem.Text = "删除本单";
            this.删除本行ToolStripMenuItem.Click += new System.EventHandler(this.mi_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView1.ColumnHeadersHeight = 35;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView1.RowHeadersWidth = 80;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridView1.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(834, 340);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.dataGridView1_CellContextMenuStripNeeded);
            this.dataGridView1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            this.dataGridView1.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView1_RowsRemoved);
            this.dataGridView1.Sorted += new System.EventHandler(this.dataGridView1_Sorted);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            this.dataGridView1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dataGridView1_KeyPress);
            // 
            // contextMenuStrip1_print
            // 
            this.contextMenuStrip1_print.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_删除此行,
            this.toolStripMenuItem_编辑此行,
            this.toolStripMenuItem_print,
            this.toolStripMenuItem_打印整张表,
            this.Delete撤回此入库ToolStripMenuItem,
            this.撤回此出库ToolStripMenuItem});
            this.contextMenuStrip1_print.Name = "contextMenuStrip1_print";
            this.contextMenuStrip1_print.Size = new System.Drawing.Size(169, 328);
            // 
            // toolStripMenuItem_删除此行
            // 
            this.toolStripMenuItem_删除此行.Image = global::Server.Properties.Resources.Eraser;
            this.toolStripMenuItem_删除此行.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem_删除此行.Name = "toolStripMenuItem_删除此行";
            this.toolStripMenuItem_删除此行.Size = new System.Drawing.Size(168, 54);
            this.toolStripMenuItem_删除此行.Text = "删除此行";
            this.toolStripMenuItem_删除此行.Visible = false;
            this.toolStripMenuItem_删除此行.Click += new System.EventHandler(this.toolStripMenuItem_删除此行_Click);
            // 
            // toolStripMenuItem_编辑此行
            // 
            this.toolStripMenuItem_编辑此行.Image = global::Server.Properties.Resources.Graphic_designer;
            this.toolStripMenuItem_编辑此行.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem_编辑此行.Name = "toolStripMenuItem_编辑此行";
            this.toolStripMenuItem_编辑此行.Size = new System.Drawing.Size(168, 54);
            this.toolStripMenuItem_编辑此行.Text = "编辑此行";
            this.toolStripMenuItem_编辑此行.Visible = false;
            this.toolStripMenuItem_编辑此行.Click += new System.EventHandler(this.toolStripMenuItem_编辑此行_Click);
            // 
            // toolStripMenuItem_print
            // 
            this.toolStripMenuItem_print.Image = global::Server.Properties.Resources.printer;
            this.toolStripMenuItem_print.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem_print.Name = "toolStripMenuItem_print";
            this.toolStripMenuItem_print.Size = new System.Drawing.Size(168, 54);
            this.toolStripMenuItem_print.Text = "打印此条码";
            this.toolStripMenuItem_print.ToolTipText = "打印本条记录条码";
            this.toolStripMenuItem_print.Click += new System.EventHandler(this.print_Click);
            // 
            // toolStripMenuItem_打印整张表
            // 
            this.toolStripMenuItem_打印整张表.Image = global::Server.Properties.Resources.ooopic_1458911634;
            this.toolStripMenuItem_打印整张表.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem_打印整张表.Name = "toolStripMenuItem_打印整张表";
            this.toolStripMenuItem_打印整张表.Size = new System.Drawing.Size(168, 54);
            this.toolStripMenuItem_打印整张表.Text = "打印整张表";
            this.toolStripMenuItem_打印整张表.Visible = false;
            this.toolStripMenuItem_打印整张表.Click += new System.EventHandler(this.toolStripMenuItem_打印整张表_Click);
            // 
            // Delete撤回此入库ToolStripMenuItem
            // 
            this.Delete撤回此入库ToolStripMenuItem.Image = global::Server.Properties.Resources.Diagram;
            this.Delete撤回此入库ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Delete撤回此入库ToolStripMenuItem.Name = "Delete撤回此入库ToolStripMenuItem";
            this.Delete撤回此入库ToolStripMenuItem.Size = new System.Drawing.Size(168, 54);
            this.Delete撤回此入库ToolStripMenuItem.Text = "撤回此入库";
            this.Delete撤回此入库ToolStripMenuItem.Click += new System.EventHandler(this.撤回此入库ToolStripMenuItem_Click);
            // 
            // 撤回此出库ToolStripMenuItem
            // 
            this.撤回此出库ToolStripMenuItem.Image = global::Server.Properties.Resources.Synchronize;
            this.撤回此出库ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.撤回此出库ToolStripMenuItem.Name = "撤回此出库ToolStripMenuItem";
            this.撤回此出库ToolStripMenuItem.Size = new System.Drawing.Size(168, 54);
            this.撤回此出库ToolStripMenuItem.Text = "撤回此出库";
            this.撤回此出库ToolStripMenuItem.Click += new System.EventHandler(this.撤回此出库ToolStripMenuItem_Click);
            // 
            // contextMenuStrip_备注
            // 
            this.contextMenuStrip_备注.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除ToolStripMenuItem,
            this.编辑ToolStripMenuItem});
            this.contextMenuStrip_备注.Name = "contextMenuStrip_备注";
            this.contextMenuStrip_备注.Size = new System.Drawing.Size(117, 80);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Image = global::Server.Properties.Resources.Erase;
            this.删除ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(116, 38);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除备注ToolStripMenuItem_Click);
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.Image = global::Server.Properties.Resources.Feather;
            this.编辑ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(116, 38);
            this.编辑ToolStripMenuItem.Text = "编辑";
            this.编辑ToolStripMenuItem.Click += new System.EventHandler(this.编辑备注ToolStripMenuItem_Click);
            // 
            // 打印此单ToolStripMenuItem
            // 
            this.打印此单ToolStripMenuItem.Image = global::Server.Properties.Resources.printer;
            this.打印此单ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.打印此单ToolStripMenuItem.Name = "打印此单ToolStripMenuItem";
            this.打印此单ToolStripMenuItem.Size = new System.Drawing.Size(168, 38);
            this.打印此单ToolStripMenuItem.Text = "打印此单";
            this.打印此单ToolStripMenuItem.Click += new System.EventHandler(this.打印此单ToolStripMenuItem_Click);
            // 
            // Form_MDIChild
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 362);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_MDIChild";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Activated += new System.EventHandler(this.Form_MDIChild_Activated);
            this.Deactivate += new System.EventHandler(this.Form_MDIChild_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_MDIChild_FormClosing);
            this.Shown += new System.EventHandler(this.Form_MDIChild_Shown);
            this.Enter += new System.EventHandler(this.Form_MDIChild_Enter);
            this.Leave += new System.EventHandler(this.Form_MDIChild_Leave);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1_print.ResumeLayout(false);
            this.contextMenuStrip_备注.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel _toolStripStatusLabel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 删除本行ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_print;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1_print;
        private System.Windows.Forms.ToolStripMenuItem Delete撤回此入库ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_打印整张表;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_删除此行;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_编辑此行;
        private System.Windows.Forms.ToolStripMenuItem 撤回此出库ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_备注;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打印此单ToolStripMenuItem;
    }
}