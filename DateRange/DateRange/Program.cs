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
        #region Date formatting

        public static string[] FormatTypes { get; } =
        {
            "dd.MM.yyyy",
            "dd.MM",
            "dd"
        };

        public static string GetVisibleDateString(VisibleDate date)
        {
            return FormatTypes[(int)date];
        }

        #endregion

        #region Checking difference of each date element

        public static string? CheckYear(DateTime firstDate, DateTime secondDate)
        {
            int yearDifference = firstDate.Year - secondDate.Year;
            return GetSortedRange(yearDifference, firstDate,
                secondDate, GetVisibleDateString(VisibleDate.Full));
        }

        public static string? CheckMonth(DateTime firstDate, DateTime secondDate)
        {
            int monthDifference = firstDate.Month - secondDate.Month;
            return GetSortedRange(monthDifference, firstDate,
                secondDate, GetVisibleDateString(VisibleDate.DayMonth));
        }

        public static string? CheckDay(DateTime firstDate, DateTime secondDate)
        {
            int dayDifference = firstDate.Day - secondDate.Day;
            return GetSortedRange(dayDifference, firstDate,
                secondDate, GetVisibleDateString(VisibleDate.Day));
        }

        private static readonly Func<DateTime, DateTime, string?>[] checkFunctions = new Func<DateTime, DateTime, string?>[]
        {
            CheckYear,
            CheckMonth,
            CheckDay
        };

        #endregion

        /// <summary>
        /// Picks only two first arguments as string value.
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            Console.WriteLine(Initialize(args));
        }

        #region Parsing DateTime to string

        /// <summary>
        /// Main method of this DateRange program. Parses string arguments to DateTime type.
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

            return Initialize(firstDate, secondDate);
        }

        private static (DateTime, DateTime) ParseDates(string[] args)
        {
            if (!DateTime.TryParse(args[0], out DateTime firstDate))
            {
                throw new FormatException($"Bad format of first date argument! \"{args[0]}\" isn't the right format of date!");
            }
            if (!DateTime.TryParse(args[1], out DateTime secondDate))
            {
                throw new FormatException($"Bad format of second date argument! \"{args[1]}\" isn't the right format of date!");
            }

            return (firstDate, secondDate);
        }

        #endregion

        /// <summary>
        /// Second main method of this DateRange program. If you are looking for date parsing there is a method with string arguments.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Range in string, null if dates are unavailable to range (for ex. 31.50.2020 - 31.49.2020).</returns>
        public static string? Initialize(DateTime firstDate, DateTime secondDate)
        {
            // Check for year, month, day
            for (int i = 0; i < checkFunctions.Length; i++)
            {
                string? dateFragmentString;
                if ((dateFragmentString = checkFunctions[i](firstDate, secondDate)) != null)
                {
                    return dateFragmentString;
                }
            }

            return firstDate.ToString(FormatTypes[0]);
        }

        private static string? GetSortedRange(int difference, DateTime firstDate, DateTime secondDate, string first)
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
            return $"{firstDate.ToString(firstString)} - {secondDate.ToString(FormatTypes[0])}";
        }
    }
}