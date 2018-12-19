using System;
using System.Text.RegularExpressions;

namespace ProjectTemplate.Core.Extensions
{
    public static class StringExtension
    {
        public static string Brief(this string word, int maxSize)
        {
            
            if (String.IsNullOrWhiteSpace(word))
                return "";


            if (word.Length > maxSize)
                word = word.Substring(0, maxSize) + " ...";

            return word;
        }

        public static string StripHtml(this string text)
        {
            string result = Regex.Replace(text, "<(.|\\n)*?>", string.Empty);
            result = result.Replace("&nbsp;", " ");
            return result;
        }

        public static string ConvertArabicNumeralsToEnglish(this string text, bool removePostOrAnteMeridiem = true)
        {
            text = text?.Replace('١', '1')
                .Replace('٢', '2')
                .Replace('٣', '3')
                .Replace('٤', '4')
                .Replace('٥', '5')
                .Replace('٦', '6')
                .Replace('٧', '7')
                .Replace('٨', '8')
                .Replace('٩', '9')
                .Replace('٠', '0').Trim();

            if (removePostOrAnteMeridiem)
            {
                text = text.RemovePostOrAnteMeridiem();
            }

            return text;

        }

        public static string RemovePostOrAnteMeridiem(this string input)
        {
            input = input?.Replace("م", "")
                .Replace("ص", "")
                .Trim();

            return input?.Replace("pm", "")
                .Replace("am", "")
                .Trim();
        }

    }
}