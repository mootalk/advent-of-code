using Day24.Arguments;

namespace Day24.Instructions
{
    public class InputInstruction : Instruction
    {
        private readonly InputReader _inputReader;
        public override Variable Variable { get; }

        public override InstructionLine InstructionLine { get; }

        public InputInstruction(Variable variable, InputReader inputReader)
        {
            Variable = variable;
            _inputReader = inputReader;

            InstructionLine = new InstructionLine("inp", variable);
        }

        public override void Execute()
        {
            var input = _inputReader.Read();
            Variable.Store(input);
        }
    }
}
