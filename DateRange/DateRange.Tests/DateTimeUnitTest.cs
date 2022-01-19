using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace DateRange.Tests
{
    public class DateTimeUnitTest : OutputUnitTest
    {
        public DateTimeUnitTest(ITestOutputHelper output) : base(output)
        {
        }

        public static IEnumerable<object[]> SplitYearDates => new List<object[]>
        {
            new object[] 
            { 
                new DateTime(2021, 12, 25), new DateTime(2021, 1, 14)
            },
            new object[]
            {
                new DateTime(2020, 12, 25), new DateTime(2021, 1, 14)
            },
            new object[]
            {
                new DateTime(2015, 2, 16), new DateTime(2015, 2, 14)
            },
            new object[]
            {
                new DateTime(1, 12, 25), new DateTime(9999, 12, 5)
            },
            new object[]
            {
                new DateTime(2016, 1, 1), new DateTime(2017, 1, 5)
            },
            new object[]
            {
                new DateTime(2017, 1, 1), new DateTime(2017, 2, 5)
            },
            new object[]
            {
                new DateTime(2017, 1, 1), new DateTime(2017, 1, 5)
            },
        };
           

        [Theory]
        [MemberData(nameof(SplitYearDates))]
        public void DateEqualTest(DateTime firstDate, DateTime secondDate)
        {
            string? response = Program.Initialize(firstDate, secondDate);
            string[] split = response.Split('-');

            bool buildDayAndMonth = false;
            bool buildYear = false;
            DateTime[] dates = new DateTime[2];
            for (int i = 0; i < split.Length; i++)
            {
                string temp = split[i].Trim();

                // same year and month:
                if (int.TryParse(temp, out int day))
                {
                    buildDayAndMonth = true;
                    // placeholder date:
                    dates[i] = new DateTime(DateTime.Now.Year, DateTime.Now.Month, day);
                    continue;
                }

                if (DateTime.TryParse(temp, out DateTime parsedDate))
                {
                    output.WriteLine(parsedDate.ToString(Program.formatTypes[0]));

                    dates[i] = parsedDate;

                    // same year:
                    if (!temp.Contains(parsedDate.Year.ToString()))
                    {
                        buildYear = true;
                        continue;
                    }
                }
            }

            if (buildYear)
            {
                dates[0] = new DateTime(dates[1].Year, dates[0].Month, dates[0].Day);
            }

            if (buildDayAndMonth)
            {
                dates[0] = new DateTime(dates[1].Year, dates[1].Month, dates[0].Day);
            }

            if (firstDate.Ticks > secondDate.Ticks)
            {
                Assert.Equal(secondDate, dates[0]);
            }

            else if (secondDate.Ticks > firstDate.Ticks)
            {
                Assert.Equal(firstDate, dates[0]);
            }
        }
    }
}
