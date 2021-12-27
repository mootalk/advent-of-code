using System.Text;

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

            StringBuilder currentProgram = new ();
            foreach (var line in ReadProgramLines())
            {
                if (line == "inp w")
                {
                    if (currentProgram.Length > 0)
                    {
                        monadProgramModules.Add(currentProgram.ToString());
                        currentProgram = new ();
                    }
                    else
                    {
                        currentProgram = new ("inp z");
                    }

                    currentProgram.AppendLine(line);
                }
            }

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

        public bool IsValid(sbyte[] input)
        {
            
        }
    }
}
