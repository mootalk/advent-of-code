using Xunit.Abstractions;

namespace Day24.Tests
{
    public class TestOutputTracer : ITracer
    {
        private readonly ITestOutputHelper _testOutput;

        public TestOutputTracer(ITestOutputHelper testOutput) 
            => _testOutput = testOutput;

        public void Trace(string message, params object[] args) 
            => _testOutput.WriteLine(message, args);
    }
}
