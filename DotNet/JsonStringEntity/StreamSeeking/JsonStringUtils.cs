using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace StreamSeeking
{
    public static class JsonStringUtils
    {
        public static (int startPos, int length) GetValuePosition(
            string propName,
            FileStream fileStream,
            int bufferSize = 20)
        {
            (int startPos, int length) position = (-1, -1);

            var seekIndex = StreamSeeker.SeekWord($"\"{propName}\":", fileStream);
            if (seekIndex == -1)
                return position;

            UTF8Encoding utf8Encoder = new UTF8Encoding(true);

            // skip the property key closing \" char and the followinf : char
            fileStream.Position = seekIndex + propName.Length + 3;

            var streamBuffer = new byte[bufferSize];

            var propertyValue = "";
            JsonPropertyType propertyType = JsonPropertyType.unset;

            while (fileStream.Read(streamBuffer, 0, streamBuffer.Length) > 0)
            {
                propertyValue += utf8Encoder.GetString(streamBuffer);

                if (propertyType == JsonPropertyType.unset)
                {
                    propertyType = GetPropertyType(propertyValue[0]);
                }

                int closingCharIdx = 0;
                if (IsJsonValueClosed(propertyValue, propertyType, out closingCharIdx))
                {
                    position = (seekIndex + propName.Length + 3, closingCharIdx);
                    break;
                }
            }


            return position;
        }

        /// <summary>
        /// determine if the string (json value) is closed by the appropiate closing char
        /// string type is tricky because its closing char is the same with the opening
        /// </summary>
        /// <param name="propValue">the string represenation of the json value</param>
        /// <param name="closingCharacter">the character to look for</param>
        /// <returns>
        /// return the index of the closing char if it is present, return 0 othervise
        /// </returns>
        public static bool IsJsonValueClosed(string propValue, JsonPropertyType propertyType, out int closingCharIndex)
        {
            closingCharIndex = -1;
            var closingCharacter = GetJsonValueClosingCharacter(propertyType);

            switch (propertyType)
            {
                case JsonPropertyType.array:
                    return IsJsonParenthesesClosed(propValue, '[', ']', out closingCharIndex);

                case JsonPropertyType.complex:
                    return IsJsonParenthesesClosed(propValue, '{', '}', out closingCharIndex);

                case JsonPropertyType.text:
                    return IsJsonParenthesesClosed(propValue, '"', '"', out closingCharIndex);

                case JsonPropertyType.number:
                    return IsJsonNumberValueClosed(propValue, out closingCharIndex);
            }

            return false;
        }

        public static string NormalizeJsonString(string json)
        {
            RegexOptions options = RegexOptions.None;
            Regex multipSpacesToSingle = new Regex("[ ]{2,}", options);

            return Regex.Replace(json.Trim(), @"\t|\n|\r|[ ]{2,}", " ");
        }

        public static char GetJsonValueClosingCharacter(char openingChar)
        {
            var type = GetPropertyType(openingChar);
            switch (type)
            {
                case JsonPropertyType.array: return ']';
                case JsonPropertyType.complex: return '}';
                case JsonPropertyType.text: return '"';
                case JsonPropertyType.number:
                default: return ',';
            }
        }

        public static char GetJsonValueClosingCharacter(JsonPropertyType type)
        {
            switch (type)
            {
                case JsonPropertyType.array: return ']';
                case JsonPropertyType.complex: return '}';
                case JsonPropertyType.text: return '\"';
                case JsonPropertyType.number:
                default: return ',';
            }
        }

        public static JsonPropertyType GetPropertyType(char c)
        {
            int i;
            if (Int32.TryParse(c.ToString(), out i))
                return JsonPropertyType.number;

            switch (c)
            {
                case '{': return JsonPropertyType.complex;
                case '[': return JsonPropertyType.array;
                case '\"': return JsonPropertyType.text;
                default: return JsonPropertyType.unset;
            }

            throw new ArgumentException($"Json value type cannot be determined. Opening char: {c}");
        }

        public static bool IsJsonNumberValueClosed(string text, out int lastDigit)
        {
            lastDigit = 0;
            int i;
            StringBuilder numAsString = new StringBuilder();

            if (Int32.TryParse($"{text[text.Length - 1]}", out i))
            {
                return false;
            }

            lastDigit = 0;
            foreach (var digit in text)
            {
                if (!Int32.TryParse($"{digit}", out i))
                    break;

                ++lastDigit;
            }

            return true;
        }

        public static bool IsJsonParenthesesClosed(string text, char opener, char closer, out int closingCharIdx)
        {
            closingCharIdx = 0;
            Stack<int> stack = new Stack<int>();

            var firstOpener = text.IndexOf(opener);
            if (firstOpener != 0)
                return false;

            stack.Push(0);
            int charIdx = 1;

            foreach (var character in text.Substring(1))
            {
                if (character == closer)
                {
                    var openerIdx = stack.Pop();
                    if (openerIdx == firstOpener)
                    {
                        closingCharIdx = charIdx - openerIdx + 1;
                        return true;
                    }
                }

                if (character == opener)
                    stack.Push(charIdx);

                ++charIdx;
            }

            return false;
        }
    }
}
