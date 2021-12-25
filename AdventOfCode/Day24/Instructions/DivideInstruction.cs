using Day24.Arguments;

namespace Day24.Instructions
{
    public class DivideInstruction : Instruction
    {
        public override Variable Variable { get; }

        public override InstructionLine InstructionLine { get; }

        public Argument Argument { get; }

        public DivideInstruction(Variable variable, Argument argument)
        {
            Variable = variable;
            Argument = argument;

            InstructionLine = new InstructionLine("div", variable, argument);
        }

        public override void Execute()
        {
            var result = Variable.Value / Argument.Value;
            Variable.Store(result);
        }
    }
}