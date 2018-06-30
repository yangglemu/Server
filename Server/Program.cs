using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Xml;
using System.Threading;

namespace Server
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Link_Form link = new Link_Form();
            link.Show();
            Application.DoEvents();

            XmlDocument doc = new XmlDocument();
            link.label1.Text = "正在读取配置文件 ……";
            Application.DoEvents();
            doc.Load(Application.StartupPath + "\\yuan.xml");
            XmlNode root = doc.SelectSingleNode("config");
            string s = string.Format("data source={0};user id={1};password={2};database={3}",
                root.SelectSingleNode("ip").InnerText,
                root.SelectSingleNode("user").InnerText,
                root.SelectSingleNode("password").InnerText,
                root.SelectSingleNode("database").InnerText);
            Form_main.printer = root.SelectSingleNode("tm_printer").InnerText;
            var title = root.SelectSingleNode("shop").InnerText;
            link.label1.Text = "配置完毕，正在连接数据库 ……";
            Application.DoEvents();
            try
            {
                Form_main.Connection = new MySqlConnection(s);
                Form_main.Command = Form_main.Connection.CreateCommand();
                Form_main.Connection.Open();
                link.label1.Text = "数据库连接成功，启动主程序 ……";
                Application.DoEvents();
                Form mf = new Form_main();
                mf.Text = title;
                mf.Show();
                link.Close();
                Application.Run(mf);
            }
            catch (MySqlException)
            {
                MessageBox.Show("连接到门店电脑数据库时错误！\r\n网络是否正常？", "连接错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
    }
}
