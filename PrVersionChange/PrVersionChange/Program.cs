using System;
using System.IO;
using System.Xml;
using System.IO.Compression;

namespace PrVersionChange
{
    class Program
    {
        static void Main(string[] args)
        {
            //Premiere CC 2019.1——37
            //Premiere CC 2019——36
            //Premiere CC 2018.1——35
            //Premiere CC 2018——34
            //Premiere CC 2017.1——33
            //Premiere CC 2017——32
            //Premiere CC 2015.5——31
            //Premiere CC 2015.2——30
            //切换至18版 -----34

            string path = string.Empty;
            ////拖拽获取文件路径
            //if (args.Length > 0)
            //{
            //    path = args[0].ToString();
            //    if (Path.GetExtension(path) != ".prproj")
            //    {
            //        Console.ForegroundColor = ConsoleColor.Red;
            //        Console.WriteLine("该文件不是 PR 的 .prproj 后缀文件!!!\n请输入回车以退出程序");
            //        Console.ReadKey();
            //        return;
            //    }
            //}
            path = @"C:\Users\26861\Desktop\本杰明·巴顿奇事 - 副本.prproj";
            //创建临时文件夹进行后续操作
            string fileName = Path.GetFileName(path);
            string tempPath = Path.Combine(Path.GetTempPath(), "PR18temp_hy");
            Directory.CreateDirectory(tempPath);
            try
            {
                //获取临时文件完整路径并拷贝至临时文件夹
                string newFilePath = Path.Combine(tempPath, fileName);
                File.Copy(path, newFilePath);
                //重命名ZIP后缀
                string zipPath = Path.ChangeExtension(newFilePath, "zip");
                File.Move(newFilePath, zipPath);
                //解压
                ZipFile.ExtractToDirectory(zipPath, tempPath);
                //解压后文件路径
                string uZipFileName = Path.ChangeExtension(newFilePath, "");
                //重命名为Xml
                string xmlPath = Path.ChangeExtension(uZipFileName, "xml");
                File.Move(uZipFileName, xmlPath);
                //对xml文件进行修改 
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlPath);
                XmlNode node = doc.SelectSingleNode("/PremiereData/Project").NextSibling;
                node.Attributes["Version"].Value = "34";
                doc.Save(xmlPath);
                //原始文件副本
                string nowPath = Path.Combine(Path.GetDirectoryName(path), "18_" + fileName);
                File.Copy(xmlPath, nowPath);
                DelTemp();
            }
            catch(Exception e)
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
