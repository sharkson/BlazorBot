using ChatModels;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBot.Services
{
    public class ChatUpdateService
    {
        HttpClient client;
        BotConfiguration configuration;
        string conversationName;

        public ChatUpdateService(HttpClient httpClient, BotConfiguration botConfiguration)
        {
            client = httpClient;
            configuration = botConfiguration;
        }

        public async Task<bool> UpdateChatAsync(Chat chat, string conversationName)
        {
            chat.botName = configuration.BotName;
            var chatRequest = new ChatRequest { chat = chat, type = configuration.ChatType, conversationName = conversationName };

            var httpContent = GetHttpContent(chatRequest);
            var response = await client.PutAsync(configuration.ApiUrl + "/api/chatupdate", httpContent);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var success = JsonConvert.DeserializeObject<bool>(jsonResponse);

            return success;
        }

        private StringContent GetHttpContent(dynamic request)
        {
            var jsonString = JsonConvert.SerializeObject(request);
            return new StringContent(jsonString, Encoding.UTF8, "application/json");
        }
    }
}
