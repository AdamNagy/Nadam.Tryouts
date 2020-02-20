using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ManifestRepositoryApi.ManifestFramework
{
    public abstract class ReadonlyManifest
    {
        public string PathWithName { get; }
        public string type;

        public ReadonlyManifest(string pathWithName)
        {
            PathWithName = pathWithName;
        }

        public virtual string ReadWhole()
            => File.ReadAllText(PathWithName);

        public abstract string ReadThumbnail();

        protected IEnumerable<string> ReadSegment(bool readMore)
        {
            using (FileStream fsSource = new FileStream(PathWithName, FileMode.Open, FileAccess.Read))
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
}