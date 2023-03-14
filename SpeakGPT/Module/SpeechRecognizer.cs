using SpeakGPT.API;
using SpeakGPT.MVVM;
using System.ComponentModel;

namespace SpeakGPT.Module
{
    public class SpeechRecognizer : BaseModel
    {
        private VoiceRecorder _voiceRecorder;
        public bool IsRecording { get { return _voiceRecorder.IsRecording; } }

        private DeepgramApi _deepgram;
        private GoogleSpeech _googleSpeech;

        public SpeechRecognizer()
        {
            _voiceRecorder = new();
            _voiceRecorder.PropertyChanged += RecorderPropertyChanged;
            _deepgram = new();
            _googleSpeech = new();
        }

        internal async Task<string> RecognizeToggle_Async()
        {
            string result = null;
            if (IsRecording == false)
            {
                _voiceRecorder.StartRecord();
            }
            else
            {
                byte[] voiceData = _voiceRecorder.EndRecord();
                //result = await _googleSpeech.Listen(voiceData);
                result = await _deepgram.Transcription(voiceData);
            }
            return result;
        }

        internal void Cancel()
        {
            _voiceRecorder.CancelRecord();
            _googleSpeech.Stop();
        }

        internal void PlayTextSound(string text)
        {
            _googleSpeech.Speak(text);
        }

        #region Events
        private void RecorderPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case "IsRecording":
                    NotifyPropertyChanged(nameof(IsRecording));
                    break;
            }
        }
        #endregion
    }
}
