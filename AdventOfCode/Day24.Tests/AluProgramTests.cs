using Xunit;

namespace Day24.Tests
{
    public class AluProgramTests
    {
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
    }
}