using System.Text;
using Day24.Arguments;

namespace Day24.Instructions
{
    public class InstructionLine
    {
        private readonly string _line;
        private readonly string _lineFormat;
        private IReadOnlyCollection<Argument> _arguments;

        public InstructionLine(string code, params Argument[] arguments)
        {
            _arguments = arguments ?? Array.Empty<Argument>();

            _lineFormat = InterpretInstructionLine(code, _arguments);

            var argumentCodes = _arguments.Select(a => a.Code).ToArray();
            _line = string.Format(_lineFormat, argumentCodes);
        }

        public string Format()
        {
            var values = _arguments.Select(a => a.Value).Cast<object>().ToArray();
            var formatted = string.Format(_lineFormat, values);
            return $"{_line} => ({formatted})";
        }

        protected static string InterpretInstructionLine(string code, IReadOnlyCollection<Argument> arguments)
        {
            var interpretedLineBuilder = new StringBuilder(code);
            for (int i = 0; i < arguments.Count; i++)
            {
                interpretedLineBuilder.Append(" {").Append(i).Append('}');
            }

            return interpretedLineBuilder.ToString();
        }

        public override string ToString()
            => _line;
    }
}
