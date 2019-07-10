using DiceRollClient.Utilities;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DiceRollClient.Client
{
    public class DiceRoll
    {
        private readonly HttpClient _client;

        public DiceRoll(HttpClient client, TimeSpan timeout)
        {
            _client = client;
            _client.Timeout = timeout;
        }

        public async Task<int []> GetDiceRolled()
        {
            try
            {
                var result = await _client.GetStringAsync(" https://www.random.org/dice/?num=10");

                return Parser.ParseDiceResult(result).ToArray();
            }

            catch (HttpRequestException ex)
            {
                // request failed, log here

                return null;
            }
        }
    }
}
