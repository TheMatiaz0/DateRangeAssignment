using System;
using System.Globalization;

namespace DateRange
{
    public enum VisibleDate
    {
        Full,
        DayMonth,
        Day
    }

    public class Program
    {
        private static readonly string[] visibleDateString =
        {
            "dd.MM.yyyy",
            "dd.MM",
            "dd"
        };

        private static string GetVisibleDateString(VisibleDate date)
        {
            return visibleDateString[(int)date];
        }

        private static void Main(string[] args)
        {
            Console.WriteLine(Initialize(args));
        }

        /// <summary>
        /// Main method of this DateRange program.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Range in string, null if dates are unavailable to range (for ex. 31.50.2020 - 31.49.2020).</returns>
        public static string? Initialize(string[] args)
        {
            if (args.Length < 2)
            {
                return null;
            }

            if (!DateTime.TryParse(args[0], out DateTime firstDate))
            {
                return null;
            }
            if (!DateTime.TryParse(args[1], out DateTime secondDate))
            {
                return null;
            }

            int yearDifference = firstDate.Year - secondDate.Year;
            if (CheckValue(yearDifference, firstDate, secondDate, GetVisibleDateString(VisibleDate.Full), out string? yearValue))
            {
                return yearValue;
            }

            int monthDifference = firstDate.Month - secondDate.Month;
            if (CheckValue(monthDifference, firstDate, secondDate, GetVisibleDateString(VisibleDate.DayMonth), out string? monthValue))
            {
                return monthValue;
            }

            int dayDifference = firstDate.Day - secondDate.Day;
            if (CheckValue(dayDifference, firstDate, secondDate, GetVisibleDateString(VisibleDate.Day), out string? dayValue))
            {
                return dayValue;
            }

            return null;
        }

        private static bool CheckValue(int difference, DateTime firstDate, DateTime secondDate, string first, out string? trueRange)
        {
            if (difference > 0)
            {
                trueRange = GetTrueRange(secondDate, firstDate, first);
                return true;
            }

            else if (difference < 0)
            {
                trueRange = GetTrueRange(firstDate, secondDate, first);
                return true;
            }

            trueRange = null;
            return false;
        }

        private static string GetTrueRange(DateTime firstDate, DateTime secondDate, string firstString)
        {
            return $"{firstDate.ToString(firstString)} - {secondDate:dd.MM.yyyy}";
        }
    }
}