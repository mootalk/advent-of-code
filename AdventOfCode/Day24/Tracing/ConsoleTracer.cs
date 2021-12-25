namespace Day24.Tracing
{
    public class ConsoleTracer : ITracer
    {
        public void Trace(string message, params object[] args) 
            => Console.WriteLine(message, args);
    }
}
