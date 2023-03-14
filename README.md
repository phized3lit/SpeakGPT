# SpeakGPT

<H2>C# .Net MAUI Smart App! (Android/iOS/Windows/etc.)</H2>
<H4>GPT와 영어 음성으로 대화할 수 있는 앱입니다.</H4>
<H4>This app allows you to speak with GPT in English.</H4>

You can enter the api_key in the path below.

    Solution_Root/Private/apiKey.json
    
and its contents

```json
{
  "OpenAI_ApiKey": "Your OpenAI API Key",
  "Deepgram_ApiKey": "Your Deepgram API Key",
  "Google_ApiKey":{
      "type": "YouAuthInfo",
      "project_id": "YouAuthInfo",
      "private_key_id": "YouAuthInfo",
      "private_key": "YouAuthInfo",
      "client_email": "YouAuthInfo",
      "client_id": "YouAuthInfo",
      "auth_uri": "YouAuthInfo",
      "token_uri": "YouAuthInfo",
      "auth_provider_x509_cert_url": "YouAuthInfo",
      "client_x509_cert_url": "YouAuthInfo"
  }
}
```
