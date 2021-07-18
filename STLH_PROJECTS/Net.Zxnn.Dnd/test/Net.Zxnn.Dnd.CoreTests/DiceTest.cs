using System;

using Net.Zxnn.Dnd.Core;
using Xunit;
using Xunit.Abstractions;

namespace Net.Zxnn.Dnd.Test
{
    public class DiceTest
    {
        private readonly ITestOutputHelper output;
        public DiceTest(ITestOutputHelper outputHelper)
        {
            this.output = outputHelper;
        }
        
        [Fact]
        public void Test()
        {
            Dice dice = new Dice(6);

            int[] numbers = {0, 0, 0, 0, 0, 0};
            for (int i = 0; i < 600_000_000; ++i)
            {
                int n = dice.Roll();
                numbers[n-1] += 1;
            }

            output.WriteLine("");
            for (int i = 0; i < numbers.Length; ++i)
            {
                output.WriteLine($"{i+1}:\t{numbers[i],9}");
            }

            Assert.True(true);
        }
    }
}