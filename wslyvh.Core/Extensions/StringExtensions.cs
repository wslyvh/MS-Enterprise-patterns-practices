
namespace wslyvh.Core.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveLastSeparator(this string str, string separator)
        {
            if (str.EndsWith(separator))
                return str.Substring(0, str.Length - separator.Length);

            return str;
        }
    }
}
