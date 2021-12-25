using Day24.Arguments;

namespace Day24.Instructions
{
    public class ModuloInstruction : Instruction
    {
        public override Variable Variable { get; }

        public override InstructionLine InstructionLine { get; }

        private readonly Argument _argument;

        public ModuloInstruction(Variable variable, Argument argument)
        {
            Variable = variable;
            _argument = argument;

            InstructionLine = new InstructionLine("mod", variable, argument);
        }

        public override void Execute()
        {
            var result = Variable.Value % _argument.Value;
            Variable.Store(result);
        }
    }
}