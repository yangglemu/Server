using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Seagull.BarTender.Print;

namespace Server
{
    public partial class Form_MDIChild : Form
    {
        string _sql;
        DataTable myTable;
        bool isFinded;
        public List<string> items;
        public DataGridView dataGridView
        {
            get { return dataGridView1; }
        }
        public ToolStripStatusLabel toolStripStatusLabel1
        {
            get { return this._toolStripStatusLabel1; }
        }

        public Form_MDIChild()
        {
            InitializeComponent();
            items = new List<string>();
            myTable = new DataTable();
            this._sql = Form_main.Command.CommandText;
            this.撤回此出库ToolStripMenuItem.Visible = false;
        }

        private void print_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.CurrentRow;
            string tm = row.Cells["条码"].Value.ToString();
            string pm = row.Cells["品名"].Value.ToString();
            float sj = float.Parse(row.Cells["售价"].Value.ToString());
            string dj = "￥" + sj.ToString("N2");
            Form_Input_fs fif = new Form_Input_fs(tm, pm, sj.ToString("N2"));
            if (fif.ShowDialog(this) != DialogResult.OK)
                return;
            string hh = fif.Printdata.hh.Trim();
            LabelFormatDocument doc;
            if (hh.Length < 1)
                doc = Form_main.labeldoc;
            else
                doc = Form_main.labeldoc2;
            doc.SubStrings["tm"].Value = tm;
            doc.SubStrings["pm"].Value = pm;
            doc.SubStrings["sj"].Value = dj;
            doc.SubStrings["fs"].Value = fif.Printdata.fs;
            if (hh.Length > 0)
            {
                doc.SubStrings["hh"].Value = hh;
            }
            doc.Print();
        }

        private void Form_MDIChild_Shown(object sender, EventArgs e)
        {
            if (this.dataGridView1 != null)
                this.dataGridView1.ClearSelection();
            if (!(this.MdiParent as Form_main).EnabledPrint)
            {
                this.toolStripMenuItem_print.Enabled = false;
                this.toolStripMenuItem_打印整张表.Enabled = false;
            }
            this.打印此单ToolStripMenuItem.Enabled = (this.MdiParent as Form_main).EnableXPPrint;
            if (!this.Text.Contains("【入库明细】"))
                this.Delete撤回此入库ToolStripMenuItem.Visible = false;
        }

        /// <summary>
        /// 给表格添加行号
        /// </summary>
        private void AddRowHeaderNumber()
        {
            for (int i = 0; i < this.dataGridView1.RowCount; i++)
            {
                this.dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
            if (this.dataGridView.DataSource == null)
                return;
            //////计算表格中数量和金额的总和
            /*
            DataTable dt = this.dataGridView.DataSource as DataTable;
            if (dt == null)
                return;
            int sumsl = 0;
            float je = 0f;
            bool ok = false;
            if (dt.Rows.Count > 0)
            {
                if (dt.Columns["数量"] != null)
                {
                    sumsl = int.Parse(dt.Compute("sum(数量)", null).ToString());
                    ok = true;
                }
                if (dt.Columns["金额"] != null)
                {
                    je = float.Parse(dt.Compute("sum(金额)", null).ToString());
                    ok = true;
                }
            }
            if (ok)
                this.toolStripStatusLabel1.Text = "共【" + dt.Rows.Count + "】条记录，【" + sumsl + "】件商品，【" + je.ToString("N2") + "】元";
             */
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            AddRowHeaderNumber();
        }

        private void Form_MDIChild_Enter(object sender, EventArgs e)
        {
            Form_main main = this.MdiParent as Form_main;

            if (this.items.Count > 0)
            {
                main.comboBox_tj.Enabled = true;
                main.comboBox_tj.Items.Clear();
                main.comboBox_tj.Items.AddRange(this.items.ToArray<string>());
                main.comboBox_tj.SelectedIndex = 0;
            }
            else
            {
                main.comboBox_tj.Items.Clear();
                main.comboBox_tj.Text = "";
            }
        }

        private void Form_MDIChild_Leave(object sender, EventArgs e)
        {
            //(this.MdiParent as Form_main).comboBox_tj.Items.Clear();
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            this.AddRowHeaderNumber();
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            this.AddRowHeaderNumber();
        }

        public void Find(string tj, string lr)
        {
            if (this.isFinded || this.dataGridView.RowCount < 1)
                return;

            int index = -1;
            foreach (DataGridViewColumn c in this.dataGridView1.Columns)
            {
                if (c.HeaderText == tj)
                {
                    index = c.Index;
                    break;
                }
            }
            if (index < 0)
            {
                MessageBox.Show("未查找到目标，过滤条件设置对否？");
                return;
            }

            DataTable dt = this.dataGridView.DataSource as DataTable;
            myTable = dt;
            dt = myTable.Clone();
            Form_main main = this.MdiParent as Form_main;
            for (int i = 0; i < myTable.Rows.Count; i++)
            {
                if (myTable.Rows[i][index].ToString() == lr)
                {
                    DataRow dr = dt.NewRow();
                    dr.ItemArray = myTable.Rows[i].ItemArray;
                    dt.Rows.Add(dr);
                }
            }
            if (dt.Rows.Count > 0)
            {
                this.dataGridView.DataSource = dt;
                this.isFinded = true;
                this.AddRowHeaderNumber();
                this.dataGridView.ClearSelection();
                main.textBox_lr.Clear();
            }
        }

        public void RestoreDataSource()
        {
            if (this.isFinded)
            {
                this.dataGridView.DataSource = myTable;
                this.isFinded = false;
            }
        }
        public void RefreshData()
        {
            if (this.Text == "本日时段")
                return;

            myTable.Clear();
            Form_main.Command.CommandText = _sql;
            MySqlDataAdapter da = new MySqlDataAdapter(Form_main.Command);
            this.dataGridView.SuspendLayout();
            DataTable dt = new DataTable();
            da.Fill(dt);
            this.dataGridView.DataSource = dt;
            this.dataGridView.ResumeLayout();
            Form_main mf = this.MdiParent as Form_main;
            mf.DataGridView1 = this.dataGridView;
            mf.SetColumnsWidth();
            this.isFinded = false;
        }
        private void dataGridView1_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= this.dataGridView.Rows.Count)
                return;
            this.dataGridView1.CurrentCell = this.dataGridView1.Rows[e.RowIndex].Cells[0];
            if (this.Text.Contains("单笔明细"))
            {
                var mf = this.MdiParent as Form_main;
                if (mf != null)
                {
                    //打印单笔交易明细
                    this.打印此单ToolStripMenuItem.Enabled = mf.EnableXPPrint;
                }
                this.contextMenuStrip1.Show(dataGridView1, this.PointToClient(MousePosition));
            }
            else if (this.Text == "商品资料" || this.Text.Contains("入库明细") || this.Text.Contains("入库汇总"))
            {

                this.contextMenuStrip1_print.Show(dataGridView1, this.PointToClient(MousePosition));
            }
            else if (this.Text == "出库明细")
            {
                this.contextMenuStrip1_print.Show(dataGridView1, this.PointToClient(MousePosition));
            }
            else if (this.Text == "查看备注")
            {
                this.contextMenuStrip_备注.Show(dataGridView1, this.PointToClient(MousePosition));
            }
        }

        void mi_Click(object sender, EventArgs e)
        {
            if ((this.MdiParent as Form_main).worker.qx != "高")
                return;
            DialogResult dr = MessageBox.Show(string.Format("要删除第{0}行吗？",
                this.dataGridView1.CurrentCell.RowIndex + 1),
                "提示",
                MessageBoxButtons.OKCancel);
            if (dr != DialogResult.OK)
                return;

            string djh = dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();

            MySqlCommand command = Form_main.Command;
            command.CommandText = string.Format("select tm,sl from sale_mx where djh='{0}'", djh);
            DataTable dt = new DataTable();
            MySqlDataAdapter a = new MySqlDataAdapter(command);
            a.Fill(dt);
            if (dt.Rows.Count == 0)
                return;

            string s = string.Format("单据号{0}共{1}种商品，将被删除？", djh, dt.Rows.Count);
            if (MessageBox.Show(s, "单据删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
            {
                return;
            }

            MySqlTransaction t = Form_main.Connection.BeginTransaction();
            //删除sale_mx            
            s = string.Format("delete from sale_mx where djh='{0}'", djh);
            command.CommandText = s;
            command.ExecuteNonQuery();
            //删除sale_db
            s = string.Format("delete from sale_db where djh='{0}'", djh);
            command.CommandText = s;
            command.ExecuteNonQuery();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                s = string.Format("update goods set kc=kc+{0} where tm='{1}'", dt.Rows[i][1], dt.Rows[i][0]);
                command.CommandText = s;
                command.ExecuteNonQuery();
            }
            try
            {
                t.Commit();
            }
            catch
            {
                t.Rollback();
                MessageBox.Show("删除单据出错，已取消本次操作！");
                return;
            }

            MessageBox.Show(string.Format("已删除本单交易，修正库存{0}处", dt.Rows.Count),
                "提示",
                MessageBoxButtons.OK);

        }

        private void Form_MDIChild_Activated(object sender, EventArgs e)
        {
            if (this.Text.Contains("【入库明细--工具】") || this.Text.Contains("【入库汇总--工具】"))
                (this.MdiParent as Form_main).toolStripMenuItem_真实入库.Enabled = true;

            if (this.Text.Contains("入库汇总") || this.Text.Contains("入库明细"))
            {
                this.toolStripMenuItem_print.Visible = true;
                this.toolStripMenuItem_打印整张表.Visible = true;
            }
            if (this.Text.Contains("【入库明细--工具】"))
            {
                this.toolStripMenuItem_删除此行.Visible = true;
                this.toolStripMenuItem_编辑此行.Visible = true;
            }
            if (this.Text.Contains("出库明细"))
            {
                this.toolStripMenuItem_print.Visible = true;
                this.toolStripMenuItem_打印整张表.Visible = false;
                this.撤回此出库ToolStripMenuItem.Visible = true;
            }
            if (this.Text.Contains("单笔明细"))
            {
                this.打印此单ToolStripMenuItem.Enabled = (this.MdiParent as Form_main).EnableXPPrint;
                this.打印此单ToolStripMenuItem.Visible = true;
            }

        }

        private void Form_MDIChild_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void Form_MDIChild_Deactivate(object sender, EventArgs e)
        {
            if (this.Text.Contains("明细") || this.Text.Contains("汇总"))
            {
                (this.MdiParent as Form_main).toolStripMenuItem_真实入库.Enabled = false;
                this.打印此单ToolStripMenuItem.Visible = false;
                this.toolStripMenuItem_print.Visible = false;
                this.toolStripMenuItem_打印整张表.Visible = false;
                this.toolStripMenuItem_删除此行.Visible = false;
                this.toolStripMenuItem_编辑此行.Visible = false;
                this.撤回此出库ToolStripMenuItem.Visible = false;
            }
        }

        private void 撤回此入库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_main mf = this.MdiParent as Form_main;
            if (mf.worker.qx != "高")
                return;
            if (MessageBox.Show("撤回此条入库操作，确定吗？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            if (!this.Text.Contains("入库明细"))
                return;
            if (this.dataGridView.Rows.Count < 1)
                return;
            DataGridViewRow row = this.dataGridView.CurrentRow;
            if (row == null)
                return;
            if (row.Index < 0 || row.Index >= this.dataGridView.Rows.Count)
                return;

            string tm = row.Cells["条码"].Value.ToString();
            string sl = row.Cells["数量"].Value.ToString();
            string date = row.Cells["日期"].Value.ToString();

            MySqlCommand command = Form_main.Command;
            MySqlConnection connection = Form_main.Connection;
            MySqlTransaction tr = connection.BeginTransaction();
            string sql;
            int ret;
            try
            {
                sql = string.Format("delete from rk where tm='{0}' and rq='{1}'", tm, date);
                command.CommandText = sql;
                ret = command.ExecuteNonQuery();
                if (ret == 0)
                {
                    MessageBox.Show("删除失败，无此记录，请重新打开入库明细表！", "提示：", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                if (ret != 1)
                    throw new Exception("删除出错！mdichild.cs line 392.");

                sql = string.Format("update goods set kc=kc-{0} where tm='{1}'", sl, tm);
                command.CommandText = sql;
                ret = command.ExecuteNonQuery();
                if (ret != 1)
                    throw new Exception("修改库存出错！mdichild.cs line 398.");
                tr.Commit();
            }
            catch (Exception se)
            {
                MessageBox.Show(se.Message + "\r\n开始回滚！", "出错提示：", MessageBoxButtons.OK);
                tr.Rollback();
                return;
            }

            MessageBox.Show("删除入库明细,及修改库存数据成功！", "提示：", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void toolStripMenuItem_打印整张表_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("将打印整张表的所有行，是吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;
            if (this.Text.Contains("入库汇总") || this.Text.Contains("入库明细"))
            {
                foreach (DataGridViewRow row in this.dataGridView.Rows)
                {
                    string tm = row.Cells["条码"].Value.ToString();
                    string pm = row.Cells["品名"].Value.ToString();
                    float sj = float.Parse(row.Cells["售价"].Value.ToString());
                    string fs = row.Cells["数量"].Value.ToString();
                    string dj = "￥" + sj.ToString("N2");


                    LabelFormatDocument doc = Form_main.labeldoc;
                    doc.SubStrings["tm"].Value = tm;
                    doc.SubStrings["pm"].Value = pm;
                    doc.SubStrings["sj"].Value = dj;
                    doc.SubStrings["fs"].Value = fs;
                    doc.Print();
                }
            }
        }

        private void toolStripMenuItem_删除此行_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("将删除此行，是吗？", "提示", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            if (!this.Text.Contains("【入库明细--工具】"))
                return;
            DataGridViewRow row = this.dataGridView.CurrentRow;
            if (row == null) return;
            string date = row.Cells["日期"].Value.ToString();
            string sql = "delete from rk_temp where rq='" + date + "'";
            IDbCommand command = Form_main.Command;
            command.CommandText = sql;
            int ret = command.ExecuteNonQuery();
            if (ret != 1)
                throw new Exception("操作数据库出错, delete from rk_temp");
            this.dataGridView.Rows.Remove(row);
        }

        private void toolStripMenuItem_编辑此行_Click(object sender, EventArgs e)
        {
            Form f = new Form_Edit_rk_temp(this.dataGridView.CurrentRow);
            f.StartPosition = FormStartPosition.Manual;
            f.Location = MousePosition;
            f.ShowDialog(this);
        }

        private void 撤回此出库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Text != "出库明细")
                return;
            if (MessageBox.Show("将撤回此出库，是吗？", "提示", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            Form_main mf = this.MdiParent as Form_main;
            if (mf.worker.qx != "高")
                return;

            if (this.dataGridView.Rows.Count < 1)
                return;

            DataGridViewRow row = this.dataGridView.CurrentRow;
            if (row == null)
                return;
            if (row.Index < 0 || row.Index >= this.dataGridView.Rows.Count)
                return;

            string tm = row.Cells["条码"].Value.ToString();
            string sl = row.Cells["数量"].Value.ToString();
            string date = row.Cells["日期"].Value.ToString();

            MySqlCommand command = Form_main.Command;
            MySqlConnection connection = Form_main.Connection;
            MySqlTransaction tr = connection.BeginTransaction();
            string sql;
            int ret;
            try
            {
                sql = string.Format("delete from ck where tm='{0}' and rq='{1}'", tm, date);
                command.CommandText = sql;
                ret = command.ExecuteNonQuery();
                if (ret == 0)
                {
                    MessageBox.Show("删除失败，无此记录，请重新打开出库明细表！", "提示：", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                if (ret != 1)
                    throw new Exception("删除出错！mdichild.cs line 304.");

                sql = string.Format("update goods set kc=kc+{0} where tm='{1}'", sl, tm);
                command.CommandText = sql;
                ret = command.ExecuteNonQuery();
                if (ret != 1)
                    throw new Exception("修改库存出错！mdichild.cs line 310.");
                tr.Commit();
            }
            catch (Exception se)
            {
                MessageBox.Show(se.Message + "\r\n开始回滚！", "出错提示：", MessageBoxButtons.OK);
                tr.Rollback();
                return;
            }
            MessageBox.Show("删除出库明细,及修改库存数据成功！", "提示：", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (this.dataGridView.RowCount < 1) return;

                if (!this.Text.Contains("【入库明细--工具】"))
                    return;

                DataGridViewRow row = this.dataGridView.CurrentRow;
                string date = row.Cells["日期"].Value.ToString();
                string sql = "delete from rk_temp where rq='" + date + "'";
                IDbCommand command = Form_main.Command;
                command.CommandText = sql;
                int ret = command.ExecuteNonQuery();
                if (ret != 1)
                    throw new Exception("操作数据库出错, delete from rk_temp");
                this.dataGridView.Rows.Remove(row);
            }
        }

        private void 删除备注ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("将删除此行，是吗？", "提示", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            if (this.Text != "查看备注") return;
            DataGridViewRow row = this.dataGridView.CurrentRow;
            if (row == null) return;
            string date = row.Cells["日期"].Value.ToString();
            string sql = "delete from bz where rq='" + date + "'";
            IDbCommand command = Form_main.Command;
            command.CommandText = sql;
            int ret = command.ExecuteNonQuery();
            if (ret != 1)
                throw new Exception("操作数据库出错, delete from bz");
            this.dataGridView.Rows.Remove(row);
        }

        private void 编辑备注ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var row = this.dataGridView.CurrentRow;
            if (row == null) return;
            var input = new Form_Input();
            if (input.ShowDialog(this) != DialogResult.OK) return;

            var rq = row.Cells["日期"].Value.ToString();
            var nr = input.Input;
            var command = Form_main.Command;
            command.CommandText = string.Format("update bz set nr='{0}' where rq='{1}'", nr, rq);
            var ret = command.ExecuteNonQuery();
            if (ret != 1)
                throw new Exception("操作数据库出错, edit table bz");
            row.Cells["内容"].Value = input.Input;
        }

        private void 打印此单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //在浏览单笔交易时，右键选择此项可打印小票

        }
    }
}
