using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Server
{
    /// <summary>
    /// 导入电子表格xls文件,自动核对库存数据
    /// </summary>
    public partial class Form_zdhd : Form
    {
        public Form_zdhd()
        {
            InitializeComponent();

            this.Icon = Properties.Resources.yuan;
        }

        private void button_kc_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.AddExtension = true;
            o.Filter = "电子表格文件(*.xls)|*.xls";
            o.Title = "打开电子表格文件";
            if (o.ShowDialog(this) != DialogResult.OK)
                return;
            this.textBox_kc.Text = o.FileName;
        }

        private void button_pd_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.AddExtension = true;
            o.Filter = "电子表格文件(*.xls)|*.xls";
            o.Title = "打开电子表格文件";
            if (o.ShowDialog(this) != DialogResult.OK)
                return;
            this.textBox_pd.Text = o.FileName;
        }

        private void button_start_hd_Click(object sender, EventArgs e)
        {
            if (this.textBox_kc.TextLength < 2 || this.textBox_pd.TextLength < 2)
                return;

            if (!this.CreateTempTable())
                return;

            if (!this.ImportExcelTable() || !this.AutoCheckSL())
            {
                MessageBox.Show("根据提示改正表格中的错误后继续操作", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private bool CreateTempTable()
        {
            string kc = "drop table if exists kc; create table kc(tm varchar(20) not null primary key,sl int(11) not null default 0)";
            string pd = "drop table if exists pd; create table pd(tm varchar(20) not null primary key,sl int(11) not null default 0)";
            try
            {
                Form_main.Command.CommandText = kc;
                Form_main.Command.ExecuteNonQuery();
                Form_main.Command.CommandText = pd;
                Form_main.Command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                MessageBox.Show("创建临时数据库失败！");
                return false;
            }
        }
        private bool ImportExcelTable()
        {
            FileStream fs_kc = new FileStream(this.textBox_kc.Text, FileMode.Open, FileAccess.Read);
            HSSFWorkbook book_kc = new HSSFWorkbook(fs_kc);
            FileStream fs_pd = new FileStream(this.textBox_pd.Text, FileMode.Open, FileAccess.Read);
            HSSFWorkbook book_pd = new HSSFWorkbook(fs_pd);
            ////库存
            ISheet sheet_kc = book_kc.GetSheetAt(0);
            for (int i = 0; i < sheet_kc.LastRowNum + 1; i++)
            {
                this.toolStripStatusLabel1.Text = "正在处理库存数据第 " + (i + 1) + " 条记录……";
                Application.DoEvents();
                IRow row = sheet_kc.GetRow(i);
                string tm;
                switch (row.GetCell(0).CellType)
                {
                    case CellType.Numeric:
                        tm = row.GetCell(0).NumericCellValue.ToString();
                        break;
                    case CellType.String:
                        tm = row.GetCell(0).StringCellValue;
                        break;
                    default:
                        MessageBox.Show("单元格的格式超出预料(库存数据" + (i + 1) + "行1列)");
                        return false;
                }

                string sl;
                try
                {
                    sl = row.GetCell(1).NumericCellValue.ToString();
                }
                catch
                {
                    MessageBox.Show("单元格的格式超出预料(库存数据" + (i + 1) + "行2列)");
                    return false;
                }
                string s = "insert into kc(tm,sl) values('" + tm + "'," + sl + ")";
                Form_main.Command.CommandText = s;
                Form_main.Command.ExecuteNonQuery();
            }

            //////盘点    
            if (!this.AutoCheckTM())
                return false;
            ISheet sheet_pd = book_pd.GetSheetAt(0);
            for (int i = 0; i < sheet_pd.LastRowNum + 1; i++)
            {
                this.toolStripStatusLabel1.Text = "正在处理盘点数据第 " + (i + 1) + " 条记录……";
                Application.DoEvents();
                IRow row = sheet_pd.GetRow(i);
                string tm;
                switch (row.GetCell(0).CellType)
                {
                    case CellType.Numeric:
                        tm = row.GetCell(0).NumericCellValue.ToString();
                        break;
                    case CellType.String:
                        tm = row.GetCell(0).StringCellValue;
                        break;
                    default:
                        MessageBox.Show("单元格的格式超出预料(盘点数据" + (i + 1) + "行1列)");
                        return false;
                }
                string sl;
                try
                {
                    sl = row.GetCell(1).NumericCellValue.ToString();
                }
                catch
                {
                    MessageBox.Show("单元格的格式超出预料(盘点数据" + (i + 1) + "行2列)");
                    return false;
                }

                string s = "select count(*) from pd where tm='" + tm + "'";
                Form_main.Command.CommandText = s;
                MySqlDataReader dr = Form_main.Command.ExecuteReader();
                dr.Read();
                int count = dr.GetInt32(0);
                dr.Close();
                if (count > 0)
                {
                    s = "update pd set sl=sl+" + sl + " where tm='" + tm + "'";
                }
                else
                {
                    s = "insert into pd(tm,sl) values('" + tm + "'," + sl + ")";
                }
                Form_main.Command.CommandText = s;
                Form_main.Command.ExecuteNonQuery();
            }

            fs_kc.Close();
            fs_pd.Close();

            this.toolStripStatusLabel1.Text = "数据处理完成，即将进行核对……";
            Application.DoEvents();
            return true;
        }
        private bool AutoCheckTM()
        {
            this.toolStripStatusLabel1.Text = "正在检查商品条码……";
            Application.DoEvents();
            //检查盘点表中的条码是否存在于数据库中，防止新商品及笔误
            FileStream fs = new FileStream(this.textBox_pd.Text, FileMode.Open, FileAccess.Read);
            HSSFWorkbook book = new HSSFWorkbook(fs);
            ISheet sheet = book.GetSheetAt(0);
            string tm;
            for (int i = 0; i < sheet.LastRowNum + 1; i++)
            {
                IRow row = sheet.GetRow(i);
                ICell cell = row.GetCell(0);
                switch (cell.CellType)
                {
                    case CellType.Numeric:
                        tm = cell.NumericCellValue.ToString();
                        break;
                    case CellType.String:
                        tm = cell.StringCellValue;
                        break;
                    default:
                        MessageBox.Show("条码校验错误,第" + (i + 1).ToString() + "行", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                }
                this.toolStripStatusLabel1.Text = "校验条码:" + tm;
                Application.DoEvents();
                string s = "select count(*) from kc where tm='" + tm + "'";
                Form_main.Command.CommandText = s;
                MySqlDataReader dr = Form_main.Command.ExecuteReader();
                dr.Read();
                int count = dr.GetInt32(0);
                dr.Close();
                if (count != 1)
                {
                    MessageBox.Show("盘点表中的商品条码不存在(条码:" + tm + ",第" + (i + 1) + "行)", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ICell cl = row.CreateCell(6);
                    IFont font = book.CreateFont();
                    font.Color = NPOI.HSSF.Util.HSSFColor.Red.Index;
                    ICellStyle sy = book.CreateCellStyle();
                    sy.SetFont(font);
                    sy.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                    cl.SetCellType(CellType.String);
                    cl.CellStyle = sy;
                    cl.SetCellValue("???");
                    fs = new FileStream(this.textBox_pd.Text, FileMode.Open, FileAccess.Write);
                    book.Write(fs);
                    fs.Close();
                    return false;
                }
            }
            return true;
        }
        private bool AutoCheckSL()
        {
            HSSFWorkbook cb = new HSSFWorkbook();
            ISheet cs = cb.CreateSheet();

            ICellStyle style = cb.CreateCellStyle();
            IDataFormat format = cb.CreateDataFormat();
            style.DataFormat = format.GetFormat("N2");
            style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;

            ICellStyle style_all = cb.CreateCellStyle();
            style_all.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;

            IRow hr = cs.CreateRow(0);
            hr.CreateCell(0).SetCellValue("条码");
            hr.CreateCell(1).SetCellValue("库存");
            hr.CreateCell(2).SetCellValue("盘点");
            hr.CreateCell(3).SetCellValue("售价");
            hr.CreateCell(4).SetCellValue("品名");
            hr.CreateCell(5).SetCellValue("进价");
            for (int m = 0; m < 6; m++)
            {
                hr.GetCell(m).CellStyle = style_all;
            }
            cs.SetColumnWidth(0, 16 * 256);
            cs.SetColumnWidth(1, 8 * 256);
            cs.SetColumnWidth(2, 8 * 256);
            cs.SetColumnWidth(3, 10 * 256);
            cs.SetColumnWidth(4, 24 * 256);
            cs.SetColumnWidth(5, 10 * 256);

            int total = 1;

            FileStream fs = new FileStream(this.textBox_pd.Text, FileMode.Open, FileAccess.Read);
            HSSFWorkbook book = new HSSFWorkbook(fs);
            ISheet sheet = book.GetSheetAt(0);
            string tm;
            for (int i = 0; i < sheet.LastRowNum + 1; i++)
            {
                IRow row = sheet.GetRow(i);
                ICell cell = row.GetCell(0);
                switch (cell.CellType)
                {
                    case CellType.Numeric:
                        tm = cell.NumericCellValue.ToString();
                        break;
                    case CellType.String:
                        tm = cell.StringCellValue;
                        break;
                    default:
                        return false;
                }

                string s = "select kc.tm,kc.sl,pd.sl,goods.sj,goods.pm,goods.jj from kc join pd on(kc.tm=pd.tm) join goods on(kc.tm=goods.tm) where pd.tm='" + tm + "'";
                Form_main.Command.CommandText = s;
                MySqlDataReader dr = Form_main.Command.ExecuteReader();
                dr.Read();
                if (dr.GetInt32(1) == dr.GetInt32(2))
                {
                    this.toolStripStatusLabel1.Text = "正在核对商品:" + tm + " OK";
                    Application.DoEvents();

                }
                else
                {
                    IRow r = cs.CreateRow(total);
                    r.CreateCell(0).SetCellType(CellType.String);
                    r.GetCell(0).CellStyle = style_all;
                    r.GetCell(0).SetCellValue(dr.GetString(0));//tm

                    r.CreateCell(1).SetCellType(CellType.Numeric);
                    r.GetCell(1).CellStyle = style_all;
                    r.GetCell(1).SetCellValue(dr.GetInt32(1));//kc-sl

                    r.CreateCell(2).SetCellType(CellType.Numeric);
                    r.GetCell(2).CellStyle = style_all;
                    r.GetCell(2).SetCellValue(dr.GetInt32(2));//pd-sl     

                    r.CreateCell(3).SetCellType(CellType.Numeric);//sj
                    r.GetCell(3).CellStyle = style;
                    r.GetCell(3).SetCellValue(dr.GetFloat(3));

                    r.CreateCell(4).SetCellType(CellType.String);//pm
                    r.GetCell(4).CellStyle = style_all;
                    r.GetCell(4).SetCellValue(dr.GetString(4));

                    r.CreateCell(5).SetCellType(CellType.Numeric);
                    r.GetCell(5).CellStyle = style;
                    r.GetCell(5).SetCellValue(dr.GetFloat(5));

                    total++;
                    this.toolStripStatusLabel1.Text = "正在核对商品:" + tm + " Error";
                    Application.DoEvents();
                }
                dr.Close();
            }
            if (total > 1)
            {
                MessageBox.Show("核对完成,共核对" + (sheet.LastRowNum + 1) + "件商品,其中不匹配商品" + cs.LastRowNum + "件需要保存", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "请选择保存的文件名及路径(核对中产生的不同数据)";
                sfd.AddExtension = true;
                sfd.Filter = "电子表格文件(*.xls)|*.xls";
                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    fs = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write);
                    cb.Write(fs);
                    fs.Close();
                    MessageBox.Show("数量不匹配条码已保存至" + sfd.FileName);
                }
                else
                {
                    MessageBox.Show("找到不匹配数据未能保存", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("核对完成,共" + (sheet.LastRowNum + 1) + "件商品,全部与电脑库存相符！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.textBox_kc.Clear();
            this.textBox_pd.Clear();
            this.toolStripStatusLabel1.Text = "就绪";
            return true;
        }
    }
}
