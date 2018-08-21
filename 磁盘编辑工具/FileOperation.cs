//根据查阅的资料对代码进行修改并完善备注后的结果。希望能对新手有所帮助。
using System;  
using System.IO;
using System.Text;
namespace 文件操作类
{
    public class FileHelper
    {
        public FileHelper()
        {
            // TODO: Complete member initialization
        }

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="filePath">文件全路径</param>
        /// <returns></returns>
        public static bool Exists(string filePath)
        {
            if (filePath == null || filePath.Trim() == "")
            {
                return false;
            }

            if (File.Exists(filePath))
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="dirPath">文件夹路径</param>
        /// <returns></returns>
        public static bool CreateDir(string dirPath)
        {
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            return true;
        }


        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static bool CreateFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                FileStream fs = File.Create(filePath);
                fs.Close();
                fs.Dispose();
            }
            return true;
        }


        /// <summary>
        /// 读文件内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="encoding">编码格式</param>
        /// <returns></returns>
        public static string Read(string filePath,Encoding encoding)
        {
            if (!Exists(filePath))
            {
                return null;
            }
            //将文件信息读入流中
            using (FileStream fs = new FileStream(filePath,FileMode.Open))
            {
                return new StreamReader(fs, encoding).ReadToEnd();
            }
        }

        /// <summary>
        /// 二进制读取文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="offset">开始读取的位置</param>
        /// <param name="lenght">读取的长度</param>
        /// <returns name="byte[]">读取到的数据</returns>
        public byte[] BinRead(string filePath,uint offset,uint lenght)
        {
            byte[] data = new byte[512];
            if (!Exists(filePath))
            {
                return null;
            }
            //将文件信息读入流中
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                fs.Position = offset;
                fs.Read(data, 0, (int)lenght);
                fs.Close();
                fs.Dispose();
                return data;
            }
        }

        /// <summary>
        /// 读取文件的一行内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="encoding">编码格式</param>
        /// <returns></returns>
        public static string ReadLine(string filePath, Encoding encoding)
        {
            if (!Exists(filePath))
            {
                return null;
            }
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                return new StreamReader(fs, encoding).ReadLine();
            }
        }


        /// <summary>
        /// 写文件(512字节)
        /// </summary>
        /// <param name="filePath">文件路径</param>
		/// <param name="seek">文件偏移</param>
        /// <param name="content">文件内容</param>
        /// <returns></returns>
        public bool Write(string filePath, uint seek, byte[] content)
        {
			try
			{
				if (content.Length != 512)
					return false;

				if (!File.Exists(filePath))
				{
					//创建文件  
					FileStream fs = File.Create(filePath);
				}

				//打开文件
				FileStream sr = File.Create(filePath);

				//设置偏移
				sr.Write(content, (int)seek, 512);

				//同步并关闭文件
				sr.Close();
				sr.Dispose();
			}
			catch (Exception ex)
			{
				return false;
			}
			return true;
        }


        /// <summary>
        /// 写入一行
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public static bool WriteLine(string filePath, string content)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate | FileMode.Append))
            {
                lock (fs)
                {
                    if (!fs.CanWrite)
                    {
                        throw new System.Security.SecurityException("文件filePath=" + filePath + "是只读文件不能写入!");
                    }

                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine(content);
                    sw.Dispose();
                    sw.Close();
                    return true;
                }
            }
        }

        //复制目录？
        public static bool CopyDir(DirectoryInfo fromDir, string toDir)
        {
            return CopyDir(fromDir, toDir, fromDir.FullName);
        }


        /// <summary>
        /// 复制目录
        /// </summary>
        /// <param name="fromDir">被复制的目录路径</param>
        /// <param name="toDir">复制到的目录路径</param>
        /// <returns></returns>
        public static bool CopyDir(string fromDir, string toDir)
        {
            if (fromDir == null || toDir == null)
            {
                throw new NullReferenceException("参数为空");
            }

            if (fromDir == toDir)
            {
                throw new Exception("两个目录都是" + fromDir);
            }

            if (!Directory.Exists(fromDir))
            {
                throw new IOException("目录fromDir=" + fromDir + "不存在");
            }

            DirectoryInfo dir = new DirectoryInfo(fromDir);
            return CopyDir(dir, toDir, dir.FullName);
        }


        /// <summary>
        /// 复制目录
        /// </summary>
        /// <param name="fromDir">被复制的目录路径</param>
        /// <param name="toDir">复制到的目录路径</param>
        /// <param name="rootDir">被复制的根目录路径</param>
        /// <returns></returns>
        private static bool CopyDir(DirectoryInfo fromDir, string toDir, string rootDir)
        {
            string filePath = string.Empty;
            foreach (FileInfo f in fromDir.GetFiles())
            {
                filePath = toDir + f.FullName.Substring(rootDir.Length);
                string newDir = filePath.Substring(0, filePath.LastIndexOf("\\"));
                CreateDir(newDir);
                File.Copy(f.FullName, filePath, true);
            }

            foreach (DirectoryInfo dir in fromDir.GetDirectories())
            {
                CopyDir(dir, toDir, rootDir);
            }

            return true;
        }


        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath">文件的完整路径</param>
        /// <returns></returns>
        public static bool DeleteFile(string filePath)
        {
            if (Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }


        public static void DeleteDir(DirectoryInfo dir)
        {
            if (dir == null)
            {
                throw new NullReferenceException("目录不存在");
            }

            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                DeleteDir(d);
            }

            foreach (FileInfo f in dir.GetFiles())
            {
                DeleteFile(f.FullName);
            }

            dir.Delete();

        }


        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="dir">指定目录路径</param>
        /// <param name="onlyDir">是否只删除目录</param>
        /// <returns></returns>
        public static bool DeleteDir(string dir, bool onlyDir)
        {
            if (dir == null || dir.Trim() == "")
            {
                throw new NullReferenceException("目录dir=" + dir + "不存在");
            }

            if (!Directory.Exists(dir))
            {
                return false;
            }

            DirectoryInfo dirInfo = new DirectoryInfo(dir);
            if (dirInfo.GetFiles().Length == 0 && dirInfo.GetDirectories().Length == 0)
            {
                Directory.Delete(dir);
                return true;
            }


            if (!onlyDir)
            {
                return false;
            }
            else
            {
                DeleteDir(dirInfo);
                return true;
            }

        }


        /// <summary>
        /// 在指定的目录中查找文件
        /// </summary>
        /// <param name="dir">目录路径</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static bool FindFile(string dir, string fileName)
        {
            if (dir == null || dir.Trim() == "" || fileName == null || fileName.Trim() == "" || !Directory.Exists(dir))
            {
                return false;
            }

            DirectoryInfo dirInfo = new DirectoryInfo(dir);
            return FindFile(dirInfo, fileName);

        }


        public static bool FindFile(DirectoryInfo dir, string fileName)
        {
            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                if (File.Exists(d.FullName + "\\" + fileName))
                {
                    return true;
                }
                FindFile(d, fileName);
            }

            return false;
        }

        //获取文件长度
        public long filelen(string filename)
        {
            if (filename == "")
                return 0;
            FileInfo fi = new FileInfo(filename);
            return fi.Length;
        }
        
    }
}