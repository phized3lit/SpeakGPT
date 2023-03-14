using Deepgram;
using Deepgram.Transcription;

namespace SpeakGPT.API
{
    internal class DeepgramApi
    {
        private readonly string _apiKey = MyApiKeys.Keys.Deepgram_ApiKey;

        private DeepgramClient _deepgram;
        internal DeepgramApi()
        {
            Credentials credentials = new Credentials(_apiKey);
            _deepgram = new DeepgramClient(credentials);
        }

        internal async Task<string> Transcription(byte[] audioBytes)
        {
            using (MemoryStream stream = new MemoryStream(audioBytes))
            {
                PrerecordedTranscription response = await _deepgram.Transcription.Prerecorded.GetTranscriptionAsync(
                    new StreamSource(stream, "audio/wav"),
                    new PrerecordedTranscriptionOptions()
                    {
                        Punctuate = true,
                        Utterances = false
                    });
                return response?.Results?.Channels[0]?.Alternatives[0]?.Transcript;
            }
        }
    }
}
