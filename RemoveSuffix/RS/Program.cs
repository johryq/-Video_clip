using System;
using System.IO;

namespace RS
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory();
            var files = Directory.GetFiles(path, "*.mp4");
            if (files.Length == 0)
            {
                Console.WriteLine("当前目录没有视频文件需要重命名!\n按回车退出程序");
                Console.ReadKey();
                return;
            }
            foreach (var file in files)
            {
                try
                {
                    string[] val = RemoveSuffix(file);
                    if(val == null)
                    {
                        continue;
                    }
                    FileInfo fi = new FileInfo(file);
                    fi.MoveTo(val[0] + ".mp4");
                    Console.WriteLine($"已将:{val[1] + val[2]}\n重命名为:{val[1]}\n");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            Console.WriteLine("替换完成\n按回车退出程序");
            Console.ReadKey();
        }

        /// <summary>
        /// 去除后缀(H264 _batch)
        /// </summary>
        /// <param name="path">要去除文件完整路径</param>
        /// <returns>
        /// 改变后的完整路径
        /// 视频文件名
        /// 原视频文件名
        /// </returns>
        public static string[] RemoveSuffix(string path)
        {
            string changePath;
            int nameIndex;
            string oldNameSuffix;
            string name;
            int suffixIndex;
            //判断是否含有两种后缀
            suffixIndex = path.LastIndexOf("_x264");
            oldNameSuffix = "_x264";
            if (suffixIndex == -1)
            {
                suffixIndex = path.LastIndexOf("_batch");
                oldNameSuffix = "_batch";
            }
            //没有直接返回空
            if(suffixIndex == -1)
            {
                string [] nu = null;
                return nu;
            }

            changePath = path.Remove(suffixIndex);
            nameIndex = changePath.LastIndexOf('\\');
            if (nameIndex == -1)
            {
                nameIndex = changePath.LastIndexOf('/');
            }
            name = changePath.Substring(nameIndex + 1);
            string[] rt = { changePath, name, oldNameSuffix };
            return rt;
        }
    }
}
