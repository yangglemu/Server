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
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;
using Seagull.BarTender.Print;
using System.Threading;
using System.Xml;
using System.Drawing.Printing;

namespace Server
{
    public partial class Form_main : Form
    {
        public static string printer;
        public static string xp_printer;

        public static Font title;
        public static Font font;
        public static float fontHeight;
        public static float x1;
        public static float x2;
        public static float x3;
        public static string windowtitle;
        public static string shop;
        public static string address;

        private bool enabledPrint;
        private bool enableXPPrint;

        public bool EnableXPPrint
        {
            get { return this.enableXPPrint; }
            set
            {
                this.enableXPPrint = value;
            }
        }

        public bool EnabledPrint
        {
            get { return this.enabledPrint; }
            set
            {
                this.enabledPrint = value;
                this.打印条码ToolStripMenuItem.Enabled = value;
                this.打印回扁ToolStripMenuItem.Enabled = value;
                this.打印赠品ToolStripMenuItem.Enabled = value;
                this.toolStripButton_打印.Enabled = value;
            }
        }

        public static Engine engine;
        private static LabelFormatDocument _labeldoc;
        private static LabelFormatDocument _labeldoc2;
        private static LabelFormat _lf;
        private static LabelFormat _lf2;

        public static LabelFormatDocument labeldoc
        {
            get
            {
                if (_labeldoc != null) _labeldoc.Close(SaveOptions.DoNotSaveChanges);
                Seagull.BarTender.Print.Messages msgs;
                _labeldoc = engine.Documents.Open(_lf, out msgs);
                _labeldoc.PrintSetup.PrinterName = Form_main.printer;
                _labeldoc.SubStrings["shop"].Value = Form_main.shop;
                return _labeldoc;
            }
        }
        public static LabelFormatDocument labeldoc2
        {
            get
            {
                if (_labeldoc2 != null) _labeldoc2.Close(SaveOptions.DoNotSaveChanges);
                Seagull.BarTender.Print.Messages msgs;
                _labeldoc2 = engine.Documents.Open(_lf2, out msgs);
                _labeldoc2.PrintSetup.PrinterName = Form_main.printer;
                _labeldoc2.SubStrings["shop"].Value = Form_main.shop;
                return _labeldoc2;
            }
        }

        static MySqlConnection connection;

        public static MySqlConnection Connection
        {
            get { return connection; }
            set { connection = value; }
        }
        static MySqlCommand command;

        public static MySqlCommand Command
        {
            get { return command; }
            set { command = value; }
        }

        DataGridView dataGridView1;

        public DataGridView DataGridView1
        {
            get { return dataGridView1; }
            set { dataGridView1 = value; }
        }
        Worker _worker;

        public Worker worker
        {
            get { return _worker; }
            set { _worker = value; }
        }
        public Form_main()
        {
            InitializeComponent();
            this.Text = Form_main.shop;
        }

        private void Form_main_Load(object sender, EventArgs e)
        {
            this.EnabledPrint = false;
            this.EnableXPPrint = false;
            this.toolStripMenuItem_真实入库.Enabled = false;
        }

        private void Form_main_Shown(object sender, EventArgs e)
        {
            Form_start f = new Form_start();
            if (f.ShowDialog(this) != DialogResult.OK)
            {
                this.Close();
                return;
            }
            this.toolStripStatusLabel1.Text = "当前操作员：" + this.worker.xm;
            this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.GetPrinter);
            this.backgroundWorker1.RunWorkerAsync();
        }

        private void GetPrinter(object sender, DoWorkEventArgs e)
        {
            foreach (string s in PrinterSettings.InstalledPrinters)
            {
                //设置条码打印机
                if (s.Equals(Form_main.printer))
                {
                    try
                    {
                        Form_main.engine = new Seagull.BarTender.Print.Engine(true);
                        _lf = new LabelFormat(Application.StartupPath + "\\mylabel.btw");
                        _lf2 = new LabelFormat(Application.StartupPath + "\\mylabel2.btw");
                        this.SetPrinterToEnable();
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.Message);
                    }
                }
                //设置小票打印机
                if(s.Equals(Form_main.xp_printer))
                {
                    this.SetXPPrinterToEnable();
                }
            }
        }

        private void SetPrinterToEnable()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(SetPrinterToEnable));
            }
            else
            {
                this.EnabledPrint = true;
            }
        }

        private void SetXPPrinterToEnable()
        {
            if(this.InvokeRequired)
            {
                this.Invoke(new Action(SetXPPrinterToEnable));
            }
            else
            {
                this.EnableXPPrint = true;
            }
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //新建商品资料
            if (this.worker.qx == "低")
                return;
            Form_NewGoods f = new Form_NewGoods("goods");
            f.ShowDialog(this);
        }

        private void 入库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.worker.qx == "低")
                return;
            Form_rk f = new Form_rk();
            f.ShowDialog(this);
        }

        private void 全部ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //查看所有商品资料
            string s;
            if (this.worker.qx == "低")
                s = "select tm as 条码,pm as 品名,sj as 售价,zq as 折扣,hyzq as 会员折扣,ghs as 供货商 from goods";
            else
                s = "select tm as 条码,pm as 品名,jj as 进价,sj as 售价,zq as 折扣,hyzq as 会员折扣,ghs as 供货商 from goods";
            command.CommandText = s;
            MySqlDataAdapter a = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            a.Fill(dt);

            Form_MDIChild f = new Form_MDIChild();
            f.Text = "商品资料";
            f.MdiParent = this;
            f.items.AddRange(new string[] { "售价", "品名", "条码" });
            this.dataGridView1 = f.dataGridView;
            this.dataGridView1.DataSource = dt;
            this.dataGridView1.Columns["售价"].DefaultCellStyle.Format = "N2";
            if (this.dataGridView1.Columns["进价"] != null)
                this.dataGridView1.Columns["进价"].DefaultCellStyle.Format = "N2";
            this.dataGridView1.Columns["折扣"].DefaultCellStyle.Format = "N2";
            this.dataGridView1.Columns["会员折扣"].DefaultCellStyle.Format = "N2";
            this.SetColumnsWidth();
            f.toolStripStatusLabel1.Text = "当前共【" + dt.Rows.Count + "】条记录";
            f.Show();
        }

        private void 本日明细_商品明细ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.MX_Goods(DateTime.Today, DateTime.Today, "本日商品明细");
        }

        public void SetColumnsWidth()
        {
            var len = this.dataGridView1.Columns.Count;
            for (int i = 0; i < len; i++)
            {
                this.dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            this.dataGridView1.ClearSelection();
        }

        private void 本日明细_单笔明细ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.MX_DB(DateTime.Today, DateTime.Today, "本日单笔明细");
        }

        private void 库存浏览ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string s;
            if (this.worker.qx == "低")
                s = "select tm as 条码,pm as 品名,sj as 售价,kc as 库存,ghs as 供货商 from goods";
            else
                s = "select tm as 条码,pm as 品名,jj as 进价,sj as 售价,kc as 库存,ghs as 供货商 from goods";
            command.CommandText = s;
            MySqlDataAdapter a = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            a.Fill(dt);

            Form_MDIChild f = new Form_MDIChild();
            f.Text = "库存浏览";
            f.MdiParent = this;
            f.items.AddRange(new string[] { "售价", "条码", "品名", "库存", "供货商" });
            this.dataGridView1 = f.dataGridView;
            this.dataGridView1.DataSource = dt;
            this.dataGridView1.Columns["售价"].DefaultCellStyle.Format = "N2";
            if (this.dataGridView1.Columns["进价"] != null)
                this.dataGridView1.Columns["进价"].DefaultCellStyle.Format = "N2";
            this.SetColumnsWidth();

            s = "select ifnull(sum(kc),0) from goods";
            command.CommandText = s;
            int kc = int.Parse(command.ExecuteScalar().ToString());
            s = "select ifnull(sum(jj*kc),0.0) from goods";
            command.CommandText = s;
            float jjkc = float.Parse(command.ExecuteScalar().ToString());
            s = "select ifnull(sum(sj*kc),0.0) from goods";
            command.CommandText = s;
            float sjkc = float.Parse(command.ExecuteScalar().ToString());
            s = "select ifnull(sum(sj*zq*kc),0.0) from goods";
            command.CommandText = s;
            float zqkc = float.Parse(command.ExecuteScalar().ToString());
            s = "select ifnull(sum(sj*hyzq*kc),0) from goods";
            command.CommandText = s;
            float hyzqkc = float.Parse(command.ExecuteScalar().ToString());
            f.toolStripStatusLabel1.Text = "当前共【" + dt.Rows.Count + "】条记录";
            f.toolStripStatusLabel1.Text += ",共【" + kc.ToString() + "】件商品";
            if (this.worker.qx != "低")
            {
                f.toolStripStatusLabel1.Text += ",总成本【" + jjkc.ToString("N2");
                f.toolStripStatusLabel1.Text += "】,按售价总额【" + sjkc.ToString("N2");
                f.toolStripStatusLabel1.Text += "】,折扣后总额【" + zqkc.ToString("N2");
                f.toolStripStatusLabel1.Text += "】,会员折扣后总额【" + hyzqkc.ToString("N2") + "】";
            }
            f.Show();
        }

        private void MX_Goods(DateTime start, DateTime end, string FormText)
        {
            string _start, _end;
            _start = start.ToShortDateString();
            _end = end.ToShortDateString();
            string s = "select sale_mx.djh as 单据号,";
            s += "sale_mx.tm as 条码,";
            s += "goods.pm as 品名,";
            s += "sale_mx.sl as 数量,";
            s += "sale_mx.sj as 售价,";
            s += "sale_mx.zqfs as 折扣方式,";
            s += "sale_mx.zq as 折扣,";
            s += "sale_mx.je as 金额,";
            s += "sale_db.hy as 会员, ";
            s += "sale_mx.syy as 收银员, ";
            s += "sale_db.rq as 时间 ";
            s += "from sale_mx left join(sale_db,goods) ";
            s += "on(goods.tm=sale_mx.tm and sale_db.djh=sale_mx.djh) ";
            s += "where (date(sale_db.rq)>='" + _start + "' ";
            s += "and date(sale_db.rq)<='" + _end + "')";
            command.CommandText = s;
            MySqlDataAdapter a = new MySqlDataAdapter(command);
            DataTable dt = new DataTable("销售");
            a.Fill(dt);

            Form_MDIChild f = new Form_MDIChild();
            f.MdiParent = this;
            f.Text = FormText;
            f.items.AddRange(new string[] { "单据号", "条码", "会员", "收银员" });
            this.dataGridView1 = f.dataGridView;
            this.dataGridView1.DataSource = dt;
            string sl = "0";
            float je = 0;
            if (dt.Rows.Count > 0)
            {
                sl = dt.Compute("sum(数量)", null).ToString();
                je = float.Parse(dt.Compute("sum(金额)", null).ToString());
            }
            this.dataGridView1.Columns["售价"].DefaultCellStyle.Format = "N2";
            this.dataGridView1.Columns["金额"].DefaultCellStyle.Format = "N2";
            this.dataGridView1.Columns["折扣"].DefaultCellStyle.Format = "N2";
            this.dataGridView1.Columns["时间"].DefaultCellStyle.Format = "yy-MM-dd HH:mm:ss";
            f.toolStripStatusLabel1.Text = "查询日期【" + _start + "—" + _end + "】";
            f.toolStripStatusLabel1.Text += ",数量共【" + sl + "】件";
            f.toolStripStatusLabel1.Text += ",金额共【" + je.ToString("N2") + "】元";

            this.SetColumnsWidth();
            f.Show();
        }

        private void MX_DB(DateTime start, DateTime end, string FormText)
        {
            string _start, _end;
            _start = start.ToShortDateString();
            _end = end.ToShortDateString();
            string s = "select djh as 单据号,";
            s += "sl as 数量,";
            s += "je as 金额,";
            s += "ss as 实收,";
            s += "zl as 找零,";
            s += "sk as 刷卡,";
            s += "hy as 会员号,";
            s += "syy as 收银员,";
            s += "rq as 日期 ";
            s += "from sale_db ";
            s += "where (date(sale_db.rq)>='" + _start + "' ";
            s += "and date(sale_db.rq)<='" + _end + "')";

            command.CommandText = s;
            MySqlDataAdapter a = new MySqlDataAdapter(command);
            DataTable dt = new DataTable("销售");
            a.Fill(dt);

            Form_MDIChild f = new Form_MDIChild();
            f.MdiParent = this;
            f.items.AddRange(new string[] { "单据号", "会员号" });
            this.dataGridView1 = f.dataGridView;
            this.dataGridView1.DataSource = dt;
            f.Text = FormText;
            string sl = "0";
            float je = 0;
            if (dt.Rows.Count > 0)
            {
                sl = dt.Compute("sum(数量)", null).ToString();
                je = float.Parse(dt.Compute("sum(金额)", null).ToString());
            }
            this.dataGridView1.Columns["金额"].DefaultCellStyle.Format = "N2";
            this.dataGridView1.Columns["实收"].DefaultCellStyle.Format = "N2";
            this.dataGridView1.Columns["找零"].DefaultCellStyle.Format = "N2";
            this.dataGridView1.Columns["日期"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            f.toolStripStatusLabel1.Text = "查询日期【" + _start + "—" + _end + "】";
            f.toolStripStatusLabel1.Text += ",数量共【" + sl + "】";
            f.toolStripStatusLabel1.Text += ",金额【" + je.ToString("N2") + "】";

            this.SetColumnsWidth();
            f.Show();
        }
        private void 本月明细_商品明细ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DateTime d = DateTime.Today;
            this.MX_Goods(new DateTime(d.Year, d.Month, 1), DateTime.Today, "本月商品明细");
        }

        private void 本月明细_单笔明细ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DateTime d = DateTime.Today;
            this.MX_DB(new DateTime(d.Year, d.Month, 1), DateTime.Today, "本月单笔明细");
        }

        private void 本年明细_商品明细ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.MX_Goods(new DateTime(DateTime.Today.Year, 1, 1), DateTime.Today, "本年商品明细");
        }

        private void 本年明细_单笔明细ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.MX_DB(new DateTime(DateTime.Today.Year, 1, 1), DateTime.Today, "本年单笔明细");
        }

        private void 选择日期_商品明细ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Form_SelectDate sd = new Form_SelectDate();
            if (sd.ShowDialog(this) != DialogResult.OK)
                return;

            this.MX_Goods(sd.dateTimePicker1.Value.Date, sd.dateTimePicker2.Value.Date, "选择日期商品明细");
        }

        private void 选择日期_单笔明细ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Form_SelectDate sd = new Form_SelectDate();
            if (sd.ShowDialog(this) != DialogResult.OK)
                return;

            this.MX_DB(sd.dateTimePicker1.Value.Date, sd.dateTimePicker2.Value.Date, "选择日期单笔明细");
        }

        private void TimeView(DateTime start, DateTime end)
        {
            string startDate = start.ToShortDateString() + " ";
            string endDate = end.ToShortDateString() + " ";
            DataTable dt = new DataTable();
            dt.Columns.Add("时间段");
            dt.Columns.Add("来客数");
            dt.Columns.Add("营业额");
            dt.Columns.Add("客单价");

            int lks;//来客数
            float yye;//营业客
            float kdj;//客单价
            int total_lks = 0;//合计总来客数
            float total_yye = 0;//合计总营业客
            string startTime;
            string endTime;

            for (int i = 6; i < 22; i += 2)
            {
                startTime = i.ToString("00") + ":00:00";
                endTime = (i + 1).ToString("00") + ":59:59";
                command.CommandText = "select ifnull(count(*),0) from sale_db where date(rq)>='" + startDate;
                command.CommandText += "' and date(rq)<='" + endDate;
                command.CommandText += "' and time(rq)>='" + startTime;
                command.CommandText += "' and time(rq)<='" + endTime + "'";
                lks = int.Parse(command.ExecuteScalar().ToString());

                command.CommandText = "select ifnull(sum(je),0) from sale_db where date(rq)>='" + startDate;
                command.CommandText += "' and date(rq)<='" + endDate;
                command.CommandText += "' and time(rq)>='" + startTime;
                command.CommandText += "' and time(rq)<='" + endTime + "'";
                yye = float.Parse(command.ExecuteScalar().ToString());

                if (lks > 0)
                    kdj = yye / lks;
                else
                    kdj = 0;
                total_lks += lks;
                total_yye += yye;

                DataRow dr = dt.NewRow();
                dr[0] = i.ToString("00") + ":00:00 —— " + (i + 2).ToString("00") + ":00:00";
                dr[1] = lks;
                dr[2] = yye.ToString("N2");
                dr[3] = kdj.ToString("N2");
                dt.Rows.Add(dr);
            }
            Form_MDIChild f = new Form_MDIChild();
            f.Text = "本日时段";
            f.MdiParent = this;
            this.dataGridView1 = f.dataGridView;
            this.dataGridView1.DataSource = dt;
            this.dataGridView1.Columns["营业额"].DefaultCellStyle.Format = "N2";
            this.dataGridView1.Columns["客单价"].DefaultCellStyle.Format = "N2";
            if (total_lks > 0)
                kdj = total_yye / total_lks;
            else
                kdj = 0;
            f.toolStripStatusLabel1.Text = "查询日期【" + startDate + "—" + endDate + "】";
            f.toolStripStatusLabel1.Text += ",来客数【" + total_lks.ToString();
            f.toolStripStatusLabel1.Text += "】，营业额【" + total_yye.ToString("N2");
            f.toolStripStatusLabel1.Text += "】，客单价【" + kdj.ToString("N2") + "】";

            this.SetColumnsWidth();
            f.Show();
        }
        private void 本日时段ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TimeView(DateTime.Today, DateTime.Today);
        }

        private void 选择日期_本日时段toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            Form_SelectDate sd = new Form_SelectDate();
            if (sd.ShowDialog(this) != DialogResult.OK)
                return;
            this.TimeView(sd.dateTimePicker1.Value.Date, sd.dateTimePicker2.Value.Date);
        }

        private void ViewFenLenZM(DateTime start, DateTime end, bool zm)
        {
            string _start = start.ToShortDateString();
            string _end = end.ToShortDateString();
            string _zm;
            if (zm)
                _zm = "1001";
            else
                _zm = "1002";

            string s = "select sale_mx.djh as 单据号,sale_mx.tm as 条码, ";
            s += "goods.pm as 品名,sale_mx.sl as 数量,sale_mx.sj as 售价, ";
            s += "sale_mx.zqfs as 折扣方式,sale_mx.zq as 折扣,sale_mx.je as 金额 ";
            s += "from sale_mx left join(sale_db, goods) on(goods.tm=sale_mx.tm and sale_db.djh=sale_mx.djh) ";
            s += "where goods.ghs='" + _zm + "' and date(sale_db.rq)>='" + _start + "' ";
            s += "and date(sale_db.rq)<='" + _end + "'";
            command.CommandText = s;
            MySqlDataAdapter a = new MySqlDataAdapter(command);
            DataTable dt = new DataTable("销售");
            a.Fill(dt);

            Form_MDIChild f = new Form_MDIChild();
            f.Text = "本日分类" + _zm.ToString();
            f.MdiParent = this;
            this.dataGridView1 = f.dataGridView;
            this.dataGridView1.DataSource = dt;
            this.SetColumnsWidth();
            f.toolStripStatusLabel1.Text = "【本日分类" + _zm + "】当前共【" + dt.Rows.Count + "】条记录";
            s = "select ifnull(sum(sale_mx.sl),0) from sale_mx left join(sale_db,goods) on(sale_db.djh=sale_mx.djh and goods.tm=sale_mx.tm) where goods.ghs='" + _zm + "' and  date(sale_db.rq)>='" + _start + "' and date(sale_db.rq)<='" + _end + "'";
            command.CommandText = s;
            int hjsl = int.Parse(command.ExecuteScalar().ToString());
            f.toolStripStatusLabel1.Text += "， 共【" + hjsl.ToString() + "】件商品";
            f.Show();
        }
                
        private void 按日统计_toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            string start, end;
            end = DateTime.Today.ToShortDateString();
            start = DateTime.Today.AddDays(-29).ToShortDateString();

            string s = "select date(rq) as 日期, ";
            s += "ifnull(sum(sl),0) as 数量, ";
            s += "ifnull(sum(je),0) as 金额 ";
            s += "from sale_db where(date(rq)>='" + start + "' ";
            s += "and date(rq)<='" + end + "') group by date(rq) order by rq desc";
            command.CommandText = s;
            DataTable dt = new DataTable();
            MySqlDataAdapter a = new MySqlDataAdapter(command);
            a.Fill(dt);

            Form_MDIChild f = new Form_MDIChild();
            f.MdiParent = this;
            f.Text = "按日统计";
            this.dataGridView1 = f.dataGridView;
            this.dataGridView1.DataSource = dt;
            this.SetColumnsWidth();
            f.toolStripStatusLabel1.Text = "统计起始日【" + start + "】，截止日【" + end + "】，共【30】天";
            this.dataGridView1.Columns["金额"].DefaultCellStyle.Format = "N2";
            f.Show();
        }

        private void 出库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.worker.qx == "低")
                return;
            Form_ckth f = new Form_ckth();
            f.ShowDialog(this);
        }

        private void 清空库存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.worker.qx == "低")
            {
                MessageBox.Show("您的操作需要中级或者更高的权限！");
                return;
            }
            DialogResult dr;
            dr = MessageBox.Show("清空后所有商品库存数量为0，确认吗？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr != DialogResult.Yes)
                return;
            dr = MessageBox.Show("清空后所有商品库存数量为0，确认吗？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr != DialogResult.Yes)
                return;
            dr = MessageBox.Show("清空后所有商品库存数量为0，确认吗？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr != DialogResult.Yes)
                return;

            string s = "update goods set kc=0";
            command.CommandText = s;
            int rows = command.ExecuteNonQuery();

            if (rows > 0)
                MessageBox.Show("OK,所有商品库存已清零！");
            else
                MessageBox.Show("未处理任何数据");
        }

        private void 导出数据为文本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.worker.qx == "低")
                return;
            SaveFileDialog sd = new SaveFileDialog();
            sd.DefaultExt = "txt";
            sd.AddExtension = true;
            sd.Title = "指定要要导出的文件名及存放位置";
            sd.Filter = "文件文件(*.txt)|*.txt";
            if (sd.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(sd.FileName, false);
                string s = "select tm,kc from goods";
                command.CommandText = s;
                MySqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    sw.Write(dr.GetString(0) + "," + dr.GetString(1) + "\r\n");
                }
                dr.Close();
                sw.Flush();
                sw.Close();
            }
        }

        private void 单一商品ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.worker.qx == "低")
                return;
            //单一商品折扣设定
            Form_cx_one f = new Form_cx_one();
            f.ShowDialog(this);
        }

        private void 自营商品ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.worker.qx == "低")
                return;
            //所有自营商品折扣设定
            Form_cx_zy f = new Form_cx_zy();
            f.ShowDialog(this);
        }

        private void 专卖商品ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.worker.qx == "低")
                return;
            //所有专卖商品的折扣设定
            Form_cx_zm f = new Form_cx_zm();
            f.ShowDialog(this);
        }

        private void 全部商品ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.worker.qx == "低")
                return;
            //全部商品统一设置折扣率
            Form_cx_qb f = new Form_cx_qb();
            f.ShowDialog(this);
        }

        private void 激励报告ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_DatePicker form = new Form_DatePicker();
            if (form.ShowDialog(this) != DialogResult.OK)
                return;

            string s = "select sale_mx.djh as 单据号,";
            s += "goods.tm as 条码,";
            s += "goods.pm as 品名,";
            s += "sale_mx.sl as 数量,";
            s += "sale_mx.je as 金额,";
            s += "sale_db.rq as 时间,";
            s += "sale_mx.syy as 收银员 ";
            s += "from sale_mx left join(sale_db,goods) ";
            s += "on(sale_mx.tm=goods.tm and sale_mx.djh=sale_db.djh) ";
            s += "where sale_mx.syy='" + form.worker.bh + "' ";
            s += "and date(sale_db.rq)>='" + form.start + "' ";
            s += "and date(sale_db.rq)<='" + form.end + "' order by sale_db.rq";

            command.CommandText = s;
            DataTable dt = new DataTable();
            MySqlDataAdapter a = new MySqlDataAdapter(command);
            a.Fill(dt);

            Form_MDIChild f = new Form_MDIChild();
            f.Text = "员工业绩";
            f.MdiParent = this;
            f.items.AddRange(new string[] { "条码", "单据号" });
            this.dataGridView1 = f.dataGridView;
            this.dataGridView1.DataSource = dt;
            this.dataGridView1.Columns["金额"].DefaultCellStyle.Format = "N2";
            this.dataGridView1.Columns["时间"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            int sl = 0;
            float je = 0;
            if (dt.Rows.Count > 0)
            {
                sl = int.Parse(dt.Compute("sum(数量)", null).ToString());
                je = float.Parse(dt.Compute("sum(金额)", null).ToString());
            }
            f.toolStripStatusLabel1.Text = "查询员工【" + form.worker.xm + "】的业绩：从" + form.start + "至" + form.end + "，";
            f.toolStripStatusLabel1.Text += "共售出【" + sl.ToString() + "】件商品，";
            f.toolStripStatusLabel1.Text += "总金额【" + je.ToString("N2") + "】";

            this.SetColumnsWidth();
            f.Show();
        }

        private void 添加员工ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.worker.qx == "低")
                return;

            Form_AddWorkerl f = new Form_AddWorkerl();
            f.ShowDialog(this);
        }

        private void 锁定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.worker = null;
            this.toolStripStatusLabel1.Text = "当前用户：已注销";
            this.关闭所有窗口ToolStripMenuItem_Click(null, null);

            Form_start f = new Form_start();
            if (f.ShowDialog(this) != DialogResult.OK)
            {
                this.Close();
                return;
            }
            this.toolStripStatusLabel1.Text = "当前操作员：" + this.worker.xm;
        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.worker.qx == "低")
                return;
        }

        private void 重置系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.worker.qx != "高")
            {
                MessageBox.Show("您的操作需要最高级权限！");
                return;
            }
            DialogResult dr;
            dr = MessageBox.Show("重置后所有数据消失，确认吗？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr != DialogResult.Yes)
                return;
            dr = MessageBox.Show("重置后所有数据消失，确认吗？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr != DialogResult.Yes)
                return;
            dr = MessageBox.Show("重置后所有数据消失，确认吗？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr != DialogResult.Yes)
                return;

            string[] ss = {
                          "delete from goods",
                          "delete from people",
                          "delete from worker",
                          "delete from ck",
                          "delete from rk",
                          "delete from sale_mx",
                          "delete from sale_db",
                          "delete from ghs" };
            try
            {
                for (int i = 0; i < ss.Length; i++)
                {
                    command.CommandText = ss[i];
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("重置成功！");
            }
            catch (Exception se)
            {
                MessageBox.Show(se.Message, "重置系统时出错");
            }
        }

        private void 修改员工toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            Form_EditWorker f = new Form_EditWorker();
            f.ShowDialog(this);
        }

        private void ViewGHS(string ghsID)
        {
            string s;
            if (this.worker.qx == "低")
                s = "select tm as 条码,pm as 品名,sj as 售价,kc as 库存,ghs as 供货商 from goods where ghs='" + ghsID + "'";
            else
                s = "select tm as 条码,pm as 品名,jj as 进价,sj as 售价,kc as 库存,ghs as 供货商 from goods where ghs='" + ghsID + "'";

            command.CommandText = s;
            MySqlDataAdapter a = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            a.Fill(dt);

            Form_MDIChild f = new Form_MDIChild();
            if (ghsID == "1001")
                f.Text = "专卖库存";
            else
                f.Text = "自营库存";
            f.MdiParent = this;
            f.items.AddRange(new string[] { "条码", "品名" });
            this.dataGridView1 = f.dataGridView;
            this.dataGridView1.DataSource = dt;
            this.SetColumnsWidth();
            s = "select ifnull(sum(kc),0) from goods where ghs='" + ghsID + "'";
            command.CommandText = s;
            int kc = int.Parse(command.ExecuteScalar().ToString());
            s = "select ifnull(sum(jj*kc),0.0) from goods where ghs='" + ghsID + "'";
            command.CommandText = s;
            float jjkc = float.Parse(command.ExecuteScalar().ToString());
            s = "select ifnull(sum(sj*kc),0.0) from goods where ghs='" + ghsID + "'";
            command.CommandText = s;
            float sjkc = float.Parse(command.ExecuteScalar().ToString());
            s = "select ifnull(sum(sj*zq*kc),0.0) from goods where ghs='" + ghsID + "'";
            command.CommandText = s;
            float zqkc = float.Parse(command.ExecuteScalar().ToString());
            s = "select ifnull(sum(sj*hyzq*kc),0) from goods where ghs='" + ghsID + "'";
            command.CommandText = s;
            float hyzqkc = float.Parse(command.ExecuteScalar().ToString());
            f.toolStripStatusLabel1.Text = "当前共【" + dt.Rows.Count + "】条记录";
            f.toolStripStatusLabel1.Text += ",共【" + kc.ToString() + "】件商品";
            if (this.worker.qx != "低")
            {
                f.toolStripStatusLabel1.Text += ",总成本【" + jjkc.ToString("N2");
                f.toolStripStatusLabel1.Text += "】,按售价总额【" + sjkc.ToString("N2");
                f.toolStripStatusLabel1.Text += "】,折扣后总额【" + zqkc.ToString("N2");
                f.toolStripStatusLabel1.Text += "】,会员折扣后总额【" + hyzqkc.ToString("N2") + "】";
            }
            f.Show();
        }
        private void 专卖ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ViewGHS("1001");
        }

        private void 排列窗口toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                f.WindowState = FormWindowState.Normal;
            }
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void 全部最小化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //全部最小华所有子窗口
            foreach (Form f in this.MdiChildren)
            {
                f.WindowState = FormWindowState.Minimized;
            }
        }

        private void 关闭所有窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                f.Close();
            }
        }

        private void 导出XLSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.worker.qx == "低")
                return;

            SaveFileDialog sd = new SaveFileDialog();
            sd.DefaultExt = "xls";
            sd.AddExtension = true;
            sd.Title = "指定要要导出的文件名及存放位置";
            sd.Filter = "文件文件(*.xls)|*.xls";
            if (sd.ShowDialog() == DialogResult.OK)
            {
                HSSFWorkbook book = new HSSFWorkbook();
                ISheet sheet = book.CreateSheet("导出的库存文件");
                string s = "select tm,kc,sj,pm,jj from goods";
                command.CommandText = s;
                MySqlDataReader dr = command.ExecuteReader();
                int i = 0;

                ICellStyle style = book.CreateCellStyle();
                IDataFormat format = book.CreateDataFormat();
                style.DataFormat = format.GetFormat("0.00");
                style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;

                ICellStyle style_all = book.CreateCellStyle();
                style_all.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;

                while (dr.Read())
                {
                    IRow row = sheet.CreateRow(i);
                    //tm
                    ICell cell = row.CreateCell(0);
                    cell.CellStyle = style_all;
                    cell.SetCellValue(dr.GetString(0));
                    //sl
                    cell = row.CreateCell(1);
                    cell.CellStyle = style_all;
                    cell.SetCellValue(dr.GetInt32(1));
                    //sj
                    cell = row.CreateCell(2);
                    cell.CellStyle = style;
                    cell.SetCellValue(dr.GetFloat(2));
                    //pm
                    cell = row.CreateCell(3);
                    cell.CellStyle = style_all;
                    cell.SetCellValue(dr.GetString(3));

                    //jj
                    cell = row.CreateCell(4);
                    cell.CellStyle = style;
                    cell.SetCellValue(dr.GetFloat(4));
                    i++;
                }
                dr.Close();
                sheet.SetColumnWidth(0, 18 * 256);
                sheet.SetColumnWidth(1, 8 * 256);
                sheet.SetColumnWidth(2, 10 * 256);
                sheet.SetColumnWidth(3, 26 * 256);
                sheet.SetColumnWidth(4, 10 * 256);
                FileStream fs = new FileStream(sd.FileName, FileMode.Create, FileAccess.Write);
                book.Write(fs);
                fs.Close();
                MessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void 新会员ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_New_People newpeople = new Form_New_People();
            newpeople.ShowDialog(this);
        }

        private void 修改资料ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Edit_People edit = new Form_Edit_People();
            edit.ShowDialog(this);
        }

        private void 积分操作ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.worker.qx == "低")
                return;
            Form_jfcz f = new Form_jfcz();
            f.ShowDialog(this);
        }

        private void 充值操作ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.worker.qx == "低")
                return;
        }

        private void 本日汇总ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.FengLeiHuiZong(DateTime.Today, DateTime.Today);
        }

        private void 选择日期ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_SelectDate sd = new Form_SelectDate();
            if (sd.ShowDialog(this) != DialogResult.OK)
                return;

            this.FengLeiHuiZong(sd.dateTimePicker1.Value.Date, sd.dateTimePicker2.Value.Date);
        }

        public void FengLeiHuiZong(DateTime start, DateTime end)
        {
            string _start = start.ToShortDateString();
            string _end = end.ToShortDateString();
            string s = "select sale_mx.tm as 条码,goods.pm as 品名,sum(sale_mx.sl) as 数量,sum(sale_mx.je) as 金额 ";
            s += "from sale_mx join goods on(goods.tm=sale_mx.tm) join sale_db on(sale_db.djh=sale_mx.djh)";
            s += " where date(sale_db.rq)>='" + _start;
            s += "' and date(sale_db.rq)<='" + _end;
            s += "' group by sale_mx.tm";
            command.CommandText = s;
            MySqlDataAdapter a = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            a.Fill(dt);
            if (dt.Rows.Count < 1)
                return;
            Form_MDIChild child = new Form_MDIChild();
            child.MdiParent = this;
            child.Text = "条码汇总";
            child.items.Add("条码");
            this.dataGridView1 = child.dataGridView;
            child.dataGridView.DataSource = dt;
            child.toolStripStatusLabel1.Text = "条码汇总【" + _start + "—" + _end + "】";
            string sumsl, sumje;
            sumsl = dt.Compute("sum(数量)", null).ToString();
            sumje = float.Parse(dt.Compute("sum(金额)", null).ToString()).ToString("N2");
            child.toolStripStatusLabel1.Text += " 合计：【" + sumsl + "】件，【" + sumje + "】元";
            this.dataGridView1.Columns["金额"].DefaultCellStyle.Format = "N2";
            this.SetColumnsWidth();
            child.Show();
        }

        private void 会员浏览ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.worker.qx == "低")
                return;
            string s = "select bh as 编号, ";
            s += "xm as 姓名, ";
            s += "ljxf as `累计消费(元)`, ";
            s += "jf as 积分, ";
            s += "dh as 手机, ";
            s += "rmb as `充值(元)`, ";
            s += "rq as 创建日期, ";
            s += "czy as 操作员 ";
            s += "from people order by rq";
            command.CommandText = s;
            DataTable dt = new DataTable();
            MySqlDataAdapter a = new MySqlDataAdapter(command);
            a.Fill(dt);

            Form_MDIChild child = new Form_MDIChild();
            child.MdiParent = this;
            child.Text = "会员浏览";
            child.items.AddRange(new string[] { "编号", "姓名", "手机" });
            this.dataGridView1 = child.dataGridView;
            child.dataGridView.DataSource = dt;
            child.toolStripStatusLabel1.Text = "共【" + dt.Rows.Count.ToString();
            child.toolStripStatusLabel1.Text += "】会员";

            this.SetColumnsWidth();
            child.Show();
        }

        private void comboBox_tj_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.textBox_lr.Select();
            this.textBox_lr.SelectAll();
        }

        private void textBox_lr_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    if (this.comboBox_tj.Text.Trim().Length < 1)
                        return;
                    if (this.textBox_lr.Text.Trim().Length < 1)
                        return;
                    Form_MDIChild child = this.ActiveMdiChild as Form_MDIChild;
                    if (child != null)
                        child.Find(this.comboBox_tj.Text.Trim(), this.textBox_lr.Text.Trim());
                    break;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Form_MDIChild child = this.ActiveMdiChild as Form_MDIChild;
            if (child != null)
                child.RestoreDataSource();
        }

        private void 员工浏览toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            string s = "select bh as 编号,xm as 姓名,dh as 手机,qx as 权限,`show` as 在职,rq as 创建时间 from worker order by rq";
            command.CommandText = s;
            DataTable dt = new DataTable();
            MySqlDataAdapter a = new MySqlDataAdapter(command);
            a.Fill(dt);

            Form_MDIChild child = new Form_MDIChild();
            child.MdiParent = this;
            child.Text = "员工浏览";
            child.items.AddRange(new string[] { "姓名", "手机" });
            this.dataGridView1 = child.dataGridView;
            child.dataGridView.DataSource = dt;
            child.toolStripStatusLabel1.Text = "共【" + dt.Rows.Count.ToString();
            child.toolStripStatusLabel1.Text += "】员工";
            child.dataGridView.Columns["创建时间"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            child.dataGridView.Columns[4].ValueType = typeof(bool);
            var cc = child.dataGridView.Columns[4] as DataGridViewCheckBoxColumn;
            cc.TrueValue = 1;
            cc.FalseValue = 0;
            child.dataGridView.ReadOnly = false;
            foreach (DataGridViewColumn dc in child.dataGridView.Columns)
            {
                if (!(dc.Equals(cc))) dc.ReadOnly = true;
            }
            child.dataGridView.CurrentCellDirtyStateChanged += (obj, arg) =>
            {
                if (child.dataGridView.IsCurrentCellDirty)
                {
                    child.dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            };
            child.dataGridView.CellValueChanged += (obj, arg) =>
            {
                if (arg.ColumnIndex == 4)
                {
                    var bh = child.dataGridView.Rows[arg.RowIndex].Cells[0].Value.ToString();
                    var show = (bool)child.dataGridView.Rows[arg.RowIndex].Cells[4].Value;
                    var sql = string.Format("update worker set `show`={0} where bh='{1}'", show ? 1 : 0, bh);
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                }
            };
            this.SetColumnsWidth();
            child.Show();
        }

        private void 操作历史ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.worker.qx == "低") return;
            string s = "select jfcz.bh as 会员号,people.xm as 会员姓名, jfcz.cz as 操作, ";
            s += "jfcz.czjf as 操作积分, jfcz.syjf as 剩余积分, jfcz.czyy as 备注, jfcz.rq as 操作日期, ";
            s += "jfcz.czy as 操作员 from jfcz left join(people) on(people.bh=jfcz.bh) order by jfcz.rq";
            command.CommandText = s;
            DataTable dt = new DataTable();
            MySqlDataAdapter a = new MySqlDataAdapter(command);
            a.Fill(dt);

            Form_MDIChild child = new Form_MDIChild();
            child.MdiParent = this;
            child.Text = "积分操作历史";
            child.items.AddRange(new string[] { "会员号", "会员姓名", "操作员" });
            this.dataGridView1 = child.dataGridView;
            child.dataGridView.DataSource = dt;
            child.toolStripStatusLabel1.Text = "共【" + dt.Rows.Count.ToString();
            child.toolStripStatusLabel1.Text += "】记录";
            this.dataGridView1.Columns["操作日期"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            this.SetColumnsWidth();
            child.Show();
        }

        private void 自营ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ViewGHS("1002");
        }

        private void 入库明细ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_SelectDate sd = new Form_SelectDate();
            if (sd.ShowDialog(this) != DialogResult.OK)
                return;

            // if (this.worker.qx == "低") return;
            string s = string.Format("select rk.tm as 条码,goods.pm as 品名,rk.sl as 数量,goods.sj as 售价,rk.sl*goods.sj as 金额,rk.rq as 日期,rk.czy as 操作员 from rk join goods using(tm) where date(rk.rq)>='{0}' and date(rk.rq)<='{1}' order by rk.rq asc",
                sd.dateTimePicker1.Value.Date.ToShortDateString(),
                sd.dateTimePicker2.Value.Date.ToShortDateString());
            command.CommandText = s;
            MySqlDataAdapter a = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            a.Fill(dt);

            Form_MDIChild child = new Form_MDIChild();
            child.MdiParent = this;
            child.Text = sd.dateTimePicker1.Value.Date.ToShortDateString() + "--" + sd.dateTimePicker2.Value.Date.ToShortDateString() + "【入库明细】";
            child.items.AddRange(new string[] { "售价", "条码", "品名", "数量" });
            child.dataGridView.DataSource = dt;
            this.dataGridView1 = child.dataGridView;
            this.dataGridView1.Columns["日期"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            this.dataGridView1.Columns["售价"].DefaultCellStyle.Format = "N2";
            this.dataGridView1.Columns["金额"].DefaultCellStyle.Format = "N2";
            this.SetColumnsWidth();
            int sumsl = 0;
            float je = 0f;
            if (dt.Rows.Count > 0)
            {
                sumsl = int.Parse(dt.Compute("sum(数量)", null).ToString());
                je = float.Parse(dt.Compute("sum(金额)", null).ToString());
            }
            child.toolStripStatusLabel1.Text = "共【" + dt.Rows.Count + "】条记录，【" + sumsl + "】件商品，【" + je.ToString("N2") + "】元";
            child.Show();
        }

        private void 入库汇总toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            Form_SelectDate sd = new Form_SelectDate();
            if (sd.ShowDialog(this) != DialogResult.OK)
                return;

            string s = string.Format("select rk.tm as 条码,goods.pm as 品名,sum(rk.sl) as 数量,goods.sj as 售价,goods.sj*sum(rk.sl) as 金额,rk.rq as 日期,rk.czy as 操作员 from rk join goods using(tm) where date(rk.rq)>='{0}' and date(rk.rq)<='{1}' group by rk.tm order by rk.rq asc",
                sd.dateTimePicker1.Value.Date.ToShortDateString(),
                sd.dateTimePicker2.Value.Date.ToShortDateString());
            command.CommandText = s;
            MySqlDataAdapter a = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            a.Fill(dt);

            Form_MDIChild child = new Form_MDIChild();
            child.MdiParent = this;
            child.Text = sd.dateTimePicker1.Value.Date.ToShortDateString() + "--" + sd.dateTimePicker2.Value.Date.ToShortDateString() + "【入库汇总】";
            child.items.AddRange(new string[] { "售价", "数量", "条码", "品名" });
            child.dataGridView.DataSource = dt;
            this.dataGridView1 = child.dataGridView;
            this.dataGridView1.Columns["日期"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            this.dataGridView1.Columns["售价"].DefaultCellStyle.Format = "N2";
            this.dataGridView1.Columns["金额"].DefaultCellStyle.Format = "N2";
            this.SetColumnsWidth();
            int sumsl = 0;
            float je = 0f;
            if (dt.Rows.Count > 0)
            {
                sumsl = int.Parse(dt.Compute("sum(数量)", null).ToString());
                je = float.Parse(dt.Compute("sum(金额)", null).ToString());
            }
            child.toolStripStatusLabel1.Text = "共【" + dt.Rows.Count + "】种商品，【" + sumsl + "】件商品，【" + je.ToString("N2") + "】元";
            child.Show();
        }

        private void 出库明细ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form_SelectDate sd = new Form_SelectDate();
            if (sd.ShowDialog(this) != DialogResult.OK)
                return;
            string s = "select ck.tm as 条码,goods.pm as 品名,ck.sl as 数量,goods.sj as 售价,goods.sj*ck.sl as 金额,ck.bz as 备注,ck.rq as 日期,ck.czy as 操作员 from ck left join goods using(tm) ";
            s += string.Format("where date(ck.rq)>='{0}' and date(ck.rq)<='{1}'",
                sd.dateTimePicker1.Value.Date.ToShortDateString(),
                sd.dateTimePicker2.Value.Date.ToShortDateString());
            command.CommandText = s;
            MySqlDataAdapter a = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            a.Fill(dt);

            Form_MDIChild child = new Form_MDIChild();
            child.MdiParent = this;
            child.Text = "出库明细";
            child.items.AddRange(new string[] { "条码", "品名", "售价" });
            child.dataGridView.DataSource = dt;
            this.dataGridView1 = child.dataGridView;
            this.dataGridView1.Columns["日期"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            this.dataGridView1.Columns["售价"].DefaultCellStyle.Format = "N2";
            this.dataGridView1.Columns["金额"].DefaultCellStyle.Format = "N2";
            this.SetColumnsWidth();
            string text = sd.dateTimePicker1.Value.Date.ToShortDateString() + "--" + sd.dateTimePicker2.Value.Date.ToShortDateString();
            text += "，共【" + dt.Rows.Count + "】条记录，【" + dt.Compute("sum(数量)", null);

            float sum_je = 0.0F;
            if (dt.Rows.Count > 0)
                sum_je = float.Parse(dt.Compute("sum(金额)", null).ToString());
            text += "】件商品，【" + sum_je.ToString("N2") + "】元";
            child.toolStripStatusLabel1.Text = text;
            child.Show();
        }

        private void 导出销售数据toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            if (this.worker.qx == "低")
                return;

            SaveFileDialog sd = new SaveFileDialog();
            sd.DefaultExt = "xls";
            sd.AddExtension = true;
            sd.Title = "指定要要导出的文件名及存放位置";
            sd.Filter = "电子表格文件(*.xls)|*.xls";
            if (sd.ShowDialog() != DialogResult.OK)
                return;

            ///////////////
            Form_SelectDate sdf = new Form_SelectDate();
            if (sdf.ShowDialog(this) != DialogResult.OK)
                return;
            string _start = sdf.dateTimePicker1.Value.Date.ToShortDateString();
            string _end = sdf.dateTimePicker2.Value.Date.ToShortDateString();

            string s = "select sale_mx.tm,sum(sale_mx.sl),goods.sj,goods.pm,goods.jj ";
            s += "from sale_mx join sale_db on(sale_db.djh=sale_mx.djh) join goods on(goods.tm=sale_mx.tm)";
            s += " where date(sale_db.rq)>='" + _start;
            s += "' and date(sale_db.rq)<='" + _end;
            s += "' group by sale_mx.tm";

            HSSFWorkbook book = new HSSFWorkbook();
            ISheet sheet = book.CreateSheet("导出的销售数据");
            command.CommandText = s;
            MySqlDataReader reader = command.ExecuteReader();
            int i = 0;
            ICellStyle style = book.CreateCellStyle();
            IDataFormat format = book.CreateDataFormat();
            style.DataFormat = format.GetFormat("0.00");
            style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;

            ICellStyle style_all = book.CreateCellStyle();
            style_all.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            while (reader.Read())
            {
                IRow row = sheet.CreateRow(i);

                ICell cell = row.CreateCell(0);
                cell.CellStyle = style_all;
                cell.SetCellValue(reader.GetString(0));

                cell = row.CreateCell(1);
                cell.CellStyle = style_all;
                cell.SetCellValue(reader.GetInt32(1));

                cell = row.CreateCell(2);
                cell.CellStyle = style;
                cell.SetCellValue(reader.GetFloat(2));

                cell = row.CreateCell(3);
                cell.CellStyle = style_all;
                cell.SetCellValue(reader.GetString(3));

                cell = row.CreateCell(4);
                cell.CellStyle = style;
                cell.SetCellValue(reader.GetFloat(4));
                i++;
            }
            reader.Close();
            sheet.SetColumnWidth(0, 18 * 256);
            sheet.SetColumnWidth(1, 8 * 256);
            sheet.SetColumnWidth(2, 10 * 256);
            sheet.SetColumnWidth(3, 26 * 256);
            sheet.SetColumnWidth(4, 10 * 256);
            FileStream fs = new FileStream(sd.FileName, FileMode.Create);
            book.Write(fs);
            fs.Close();
            MessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form_main_FormClosed(object sender, FormClosedEventArgs e)
        {
            connection.Close();
            if (Form_main.engine != null)
            {
                Form_main.engine.Documents.CloseAll(SaveOptions.DoNotSaveChanges);
                Form_main.engine.Stop();
            }
        }

        private void 导入盘点数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.worker.qx == "低")
                return;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.AddExtension = true;
            ofd.DefaultExt = "xls";
            ofd.Title = "指定要导入的电子表格文件";
            ofd.Filter = "电子表格文件(*.xls)|*.xls";
            if (ofd.ShowDialog(this) != DialogResult.OK)
                return;

            FileStream fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read);
            HSSFWorkbook book = new HSSFWorkbook(fs);
            ISheet sheet = book.GetSheetAt(0);
            int total = 0;

            IFont font = book.CreateFont();
            font.Color = NPOI.HSSF.Util.HSSFColor.Red.Index;
            ICellStyle style = book.CreateCellStyle();
            style.SetFont(font);
            //检查格式
            for (int i = 0; i < sheet.LastRowNum + 1; i++)
            {
                IRow row = sheet.GetRow(i);
                string tm, sl;
                try
                {
                    //防止格式不正确造成读取错误
                    tm = row.GetCell(0).StringCellValue.Trim();
                    sl = row.GetCell(1).NumericCellValue.ToString();
                }
                catch
                {
                    MessageBox.Show("第" + (i + 1).ToString() + " 条记录未能导入成功，请稍后检查！\n(已标记?号，读表错误)", "提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    row.CreateCell(5).SetCellValue("?(读取错误)");
                    row.GetCell(5).CellStyle = style;
                    fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Write);
                    book.Write(fs);
                    fs.Close();
                    return;
                }
            }
            //检查条码
            for (int i = 0; i < sheet.LastRowNum + 1; i++)
            {
                IRow row = sheet.GetRow(i);
                string tm, sl;
                tm = row.GetCell(0).StringCellValue.Trim();
                sl = row.GetCell(1).NumericCellValue.ToString();
                command.CommandText = "select count(*) from goods where tm='" + tm + "'";
                MySqlDataReader reader = command.ExecuteReader();
                reader.Read();
                int count = reader.GetInt32(0);
                reader.Close();
                if (count != 1)
                {
                    MessageBox.Show("第" + (i + 1).ToString() + " 条记录未能导入成功，请稍后检查！\n(已标记*号，条码不存在)", "提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    row.CreateCell(5).SetCellValue("*(条码不存在)");
                    row.GetCell(row.LastCellNum).CellStyle = style;
                    fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Write);
                    book.Write(fs);
                    fs.Close();
                    return;
                }
            }
            //更新数据库
            for (int i = 0; i < sheet.LastRowNum + 1; i++)
            {
                IRow row = sheet.GetRow(i);
                string tm, sl;
                tm = row.GetCell(0).StringCellValue.Trim();
                sl = row.GetCell(1).NumericCellValue.ToString();
                command.CommandText = string.Format("update goods set kc=kc+{0} where tm='{1}'", sl, tm);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (MySql.Data.MySqlClient.MySqlException)
                {
                    MessageBox.Show("第" + (i + 1).ToString() + " 条记录未能导入成功，请稍后检查！\n(已标记X号,数据库操作错误)", "提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    row.CreateCell(5).SetCellValue("X(数据库操作错误)");
                    row.GetCell(5).CellStyle = style;
                    fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Write);
                    book.Write(fs);
                    fs.Close();
                    return;
                }
                total++;
            }
            int sy = sheet.LastRowNum + 1 - total;
            MessageBox.Show("共" + (sheet.LastRowNum + 1).ToString() + "条记录，导入" + total.ToString() + "条记录成功！\n（" + sy.ToString() + "条数据未能导入,已标记）", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void 自动核对toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            if (this.worker.qx == "低")
                return;

            Form_zdhd f = new Form_zdhd();
            f.ShowDialog(this);
        }

        private void toolStripMenuItem11删除本日销售数据_Click(object sender, EventArgs e)
        {
            if (this.worker.qx != "高")
            {
                MessageBox.Show("权限不够！");
                return;
            }
            if (MessageBox.Show("删除后不可恢复，确定吗？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            string sdate = DateTime.Now.ToShortDateString();
            command.CommandText = string.Format("select sale_mx.tm,sum(sale_mx.sl) from sale_mx join sale_db using(djh) where date(sale_db.rq)='{0}' group by sale_mx.tm", sdate);
            DataTable dt = new DataTable();
            MySqlDataAdapter a = new MySqlDataAdapter(command);
            a.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("本日无销售数据可供删除！");
                return;
            }
            string s = "共" + dt.Rows.Count + "种商品销售记录将被删除？";
            if (MessageBox.Show(s, "本日销售数据删除操作", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                DataTable djhs = new DataTable();
                s = string.Format("select djh from sale_db where date(rq)='{0}'", sdate);
                command.CommandText = s;
                MySqlDataAdapter ma = new MySqlDataAdapter(command);
                ma.Fill(djhs);
                int idb = 0;
                int ikc = 0;
                MySqlTransaction t = connection.BeginTransaction();
                for (int i = 0; i < djhs.Rows.Count; i++)
                {
                    s = string.Format("delete from sale_mx where djh='{0}'", djhs.Rows[i][0]);
                    command.CommandText = s;
                    command.ExecuteNonQuery();

                    s = string.Format("delete from sale_db where djh='{0}'", djhs.Rows[i][0]);
                    command.CommandText = s;
                    command.ExecuteNonQuery();

                    idb++;
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    s = string.Format("update goods set kc=kc+{0} where tm='{1}'", dt.Rows[i][1], dt.Rows[i][0]);
                    command.CommandText = s;
                    command.ExecuteNonQuery();
                    ikc++;
                }
                try
                {
                    t.Commit();
                }
                catch (MySqlException)
                {
                    t.Rollback();
                    return;
                }

                MessageBox.Show(string.Format("共删除{0}笔交易，修正库存{1}处", idb, ikc),
                    "提示", MessageBoxButtons.OK);
            }
        }

        private void 新建大类ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (this.worker.qx == "低")
                return;
            AddLB addlb = new AddLB(1);
            addlb.ShowDialog(this);
        }

        private void 浏览大类ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ViewFL(1);
        }

        public void ViewFL(int lb)
        {
            int length = lb * 2;
            string sql = string.Format("select bh as 编号,pm as 品名,dnm as 店内码 from fl where char_length(dnm)={0} order by dnm asc", length);
            command.CommandText = sql;
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(dt);

            Form_MDIChild child = new Form_MDIChild();
            switch (lb)
            {
                case 1:
                    child.Text = "大类浏览";
                    break;
                case 2:
                    child.Text = "中类浏览";
                    break;
                case 3:
                    child.Text = "小类浏览";
                    break;
                default:
                    break;
            }

            child.MdiParent = this;
            child.toolStripStatusLabel1.Text = "商品分类浏览";
            child.dataGridView.DataSource = dt;
            this.dataGridView1 = child.dataGridView;
            this.SetColumnsWidth();
            child.Show();
        }

        private void 新建中类ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (this.worker.qx == "低")
                return;

            AddZL az = new AddZL();
            az.ShowDialog(this);
        }

        private void 浏览中类ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.ViewFL(2);
        }

        private void 新建小类ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (this.worker.qx == "低")
                return;

            AddXL ax = new AddXL();
            ax.ShowDialog(this);
        }

        private void 浏览小类ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.ViewFL(3);
        }

        private void 价格汇总toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            Form_SelectDate sd = new Form_SelectDate();
            if (sd.ShowDialog(this) != DialogResult.OK)
                return;
            string s = "select goods.sj as 售价,";
            s += "sum(sale_mx.sl) as 数量,sum(sale_mx.je) as 金额 ";
            s += "from sale_mx join(goods,sale_db) ";
            s += "on(goods.tm=sale_mx.tm and sale_db.djh=sale_mx.djh) ";
            s += string.Format("where date(sale_db.rq)>='{0}' ", sd.dateTimePicker1.Value.ToShortDateString());
            s += string.Format("and date(sale_db.rq)<='{0}'", sd.dateTimePicker2.Value.ToShortDateString());
            s += "group by goods.sj";
            command.CommandText = s;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count < 1)
                return;
            Form_MDIChild mdi = new Form_MDIChild();
            mdi.MdiParent = this;
            mdi.Text = "价格汇总";
            mdi.toolStripStatusLabel1.Text = sd.dateTimePicker1.Value.ToShortDateString();
            mdi.toolStripStatusLabel1.Text += "--";
            mdi.toolStripStatusLabel1.Text += sd.dateTimePicker2.Value.ToShortDateString();
            this.dataGridView1 = mdi.dataGridView;
            this.dataGridView1.DataSource = dt;
            string sumsl;
            string sumje;
            sumsl = dt.Compute("sum(数量)", null).ToString();
            sumje = float.Parse(dt.Compute("sum(金额)", null).ToString()).ToString("N2");
            mdi.toolStripStatusLabel1.Text += " 合计：【" + sumsl + "】件，【" + sumje + "】元";
            this.dataGridView1.Columns["售价"].DefaultCellStyle.Format = "N2";
            this.dataGridView1.Columns["金额"].DefaultCellStyle.Format = "N2";
            SetColumnsWidth();
            mdi.Show();
        }

        private void 大类汇总ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_SelectDate sd = new Form_SelectDate();
            if (sd.ShowDialog(this) != DialogResult.OK)
                return;
            string s = "select fl.dnm as 大类,fl.pm as 品名,";
            s += "sum(sale_mx.sl) as 数量,sum(sale_mx.je) as 金额 ";
            s += "from sale_mx join(sale_db) using(djh) ";
            s += "join fl on( ";
            s += "fl.dnm=substring(sale_mx.tm,1,2)) where ";
            s += string.Format("date(sale_db.rq)>='{0}' ", sd.dateTimePicker1.Value.ToShortDateString());
            s += string.Format("and date(sale_db.rq)<='{0}'", sd.dateTimePicker2.Value.ToShortDateString());
            s += "group by fl.dnm";

            command.CommandText = s;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count < 1)
                return;
            Form_MDIChild mdi = new Form_MDIChild();
            mdi.MdiParent = this;
            mdi.Text = "大类汇总";
            mdi.toolStripStatusLabel1.Text = "时间范围：";
            mdi.toolStripStatusLabel1.Text += sd.dateTimePicker1.Value.ToShortDateString();
            mdi.toolStripStatusLabel1.Text += "--";
            mdi.toolStripStatusLabel1.Text += sd.dateTimePicker2.Value.ToShortDateString();
            this.dataGridView1 = mdi.dataGridView;
            this.dataGridView1.DataSource = dt;
            this.dataGridView1.Columns["金额"].DefaultCellStyle.Format = "N2";
            string sumsl, sumje;
            sumsl = dt.Compute("sum(数量)", null).ToString();
            sumje = float.Parse(dt.Compute("sum(金额)", null).ToString()).ToString("N2");
            mdi.toolStripStatusLabel1.Text += " 合计：【" + sumsl + "】件，【" + sumje + "】元";
            SetColumnsWidth();
            mdi.Show();
        }

        private void 中类汇总ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_SelectDate sd = new Form_SelectDate();
            if (sd.ShowDialog(this) != DialogResult.OK)
                return;
            string s = "select fl.dnm as 中类,fl.pm as 品名,";
            s += "sum(sale_mx.sl) as 数量,sum(sale_mx.je) as 金额 ";
            s += "from sale_mx join(sale_db,fl) ";
            s += "on(sale_db.djh=sale_mx.djh and substring(sale_mx.tm,1,4)=fl.dnm) ";
            s += string.Format("where date(sale_db.rq)>='{0}' ", sd.dateTimePicker1.Value.ToShortDateString());
            s += string.Format("and date(sale_db.rq)<='{0}'", sd.dateTimePicker2.Value.ToShortDateString());
            s += "group by fl.dnm";
            command.CommandText = s;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count < 1)
                return;
            Form_MDIChild mdi = new Form_MDIChild();
            mdi.MdiParent = this;
            mdi.Text = "中类汇总";
            mdi.toolStripStatusLabel1.Text = "时间范围：";
            mdi.toolStripStatusLabel1.Text += sd.dateTimePicker1.Value.ToShortDateString();
            mdi.toolStripStatusLabel1.Text += "--";
            mdi.toolStripStatusLabel1.Text += sd.dateTimePicker2.Value.ToShortDateString();
            this.dataGridView1 = mdi.dataGridView;
            this.dataGridView1.DataSource = dt;
            this.dataGridView1.Columns["金额"].DefaultCellStyle.Format = "N2";
            string sumsl, sumje;
            sumsl = dt.Compute("sum(数量)", null).ToString();
            sumje = float.Parse(dt.Compute("sum(金额)", null).ToString()).ToString("N2");
            mdi.toolStripStatusLabel1.Text += " 合计：【" + sumsl + "】件，【" + sumje + "】元";
            SetColumnsWidth();
            mdi.Show();
        }

        private void 小类汇总ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_SelectDate sd = new Form_SelectDate();
            if (sd.ShowDialog(this) != DialogResult.OK)
                return;
            string s = "select fl.dnm as 小类,fl.pm as 品名,";
            s += "sum(sale_mx.sl) as 数量,sum(sale_mx.je) as 金额 ";
            s += "from sale_mx join(sale_db,fl) ";
            s += "on(sale_db.djh=sale_mx.djh and substring(sale_mx.tm,1,6)=fl.dnm) ";
            s += string.Format("where date(sale_db.rq)>='{0}' ", sd.dateTimePicker1.Value.ToShortDateString());
            s += string.Format("and date(sale_db.rq)<='{0}'", sd.dateTimePicker2.Value.ToShortDateString());
            s += "group by fl.dnm";
            command.CommandText = s;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count < 1)
                return;
            Form_MDIChild mdi = new Form_MDIChild();
            mdi.MdiParent = this;
            mdi.Text = "小类汇总";
            mdi.toolStripStatusLabel1.Text = "时间范围：";
            mdi.toolStripStatusLabel1.Text += sd.dateTimePicker1.Value.ToShortDateString();
            mdi.toolStripStatusLabel1.Text += "--";
            mdi.toolStripStatusLabel1.Text += sd.dateTimePicker2.Value.ToShortDateString();
            this.dataGridView1 = mdi.dataGridView;
            this.dataGridView1.DataSource = dt;
            this.dataGridView1.Columns["金额"].DefaultCellStyle.Format = "N2";
            string sumsl, sumje;
            sumsl = dt.Compute("sum(数量)", null).ToString();
            sumje = float.Parse(dt.Compute("sum(金额)", null).ToString()).ToString("N2");
            mdi.toolStripStatusLabel1.Text += " 合计：【" + sumsl + "】件，【" + sumje + "】元";
            SetColumnsWidth();
            mdi.Show();
        }

        private void 清空入库toolStripMenuItem13_Click_工具(object sender, EventArgs e)
        {
            //删除入库操作历史记录，但不整理库存数据。
            if (this.worker.qx != "高")
                return;
            if (MessageBox.Show("将清空全部入库历史（工具）？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) != DialogResult.OK)
                return;
            command.CommandText = "delete from rk_temp";
            int ret = command.ExecuteNonQuery();
            MessageBox.Show("已清空" + ret + "条记录！");
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            Form_rk_工具 rk = new Form_rk_工具();
            rk.ShowDialog(this);
        }

        private void 入库明细ToolStripMenuItem_Click_工具(object sender, EventArgs e)
        {
            if (this.worker.qx == "低") return;
            DateTime start, end;
            if (sender is ToolStripButton)
            {
                start = end = DateTime.Now;
            }
            else
            {
                Form_SelectDate sd = new Form_SelectDate();
                if (sd.ShowDialog(this) != DialogResult.OK)
                    return;
                start = sd.dateTimePicker1.Value.Date;
                end = sd.dateTimePicker2.Value.Date;
            }
            string s = string.Format("select rk_temp.tm as 条码,goods.pm as 品名,rk_temp.sl as 数量,goods.sj as 售价,rk_temp.sl*goods.sj as 金额,rk_temp.rq as 日期,rk_temp.czy as 操作员 from rk_temp join goods using(tm) where date(rk_temp.rq)>='{0}' and date(rk_temp.rq)<='{1}' order by rk_temp.rq asc",
                start.ToShortDateString(),
                end.ToShortDateString());
            command.CommandText = s;
            MySqlDataAdapter a = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            a.Fill(dt);

            Form_MDIChild child = new Form_MDIChild();
            child.MdiParent = this;
            child.Text = start.ToShortDateString() + "--" + end.ToShortDateString() + "【入库明细--工具】";
            child.items.AddRange(new string[] { "售价", "条码", "品名" });
            child.dataGridView.DataSource = dt;
            this.dataGridView1 = child.dataGridView;
            this.dataGridView1.Columns["日期"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            this.dataGridView1.Columns["售价"].DefaultCellStyle.Format = "N2";
            this.dataGridView1.Columns["金额"].DefaultCellStyle.Format = "N2";
            this.SetColumnsWidth();
            int sumsl = 0;
            float je = 0f;
            if (dt.Rows.Count > 0)
            {
                sumsl = int.Parse(dt.Compute("sum(数量)", null).ToString());
                je = float.Parse(dt.Compute("sum(金额)", null).ToString());
            }
            child.toolStripStatusLabel1.Text = "共【" + dt.Rows.Count + "】条记录，【" + sumsl + "】件商品，【" + je.ToString("N2") + "】元";
            this.toolStripMenuItem_真实入库.Enabled = true;
            child.Show();
        }

        private void 入库汇总ToolStripMenuItem_Click_工具(object sender, EventArgs e)
        {
            if (this.worker.qx == "低") return;

            DateTime start, end;
            if (sender is ToolStripButton)
            {
                start = end = DateTime.Now;
            }
            else
            {
                Form_SelectDate sd = new Form_SelectDate();
                if (sd.ShowDialog(this) != DialogResult.OK)
                    return;
                start = sd.dateTimePicker1.Value.Date;
                end = sd.dateTimePicker2.Value.Date;
            }

            string s = string.Format("select rk_temp.tm as 条码,goods.pm as 品名,sum(rk_temp.sl) as 数量,goods.sj as 售价,goods.sj*sum(rk_temp.sl) as 金额,rk_temp.rq as 日期,rk_temp.czy as 操作员 from rk_temp join goods using(tm) where date(rk_temp.rq)>='{0}' and date(rk_temp.rq)<='{1}' group by rk_temp.tm order by rk_temp.rq asc",
                start.ToShortDateString(),
                end.ToShortDateString());
            command.CommandText = s;
            MySqlDataAdapter a = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            a.Fill(dt);

            Form_MDIChild child = new Form_MDIChild();
            child.MdiParent = this;
            child.Text = start.ToShortDateString() + "--" + end.ToShortDateString() + "【入库汇总--工具】";
            child.items.AddRange(new string[] { "条码", "品名" });
            child.dataGridView.DataSource = dt;
            this.dataGridView1 = child.dataGridView;
            this.dataGridView1.Columns["日期"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            this.dataGridView1.Columns["售价"].DefaultCellStyle.Format = "N2";
            this.dataGridView1.Columns["金额"].DefaultCellStyle.Format = "N2";
            this.SetColumnsWidth();
            int sumsl = 0;
            float je = 0f;
            if (dt.Rows.Count > 0)
            {
                sumsl = int.Parse(dt.Compute("sum(数量)", null).ToString());
                je = float.Parse(dt.Compute("sum(金额)", null).ToString());
            }
            child.toolStripStatusLabel1.Text = "共【" + dt.Rows.Count + "】种商品，【" + sumsl + "】件商品，【" + je.ToString("N2") + "】元";
            this.toolStripMenuItem_真实入库.Enabled = true;
            child.Show();
        }

        private void menuStrip1_ItemAdded(object sender, ToolStripItemEventArgs e)
        {
            if (e.Item.Text.Length == 0)
                e.Item.Visible = false;
        }

        private void 打印条码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!this.EnabledPrint) return;
            Form_Print_BarCode pb = new Form_Print_BarCode();
            pb.ShowDialog(this);
        }

        private void toolStripMenuItem16工时设定_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem15业绩汇总_Click(object sender, EventArgs e)
        {
            Form_SelectDate sd = new Form_SelectDate();
            if (sd.ShowDialog(this) != DialogResult.OK)
                return;
            string sql = "select sum(sale_mx.sl) as `数量`, ";
            sql += "sum(sale_mx.je) as `金额`, ";
            sql += "worker.xm as `收银员` from sale_mx ";
            sql += "join sale_db using(djh) ";
            sql += "join worker on (worker.bh=sale_mx.syy) ";
            sql += "where date(sale_db.rq)>='" + sd.dateTimePicker1.Value.ToShortDateString() + "' ";
            sql += "and date(sale_db.rq)<='" + sd.dateTimePicker2.Value.ToShortDateString() + "' ";
            sql += "group by sale_mx.syy order by `金额`";
            command.CommandText = sql;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            Form_MDIChild mdi = new Form_MDIChild();
            mdi.Text = "业绩汇总";
            mdi.MdiParent = this;
            mdi.dataGridView.DataSource = dt;
            this.dataGridView1 = mdi.dataGridView;
            this.dataGridView1.Columns["金额"].DefaultCellStyle.Format = "N2";
            mdi.toolStripStatusLabel1.Text = "查询所有员工的业绩，从";
            mdi.toolStripStatusLabel1.Text += sd.dateTimePicker1.Value.ToShortDateString() + "至";
            mdi.toolStripStatusLabel1.Text += sd.dateTimePicker2.Value.ToShortDateString();
            this.SetColumnsWidth();
            mdi.Show();
        }

        private void toolStripMenuItem15改库存负数为零_Click(object sender, EventArgs e)
        {
            if (this.worker.qx == "低")
                return;
            if (MessageBox.Show("库存负数将全部置为零？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) != DialogResult.OK)
                return;
            string sql = "update goods set kc=0 where kc<0";
            command.CommandText = sql;
            int ret = command.ExecuteNonQuery();
            MessageBox.Show(string.Format("共{0}条库存负数已置为零！", ret));
        }

        private void toolStripMenuItem16扫描自动入库_Click(object sender, EventArgs e)
        {
            if (this.worker.qx == "低")
                return;
            var form = new Form_rk_auto();
            form.ShowDialog(this);
        }

        private void toolStripMenuItem_大类_Click(object sender, EventArgs e)
        {
            if (this.worker.qx == "低")
                return;
            string s = "select fl.dnm as 大类,fl.pm as 品名,";
            s += "sum(goods.kc) as 数量,sum(goods.sj*goods.kc) as 金额 ";
            s += "from goods join(fl) ";
            s += "on(fl.dnm=substring(goods.tm,1,2)) ";
            s += "group by fl.dnm order by fl.dnm";
            command.CommandText = s;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            Form_MDIChild mdi = new Form_MDIChild();
            mdi.MdiParent = this;
            mdi.Text = "库存大类汇总";
            this.dataGridView1 = mdi.dataGridView;
            this.dataGridView1.DataSource = dt;
            this.dataGridView1.Columns["金额"].DefaultCellStyle.Format = "N2";
            string sl = "0";
            float je = 0.0f;
            if (mdi.dataGridView.Rows.Count > 0)
            {
                sl = dt.Compute("sum(数量)", null).ToString();
                je = float.Parse(dt.Compute("sum(金额)", null).ToString());
            }
            mdi.toolStripStatusLabel1.Text = string.Format("合计：【{0}】件，【{1}】元", sl, je.ToString("N2"));

            SetColumnsWidth();
            mdi.Show();
        }

        private void toolStripMenuItem_中类_Click(object sender, EventArgs e)
        {
            if (this.worker.qx == "低")
                return;
            string s = "select fl.dnm as 中类,fl.pm as 品名,";
            s += "sum(goods.kc) as 数量,sum(goods.sj*goods.kc) as 金额 ";
            s += "from goods join(fl) ";
            s += "on(fl.dnm=substring(goods.tm,1,4)) ";
            s += "group by fl.dnm order by fl.dnm";
            command.CommandText = s;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            Form_MDIChild mdi = new Form_MDIChild();
            mdi.MdiParent = this;
            mdi.Text = "库存中类汇总";
            this.dataGridView1 = mdi.dataGridView;
            this.dataGridView1.DataSource = dt;
            this.dataGridView1.Columns["金额"].DefaultCellStyle.Format = "N2";
            string sl = "0";
            float je = 0.0f;
            if (mdi.dataGridView.Rows.Count > 0)
            {
                sl = dt.Compute("sum(数量)", null).ToString();
                je = float.Parse(dt.Compute("sum(金额)", null).ToString());
            }
            mdi.toolStripStatusLabel1.Text = string.Format("合计：【{0}】件，【{1}】元", sl, je.ToString("N2"));
            SetColumnsWidth();
            mdi.Show();
        }

        private void toolStripMenuItem_小类_Click(object sender, EventArgs e)
        {
            if (this.worker.qx == "低")
                return;
            string s = "select fl.dnm as 小类,fl.pm as 品名,";
            s += "sum(goods.kc) as 数量,sum(goods.sj*goods.kc) as 金额 ";
            s += "from goods join(fl) ";
            s += "on(fl.dnm=substring(goods.tm,1,6)) ";
            s += "group by fl.dnm order by fl.dnm";
            command.CommandText = s;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            Form_MDIChild mdi = new Form_MDIChild();
            mdi.MdiParent = this;
            mdi.Text = "库存中小汇总";
            this.dataGridView1 = mdi.dataGridView;
            this.dataGridView1.DataSource = dt;
            this.dataGridView1.Columns["金额"].DefaultCellStyle.Format = "N2";
            string sl = "0";
            float je = 0f;
            if (mdi.dataGridView.Rows.Count > 0)
            {
                sl = dt.Compute("sum(数量)", null).ToString();
                je = float.Parse(dt.Compute("sum(金额)", null).ToString());
            }
            mdi.toolStripStatusLabel1.Text = string.Format("合计：【{0}】件，【{1}】元", sl, je.ToString("N2"));
            SetColumnsWidth();
            mdi.Show();
        }

        private void toolStripMenuItem_真实入库_Click(object sender, EventArgs e)
        {
            if (this.worker.qx != "高")
                return;

            Form_MDIChild mdi = this.ActiveMdiChild as Form_MDIChild;
            if (mdi == null)
                throw new NullReferenceException();

            if (!(mdi.Text.Contains("【入库明细--工具】") || mdi.Text.Contains("【入库汇总--工具】")))
                return;

            this.dataGridView1 = mdi.dataGridView;
            if (this.dataGridView1.Rows.Count < 1)
                return;
            if (MessageBox.Show("确定要将" + dataGridView1.Rows.Count + "条数据进行入库操作吗？"
                , "提示",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) != DialogResult.OK)
                return;
            string sql;
            string tm, sl, rq;
            MySqlTransaction tr = Connection.BeginTransaction();
            try
            {
                foreach (DataGridViewRow row in this.dataGridView1.Rows)
                {
                    tm = row.Cells["条码"].Value.ToString();
                    sl = row.Cells["数量"].Value.ToString();
                    rq = row.Cells["日期"].Value.ToString();
                    sql = string.Format("insert into rk(rq,tm,czy,sl) values('{0}','{1}','{2}',{3})",
                        rq, tm, worker.bh, sl);
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                    sql = string.Format("update goods set kc=kc+{0} where tm='{1}'", sl, tm);
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                }

                tr.Commit();
            }
            catch
            {
                tr.Rollback();
                MessageBox.Show("入库操作失败！回滚。");
                return;
            }
            MessageBox.Show("入库操作成功！\r\n更新" + dataGridView1.Rows.Count + "条库存数据。",
                "提示",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);
        }

        private void toolStripButton_刷新_Click(object sender, EventArgs e)
        {
            Form_MDIChild mdi = this.ActiveMdiChild as Form_MDIChild;
            if (mdi != null)
                mdi.RefreshData();
        }

        private void toolStripButton打印_Click(object sender, EventArgs e)
        {
            this.打印条码ToolStripMenuItem_Click(null, null);
        }

        private void toolStripButton_新建商品_Click(object sender, EventArgs e)
        {
            this.新建ToolStripMenuItem_Click(null, null);
        }

        private void 赠品库存浏览ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string sql = "select tm as 条码,pm as 品名,jj as 进价,sj as 售价,kc as 库存 from zp_goods";
            command.CommandText = sql;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            Form_MDIChild child = new Form_MDIChild();
            child.Text = "赠品库存浏览";
            child.MdiParent = this;
            this.dataGridView1 = child.dataGridView;
            this.dataGridView1.DataSource = dt;

            child.items.AddRange(new string[] { "售价", "条码", "品名", "库存", "供货商" });
            this.dataGridView1 = child.dataGridView;
            this.dataGridView1.DataSource = dt;
            this.dataGridView1.Columns["售价"].DefaultCellStyle.Format = "N2";
            if (this.dataGridView1.Columns["进价"] != null)
                this.dataGridView1.Columns["进价"].DefaultCellStyle.Format = "N2";

            sql = "select ifnull(sum(kc),0) from goods";
            command.CommandText = sql;
            int kc = int.Parse(command.ExecuteScalar().ToString());
            sql = "select ifnull(sum(jj*kc),0.0) from goods";
            command.CommandText = sql;
            float jjkc = float.Parse(command.ExecuteScalar().ToString());
            sql = "select ifnull(sum(sj*kc),0.0) from goods";
            command.CommandText = sql;
            float sjkc = float.Parse(command.ExecuteScalar().ToString());
            sql = "select ifnull(sum(sj*zq*kc),0.0) from goods";
            command.CommandText = sql;
            float zqkc = float.Parse(command.ExecuteScalar().ToString());
            sql = "select ifnull(sum(sj*hyzq*kc),0) from goods";
            command.CommandText = sql;
            float hyzqkc = float.Parse(command.ExecuteScalar().ToString());
            child.toolStripStatusLabel1.Text = "当前共【" + dt.Rows.Count + "】条记录";
            child.toolStripStatusLabel1.Text += ",共【" + kc.ToString() + "】件商品";
            if (this.worker.qx != "低")
            {
                child.toolStripStatusLabel1.Text += ",总成本【" + jjkc.ToString("N2");
                child.toolStripStatusLabel1.Text += "】,按售价总额【" + sjkc.ToString("N2");
                child.toolStripStatusLabel1.Text += "】,折扣后总额【" + zqkc.ToString("N2");
                child.toolStripStatusLabel1.Text += "】,会员折扣后总额【" + hyzqkc.ToString("N2") + "】";
            }
            this.SetColumnsWidth();
            child.Show();
        }

        private void 赠品出库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.worker.qx == "低")
                return;
            Form_zp_ck zpck = new Form_zp_ck();
            zpck.ShowDialog(this);
        }

        private void 赠品入库ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.worker.qx == "低")
                return;
            Form_zp_rk zprk = new Form_zp_rk();
            zprk.Text = "赠品入库";
            zprk.ShowDialog(this);
        }

        private void 赠品入库历史ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_SelectDate sd = new Form_SelectDate();
            if (sd.ShowDialog(this) != DialogResult.OK)
                return;

            string s = string.Format("select zp_rk.tm as 条码," +
            "zp_goods.pm as 品名," +
            "zp_rk.sl as 数量," +
            "zp_goods.sj as 售价," +
            "zp_rk.sl*zp_goods.sj as 金额," +
            "zp_rk.rq as 日期," +
            "zp_rk.czy as 操作员 " +
            "from zp_rk join zp_goods using(tm) where date(zp_rk.rq)>='{0}' and date(zp_rk.rq)<='{1}' order by zp_rk.rq asc",
                sd.dateTimePicker1.Value.Date.ToShortDateString(),
                sd.dateTimePicker2.Value.Date.ToShortDateString());
            command.CommandText = s;
            MySqlDataAdapter a = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            a.Fill(dt);

            Form_MDIChild child = new Form_MDIChild();
            child.MdiParent = this;
            child.Text = sd.dateTimePicker1.Value.Date.ToShortDateString() + "--" + sd.dateTimePicker2.Value.Date.ToShortDateString() + "【赠品入库明细】";
            child.items.AddRange(new string[] { "售价", "条码", "品名", "数量" });
            child.dataGridView.DataSource = dt;
            this.dataGridView1 = child.dataGridView;
            this.dataGridView1.Columns["日期"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            this.dataGridView1.Columns["售价"].DefaultCellStyle.Format = "N2";
            this.dataGridView1.Columns["金额"].DefaultCellStyle.Format = "N2";
            this.SetColumnsWidth();
            int sumsl = 0;
            float je = 0f;
            if (dt.Rows.Count > 0)
            {
                sumsl = int.Parse(dt.Compute("sum(数量)", null).ToString());
                je = float.Parse(dt.Compute("sum(金额)", null).ToString());
            }
            child.toolStripStatusLabel1.Text = "共【" + dt.Rows.Count + "】条记录，【" + sumsl + "】件赠品，【" + je.ToString("N2") + "】元";
            child.Show();
        }

        private void toolStripMenuItem新建赠品_Click(object sender, EventArgs e)
        {
            if (this.worker.qx == "低")
                return;
            Form_NewGoods ng = new Form_NewGoods("zp_goods");
            ng.Text = "新建赠品";
            ng.ShowDialog(this);
        }

        private void 赠品出库历史toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Form_SelectDate sd = new Form_SelectDate();
            if (sd.ShowDialog(this) != DialogResult.OK)
                return;
            string s = string.Format("select zp_ck.tm as 条码," +
             "zp_goods.pm as 品名," +
             "zp_ck.sl as 数量," +
             "zp_goods.sj as 售价," +
             "zp_ck.sl*zp_goods.sj as 金额," +
             "zp_ck.rq as 日期," +
             "zp_ck.czy as 操作员 " +
             "from zp_ck join zp_goods using(tm) where date(zp_ck.rq)>='{0}' and date(zp_ck.rq)<='{1}' order by zp_ck.rq asc",
                 sd.dateTimePicker1.Value.Date.ToShortDateString(),
                 sd.dateTimePicker2.Value.Date.ToShortDateString());
            command.CommandText = s;
            MySqlDataAdapter a = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            a.Fill(dt);

            Form_MDIChild child = new Form_MDIChild();
            child.MdiParent = this;
            child.Text = sd.dateTimePicker1.Value.Date.ToShortDateString() + "--" + sd.dateTimePicker2.Value.Date.ToShortDateString() + "赠品出库明细";
            child.items.AddRange(new string[] { "条码", "品名" });
            child.dataGridView.DataSource = dt;
            this.dataGridView1 = child.dataGridView;

            this.SetColumnsWidth();
            child.toolStripStatusLabel1.Text = "共【" + dt.Rows.Count + "】条记录";
            child.Show();
        }

        private void toolStripMenuItem_价格分类_Click(object sender, EventArgs e)
        {
            string sql = "select sj as 售价,sum(kc) as 数量,sj*sum(kc) as 金额 from goods where kc!=0 group by sj order by sj asc";
            command.CommandText = sql;
            MySqlDataAdapter a = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            a.Fill(dt);
            Form_MDIChild child = new Form_MDIChild();
            child.MdiParent = this;
            child.Text = "按价格浏览库存";
            child.items.AddRange(new string[] { "售价" });
            this.dataGridView1 = child.dataGridView;
            child.dataGridView.DataSource = dt;
            string sumsl;
            string sumje;
            sumsl = dt.Compute("sum(数量)", null).ToString();
            sumje = float.Parse(dt.Compute("sum(金额)", null).ToString()).ToString("N2");
            child.toolStripStatusLabel1.Text = " 合计：【" + sumsl + "】件，【" + sumje + "】元";
            this.dataGridView1.Columns["售价"].DefaultCellStyle.Format = "N2";
            this.dataGridView1.Columns["金额"].DefaultCellStyle.Format = "N2";
            this.SetColumnsWidth();
            child.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)/////出库汇总
        {
            Form_SelectDate sd = new Form_SelectDate();
            if (sd.ShowDialog(this) != DialogResult.OK)
                return;
            string s = string.Format("select ck.tm as 条码,goods.pm as 品名,sum(ck.sl) as 数量,goods.sj as 售价,goods.sj*sum(ck.sl) as 金额,ck.rq as 日期,ck.czy as 操作员 from ck join goods using(tm) where date(ck.rq)>='{0}' and date(ck.rq)<='{1}' group by ck.tm order by ck.rq asc",
                sd.dateTimePicker1.Value.Date.ToShortDateString(),
                sd.dateTimePicker2.Value.Date.ToShortDateString());
            command.CommandText = s;
            MySqlDataAdapter a = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            a.Fill(dt);

            Form_MDIChild child = new Form_MDIChild();
            child.MdiParent = this;
            child.Text = sd.dateTimePicker1.Value.Date.ToShortDateString() + "--" + sd.dateTimePicker2.Value.Date.ToShortDateString() + "【出库汇总】";
            child.items.AddRange(new string[] { "售价", "数量", "条码", "品名" });
            child.dataGridView.DataSource = dt;
            this.dataGridView1 = child.dataGridView;
            this.dataGridView1.Columns["日期"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            this.dataGridView1.Columns["售价"].DefaultCellStyle.Format = "N2";
            this.dataGridView1.Columns["金额"].DefaultCellStyle.Format = "N2";
            this.SetColumnsWidth();
            int sumsl = 0;
            float je = 0f;
            if (dt.Rows.Count > 0)
            {
                sumsl = int.Parse(dt.Compute("sum(数量)", null).ToString());
                je = float.Parse(dt.Compute("sum(金额)", null).ToString());
            }
            child.toolStripStatusLabel1.Text = "共【" + dt.Rows.Count + "】种商品，【" + sumsl + "】件商品，【" + je.ToString("N2") + "】元";
            child.Show();
        }

        private void toolStripButton_本日入库明细_Click(object sender, EventArgs e)
        {
            var now = DateTime.Now.ToShortDateString();

            // if (this.worker.qx == "低") return;
            string s = string.Format("select rk.tm as 条码,goods.pm as 品名,rk.sl as 数量,goods.sj as 售价,rk.sl*goods.sj as 金额,rk.rq as 日期,rk.czy as 操作员 from rk join goods using(tm) where date(rk.rq)>='{0}' and date(rk.rq)<='{1}' order by rk.rq asc",
                now,
                now);
            command.CommandText = s;
            MySqlDataAdapter a = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            a.Fill(dt);

            Form_MDIChild child = new Form_MDIChild();
            child.MdiParent = this;
            child.Text = now + "--" + now + "【入库明细】";
            child.items.AddRange(new string[] { "售价", "条码", "品名", "数量" });
            child.dataGridView.DataSource = dt;
            this.dataGridView1 = child.dataGridView;
            this.dataGridView1.Columns["日期"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            this.dataGridView1.Columns["售价"].DefaultCellStyle.Format = "N2";
            this.dataGridView1.Columns["金额"].DefaultCellStyle.Format = "N2";
            this.SetColumnsWidth();
            int sumsl = 0;
            float je = 0f;
            if (dt.Rows.Count > 0)
            {
                sumsl = int.Parse(dt.Compute("sum(数量)", null).ToString());
                je = float.Parse(dt.Compute("sum(金额)", null).ToString());
            }
            child.toolStripStatusLabel1.Text = "共【" + dt.Rows.Count + "】条记录，【" + sumsl + "】件商品，【" + je.ToString("N2") + "】元";
            child.Show();
        }

        private void 添加备注ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var input = new Form_Input();
            input.Icon = Server.Properties.Resources.yuan;
            if (input.ShowDialog() != DialogResult.OK) return;

            var date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var s = string.Format("insert into bz(rq,nr) values('{0}','{1}')", date, input.Input);
            command.CommandText = s;
            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("添加备注成功！");
            }
            catch (MySqlException me)
            {
                MessageBox.Show("添加备注失败！", me.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 查看备注ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sd = new Form_SelectDate();
            if (sd.ShowDialog(this) != DialogResult.OK) return;
            var start = sd.dateTimePicker1.Value.ToShortDateString();
            var end = sd.dateTimePicker2.Value.ToShortDateString();

            var s = string.Format("select rq as 日期,nr as 内容 from bz where date(rq)>='{0}' and date(rq)<='{1}'", start, end);
            command.CommandText = s;
            var a = new MySqlDataAdapter(command);
            var dt = new DataTable();
            a.Fill(dt);

            var child = new Form_MDIChild();
            child.Text = "查看备注";
            child.MdiParent = this;
            child.toolStripStatusLabel1.Text = "日期：" + start + "--" + end;
            child.dataGridView.DataSource = dt;
            child.dataGridView.Columns["日期"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";

            child.dataGridView.Columns[0].Width = 160;
            child.dataGridView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            child.dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            child.Show();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            this.添加备注ToolStripMenuItem_Click(null, null);
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            this.查看备注ToolStripMenuItem_Click(null, null);
        }

        private void 打印赠品ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!this.EnabledPrint) return;
            var input = new Form_Input();
            input.Text = "请输入赠品价值：";
            if (input.ShowDialog(this) != DialogResult.OK) return;
            var zp_format = new LabelFormat(Application.StartupPath + "\\ZengPing.btw");
            Seagull.BarTender.Print.Messages msgs;
            var zp_document = engine.Documents.Open(zp_format, out msgs);
            zp_document.PrintSetup.PrinterName = Form_main.printer;
            zp_document.SubStrings["shop"].Value = Form_main.shop;
            zp_document.SubStrings["tm"].Value = input.Input;
            zp_document.Print();
            zp_document.Close(SaveOptions.DoNotSaveChanges);
        }

        private void 打印回扁ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!this.EnabledPrint) return;
            var input = new Form_Input();
            input.Text = "请输入打印份数";
            if (input.ShowDialog(this) != DialogResult.OK) return;
            var hb_format = new LabelFormat(Application.StartupPath + "\\HuiBian.btw");
            Seagull.BarTender.Print.Messages msgs;
            var hb_document = engine.Documents.Open(hb_format, out msgs);
            hb_document.PrintSetup.PrinterName = Form_main.printer;
            hb_document.SubStrings["shop"].Value = Form_main.shop;
            hb_document.SubStrings["fs"].Value = input.Input;
            hb_document.Print();
            hb_document.Close(SaveOptions.DoNotSaveChanges);
        }
        private void 导出XLS按价格toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (this.worker.qx == "低")
                return;

            SaveFileDialog sd = new SaveFileDialog();
            sd.DefaultExt = "xls";
            sd.AddExtension = true;
            sd.Title = "指定要要导出的文件名及存放位置";
            sd.Filter = "文件文件(*.xls)|*.xls";
            if (sd.ShowDialog() == DialogResult.OK)
            {
                HSSFWorkbook book = new HSSFWorkbook();
                ISheet sheet = book.CreateSheet("导出的库存文件");
                string s = "select sj,sum(kc) as kc from goods where kc!=0 group by sj";
                command.CommandText = s;
                MySqlDataReader dr = command.ExecuteReader();
                int i = 0;

                ICellStyle style = book.CreateCellStyle();
                IDataFormat format = book.CreateDataFormat();
                style.DataFormat = format.GetFormat("0.00");
                style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;

                ICellStyle style_all = book.CreateCellStyle();
                style_all.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;

                while (dr.Read())
                {
                    IRow row = sheet.CreateRow(i);

                    //sj
                    ICell cell = null;
                    cell = row.CreateCell(0);
                    cell.CellStyle = style;
                    cell.SetCellValue(dr.GetFloat(0));
                    //kc
                    cell = row.CreateCell(1);
                    cell.CellStyle = style_all;
                    cell.SetCellValue(dr.GetInt32(1));
                    i++;
                }
                dr.Close();
                sheet.SetColumnWidth(0, 8 * 256);
                sheet.SetColumnWidth(1, 6 * 256);
                FileStream fs = new FileStream(sd.FileName, FileMode.Create, FileAccess.Write);
                book.Write(fs);
                fs.Close();
                MessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
