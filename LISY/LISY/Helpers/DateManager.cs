using System;

namespace LISY.Helpers
{
    public static class DateManager
    {
        public static DateTime GetDate(long t)
        {
            string time = Convert.ToString(t);
            if (time.Length < 8)
                time = "0" + time;
            int day = Convert.ToInt32(time.Substring(0, 2)),
                month = Convert.ToInt32(time.Substring(2, 2)),
                year = Convert.ToInt32(time.Substring(4, 4));
            return new DateTime(year, month, day);
        }

        public static long GetLong(DateTime time)
        {
            string day = Convert.ToString(time.Day),
                month = Convert.ToString(time.Month),
                year = Convert.ToString(time.Year);
            if (day.Length < 2)
            {
                day = "0" + day;
            }
            if (month.Length < 2)
            {
                month = "0" + month;
            }
            if (year.Length < 2)
            {
                year = "0" + year;
            }
            return Convert.ToInt64(day + month + year);
        }
    }
}
