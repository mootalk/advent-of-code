using System.Text;
using Day24.Instructions;

namespace Day24
{
    public class MonadProgram
    {
        private readonly List<AluProgram> _modules;

        private MonadProgram(List<AluProgram> modules)
            => _modules = modules;

        public static MonadProgram Parse(string programCode)
        {
            var monadProgramModules = new List<string>();

            StringBuilder currentProgram = new();
            foreach (var line in ReadProgramLines())
            {
                if (line == "inp w")
                {
                    if (currentProgram.Length > 0)
                    {
                        monadProgramModules.Add(currentProgram.ToString());
                        currentProgram = new();
                        currentProgram.AppendLine("inp z");
                    }
                    else
                    {
                        currentProgram = new();
                    }

                }

                currentProgram.AppendLine(line);
            }
            monadProgramModules.Add(currentProgram.ToString());

            var modules = new List<AluProgram>();
            foreach (var monadProgramModule in monadProgramModules)
            {
                modules.Add(AluProgram.Parse(monadProgramModule));
            }

            return new MonadProgram(modules);

            IEnumerable<string> ReadProgramLines()
            {
                var programReader = new StringReader(programCode);
                while (true)
                {
                    var currentLine = programReader.ReadLine();
                    if (currentLine is null)
                    {
                        break;
                    }

                    yield return currentLine;
                }
            }
        }

        private const sbyte ModelNumberSize = 14;
        public string? FindNumber()
        {
            var result = FindNext(0, Array.Empty<sbyte>());
            if (result == null)
            {
                return null;
            }

            return string.Join("", result);
        }

        private readonly static sbyte[] Digits = new sbyte[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 };

        private sbyte[]? FindNext(int position, sbyte[] currentNumber)
        {
            AluProgram? previousModule = null;
            if (position > 0)
            {
                previousModule = _modules[position - 1];
            }

            if (position >= ModelNumberSize)
            {
                var z = previousModule.GetVariableValue('z');
                if (z == 0)
                {
                    return currentNumber;
                }
                else
                {
                    return null;
                }
            }

            var module = _modules[position];

            foreach (var digit in Digits)
            {
                var nextNumber = GetNextNumber(digit);
                int[] arguments = previousModule is not null
                    ? new[] { previousModule.GetVariableValue('z'), digit }
                    : new int[] { digit };

                module.Run(arguments);
                var x = module.GetVariableValue('x');

                if (module.HasInstruction<DivideInstruction>())
                {
                    if (x == 0)
                    {
                        var next1 = FindNext(position + 1, nextNumber);
                        if (next1 != null)
                        {
                            return next1;
                        }
                    }

                    continue;
                }

                var next = FindNext(position + 1, nextNumber);
                if (next != null)
                {
                    return next;
                }
            }

            return null;

            sbyte[] GetNextNumber(sbyte digit)
            {
                var result = new sbyte[currentNumber.Length + 1];
                currentNumber.CopyTo(result, 0);
                result[^1] = digit;

                return result;
            }
        }
    }
}
