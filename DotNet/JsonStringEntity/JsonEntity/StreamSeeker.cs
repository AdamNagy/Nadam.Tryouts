using System;
using System.IO;
using System.Text;

namespace DataEntity
{
    public class StreamSeeker
    {
        public static int SeekWord(string word, string file)
        {
            if(!File.Exists(file))
                throw new ArgumentException($"file does not exist: {file}");

            var buffer = new byte[word.Length];
            string prevBufferText = "";
            var seekIndex = -1;
            UTF8Encoding temp = new UTF8Encoding(true);

            using (FileStream fileStream = File.OpenRead(file))
            {
                while (fileStream.Read(buffer, 0, buffer.Length) > 0)
                {
                    var textToCheck = $"{prevBufferText}{temp.GetString(buffer)}";

                    if (textToCheck.Contains(word))
                    {
                        prevBufferText = textToCheck;
                        ++seekIndex;
                        break;
                    }

                    prevBufferText = temp.GetString(buffer);
                    seekIndex += buffer.Length;
                }

                seekIndex += prevBufferText.IndexOf(word);

                if( seekIndex > 0 )
                    seekIndex -= word.Length;
            }

            return seekIndex;
        }

        public static int SeekWord(string word, FileStream fileStream)
        {
            var buffer = new byte[word.Length];
            string prevBufferText = "";
            var seekIndex = -1;
            UTF8Encoding temp = new UTF8Encoding(true);

            while (fileStream.Read(buffer, 0, buffer.Length) > 0)
            {
                var textToCheck = $"{prevBufferText}{temp.GetString(buffer)}";

                if (textToCheck.Contains(word))
                {
                    prevBufferText = textToCheck;
                    ++seekIndex;
                    break;
                }

                prevBufferText = temp.GetString(buffer);
                seekIndex += buffer.Length;
            }

            seekIndex += prevBufferText.IndexOf(word);
            seekIndex -= word.Length;
            

            return seekIndex;
        }

        public static string ReadFrom(int startPos, FileStream fileStream)
        {
            var text = "";
            UTF8Encoding temp = new UTF8Encoding(true);
            var buffer = new byte[50];
            // --startPos;
            if (startPos > 0)
            {
                fileStream.Position = startPos;
            }
                // fileStream.Seek(startPos, SeekOrigin.Begin);

            while (fileStream.Read(buffer, 0, buffer.Length) > 0)
            {
                text += temp.GetString(buffer);
            }

            return text.Replace("\0", string.Empty);
        }

        public static void WriteFrom(int startPos, FileStream fileStream, string text)
        {
            var encoder = new UTF8Encoding();
            fileStream.Seek(startPos, SeekOrigin.Begin);

            fileStream.Write(encoder.GetBytes(text), 0, text.Length);
        }
    }
}
