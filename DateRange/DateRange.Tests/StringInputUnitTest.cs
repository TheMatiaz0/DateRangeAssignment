using DateRange;
using System;
using Xunit;
using Xunit.Abstractions;

namespace DateRange.Tests
{
    public class StringInputUnitTest : OutputUnitTest
    {
        public StringInputUnitTest(ITestOutputHelper output) : base(output)
        {
        }

        private static string CutToMonth(string date)
        {
            return date.Remove(5);
        }

        private static string CutToDay(string date)
        {
            return date.Remove(2);
        }

        private static string FormatDatesToRange(VisibleDate dateType, string date1, string date2)
        {
            switch(dateType)
            {
                case VisibleDate.DayMonth:
                    date1 = CutToMonth(date1);
                    break;

                case VisibleDate.Day:
                    date1 = CutToDay(date1);
                    break;
            }

            return $"{date1} - {date2}";
        }

        [Theory]
        [InlineData("14.01.2020", "14.02.2021")]
        [InlineData("21.01.2020", "01.05.2050")]
        [InlineData("01.01.2016", "05.01.2017")]
        [InlineData("20.07.2016", "05.01.2017")]
        [InlineData("21.01.1090", "01.05.2040")]
        [InlineData("21.01.1000", "01.05.3000")]
        public void YearEqualTest(params string[] args)
        {
            string? programOutput = InitializeWithOutput(args);
            Assert.Equal(FormatDatesToRange(VisibleDate.Full, args[0], args[1]), programOutput);
        }

        [Theory]
        [InlineData("01.01.2020", "31.01.2020")]
        [InlineData("20.06.2020", "30.06.2020")]
        [InlineData("25.10.2020", "31.10.2020")]
        [InlineData("01.01.2017", "05.01.2017")]
        [InlineData("20.01.2020", "31.01.2020", "21.01.2020", "29.01.2020")]
        public void DayEqualTest(params string[] args)
        {
            string? programOutput = InitializeWithOutput(args);
            Assert.Equal(FormatDatesToRange(VisibleDate.Day, args[0], args[1]), programOutput);
        }

        [Theory]
        [InlineData("31.01.2020", "01.01.2020")]
        [InlineData("30.06.2020", "20.06.2020")]
        [InlineData("31.10.2020", "25.10.2020")]
        [InlineData("05.01.2017", "01.01.2017")]
        public void ReverseDayEqualTest(params string[] args)
        {
            string? programOutput = InitializeWithOutput(args);
            Assert.Equal(FormatDatesToRange(VisibleDate.Day, args[1], args[0]), programOutput);
        }

        [Theory]
        [InlineData("31.01.2021", "14.02.2021")]
        [InlineData("31.01.2077", "01.12.2077")]
        [InlineData("01.01.2016", "01.12.2016")]
        [InlineData("01.01.2099", "01.12.2099")]
        [InlineData("01.01.2017", "05.02.2017")]
        public void MonthEqualTest(params string[] args)
        {
            string? programOutput = InitializeWithOutput(args);
            Assert.Equal(FormatDatesToRange(VisibleDate.DayMonth, args[0], args[1]), programOutput);
        }

        [Theory]
        [InlineData("28.02.2021", "14.01.2021")]
        [InlineData("31.12.2077", "01.01.2077")]
        [InlineData("01.11.2016", "01.03.2016")]
        [InlineData("01.10.2099", "01.04.2099")]
        public void ReverseMonthEqualTest(params string[] args)
        {
            string? programOutput = InitializeWithOutput(args);
            Assert.Equal(FormatDatesToRange(VisibleDate.DayMonth, args[1], args[0]), programOutput);
        }

        [Theory]
        [InlineData("20.30.2016", "21.31.2017")]
        [InlineData("40.07.2016", "50.01.2017")]
        [InlineData("XX.07.2016", "XX.01.2017")]
        [InlineData("test", "test")]
        [InlineData("00.00.0000", "00.00.0000")]
        [InlineData("00.00.0000", "00.00.0001")]
        [InlineData(".0000", ".0001")]
        [InlineData(".0000.", ".0001.")]
        [InlineData("...", "...")]
        [InlineData("///", "///")]
        [InlineData("20072020", "31072031")]
        [InlineData("", "")]
        public void FormatExceptionTest(params string[] args)
        {
            Assert.Throws<FormatException>(() => Program.Initialize(args));
        }

        [Theory]
        [InlineData("18.01.2022")]
        [InlineData("31.02.2022")]
        public void OutOfRangeExceptionTest(params string[] args)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Program.Initialize(args));
        }

        [Theory]
        [InlineData("14.02.2069", "14.01.2045")]
        public void ReverseYearEqualTest(params string[] args)
        {
            string? programOutput = InitializeWithOutput(args);
            Assert.Equal(FormatDatesToRange(VisibleDate.Full, args[1], args[0]), programOutput);
        }

        [Theory]
        [InlineData("18.01.2022", "18.01.2022")]
        public void SameDateEqualTest(params string[] args)
        {
            var programOutput = InitializeWithOutput(args);
            Assert.Equal(args[0], programOutput);
        }

        [Theory]
        [InlineData("5.01.2020", "1.05.2050")]
        public void FormatDayEqualTest(params string[] args)
        {
            string? programOutput = InitializeWithOutput(args);
            Assert.Equal(FormatDatesToRange(VisibleDate.Full, "05.01.2020", "01.05.2050"), programOutput);
        }

        [Theory]
        [InlineData("20.07.999", "05.01.1020")]
        public void FormatYearEqualTest(params string[] args)
        {
            string? programOutput = InitializeWithOutput(args);
            Assert.Equal(FormatDatesToRange(VisibleDate.Full, "20.07.0999", "05.01.1020"), programOutput);
        }
    }
}