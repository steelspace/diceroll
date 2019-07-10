using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DiceRoll.Test
{
    public class RequestBinClient
    {
        [Fact]
        public async Task ReturnArrayOfRolledDice()
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            // mock the dice roll response
            handlerMock.Protected()
                           .Setup<Task<HttpResponseMessage>>(
                              "SendAsync",
                              ItExpr.IsAny<HttpRequestMessage>(),
                              ItExpr.IsAny<CancellationToken>()
                           )
                           .ReturnsAsync(new HttpResponseMessage()
                           {
                               StatusCode = HttpStatusCode.OK,
                               Content = new StringContent("3,5,1,2,6,5,1,6,4,2")
                           })
                           .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object);

            var diceRoll = new DiceRollClient.Client.DiceRoll(httpClient, TimeSpan.FromSeconds(1));

            var dice = await diceRoll.GetDiceRolled();

            Assert.Equal(new int [] { 3, 5, 1, 2, 6, 5, 1, 6, 4, 2 }, dice);
        }

        [Fact]
        public async Task ReturnNullIfHttpRequestFails()
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            // mock the dice roll response
            handlerMock.Protected()
                           .Setup<Task<HttpResponseMessage>>(
                              "SendAsync",
                              ItExpr.IsAny<HttpRequestMessage>(),
                              ItExpr.IsAny<CancellationToken>()
                           )
                           .ReturnsAsync(new HttpResponseMessage()
                           {
                               // an error
                               StatusCode = HttpStatusCode.BadRequest
                           })
                           .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object);

            var diceRoll = new DiceRollClient.Client.DiceRoll(httpClient, TimeSpan.FromSeconds(1));

            var dice = await diceRoll.GetDiceRolled();

            Assert.Null(dice);
        }
    }
}
