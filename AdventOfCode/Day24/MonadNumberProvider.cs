namespace Day24
{
    public static class MonadNumberProvider
    {
        private const long MaxNumber = 99_999_999_999_999;
        private const long MinNumber = 11_111_111_111_111;

        public static IEnumerable<int[]> AllFromLargestFirst()
        {
            var current = MaxNumber;
            while (current >= MinNumber)
            {
                var array = CreateArray(current--);

                if (!ContainsZero(array))
                {
                    yield return array;
                }
            }

            static bool ContainsZero(int[] array)
            {
                foreach (var digit in array)
                {
                    if (digit == 0)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public static int[] CreateArray(long number)
        {
            var array = new int[14];
            var currentPosition = 13;
            while (number > 0)
            {
                array[currentPosition--] = (int)number % 10;
                number /= 10;
            }

            return array;
        }
    }
}
