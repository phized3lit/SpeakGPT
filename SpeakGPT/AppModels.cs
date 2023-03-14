using SpeakGPT.API;
using SpeakGPT.Module;
using SpeakGPT.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeakGPT
{
    internal class AppModels
    {
        public Settings Settings { get; private set; }
        public Conversation Conversation { get; private set; }
        public SpeechRecognizer SpeechRecognizer { get; private set; }
        internal AppModels()
        {
            LateInit();
        }
        private void Init()
        {
            Settings = new Settings();
            Conversation = new Conversation();
            SpeechRecognizer = new SpeechRecognizer();
        }
        private async void LateInit()
        {
            await Task.Delay(TimeSpan.FromMilliseconds(100))
                .ContinueWith(task =>
                {
                    Init();
                });
        }


    }
}
