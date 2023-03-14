using Deepgram.Keys;
using Newtonsoft.Json.Linq;
using SpeakGPT.API;
using SpeakGPT.MVVM.Model;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SpeakGPT
{
    public class ChatGPTApi
    {
        private readonly string _apiKey = MyApiKeys.Keys.OpenAI_ApiKey;
        private readonly string _apiEndpoint = "https://api.openai.com/v1/chat/completions";

        private List<object> ConvertMessages(List<Chat> messages)
        {
            List<object> result = new List<object>();
            result.AddRange(messages.Select(c => new { role = c.SenderType.ToString().ToLower(), content = c.Message }));
            return result;
        }

        internal async Task<string> Chat(List<Chat> messages, double temperature, int maxTokens)
        {
            using var client = new HttpClient();
            var request = new
            {
                model = "gpt-3.5-turbo",
                messages = ConvertMessages(messages),
                temperature = temperature,
                n = 1,
                max_tokens = maxTokens,
            };

            var json = JsonSerializer.Serialize(request);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            var response = await client.PostAsync(_apiEndpoint, content);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();

            JObject responseObj = JObject.Parse(responseJson);
            JToken responseToken = responseObj["choices"][0]["message"]["content"];
            string choice = responseToken.Value<string>();
            if (choice != null)
                choice = choice.Replace("\n", "");

            return choice;
        }
    }
}
