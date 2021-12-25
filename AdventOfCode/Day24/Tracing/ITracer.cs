namespace Day24
{
    public interface ITracer
    {
        void Trace(string message, params object[] args);
    }
}
