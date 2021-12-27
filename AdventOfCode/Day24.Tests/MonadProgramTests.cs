using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace Day24.Tests
{
    public class MonadProgramTests
    {
        private readonly ITestOutputHelper _testOutput;
        private readonly ITracer _testTracer;

        public MonadProgramTests(ITestOutputHelper testOutput)
        {
            _testOutput = testOutput;
            _testTracer = new TestOutputTracer(_testOutput);
        }

        [Fact]
        public void TestFile()
        {
            var programCode = File.ReadAllText(@"c:\git\mootalk\advent-of-code\AdventOfCode\Day24\input.txt");
            var program = MonadProgram.Parse(programCode);

            var number = program.FindNumber();

            if (number is null)
            {
                _testOutput.WriteLine("Number is null");
            }
            else
            {
                _testOutput.WriteLine($"Number is: {number}");
            }
        }
    }
}
