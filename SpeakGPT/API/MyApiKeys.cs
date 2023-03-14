using Newtonsoft.Json;
using System.Reflection;

namespace SpeakGPT.API
{
    internal static class MyApiKeys
    {
        public class GoogleApiKey
        {
            public string type;
            public string project_id;
            public string private_key_id;
            public string private_key;
            public string client_email;
            public string client_id;
            public string auth_uri;
            public string token_uri;
            public string auth_provider_x509_cert_url;
            public string client_x509_cert_url;

            public override string ToString()
            {
                return JsonConvert.SerializeObject(this);
            }
        }
        public class ApiKeys
        {
            public string OpenAI_ApiKey;
            public string Deepgram_ApiKey;
            public GoogleApiKey Google_ApiKey;
        }
        public static ApiKeys Keys { get; set; }
        static MyApiKeys()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "SpeakGPT.apiKey.json";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string content = reader.ReadToEnd();
                Keys = JsonConvert.DeserializeObject<ApiKeys>(content);
            }
        }
    }
}
