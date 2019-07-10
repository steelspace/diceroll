using System;
using System.Collections.Generic;

namespace DiceRollClient.Utilities
{
    public static class Parser
    {
        public static IEnumerable<int> ParseDiceResult(string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                yield break;
            }

            foreach (var s in str.Split(','))
            {
                if (int.TryParse(s, out int num))
                {
                    yield return num;
                }
            }
        }
    }
}
