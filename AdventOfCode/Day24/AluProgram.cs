using System.Collections.Concurrent;
using Day24.Arguments;
using Day24.Instructions;

namespace Day24
{
    public class AluProgram
    {
        private readonly IDictionary<char, Variable> _variables;
        public IReadOnlyList<Instruction> Instructions { get; }
        private readonly InputReader _inputReader;
        private readonly bool _isTraceEnabled;
        private readonly ITracer? _tracer;

        private AluProgram(List<Instruction> instructions, IDictionary<char, Variable> variables, InputReader inputReader, bool isTraceEnabled, ITracer? tracer)
        {
            Instructions = instructions;
            _variables = variables;
            _inputReader = inputReader;
            _isTraceEnabled = isTraceEnabled;
            _tracer = tracer;
        }

        public bool HasInstruction<TInstruction>()
            where TInstruction : Instruction
        {
            foreach (var instruction in Instructions)
            {
                if (instruction is TInstruction)
                {
                    return true;
                }
            }

            return false;
        }

        public int GetVariableValue(char variableRef)
        {
            var variable = _variables[variableRef];
            return variable.Value;
        }

        public void Run(params int[] input)
        {
            Reset();
            _inputReader.SetInput(input);

            foreach (var instruction in Instructions)
            {
                if (_isTraceEnabled)
                {
                    var instructionLine = instruction.InstructionLine.Format();
                    instruction.Execute();
                    _tracer!.Trace($"{instructionLine} = {instruction.Variable.Value}");
                }
                else
                {
                    instruction.Execute();
                }
            }
        }

        private void Reset()
        {
            foreach (var variable in _variables.Values)
            {
                variable.Clear();
            }
        }

        public static AluProgram Parse(string programInstructions, bool isTraceEnabled = false, ITracer? tracer = null)
        {
            var instructionLines = programInstructions.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var inputReader = new InputReader();

            var instructions = new List<Instruction>();
            var variables = new ConcurrentDictionary<char, Variable>();
            foreach (var instructionLine in instructionLines)
            {
                var instructionParts = instructionLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.RemoveEmptyEntries);
                var instructionCode = instructionParts[0];
                var variableRef = instructionParts[1];

                switch (instructionCode)
                {
                    case "inp":
                        AddInputInstruction(instructionLine, variableRef);
                        break;
                    case "add":
                        AddAddInstruction(instructionLine, variableRef, instructionParts[2]);
                        break;
                    case "mul":
                        AddMultiplierInstruction(instructionLine, variableRef, instructionParts[2]);
                        break;
                    case "div":
                        AddDivideInstruction(instructionLine, variableRef, instructionParts[2]);
                        break;
                    case "mod":
                        AddModuloInstruction(instructionLine, variableRef, instructionParts[2]);
                        break;
                    case "eql":
                        AddEqualsInstruction(instructionLine, variableRef, instructionParts[2]);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException($"Instruction {instructionCode} cannot be interpreted");
                }
            }

            instructions = InstructionOptimizer.OptimizeInstructions(instructions);

            return new AluProgram(instructions, variables, inputReader, isTraceEnabled, tracer);

            void AddInputInstruction(string line, string variableRef)
            {
                var variable = GetVariable(variableRef);

                instructions.Add(new InputInstruction(variable, inputReader));
            }

            void AddAddInstruction(string line, string variableRef, string argumentRef)
            {
                var variable = GetVariable(variableRef);
                var argument = GetArgument(argumentRef);

                instructions.Add(new AddInstruction(variable, argument));
            }

            void AddMultiplierInstruction(string line, string variableRef, string argumentRef)
            {
                var variable = GetVariable(variableRef);
                var argument = GetArgument(argumentRef);


                instructions.Add(new MultiplierInstruction(variable, argument));
            }

            void AddDivideInstruction(string line, string variableRef, string argumentRef)
            {
                var variable = GetVariable(variableRef);
                var argument = GetArgument(argumentRef);

                instructions.Add(new DivideInstruction(variable, argument));
            }

            void AddModuloInstruction(string line, string variableRef, string argumentRef)
            {
                var variable = GetVariable(variableRef);
                var argument = GetArgument(argumentRef);

                instructions.Add(new ModuloInstruction(variable, argument));
            }

            void AddEqualsInstruction(string line, string variableRef, string argumentRef)
            {
                var variable = GetVariable(variableRef);
                var argument = GetArgument(argumentRef);

                instructions.Add(new EqualsInstruction(variable, argument));
            }


            Argument GetArgument(string argumentRef)
            {
                if (argumentRef.Length == 1 && !char.IsDigit(argumentRef[0]))
                {
                    return GetVariable(argumentRef);
                }

                var number = int.Parse(argumentRef);
                return new Number(number);
            }

            Variable GetVariable(string variableRef)
                => variables.GetOrAdd(variableRef[0], @ref => new(@ref));
        }
    }
}
