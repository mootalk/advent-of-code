using Day24.Arguments;

namespace Day24.Instructions
{
    public abstract class Instruction
    {
        public abstract Variable Variable { get; }

        public abstract void Execute();

        public abstract InstructionLine InstructionLine { get; }

        public override string ToString()
            => InstructionLine.Format();
    }
}
