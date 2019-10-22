using Microsoft.Extensions.Configuration;

namespace BlazorBot.Services
{
    public class BotConfigurationService
    {
        public BotConfiguration LoadConfiguration(IConfiguration configuration)
        {
            var botConfiguration = new BotConfiguration();

            botConfiguration.ApiUrl = configuration.GetSection("ApiUrl").Value;
            botConfiguration.BotName = configuration.GetSection("BotName").Value;
            botConfiguration.ChatType = configuration.GetSection("ChatType").Value;

            return botConfiguration;
        }
    }
}