using System;
using System.Collections.Generic;
using System.Text;

namespace StaplePuck.Core
{
    public static class DateExtensions
    {
        public static bool IsToday(this DateTime date)
        {
            var todaysDate = TodaysDate();
            var newDate = date.Subtract(new TimeSpan(5, 0, 0));
            var value = newDate.Date.Equals(todaysDate.Date);
            return value;
        }

        public static string ToGameDateId(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }

        public static DateTime TodaysDate()
        {
            var date = DateTime.UtcNow;

            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                if (date.Hour < 16)
                {
                    date = date.Subtract(new TimeSpan(1, 0, 0, 0));
                }
            }
            else
            {
                if (date.Hour < 20)
                {
                    date = date.Subtract(new TimeSpan(1, 0, 0, 0));
                }
            }
            return date;
        }

        public static string TodaysDateId()
        {
            var date = TodaysDate();
            return date.ToString("yyyy-MM-dd");
        }
    }
}
