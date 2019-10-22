using ChatModels;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBot.Services
{
    public class ChatResponseService
    {
        HttpClient client;
        BotConfiguration configuration;

        public ChatResponseService(HttpClient httpClient, BotConfiguration botConfiguration)
        {
            client = httpClient;
            configuration = botConfiguration;
        }

        public async Task<ChatResponse> GetChatResponseAsync(string conversationName)
        {
            var chatRequest = new ChatRequest { type = configuration.ChatType, conversationName = conversationName };
            var httpContent = GetHttpContent(chatRequest);
            var response = await client.PutAsync(configuration.ApiUrl + "/api/response", httpContent);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var chatResponse = JsonConvert.DeserializeObject<ChatResponse>(jsonResponse);

            return chatResponse;
        }

        private StringContent GetHttpContent(dynamic request)
        {
            var jsonString = JsonConvert.SerializeObject(request);
            return new StringContent(jsonString, Encoding.UTF8, "application/json");
        }
    }
}
