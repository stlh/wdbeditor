using System;

using Net.Zxnn.Dnd.Core;
using Xunit;
using Xunit.Abstractions;

namespace Net.Zxnn.Dnd.Test
{
    public class AbilityToolsTest
    {
        private readonly ITestOutputHelper output;
        public AbilityToolsTest(ITestOutputHelper outputHelper)
        {
            this.output = outputHelper;
        }
        
        [Fact]
        public void TestAbilityModifier()
        {
            Assert.Equal(-6, AbilityTools.GetAbilityModifier(-1));
            // 0~9
            Assert.Equal(-5, AbilityTools.GetAbilityModifier(0));
            Assert.Equal(-5, AbilityTools.GetAbilityModifier(1));
            Assert.Equal(-4, AbilityTools.GetAbilityModifier(2));
            Assert.Equal(-4, AbilityTools.GetAbilityModifier(3));
            Assert.Equal(-3, AbilityTools.GetAbilityModifier(4));
            Assert.Equal(-3, AbilityTools.GetAbilityModifier(5));
            Assert.Equal(-2, AbilityTools.GetAbilityModifier(6));
            Assert.Equal(-2, AbilityTools.GetAbilityModifier(7));
            Assert.Equal(-1, AbilityTools.GetAbilityModifier(8));
            Assert.Equal(-1, AbilityTools.GetAbilityModifier(9));

            // 10~19
            Assert.Equal(0, AbilityTools.GetAbilityModifier(10));
            Assert.Equal(0, AbilityTools.GetAbilityModifier(11));
            Assert.Equal(1, AbilityTools.GetAbilityModifier(12));
            Assert.Equal(1, AbilityTools.GetAbilityModifier(13));
            Assert.Equal(2, AbilityTools.GetAbilityModifier(14));
            Assert.Equal(2, AbilityTools.GetAbilityModifier(15));
            Assert.Equal(3, AbilityTools.GetAbilityModifier(16));
            Assert.Equal(3, AbilityTools.GetAbilityModifier(17));
            Assert.Equal(4, AbilityTools.GetAbilityModifier(18));
            Assert.Equal(4, AbilityTools.GetAbilityModifier(19));

            // 20~29
            Assert.Equal(5, AbilityTools.GetAbilityModifier(20));
            Assert.Equal(5, AbilityTools.GetAbilityModifier(21));
            Assert.Equal(6, AbilityTools.GetAbilityModifier(22));
            Assert.Equal(6, AbilityTools.GetAbilityModifier(23));
            Assert.Equal(7, AbilityTools.GetAbilityModifier(24));
            Assert.Equal(7, AbilityTools.GetAbilityModifier(25));
            Assert.Equal(8, AbilityTools.GetAbilityModifier(26));
            Assert.Equal(8, AbilityTools.GetAbilityModifier(27));
            Assert.Equal(9, AbilityTools.GetAbilityModifier(28));
            Assert.Equal(9, AbilityTools.GetAbilityModifier(29));

            // 30~33
            Assert.Equal(10, AbilityTools.GetAbilityModifier(30));
            Assert.Equal(10, AbilityTools.GetAbilityModifier(31));
            Assert.Equal(11, AbilityTools.GetAbilityModifier(32));
            Assert.Equal(11, AbilityTools.GetAbilityModifier(33));
        }
    }
}