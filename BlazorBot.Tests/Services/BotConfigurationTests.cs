using BlazorBot.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace BlazorBot.Tests.Services
{
    public class BotConfigurationTests
    {
        [Fact]
        public void LoadConfigurationReturnsCorrectConfiguration()
        {
            var service = new BotConfigurationService();

            var apiUrl = "test.com";
            var apiSection = new Mock<IConfigurationSection>();
            apiSection.Setup(a => a.Value).Returns(apiUrl);

            var botName = "test bot";
            var nameSection = new Mock<IConfigurationSection>();
            nameSection.Setup(a => a.Value).Returns(botName);

            var chatType = "test";
            var typeSection = new Mock<IConfigurationSection>();
            typeSection.Setup(a => a.Value).Returns(chatType);

            var configuration = new Mock<IConfiguration>();

            configuration.Setup(a => a.GetSection("ApiUrl")).Returns(apiSection.Object);
            configuration.Setup(a => a.GetSection("BotName")).Returns(nameSection.Object);
            configuration.Setup(a => a.GetSection("ChatType")).Returns(typeSection.Object);

            var result = service.LoadConfiguration(configuration.Object);

            Assert.Equal(apiUrl, result.ApiUrl);
            Assert.Equal(botName, result.BotName);
            Assert.Equal(chatType, result.ChatType);
        }
    }
}
