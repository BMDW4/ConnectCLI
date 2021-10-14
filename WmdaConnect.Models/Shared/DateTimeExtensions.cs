using System;

namespace WmdaConnect.Models.Shared
{
    public static class DateTimeExtensions
    {
        public static string ToEmdis(this DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMdd");
        }
    }
}
