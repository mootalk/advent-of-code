using Day24;

Console.WriteLine();
Console.WriteLine();
Console.WriteLine();
Console.WriteLine();
Console.WriteLine();

Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss.ffff}] Started");
var monadAluProgramCode = File.ReadAllText(args[0]);
Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss.ffff}] Program code read");
var largestMonadNumber = Solution.FindLargestMonadNumberMultiThreaded(monadAluProgramCode);
Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss.ffff}] Number found");

Console.WriteLine($"Largest monad number is: { largestMonadNumber }");