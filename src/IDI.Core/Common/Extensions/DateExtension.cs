using System;

namespace IDI.Core.Common.Extensions
{
    public static class DateExtension
    {
        public static string AsShortDate(this DateTime date)
        {
            return date.ToString("yyyy/M/d");
        }

        public static string AsLongDate(this DateTime date)
        {
            return date.ToString("yyyy/M/d HH:mm:ss");
        }

        public static DateTime AsDate(this string s)
        {

            DateTime date = new DateTime(2000, 1, 1);

            if (DateTime.TryParse(s, out date))
                return date;

            return date;
        }
    }
}
