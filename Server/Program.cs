using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Xml;

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
            doc.Load("yuan.xml");
            XmlNode root = doc.SelectSingleNode("config");
            string s = string.Format("data source={0};user id={1};password={2};database={3}",
                root.SelectSingleNode("ip").InnerText,
                root.SelectSingleNode("user").InnerText,
                root.SelectSingleNode("password").InnerText,
                root.SelectSingleNode("database").InnerText);
            Form_main.printer = root.SelectSingleNode("printer").InnerText;
            try
            {
                Form_main.Connection = new MySqlConnection(s);
                Form mf = new Form_main();
                mf.Show();
                link.Close();
                Application.Run(mf);
            }
            catch(MySqlException)
            {
                //MessageBox.Show(se.Message+"\r\n"+se.Source);
                MessageBox.Show("连接到门店电脑数据库时错误！\r\n网络是否正常？", "连接错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }
            //Application.Run(new Form_main());
        }
    }
}
