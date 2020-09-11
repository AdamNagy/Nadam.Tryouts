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
                JsonTypes types = JsonTypes.unset;

                while (fileStream.Read(streamBuffer, 0, streamBuffer.Length) > 0)
                {
                    propertyValue += utf8Encoder.GetString(streamBuffer);

                    if (types == JsonTypes.unset)
                    {
                        types = JsonStringUtils.GetPropertyType(propertyValue[0]);
                    }

                    int closingCharIdx = 0;
                    if (JsonStringUtils.IsJsonValueClosed(propertyValue, types, out closingCharIdx))
                    {
                        propertyValue = propertyValue.Substring(0, closingCharIdx).Trim('"');
                        break;
                    }
                }
            }

            return JsonStringUtils.NormalizeJsonString(propertyValue);
        }

        public IEnumerable<string> ReadArray(string propertyName = "")
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
                }
                else
                {
                    seekIndex = 1;
                }

                fileStream.Position = seekIndex;

                ASCIIEncoding utf8Encoder = new ASCIIEncoding();
                var streamBuffer = new byte[_streamBuffer];

                var arrayValueType = JsonTypes.unset;
                while (fileStream.Read(streamBuffer, 0, streamBuffer.Length) > 0)
                {
                    propertyValue += utf8Encoder.GetString(streamBuffer);
                    fileContent += propertyValue;

                    bool needBreak = false;

                    int mainClosingCharIdx = -1;
                    if (JsonStringUtils.IsJsonValueClosed(fileContent, JsonTypes.array, out mainClosingCharIdx))
                    {
                        propertyValue = propertyValue.Substring(0, propertyValue.LastIndexOf(']'));
                        needBreak = true;
                    }
                    
                    if(arrayValueType == JsonTypes.unset)
                        arrayValueType = JsonStringUtils.GetPropertyType(propertyValue[0]);

                    //if (arrayValueType == JsonTypes.array || arrayValueType == JsonTypes.complex)
                    //{
                    //    int closingCharIdx = 0;
                    //    if (JsonStringUtils.IsJsonValueClosed(propertyValue, arrayValueType, out closingCharIdx))
                    //    {
                    //        var arrayItemValue = propertyValue.Substring(0, closingCharIdx).Trim('"');
                    //        propertyValue = closingCharIdx >= (propertyValue.Length + 1) ? "" : propertyValue.Substring(closingCharIdx + 1);
                    //        yield return arrayItemValue;
                    //    }
                    //}
                    //else if (propertyValue.Contains(","))
                    //{
                    //    var values = needBreak
                    //        ? propertyValue.Split(',')
                    //        : propertyValue.Split(',').Reverse().Skip(1).Reverse()
                    //            .ToArray();

                    //    foreach (var arrayVal in values)
                    //        yield return arrayVal;

                    //    propertyValue = propertyValue.Substring(propertyValue.LastIndexOf(',') + 1);
                    //}

                    switch (arrayValueType)
                    {
                        case JsonTypes.array:
                        case JsonTypes.complex:
                            int closingCharIdx = 0;
                            if (JsonStringUtils.IsJsonValueClosed(propertyValue, arrayValueType, out closingCharIdx))
                            {
                                var arrayItemValue = propertyValue.Substring(0, closingCharIdx).Trim('"');
                                propertyValue = closingCharIdx >= (propertyValue.Length + 1) ? "" : propertyValue.Substring(closingCharIdx + 1);
                                yield return arrayItemValue;
                            }
                            break;

                        case JsonTypes.number:
                        case JsonTypes.text:
                            if (propertyValue.Contains(","))
                            {
                                var values = needBreak
                                    ? propertyValue.Split(',')
                                    : propertyValue.Split(',').Reverse().Skip(1).Reverse()
                                        .ToArray();

                                foreach (var arrayVal in values)
                                    yield return arrayVal;

                                propertyValue = propertyValue.Substring(propertyValue.LastIndexOf(',') + 1);
                            }
                            break;
                    }

                    if(needBreak)
                        break;
                }
            }
        }

        public IEnumerable<(string key, string value)> ReadObject(string propertyName = "")
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
                        yield return (key: "", value: "");

                    // skip the property key closing " char and the following : char and the opening [
                    seekIndex += propertyName.Length + 4;
                }
                else
                {
                    seekIndex = 1;
                }

                fileStream.Position = seekIndex;

                ASCIIEncoding utf8Encoder = new ASCIIEncoding();
                var streamBuffer = new byte[_streamBuffer];

                var currentPropertyValue = "";
                var currentPropertyName = "";

                while (fileStream.Read(streamBuffer, 0, streamBuffer.Length) > 0)
                {
                    propertyValue += utf8Encoder.GetString(streamBuffer);
                    fileContent += propertyValue;

                    bool needBreak = false;

                    int mainClosingCharIdx = -1;
                    if (JsonStringUtils.IsJsonValueClosed(fileContent, JsonTypes.complex, out mainClosingCharIdx))
                    {
                        propertyValue = propertyValue.Substring(0, propertyValue.LastIndexOf('}'));
                        needBreak = true;
                    }

                    if (JsonStringUtils.ContainsPropertyName(propertyValue, out var result))
                    {
                        currentPropertyValue += propertyValue.Substring(0, result.startPos);
                        if( !string.IsNullOrEmpty(currentPropertyValue) )
                            yield return (key: currentPropertyName, value: currentPropertyValue.Trim(',').Trim('\"'));

                        currentPropertyValue = "";
                        currentPropertyName = result.name;

                        propertyValue = propertyValue.Substring(result.startPos + result.length);
                    }

                    if (needBreak)
                    {
                        currentPropertyValue += propertyValue;
                        break;
                    }
                }

                yield return (key: currentPropertyName, value: currentPropertyValue);
            }
        }

        public void SetProperty(
            string propertyName,
            string newValue)
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
