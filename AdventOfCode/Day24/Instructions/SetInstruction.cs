using Day24.Arguments;

namespace Day24.Instructions
{
    public class SetInstruction : Instruction
    {
        public IReadOnlyCollection<Argument> Arguments { get; }

        public override Variable Variable { get; }

        public override InstructionLine InstructionLine { get; }

        public SetInstruction(Variable variable, params Argument[] arguments)
        {
            Variable = variable;
            Arguments = SimplifyArguments(arguments ?? Array.Empty<Argument>());

            var allArguments = new List<Argument>
            {
                variable
            };
            allArguments.AddRange(Arguments);
            InstructionLine = new InstructionLine("set", allArguments.ToArray());
        }

        private static List<Argument> SimplifyArguments(IEnumerable<Argument> arguments)
        {
            Number? numberArgument = null;
            var otherArguments = new List<Argument>();
            foreach (var argument in arguments)
            {
                if (argument is Number numberArg)
                {
                    if (numberArgument is null)
                    {
                        numberArgument = numberArg;
                    }
                    else
                    {
                        numberArgument += numberArg;
                    }
                }
                else
                {
                    otherArguments.Add(argument);
                }
            }

            if (numberArgument is not null)
            {
                otherArguments.Add(numberArgument);
            }

            return otherArguments;
        }


        public override void Execute()
        {
            var result = Variable.Value;
            foreach (var argument in Arguments)
            {
                result += argument.Value;
            }
            Variable.Store(result);
        }
    }
}
