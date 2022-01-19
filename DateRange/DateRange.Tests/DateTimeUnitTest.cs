using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public static IEnumerable<DateTime[]> SplitYearDates => new List<DateTime[]>
        {
            new DateTime[] { new DateTime(2020, 14, 25), new DateTime(2021, 1, 4)}
        };
           

        [Theory]
        [MemberData(nameof(SplitYearDates))]
        public void YearEqualTest(params DateTime[] dates)
        {

        }
    }
}
