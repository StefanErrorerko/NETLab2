using System;

namespace NET_Lab2.Instruments
{
    public static class DateTimeAccuracyMonitor
    {
        public static void CheckDate(DateTime date)
        {
            if (date >= DateTime.Now || date.Year <= 1800)
            {
                throw new ImpossibleDateException($"{date} is an unappropriate date");
            }
        }
        public static void CheckDate(int year)
        {
            if (year >= DateTime.Now.Year || year <= 1800)
            { 
                throw new ImpossibleDateException($"{year} is an unappropriate date"); 
            }
        }
    }
}
