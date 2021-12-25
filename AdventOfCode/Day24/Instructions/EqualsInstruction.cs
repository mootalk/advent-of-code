using Day24.Arguments;

namespace Day24.Instructions
{
    public class EqualsInstruction : Instruction
    {
        public override Variable Variable { get; }

        public override InstructionLine InstructionLine { get; }

        public Argument Argument { get; }

        public EqualsInstruction(Variable variable, Argument argument)
        {
            Variable = variable;
            Argument = argument;

            InstructionLine = new InstructionLine("eql", variable, argument);
        }

        public override void Execute()
        {
            var result = Variable.Value == Argument.Value;
            Variable.Store(result ? 1 : 0);
        }
    }
}