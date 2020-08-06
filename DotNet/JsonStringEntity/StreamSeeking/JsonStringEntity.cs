using System;
using System.IO;
using System.Text;

namespace StreamSeeking
{
    public class JsonStringEntity : IJsonStringEntity
    {
        private readonly int _streamBuffer;
        private readonly string _jsonFile;

        public JsonStringEntity(string jsonFile, int streamBuffer = 20)
        {
            _jsonFile = jsonFile;
            _streamBuffer = streamBuffer;
        }

        public string Read(string propertyName = "")
        {
            var seekIndex = 0;
            if (!String.IsNullOrEmpty(propertyName))
            {
                seekIndex = StreamSeeker.SeekWord($"\"{propertyName}\":", _jsonFile);
                if (seekIndex == -1)
                    return "";

                // skip the property key closing " char and the followinf : char
                seekIndex += propertyName.Length + 3;
            }

            ASCIIEncoding utf8Encoder = new ASCIIEncoding();

            var streamBuffer = new byte[_streamBuffer];

            var propertyValue = "";
            JsonPropertyType propertyType = JsonPropertyType.unset;

            using (FileStream fs = File.OpenRead(_jsonFile))
            {
                fs.Seek(seekIndex, SeekOrigin.Begin);
                while (fs.Read(streamBuffer, 0, streamBuffer.Length) > 0)
                {
                    propertyValue += utf8Encoder.GetString(streamBuffer);

                    if (propertyType == JsonPropertyType.unset)
                    {
                        propertyType = JsonStringUtils.GetPropertyType(propertyValue[0]);
                    }

                    int closingCharIdx = 0;
                    if (JsonStringUtils.IsJsonValueClosed(propertyValue, propertyType, out closingCharIdx))
                    {
                        propertyValue = propertyValue.Substring(0, closingCharIdx).Trim('"');
                        break;
                    }
                }
            }

            return JsonStringUtils.NormalizeJsonString(propertyValue);
        }

        public void SetProperty(
            string propertyName,
            string newValue,
            AppendPosition appendTo = AppendPosition.end)
        {
            using (FileStream fileStream = File.Open(_jsonFile, FileMode.Open))
            {
                var valuePosition = JsonStringUtils.GetValuePosition(propertyName, fileStream);

                if( valuePosition.startPos == -1 && valuePosition.length == -1 )
                    throw new ArgumentException($"Property {propertyName} does not exist in file: {_jsonFile}");

                var restOfTheFile = StreamSeeker.ReadFrom(valuePosition.startPos + valuePosition.length, fileStream);

                StreamSeeker.WriteFrom(valuePosition.startPos, fileStream, newValue);

                ASCIIEncoding encoder = new ASCIIEncoding();
                var restOfTheFile_AsByteArray = encoder.GetBytes(restOfTheFile);
                fileStream.Write(restOfTheFile_AsByteArray, 0, restOfTheFile_AsByteArray.Length);
            }
        }

        public void ExtendProperty(
            string newValue,
            string arrayPropertyName,
            AppendPosition appendTo = AppendPosition.end)
        {
            using (FileStream fs = File.Open(_jsonFile, FileMode.Open))
            {
                var valuePosition = JsonStringUtils.GetValuePosition(arrayPropertyName, fs);

                if (valuePosition.startPos == -1 && valuePosition.length == -1)
                    throw new ArgumentException($"Property {arrayPropertyName} does not exist in json file {_jsonFile}");
                
                if (appendTo == AppendPosition.begining)
                {
                    var seekPosition = valuePosition.startPos + 1;
                    var restOfFIle = StreamSeeker.ReadFrom(seekPosition, fs);
                    var newTextSegment = $"{newValue},{restOfFIle}";

                    StreamSeeker.WriteFrom(seekPosition, fs, newTextSegment);
                }
                else
                {
                    var seekPosition = valuePosition.startPos + valuePosition.length - 1;
                    var restOfFIle = StreamSeeker.ReadFrom(seekPosition, fs);
                    var newTextSegment = $",{newValue}{restOfFIle}";

                    StreamSeeker.WriteFrom(seekPosition, fs, newTextSegment);
                }
            }
        }

        public string ReduceProperty(string value, string arrayPropertyName = "")
        {
            var property = Read(arrayPropertyName);
            var arrayItems = property.TrimStart('[').TrimEnd(']').Split(',');
            var newArrayValueSB = new StringBuilder();
            newArrayValueSB.Append('[');

            foreach (var arrayItem in arrayItems)
            {
                if( arrayItem == value )
                    continue;

                newArrayValueSB.Append($"{arrayItem},");
            }

            var newArrayValue = $"{newArrayValueSB.ToString().TrimEnd(',')}]";
            SetProperty(arrayPropertyName, newArrayValue);
            return newArrayValue;
        }
    }
}
