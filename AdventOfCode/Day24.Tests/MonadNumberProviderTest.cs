using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Day24.Tests
{
    public class MonadNumberProviderTest
    {
        [Fact]
        public void MonadNumber_is_provided_with_largest_first()
        {
            var numbers = MonadNumberProvider.AllFromLargestFirst();

            var firstNumber = numbers.First();

            Assert.All(firstNumber, item => Assert.Equal(9, item));
        }

        [Fact]
        public void MonadNumber_is_provided_without_zero()
        {
            var numbers = MonadNumberProvider.AllFromLargestFirst();

            var firstNumbers = numbers.Take(15);

            foreach (var firstNumber in firstNumbers)
            {
                Assert.All(firstNumber, item => Assert.NotEqual(0, item));
            }
        }
    }
}
