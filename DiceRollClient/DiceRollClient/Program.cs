using DiceRollClient.Client;
using DiceRollClient.Utilities;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DiceRollClient
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var httpClient = new HttpClient();
            var httpClient2 = new HttpClient();

            var clientDiceRoll = new DiceRoll(httpClient, TimeSpan.FromSeconds(1));

            var dice = await clientDiceRoll.GetDiceRolled();

            if (dice != null)
            {
                var diceCount = FormattingHelper.FormatDiceCount(dice);

                foreach (var line in diceCount)
                {
                    Console.WriteLine(line);
                }

                string ordered = FormattingHelper.FormatDiceOrdered(dice);

                Console.Error.WriteLine(ordered);

                var json = FormattingHelper.FormatToJsonOrdered(dice);

                var clientRequestBin = new RequestBin(httpClient2, TimeSpan.FromSeconds(1));

                if (!await clientRequestBin.Post(json))
                {
                    Console.Error.WriteLine("Post to request bin service failed");
                }
            }
        }
    }
}
