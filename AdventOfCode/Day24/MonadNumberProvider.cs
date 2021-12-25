namespace Day24
{
    public static class MonadNumberProvider
    {
        private const long MaxNumber = 99_999_999_999_999;
        private const long MinNumber = 11_111_111_111_111;

        public static IEnumerable<sbyte[]> AllFromLargestFirst()
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

            static bool ContainsZero(sbyte[] array)
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

        public static sbyte[] CreateArray(long number)
        {
            var array = new sbyte[14];
            var currentPosition = 13;
            while (number > 0)
            {
                array[currentPosition--] = (sbyte)(number % 10);
                number /= 10;
            }

            return array;
        }
    }
}
