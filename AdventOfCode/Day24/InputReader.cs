namespace Day24
{
    public class InputReader
    {
        private sbyte[] _input = Array.Empty<sbyte>();
        private int _index;

        public void SetInput(sbyte[] input)
        {
            _input = input;
            _index = 0;
        }

        public int Read()
            => _input[_index++];
    }
}
