using Deepgram.Common;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Speech.V1;
using Google.Cloud.TextToSpeech.V1;
using SpeakGPT.MVVM;
using Plugin.Maui.Audio;

namespace SpeakGPT.API
{
    public class GoogleSpeech : BaseModel
    {
        private GoogleCredential _credential;
        private SpeechClient _speechClient;
        private TextToSpeechClient _ttsClient;

        private IAudioPlayer AudioPlayer { get; set; }
        private string GoogleAuth = MyApiKeys.Keys.Google_ApiKey.ToString();
        internal GoogleSpeech()
        {
            AutorizeAsync();
        }
        internal async void AutorizeAsync()
        {
            await Task.Run(() =>
            {
                _credential = GoogleCredential.FromJson(GoogleAuth);
                _speechClient = new SpeechClientBuilder
                {
                    Credential = _credential
                }.Build();

                _ttsClient = new TextToSpeechClientBuilder
                {
                    Credential = _credential
                }.Build();
            });
        }
        internal async Task<string> Listen(byte[] audioBuffer)
        {
            if (_speechClient == null)
                return null;

            var config = new RecognitionConfig
            {
                //Encoding = RecognitionConfig.Types.AudioEncoding.Flac,
                Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                SampleRateHertz = 16000,
                LanguageCode = LanguageCodes.English.UnitedStates,
                //Model = "command_and_search"
                Model = "default"
            };
            var audio = RecognitionAudio.FromBytes(audioBuffer);
            var response = _speechClient.Recognize(config, audio);

            string result = string.Empty;
            foreach (var r in response.Results)
            {
                foreach (var alternative in r.Alternatives)
                {
                    result += alternative.Transcript;
                    //Console.WriteLine(alternative.Transcript);
                    //Debug.WriteLine("결과: " + alternative.Transcript);
                }
            }
            return result;
        }
        internal async void Speak(string text)
        {
            var input = new SynthesisInput
            {
                Text = text
            };
            var voiceSelection = new VoiceSelectionParams
            {
                //LanguageCode = "en-US-Wavenet-F",
                //SsmlGender = SsmlVoiceGender.Neutral
                LanguageCode = "en-US",
                //SsmlGender = SsmlVoiceGender.Neutral
                Name = "en-US-Wavenet-F"
            };
            var audioConfig = new AudioConfig
            {
                AudioEncoding = Google.Cloud.TextToSpeech.V1.AudioEncoding.Mp3
            };
            var response = _ttsClient.SynthesizeSpeech(input, voiceSelection, audioConfig);
            var audioContent = response.AudioContent.ToByteArray();
            var audioStream = new MemoryStream(audioContent);
            AudioPlayer = AudioManager.Current.CreatePlayer(audioStream);
            AudioPlayer?.Play();
        }

        internal void Stop()
        {
            AudioPlayer?.Stop();
        }
        internal void Reset()
        {
            AudioPlayer?.Stop();
        }
    }
}
