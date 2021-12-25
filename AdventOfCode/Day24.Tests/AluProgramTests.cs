using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace Day24.Tests
{
    public class AluProgramTests
    {
        private readonly ITestOutputHelper _testOutput;
        private readonly ITracer _testTracer;

        public AluProgramTests(ITestOutputHelper testOutput)
        {
            _testOutput = testOutput;
            _testTracer = new TestOutputTracer(_testOutput);
        }

        [Fact]
        public void AluProgram_knows_all_instructions()
        {
            var programCode = @"
inp a
add a 1
mul a 2
div a 2
mod a 2
eql a 0
".Trim();
            var program = AluProgram.Parse(programCode);

            program.Run(1);

            var result = program.GetVariableValue('a');
            Assert.Equal(1, result);
        }

        [Fact]
        public void AluProgram_variables_have_default_value_of_0()
        {
            var programCode = @"
mul w 1
mul x 1
mul y 1
mul z 1
".Trim();
            var program = AluProgram.Parse(programCode);

            program.Run();

            Assert.Equal(0, program.GetVariableValue('w'));
            Assert.Equal(0, program.GetVariableValue('x'));
            Assert.Equal(0, program.GetVariableValue('y'));
            Assert.Equal(0, program.GetVariableValue('z'));
        }

        [Fact]
        public void TestFile()
        {
            var programCode = File.ReadAllText(@"c:\git\mootalk\advent-of-code\AdventOfCode\Day24\input.txt");
            var program = AluProgram.Parse(programCode, isTraceEnabled:true, _testTracer);

            program.Run(9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9);
        }
    }
}