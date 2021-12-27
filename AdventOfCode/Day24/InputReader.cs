namespace Day24
{
    public class InputReader
    {
        private int[] _input = Array.Empty<int>();
        private int _index;

        public void SetInput(int[] input)
        {
            _input = input;
            _index = 0;
        }

        public int Read()
            => _input[_index++];
    }
}
