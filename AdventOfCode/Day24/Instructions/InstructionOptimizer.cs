using Day24.Arguments;

namespace Day24.Instructions
{
    public static class InstructionOptimizer
    {
        public static List<Instruction> OptimizeInstructions(List<Instruction> original)
        {
            var optimizedInstructions = new List<Instruction>();

            var isOptimized = false;
            var initializedVariables = new HashSet<Variable>();
            for (int i = 0; i < original.Count; i++)
            {
                var current = original[i];

                var (isCurrentOptimized, optimizedInstruction) = OptimizeInstruction();
                var instructionToStore = isCurrentOptimized
                    ? optimizedInstruction
                    : current;
                isOptimized |= isCurrentOptimized;

                if (instructionToStore is not null)
                {
                    optimizedInstructions.Add(instructionToStore);
                }

                (bool isOptimized, Instruction? optimizedInstruction) OptimizeInstruction()
                {
                    if (current is DivideInstruction div)
                    {
                        if (!initializedVariables.Contains(div.Variable))
                        {
                            return (true, null);
                        }

                        if (div.Argument is Number divNumber && divNumber.Value == 1)
                        {
                            return (true, null);
                        }
                    }

                    if (current is ModuloInstruction mod)
                    {
                        if (!initializedVariables.Contains(mod.Variable))
                        {
                            return (true, null);
                        }
                    }

                    if (current is MultiplierInstruction mul)
                    {
                        if (!initializedVariables.Contains(mul.Variable))
                        {
                            return (true, null);
                        }

                        if (mul.Argument is Number mulNumber && mulNumber.Value == 0)
                        {
                            initializedVariables.Remove(mul.Variable);
                            return (true, new ResetInstruction(mul.Variable));
                        }
                    }

                    if (current is AddInstruction add)
                    {
                        if (add.Argument is Variable addVariable
                            && !initializedVariables.Contains(addVariable))
                        {
                            return (true, null);
                        }
                    }

                    if (current is ResetInstruction rst)
                    {
                        var addArguments = new List<Argument>();
                        while (HasNext() && original[i + 1] is AddInstruction addNext
                            && rst.Variable == addNext.Variable)
                        {
                            i += 1;
                            addArguments.Add(addNext.Argument);
                        }

                        if (addArguments.Any())
                        {
                            initializedVariables.Add(rst.Variable);
                            return (true, new SetInstruction(rst.Variable, addArguments.ToArray()));
                        }
                    }

                    if (current is SetInstruction set)
                    {
                        var addArguments = new List<Argument>();
                        while (HasNext() && original[i + 1] is AddInstruction addNext
                            && set.Variable == addNext.Variable)
                        {
                            i += 1;
                            addArguments.Add(addNext.Argument);
                        }

                        if (addArguments.Any())
                        {
                            initializedVariables.Add(set.Variable);

                            var combinedArguments = set.Arguments.Concat(addArguments).ToArray();
                            return (true, new SetInstruction(set.Variable, combinedArguments));
                        }
                    }

                    if (current is EqualsInstruction equal
                        && HasNext() && original[i + 1] is EqualsInstruction nextEqual
                        && nextEqual.Argument is Number equalNumber && equalNumber.Value == 0
                        && equal.Variable.Reference == nextEqual.Variable.Reference)
                    {
                        initializedVariables.Add(equal.Variable);
                        i += 1;
                        return (true, new NotEqualsInstruction(equal.Variable, equal.Argument));
                    }

                    initializedVariables.Add(current.Variable);
                    return (false, null);
                }

                bool HasNext()
                    => i < original.Count - 1;
            }

            if (isOptimized)
            {
                return OptimizeInstructions(optimizedInstructions);
            }

            return optimizedInstructions;
        }
    }
}
