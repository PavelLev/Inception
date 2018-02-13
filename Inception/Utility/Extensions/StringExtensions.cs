using System;

namespace Inception.Utility.Extensions
{
    public static class StringExtensions
    {
        public static string Substring(this string source, string value, int startIndex = 0)
        {
            if (startIndex < 0)
            {
                startIndex = source.Length + startIndex;
            }

            var length = source.IndexOf(value, startIndex, StringComparison.Ordinal);

            if (length == -1)
            {
                length = source.Length;
            }

            var result = source.Substring(0, length);

            return result;
        }



        public static string RemoveFromStart
            (
            this string source,
            string value
            )
        {
            return source.StartsWith(value)
                ?
                source.Substring(value.Length)
                :
                source;
        }
    }
}
