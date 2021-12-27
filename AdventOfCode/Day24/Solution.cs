using System.Collections.Concurrent;
using System.Diagnostics;

namespace Day24
{
    public static class Solution
    {
        private static readonly BlockingCollection<IEnumerable<int[]>> Queue = new(100);
        private static Timer? StatsUpdater = null;

        public static string? FindLargestMonadNumberMultiThreaded(string monadProgramCode)
        {
            var timer = Stopwatch.StartNew();
            long count = 1;
            long absBest = int.MaxValue;

            var results = new List<int[]>();

            var threads = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                threads.Add(Task.Run(async () =>
                {
                    var monadAluProgram = AluProgram.Parse(monadProgramCode);

                    while (true)
                    {
                        while (Queue.TryTake(out var item))
                        {
                            var size = 0;
                            foreach (var number in item)
                            {
                                size++;
                                monadAluProgram.Run(number);
                                var result = monadAluProgram.GetVariableValue('z');

                                Interlocked.Read(ref absBest);

                                if (result == 0)
                                {
                                    results.Add(number);
                                    Interlocked.Add(ref count, size);
                                    return;
                                }
                            }

                            Interlocked.Add(ref count, size);
                        }
                        await Task.Delay(250);
                    }
                }));
            }

            Task.Run(() =>
            {
                var possibleNumbers = MonadNumberProvider.AllFromLargestFirst();
                var numberChunks = Chunk(possibleNumbers, 5_000);
                foreach (var chunk in numberChunks)
                {
                    Queue.Add(chunk);
                }
            });

            StatsUpdater = new Timer(_ =>
            {
                var currentCount = Interlocked.Read(ref count);

                Console.SetCursorPosition(0, 0);
                var average = currentCount / timer.Elapsed.TotalSeconds;
                Console.WriteLine($"Elapsed: {timer.Elapsed:hh\\:mm\\:ss\\.ffff}");
                Console.WriteLine($"Queue size: {Queue.Count:N0}");
                Console.WriteLine($"Threads: {threads.Count}");
                Console.WriteLine($"Total: {currentCount:N0} #/sec {average:N2}");

            }, null, 0, 500);
            GC.KeepAlive(StatsUpdater);

            Console.ReadLine();

            return null;
        }

        public static string? FindLargestMonadNumber(string monadProgramCode)
        {
            var timer = Stopwatch.StartNew();
            long count = 1;

            StatsUpdater = new Timer(_ =>
            {
                var currentCount = Interlocked.Read(ref count);

                Console.SetCursorPosition(0, 0);
                var average = currentCount / timer.Elapsed.TotalSeconds;
                Console.WriteLine($"Elapsed: {timer.Elapsed:hh\\:mm\\:ss\\.ffff}");
                Console.WriteLine($"Total: {currentCount:N0} #/sec {average:N2}");

            }, null, 0, 500);
            GC.KeepAlive(StatsUpdater);

            var monadAluProgram = AluProgram.Parse(monadProgramCode);
            var possibleNumbers = MonadNumberProvider.AllFromLargestFirst();
            foreach (var possibleNumber in possibleNumbers)
            {
                monadAluProgram.Run(possibleNumber);
                var result = monadAluProgram.GetVariableValue('z');
                if (result == 0)
                {
                    return possibleNumber.ToString();
                }

                count += 1;
            }

            return null;
        }

        private static IEnumerable<IEnumerable<T>> Chunk<T>(IEnumerable<T> items, int chunkSize)
        {
            var chunk = new List<T>();
            foreach (var item in items)
            {
                chunk.Add(item);
                if (chunk.Count >= chunkSize)
                {
                    yield return chunk;
                    chunk = new List<T>();
                }
            }

            yield return chunk;
        }
    }
}
