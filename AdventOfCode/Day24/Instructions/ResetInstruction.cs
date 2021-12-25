using Day24.Arguments;

namespace Day24.Instructions
{
    public class ResetInstruction : Instruction
    {
        public override Variable Variable { get; }

        public override InstructionLine InstructionLine { get; }


        public ResetInstruction(Variable variable)
        {
            Variable = variable;

            InstructionLine = new InstructionLine("rst", variable);
        }

        public override void Execute() 
            => Variable.Clear();
    }
}
