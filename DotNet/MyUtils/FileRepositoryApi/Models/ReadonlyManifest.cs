using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ManifestRepositoryApi.Models
{
    public abstract class ReadonlyManifest
    {
        public string type;

        private readonly string _filePath;

        public ReadonlyManifest(string filePath)
        {
            _filePath = filePath;
        }

        public virtual string ReadWhole()
            => File.ReadAllText(_filePath);

        public abstract string ReadThumbnail();

        protected IEnumerable<string> ReadSegment(bool readMore)
        {
            using (FileStream fsSource = new FileStream(_filePath, FileMode.Open, FileAccess.Read))
            {
                // Read the source file into a byte array.
                byte[] bytes = new byte[30];
                int numBytesToRead = 30;
                int numBytesRead = 0;
                while (true)
                {
                    // Read may return anything from 0 to numBytesToRead.
                    int n = fsSource.Read(bytes, numBytesRead, numBytesToRead);
                    yield return Encoding.Default.GetString(bytes);//.Replace("\"", "'");//Regex.Replace(, @"^""|""$?", "");

                    // Break when the end of the file is reached.
                    if (n == 0 || !readMore)
                        break;
                }
            }
        }
    }

/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    public class WebGalleryManifest : ReadonlyManifest
    {
        public WebGalleryManifest(string file) : base(file)
        {
            type = "web-gallery";
        }

        public override string ReadThumbnail()
        {
            bool loadMore = true;
            var fileContent = "";
            var numOfImages = 8;

            foreach (var item in ReadSegment(loadMore).Take(int.MaxValue))
            {
                fileContent += item;
                loadMore = fileContent.Count(ch => ch == '}') < numOfImages;
                if (!loadMore)
                    break;
            }

            // Read(loadMore);

            var lastCommaIdx = fileContent.LastIndexOf('}');
            var diff = fileContent.Length - lastCommaIdx;
            var jsonString = fileContent.Substring(0, lastCommaIdx);

            return $"{jsonString}}}]}}";
        }
    }


/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */


    public class LocalGalleryManifest : ReadonlyManifest
    {
        public LocalGalleryManifest(string file) : base(file)
        {
            type = "local-gallery";
        }

        public override string ReadThumbnail()
        {
            throw new NotImplementedException();
        }
    }
}