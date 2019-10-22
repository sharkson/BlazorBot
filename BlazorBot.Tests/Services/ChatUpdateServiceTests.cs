using BlazorBot.Services;
using ChatModels;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BlazorBot.Tests.Services
{
    public class ChatUpdateServiceTests
    {
        [Fact]
        public async Task UpdateChatAsyncReturnsApiResponse()
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock.Protected().Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               ).ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(JsonConvert.SerializeObject(true)),
               }).Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };


            var botConfiguration = new BotConfiguration();
            var service = new ChatUpdateService(httpClient, botConfiguration);

            var result = await service.UpdateChatAsync(new Chat(), "test");
            Assert.True(result);
        }
    }
}
