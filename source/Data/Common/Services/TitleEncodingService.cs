using System;
using System.Text;

namespace Data.Common.Services
{
    public static class TitleEncodingService
    {
        public static string Encode(this String title) => title.RemoveSpecialCharacters().ReduceWhiteSpace().ConvertWhiteSpace().ToLower();

        private static string RemoveSpecialCharacters(this String text)
        {
            var tempString = new StringBuilder();
            foreach(char c in text)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c == ' '))
                    tempString.Append(c);
            }
            return tempString.ToString();
        }

        private static string ReduceWhiteSpace(this String text)
        {
            var tempString = new StringBuilder();
            bool consecutiveWhiteSpace = false;
            foreach(char c in text)
            {
                if (char.IsWhiteSpace(c) && consecutiveWhiteSpace)
                    continue;
                else if (char.IsWhiteSpace(c))
                    consecutiveWhiteSpace = true;
                else
                    consecutiveWhiteSpace = false;

                tempString.Append(c);
            }
            return tempString.ToString();
        }

        private static string ConvertWhiteSpace(this String text) => text.Replace(" ", "-");
    }
}