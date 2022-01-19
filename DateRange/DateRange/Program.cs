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
        public static readonly string[] formatDateString =
        {
            "dd.MM.yyyy",
            "dd.MM",
            "dd"
        };

        public static string GetVisibleDateString(VisibleDate date)
        {
            return formatDateString[(int)date];
        }

        private static string? CheckYear(DateTime firstDate, DateTime secondDate)
        {
            int yearDifference = firstDate.Year - secondDate.Year;
            return CheckValue(yearDifference, firstDate,
                secondDate, GetVisibleDateString(VisibleDate.Full));
        }

        private static string? CheckDayMonth(DateTime firstDate, DateTime secondDate)
        {
            int monthDifference = firstDate.Month - secondDate.Month;
            return CheckValue(monthDifference, firstDate,
                secondDate, GetVisibleDateString(VisibleDate.DayMonth));
        }

        private static string? CheckDay(DateTime firstDate, DateTime secondDate)
        {
            int dayDifference = firstDate.Day - secondDate.Day;
            return CheckValue(dayDifference, firstDate,
                secondDate, GetVisibleDateString(VisibleDate.Day));
        }

        private static Func<DateTime, DateTime, string?>[] checkFunctions = new Func<DateTime, DateTime, string?>[]
        {
            CheckYear,
            CheckDayMonth,
            CheckDay
        };

        /// <summary>
        /// Picks only two first arguments as string value.
        /// </summary>
        /// <param name="args"></param>
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
                throw new ArgumentOutOfRangeException(nameof(args), $"Argument or arguments are missing! Launch the program with two dates as arguments");
            }

            (DateTime firstDate, DateTime secondDate) = ParseDates(args);

            for (int i = 0; i < 3; i++)
            {
                string? dateFragmentString;
                if ((dateFragmentString = checkFunctions[i](firstDate, secondDate)) != null)
                {
                    return dateFragmentString;
                }
            }

            return firstDate.ToString(formatDateString[0]);
        }

        private static (DateTime, DateTime) ParseDates(string[] args)
        {
            if (!DateTime.TryParse(args[0], out DateTime firstDate))
            {
                throw new FormatException($"Bad format of first date argument! {args[0]} isn't the right format of date!");
            }
            if (!DateTime.TryParse(args[1], out DateTime secondDate))
            {
                throw new FormatException($"Bad format of second date argument! {args[1]} isn't the right format of date!");
            }

            return (firstDate, secondDate);
        }

        private static string? CheckValue(int difference, DateTime firstDate, DateTime secondDate, string first)
        {
            if (difference == 0)
            {
                return null;
            }

            if (difference > 0)
            {
                DateTime temp = firstDate;
                firstDate = secondDate;
                secondDate = temp;
            }

            return GetTrueRange(firstDate, secondDate, first);
        }

        private static string GetTrueRange(DateTime firstDate, DateTime secondDate, string firstString)
        {
            return $"{firstDate.ToString(firstString)} - {secondDate.ToString(formatDateString[0])}";
        }
    }
}