using System.Diagnostics;

namespace Day24
{
    public static class Solution
    {
        private const int MonadNumberSize = 14;

        public static string? FindLargestMonadNumber(string monadProgramCode)
        {
            var timer = Stopwatch.StartNew();
            int count = 1;
            var monadAluProgram = AluProgram.Parse(monadProgramCode);

            var possibleNumbers = MonadNumberProvider.AllFromLargestFirst();
            foreach (var possibleNumber in possibleNumbers)
            {
                monadAluProgram.Run(possibleNumber);
                var result = monadAluProgram.GetVariableValue('z');
                if (result == 0)
                {
                    WriteStats();
                    return possibleNumber.ToString();
                }

                if (count % 100_000 == 0)
                {
                    WriteStats();
                }

                count += 1;
            }

            void WriteStats()
            {
                var currentPos = Console.CursorLeft;
                var average = count / timer.Elapsed.TotalSeconds;
                Console.Write($"[{timer.Elapsed:hh\\:mm\\:ss\\.ffff}] Total: {count:N0} #/sec {average:N2}");
                Console.CursorLeft = currentPos;
            }

            return null;
        }
    }
}
