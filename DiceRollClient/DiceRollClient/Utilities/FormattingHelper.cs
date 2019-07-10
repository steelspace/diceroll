using System.Collections.Generic;
using System.Linq;

namespace DiceRollClient.Utilities
{
    public static class FormattingHelper
    {
        public static IEnumerable<string> FormatDiceCount(IEnumerable<int> dice)
        {
            return dice.GroupBy(d => d)
                    .Select(group => new
                    {
                        Die = group.Key,
                        Count = group.Count()
                    }).OrderBy(x => x.Die).Select(a => $"{a.Die} -> {a.Count}");
        }

        public static string FormatDiceOrdered(IEnumerable<int> dice)
        {
            return string.Join(" ", dice.OrderBy(d => d));
        }

        public static string FormatToJsonOrdered(IEnumerable<int> dice)
        {
            return $"{{ \"dice\": [ {string.Join(",", dice.OrderBy(d => d))} ] }}";
        }
    }
}
