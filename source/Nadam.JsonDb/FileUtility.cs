using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;


namespace Nadam.Global.JsonDb
{
    public class FileUtility
    {
        public bool CreateFile(string folder, string name, string extension)
        {
            string path = folder + "\\" + name + extension;

            try
            {
                using (File.Create(path)){}

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public void WriteDataToFileAsJson(string folder, string name, string extension, string tableData)
        {
            string path = folder + "\\" + name + extension;
            using (FileStream fs = File.Create(path))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes(tableData);
                fs.Write(info, 0, info.Length);
            }
        }
    }
}
