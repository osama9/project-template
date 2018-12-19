using System;
using System.Globalization;

namespace ProjectTemplate.Core.Extensions
{
    public static class DateExtensions
    {
        public static string ToSystemDate(this DateTime date)
        {
            return date.ToString("dd-MM-yyyy");
        }

        public static string ToHijriDate(this DateTime date, string format = null)
        {
            var hijri = new UmAlQuraCalendar();

            var day = hijri.GetDayOfMonth(date);
            var month = hijri.GetMonth(date);
            var year = hijri.GetYear(date);

            var hijriDate = new DateTime(year, month, day);

            return hijriDate.ToString(format ?? "yyyy/MM/dd هـ");
        }

        public static DateTime ToStartOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        }
        public static DateTime ToEndOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
        }
    }
}