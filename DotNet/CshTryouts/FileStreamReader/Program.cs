using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStreamReader
{
    class Program
    {
        static void Main(string[] args)
        {
            bool loadMore = true;
            var fileContent = "";
            var numOfImages = 3;

            foreach (var item in LoadPartial(loadMore).Take(int.MaxValue))
            {
                fileContent += item;
                loadMore = fileContent.Count(ch => ch == '}') < numOfImages;
                if (!loadMore)
                    break;
            }
            LoadPartial(loadMore);
            var lastCommaIdx = fileContent.LastIndexOf('}');
            var diff = fileContent.Length - lastCommaIdx;
            var jsonString = fileContent.Substring(0, lastCommaIdx);
        }

        public static IEnumerable<string> LoadPartial(bool loadMore)
        {
            string pathSource = @".\test.json";
            string pathNew = @"c:\tests\newfile.txt";  

            using (FileStream fsSource = new FileStream(pathSource,
                FileMode.Open, FileAccess.Read))
            {

                // Read the source file into a byte array.
                byte[] bytes = new byte[10];
                int numBytesToRead = 10;
                int numBytesRead = 0;
                while (true)
                {
                    // Read may return anything from 0 to numBytesToRead.
                    int n = fsSource.Read(bytes, numBytesRead, numBytesToRead);
                    yield return Encoding.UTF8.GetString(bytes);

                    // Break when the end of the file is reached.
                    if (n == 0 || !loadMore)
                        break;
                }                
            }
        }
    }
}
