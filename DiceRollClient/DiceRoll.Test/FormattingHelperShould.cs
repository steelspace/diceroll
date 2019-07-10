using DiceRollClient.Utilities;
using System.Linq;
using Xunit;

namespace DiceRoll.Test
{
    public class FormattingHelperShould
    {
        readonly int[] _dice = new int[] { 3, 5, 1, 2, 6, 5, 1, 6, 4, 2 };

        [Fact]
        public void FormatDiceCount()
        {
            var result = FormattingHelper.FormatDiceCount(_dice).ToArray();

            var expected = new string[] {
                "1 -> 2",
                "2 -> 2",
                "3 -> 1",
                "4 -> 1",
                "5 -> 2",
                "6 -> 2" };

            Assert.True(expected.All(shouldItem => result.Any(isItem => isItem == shouldItem)));
        }

        [Fact]
        public void FormatDiceOrdered()
        {
            var result = FormattingHelper.FormatDiceOrdered(_dice);

            // you have a bug in your spec, expected result is not "1 1 2 2 3 4 4 5 5 6 6" (number 4 is only once in the input)
            Assert.Equal("1 1 2 2 3 4 5 5 6 6", result);
        }

        [Fact]
        public void FormatDiceToJsonOrdered()
        {
            var result = FormattingHelper.FormatToJsonOrdered(_dice);

            Assert.Equal("{ \"dice\": [ 1,1,2,2,3,4,5,5,6,6 ] }", result);
        }
    }
}
