using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DiceRollClient.Client
{
    public class RequestBin
    {
        private readonly HttpClient _client;

        public RequestBin(HttpClient client, TimeSpan timeout)
        {
            _client = client;
            _client.Timeout = timeout;
        }

        public async Task<bool> Post(string json)
        {
            try
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var result = await _client.PostAsync("https://requestb.in/", content);

                return true;
            }

            catch (OperationCanceledException)
            {
                // timed out

                return false;
            }

            catch (HttpRequestException)
            {
                // request failed, log here

                return false;
            }
        }
    }
}
