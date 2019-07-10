using DiceRollClient.Client;
using System;
using System.Net.Http;

namespace DiceRollClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var httpCLient = new HttpClient();

            var client = new DiceRoll(httpCLient, TimeSpan.FromMilliseconds(100));


        }
    }
}
