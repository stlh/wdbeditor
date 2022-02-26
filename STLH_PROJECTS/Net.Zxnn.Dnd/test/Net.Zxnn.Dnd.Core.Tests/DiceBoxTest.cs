using Net.Zxnn.Dnd.Core;
using Xunit;
using Xunit.Abstractions;

namespace Net.Zxnn.Dnd.Core.Tests
{
    public class DiceBoxTest
    {
        private readonly ITestOutputHelper output;
        public DiceBoxTest(ITestOutputHelper outputHelper)
        {
            this.output = outputHelper;
        }

        [Fact]
        public void Test()
        {
            int[] numbers = {0, 0, 0, 0, 0, 0
                            , 0, 0, 0, 0, 0};
            for (int i = 0; i < 1_000_000; ++i)
            {
                int points = DiceBox.Roll("2d6");
                
                Assert.InRange(points, 2, 12);

                output.WriteLine($"Points: {points}");

                numbers[points-2] += 1;
            }

            output.WriteLine("");
            for (int i = 0; i < numbers.Length; ++i)
            {
                output.WriteLine($"{i+2}:\t{numbers[i],9}");
            }
        }
    }
}