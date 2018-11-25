using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Common.Data
{
    public class Txt
    {
        private string path;

        public string Path
        {
            get { return path; }
            set { path = value; }
        }
        private string txtName;

        public string TxtName
        {
            get { return txtName; }
            set { txtName = value; }
        }
        FileStream fileStream;
        StreamWriter sw;
        public void Open()
        {
            if (!File.Exists(path + "\\" + txtName + ".txt"))
            {
                fileStream = new FileStream(path + "\\" + txtName + ".txt", FileMode.Create, FileAccess.Write);//创建写入文件 
                sw = new StreamWriter(fileStream);
            }
            else
            {
                fileStream = new FileStream(path + "\\" + txtName + ".txt", FileMode.Open, FileAccess.Write);//创建写入文件 
                sw = new StreamWriter(fileStream);
            }

        }
        public void Close()
        {
            sw.Close();
            fileStream.Close();
        }

        public void WriteLine(string txt)
        {
            sw.WriteLine(txt);//开始写入值
        }
    }
}
