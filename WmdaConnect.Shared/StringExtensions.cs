using System.Text.RegularExpressions;

namespace WmdaConnect.Shared
{
    public static class StringExtensions
    {
        public static bool IsValidION(this string s)
        {
            return s != null && Regex.IsMatch(s, @"^\d\d\d\d$");
        }

        public static void ValidateION(this string s)
        {
            if (s.IsValidION()) return;

            throw new WmdaConnectException("ION must be between 0000-9999");
        }

        public static bool IsUnvalidatedTestION(this string s)
        {
            return s is {Length: 4} && string.CompareOrdinal(s, "0000") > -1 && string.CompareOrdinal(s, "0500") < 1;
        }
    }
}
