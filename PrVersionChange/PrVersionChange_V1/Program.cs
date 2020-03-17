using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace PrVersionChange_V1
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = string.Empty;
            //拖拽获取文件路径
            if (args.Length > 0)
            {
                path = args[0].ToString();
                if (Path.GetExtension(path) != "")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("请将 PR 文件更改为 ZIP 后使用解压后文件执行 !!!\n请输入回车以退出程序");
                    Console.ReadKey();
                    return;
                }
            }
            //创建临时文件夹进行后续操作
            string fileName = Path.GetFileName(path);
            string tempPath = Path.Combine(Path.GetTempPath(), "PR18temp_hy");
            Directory.CreateDirectory(tempPath);
            try
            {
                //获取临时文件完整路径并拷贝至临时文件夹
                string newFilePath = Path.Combine(tempPath, fileName);
                File.Copy(path, newFilePath);
                //重命名为Xml
                string xmlPath = Path.ChangeExtension(newFilePath, "xml");
                File.Move(newFilePath, xmlPath);
                //对xml文件进行修改 
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlPath);
                XmlNode node = doc.SelectSingleNode("/PremiereData/Project").NextSibling;
                node.Attributes["Version"].Value = "34";
                doc.Save(xmlPath);
                //原始文件副本
                string savePath = Path.Combine(Path.GetDirectoryName(path), "18_" + fileName+ ".prproj");
                File.Copy(xmlPath, savePath);
                DelTemp();
            }
            catch (Exception e)
            {
                DelTemp();
                Console.WriteLine(e);
                Console.ReadKey();
            }
            Console.ReadKey();
            void DelTemp()
            {
                DirectoryInfo dicInfo = new DirectoryInfo(tempPath);
                if (dicInfo.Exists)
                {
                    dicInfo.Delete(true);
                }
            }
        }
    }
}
