using Day24.Arguments;

namespace Day24.Instructions
{
    public class MultiplierInstruction : Instruction
    {
        public override Variable Variable { get; }
        public Argument Argument { get; }

        public override InstructionLine InstructionLine { get; }

        public MultiplierInstruction(Variable variable, Argument argument)
        {
            Variable = variable;
            Argument = argument;

            InstructionLine = new InstructionLine("mul", variable, argument);
        }

        public override void Execute()
        {
            var result = Variable.Value * Argument.Value;
            Variable.Store(result);
        }
    }
}