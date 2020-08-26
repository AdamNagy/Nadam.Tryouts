using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataEntity
{
    public class JsonStringEntity : IJsonStringEntity
    {
        private readonly string _jsonFileName;
        private readonly int _streamBuffer;
        
        public JsonStringEntity(string jsonFileName, int streamBuffer = 20)
        {
            _jsonFileName = jsonFileName;
            _streamBuffer = streamBuffer;
        }

        public string Read(string propertyName = "")
        {
            var propertyValue = "";

            using (FileStream fileStream = File.OpenRead(_jsonFileName))
            {
                if (!String.IsNullOrEmpty(propertyName))
                {
                    var seekIndex = 0;
                    seekIndex = StreamSeeker.SeekWord($"\"{propertyName}\":", fileStream);
                    if (seekIndex == -1)
                        return "";

                    // skip the property key closing " char and the following : char
                    seekIndex += propertyName.Length + 3;
                    fileStream.Position = seekIndex;
                }

                ASCIIEncoding utf8Encoder = new ASCIIEncoding();
                var streamBuffer = new byte[_streamBuffer];
                JsonPropertyType propertyType = JsonPropertyType.unset;

                while (fileStream.Read(streamBuffer, 0, streamBuffer.Length) > 0)
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

        public IEnumerable<string> ReadArray(string propertyName)
        {
            string propertyValue = "",
                   fileContent = "[";

            using (FileStream fileStream = File.OpenRead(_jsonFileName))
            {
                var seekIndex = 0;
                if (!String.IsNullOrEmpty(propertyName))
                {
                    seekIndex = StreamSeeker.SeekWord($"\"{propertyName}\":", fileStream);
                    if (seekIndex == -1)
                        yield return null;

                    // skip the property key closing " char and the following : char and the opening [
                    seekIndex += propertyName.Length + 4;
                    fileStream.Position = seekIndex;
                }

                ASCIIEncoding utf8Encoder = new ASCIIEncoding();
                var streamBuffer = new byte[_streamBuffer];

                var arrayValueType = JsonPropertyType.unset;
                while (fileStream.Read(streamBuffer, 0, streamBuffer.Length) > 0)
                {
                    propertyValue += utf8Encoder.GetString(streamBuffer);
                    fileContent += propertyValue;

                    bool needBreak = false;

                    int mainClosingCharIdx = -1;
                    if (JsonStringUtils.IsJsonValueClosed(fileContent, JsonPropertyType.array, out mainClosingCharIdx))
                    {
                        propertyValue = propertyValue.Substring(0, propertyValue.IndexOf(']'));
                        needBreak = true;
                    }
                    
                    if(arrayValueType == JsonPropertyType.unset)
                        arrayValueType = JsonStringUtils.GetPropertyType(propertyValue[0]);

                    if (arrayValueType == JsonPropertyType.array || arrayValueType == JsonPropertyType.complex)
                    {
                        int closingCharIdx = 0;
                        if (JsonStringUtils.IsJsonValueClosed(propertyValue, arrayValueType, out closingCharIdx))
                        {
                            var arrayItemValue = propertyValue.Substring(0, closingCharIdx).Trim('"');
                            propertyValue = propertyValue.Length >= closingCharIdx ? "" : propertyValue.Substring(closingCharIdx + 1);
                            yield return arrayItemValue;
                        }
                    }
                    else if (propertyValue.Contains(","))
                    {
                        var values = needBreak
                            ? propertyValue.Split(',')
                            : propertyValue.Split(',').Reverse().Skip(1).Reverse()
                                .ToArray();

                        foreach (var arrayVal in values)
                            yield return arrayVal;

                        propertyValue = propertyValue.Substring(propertyValue.LastIndexOf(',') + 1);
                    }
                    
                    
                    if(needBreak)
                        break;
                }
            }
        }

        public IEnumerable<string> ReadObject(string propertyName)
        {
            string propertyValue = "",
                   fileContent = "{";

            using (FileStream fileStream = File.OpenRead(_jsonFileName))
            {
                var seekIndex = 0;
                if (!String.IsNullOrEmpty(propertyName))
                {
                    seekIndex = StreamSeeker.SeekWord($"\"{propertyName}\":", fileStream);
                    if (seekIndex == -1)
                        yield return null;

                    // skip the property key closing " char and the following : char and the opening [
                    seekIndex += propertyName.Length + 4;
                    fileStream.Position = seekIndex;
                }

                ASCIIEncoding utf8Encoder = new ASCIIEncoding();
                var streamBuffer = new byte[_streamBuffer];

                var propertyValueType = JsonPropertyType.unset;
                while (fileStream.Read(streamBuffer, 0, streamBuffer.Length) > 0)
                {
                    propertyValue += utf8Encoder.GetString(streamBuffer);
                    fileContent += propertyValue;

                    bool needBreak = false;

                    int mainClosingCharIdx = -1;
                    if (JsonStringUtils.IsJsonValueClosed(fileContent, JsonPropertyType.complex, out mainClosingCharIdx))
                    {
                        propertyValue = propertyValue.Substring(0, propertyValue.IndexOf('}'));
                        needBreak = true;
                    }

                    if (propertyValueType == JsonPropertyType.unset)
                        propertyValueType = JsonStringUtils.GetPropertyType(propertyValue[0]);

                    if (propertyValueType == JsonPropertyType.array || propertyValueType == JsonPropertyType.complex)
                    {
                        int closingCharIdx = 0;
                        if (JsonStringUtils.IsJsonValueClosed(propertyValue, propertyValueType, out closingCharIdx))
                        {
                            var arrayItemValue = propertyValue.Substring(0, closingCharIdx).Trim('"');
                            propertyValue = propertyValue.Length >= closingCharIdx ? "" : propertyValue.Substring(closingCharIdx + 1);
                            yield return arrayItemValue;
                        }
                    }
                    else if (propertyValue.Contains(","))
                    {
                        var values = needBreak
                            ? propertyValue.Split(',')
                            : propertyValue.Split(',').Reverse().Skip(1).Reverse()
                                .ToArray();

                        foreach (var arrayVal in values)
                            yield return arrayVal;

                        propertyValue = propertyValue.Substring(propertyValue.LastIndexOf(',') + 1);
                    }


                    if (needBreak)
                        break;
                }
            }
        }

        public void SetProperty(
            string propertyName,
            string newValue,
            AppendPosition appendTo = AppendPosition.end)
        {
            using (FileStream fileStream = File.Open(_jsonFileName, FileMode.Open))
            {
                var valuePosition = JsonStringUtils.GetValuePosition(propertyName, fileStream);

                if( valuePosition.startPos == -1 && valuePosition.length == -1 )
                    throw new ArgumentException($"Property {propertyName} does not exist in file: {_jsonFileName}");

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
            using (FileStream fileStream = File.Open(_jsonFileName, FileMode.Open))
            {
                var valuePosition = JsonStringUtils.GetValuePosition(arrayPropertyName, fileStream);

                if (valuePosition.startPos == -1 && valuePosition.length == -1)
                    throw new ArgumentException($"Property {arrayPropertyName} does not exist in json file {_jsonFileName}");
                
                if (appendTo == AppendPosition.begining)
                {
                    var seekPosition = valuePosition.startPos + 1;
                    var restOfFIle = StreamSeeker.ReadFrom(seekPosition, fileStream);
                    var newTextSegment = $"{newValue},{restOfFIle}";

                    StreamSeeker.WriteFrom(seekPosition, fileStream, newTextSegment);
                }
                else
                {
                    var seekPosition = valuePosition.startPos + valuePosition.length - 1;
                    var restOfFIle = StreamSeeker.ReadFrom(seekPosition, fileStream);
                    var newTextSegment = $",{newValue}{restOfFIle}";

                    StreamSeeker.WriteFrom(seekPosition, fileStream, newTextSegment);
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
