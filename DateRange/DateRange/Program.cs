using System;
using System.Globalization;

namespace DateRange
{
    public class Program
    {
        private static void Main(string[] args)
        {
            DateTime firstDate = DateTime.Parse(args[0]);
            DateTime secondDate = DateTime.Parse(args[1]);

            Console.WriteLine(firstDate.ToString("dd.MM.yyyy"));
            Console.WriteLine(secondDate.ToString("dd.MM.yyyy"));
            int yearDifference = firstDate.Year - secondDate.Year;
            if (CheckValue(yearDifference, firstDate, secondDate, "dd.MM.yyyy"))
            {
                return;
            }

            int monthDifference = firstDate.Month - secondDate.Month;
            if (CheckValue(monthDifference, firstDate, secondDate, "dd.MM"))
            {
                return;
            }

            int dayDifference = firstDate.Day - secondDate.Day;
            if (CheckValue(dayDifference, firstDate, secondDate, "dd"))
            {
                return;
            }
        }

        private static bool CheckValue(int difference, DateTime firstDate, DateTime secondDate, string first)
        {
            if (difference > 0)
            {
                Console.WriteLine(GetTrueRange(secondDate, firstDate, first));
                return true;
            }

            else if (difference < 0)
            {
                Console.WriteLine(GetTrueRange(firstDate, secondDate, first));
                return true;
            }

            return false;
        }

        private static string GetTrueRange(DateTime firstDate, DateTime secondDate, string first)
        {
            return ($"{firstDate.ToString(first)} - {secondDate.ToString("dd.MM.yyyy")}");
        }
    }
}