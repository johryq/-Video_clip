using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ALC2
{
    class Program
    {
        static void Main(string[] args)
        {
            string lanaguage = string.Empty;
            string changeLag = string.Empty;
            string path = string.Empty;
            string[] paths = {
                @"C:\Program Files\Adobe\Adobe After Effects CC 2017\Support Files\AMT\application.xml",
                 @"C:\Program Files\Adobe\Adobe After Effects CC 2018\Support Files\AMT\application.xml",
                  @"C:\Program Files\Adobe\Adobe After Effects CC 2019\Support Files\AMT\application.xml",
                @"C:\Program Files\Adobe\Adobe After Effects 2017\Support Files\AMT\application.xml" ,
                @"C:\Program Files\Adobe\Adobe After Effects 2018\Support Files\AMT\application.xml",
                @"C:\Program Files\Adobe\Adobe After Effects 2019\Support Files\AMT\application.xml",
                @"C:\Program Files\Adobe\Adobe After Effects 2020\Support Files\AMT\application.xml" };
            foreach(string file in paths)
            {
                if (File.Exists(file))
                {
                    path = file;
                }
            }
            if( path == null || path == "")

            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("该程序无法更改!请尝试其他方法\n");
                Console.WriteLine("输入回车以退出程序");
                Console.ReadKey();
                return;
            }
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            XmlNode xmlNode = xDoc.SelectSingleNode(@"/Configuration/Payload/Data[@key='installedLanguages']");
            if (xmlNode.InnerText == "zh_CN")
            {
                lanaguage = "中文";
                changeLag = "英语";
                xmlNode.InnerText = "en_US";
            }
            else
            {
                changeLag = "中文";
                lanaguage = "英语";
                xmlNode.InnerText = "zh_CN";
            }
            Console.WriteLine("当前语言为:" + lanaguage);
            xDoc.Save(path);
            Console.WriteLine("成功更改为:" + "\t");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(changeLag);
            Console.WriteLine("输入回车以退出程序");
            Console.ReadKey();
        }
    }
}
