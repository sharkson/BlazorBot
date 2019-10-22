using BlazorBot.Services;
using ChatModels;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BlazorBot.Tests.Services
{
    public class ChatResponseServiceTests
    {
        [Fact]
        public async Task GetChatResponseAsyncReturnsApiResponse()
        {
            var chatResponse = new ChatResponse();
            chatResponse.response = new List<string>();
            chatResponse.response.Add("hello world");
            chatResponse.confidence = .42;

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock.Protected().Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               ).ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(JsonConvert.SerializeObject(chatResponse)),
               }).Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };


            var botConfiguration = new BotConfiguration();
            var service = new ChatResponseService(httpClient, botConfiguration);

            var result = await service.GetChatResponseAsync("test");
            Assert.Equal(chatResponse.response, result.response);
            Assert.Equal(chatResponse.confidence, result.confidence);
        }
    }
}
