using System;

namespace ProjectEulerWebApp.Util
{
    public static class DateParser
    {
        public static DateTime ParseEulerDate(string eulerDate)
        {
            var values = eulerDate.Replace(",", "").Split(' ');
            return DateTime.Parse(
                values[2] + " " + // Month
                values[1].Substring(0, values[1].Length - 2) + " " + // Day
                values[3] + " " + // Year
                values[4] // Time
                + "Z" // UTC
            );
        }
    }
}