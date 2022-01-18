using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace DateRange.Tests
{
    public class OutputUnitTest
    {
        private readonly ITestOutputHelper output;

        public OutputUnitTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        protected string? InitializeWithOutput(string[] args)
        {
            string? programOutput = Program.Initialize(args);
            output.WriteLine(programOutput);
            return programOutput;
        }

    }
}
