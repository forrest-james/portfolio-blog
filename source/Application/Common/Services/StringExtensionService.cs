using System.Collections.Generic;

namespace Application.Common.Services
{
    public static class StringExtensionService
    {
        public static string Truncate(this string text, int length)
        {
            if (text == null || text.Length <= length)
                return text;

            List<char> wordCutOffs = new List<char>()
            {
                ' ',
                ',',
                '.',
                '?',
                '/',
                ':',
                ';',
                '\'',
                '\"',
                '-'
            };
            string temp = text.Substring(0, length);
            int lastWhiteSpace = temp.LastIndexOf(' ');

            if (lastWhiteSpace != -1 && (text.Length >= length + 1 && !wordCutOffs.Contains(text.ToCharArray()[length])))
                temp = temp.Remove(lastWhiteSpace);

            temp += "...";

            return temp;
        }
    }
}