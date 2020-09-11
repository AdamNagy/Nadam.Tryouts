namespace DataEntity
{
    public static class StringUtils
    {
        public static string FirstLetterToLower(this string text)
        {
            if (text != string.Empty && char.IsUpper(text[0]))
                return $"{char.ToLower(text[0])}{text.Substring(1)}";

            return text;
        }

        public static string ToJsonString(this string text)
            => $"\"{text}\"";
    }
}
