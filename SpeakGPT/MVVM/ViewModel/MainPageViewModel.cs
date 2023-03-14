using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;
using SpeakGPT.API;
using SpeakGPT.Module;
using SpeakGPT.MVVM.Model;
using SpeakGPT.MVVM.View;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace SpeakGPT.MVVM.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {
        private ContentPage _chaingingPage;
        public Conversation Conversation { get { return App.Instance.Models.Conversation; } }
        public SpeechRecognizer SpeechRecognizer { get { return App.Instance.Models.SpeechRecognizer; } }
        private string _myMessage;
        public string MyMessage { get { return _myMessage; } set { _myMessage = value; NotifyPropertyChanged(); } }
        public string SpeakButonText { get { return SpeechRecognizer.IsRecording ? "Recoring" : "Speak"; } }
        public ICommand SettingPageCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand SpeakCommand { get; }
        public ICommand WriteCommand { get; }
        public ICommand ResetCommand { get; }
        public ICommand StopCommand { get; }

        public MainPageViewModel()
        {
            MyMessage = null;

            SettingPageCommand = new RelayCommand(ShowSettingPage, CanNavigate);
            ClearCommand = new RelayCommand(ClearMyMessage);
            SpeakCommand = new RelayCommand(Speak);
            WriteCommand = new RelayCommand<string>(Write);
            ResetCommand = new RelayCommand(Reset);
            StopCommand = new RelayCommand(Cancel);

            SpeechRecognizer.PropertyChanged += SpeechRecognizerPropertyChanged;

            RequestPermissions();
        }

        private void SpeechRecognizerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case "IsRecording":
                    NotifyPropertyChanged(nameof(SpeakButonText));
                    break;
            }
        }

        internal async void RequestPermissions()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.Microphone>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.Microphone>();
                if (status != PermissionStatus.Granted)
                {
                    return;
                }
            }
            status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.StorageWrite>();
                if (status != PermissionStatus.Granted)
                {
                    return;
                }
            }
        }
        private bool CanNavigate()
        {
            return _chaingingPage == null;
        }
        private void ShowSettingPage()
        {
            ChangePageAsync(new SettingPage());
        }
        private void ClearMyMessage()
        {
            MyMessage = null;
        }
        private void Speak()
        {
            SpeakAsync();
        }
        private async void SpeakAsync()
        {
            string result = await SpeechRecognizer.RecognizeToggle_Async();

            if (string.IsNullOrEmpty(result) == false)
            {
                SendMessageAndGetResponse(result);
            }
        }
        private void Write(string message) => SendMessageAndGetResponse(message);
        private void Reset()
        {
            Conversation.ClearChat();
        }
        private void Cancel()
        {
            SpeechRecognizer.Cancel();
        }
        private void SendMessageAndGetResponse(string message)
        {
            MyMessage = message;

            if (string.IsNullOrEmpty(message))
            {
                Toast.Make("Please enter a message before sending.", ToastDuration.Short).Show();
                return;
            }

            Conversation.AddChatAndGetResponse(SenderTypes.USER, message);
        }
        private async void ChangePageAsync(ContentPage page)
        {
            if (page == null)
                return;

            if (_chaingingPage != null && _chaingingPage.GetType() == page.GetType())
                return;

            _chaingingPage = page;
            await Application.Current.MainPage.Navigation.PushAsync(page);
            await Task.Delay(2000);
            _chaingingPage = null;
        }
    }
}
