using SpeakGPT.Custom;
using System.Collections.Specialized;
using System.ComponentModel;

namespace SpeakGPT.MVVM.Model
{
    public class Conversation : BaseModel
    {
        public ThreadSafeObservableCollection<Chat> ChatList { get; private set; }
        public ChatGPTApi ChatGPT { get; private set; }
        public Conversation()
        {
            ChatGPT = new();

            ChatList = new();
            ChatList.CollectionChanged += OnChatListChanged;
            App.Instance.Models.Settings.PropertyChanged += OnSettingChanged;
        }
        private void OnChatListChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpddateChatUI();
        }
        private void OnSettingChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(App.Instance.Models.Settings.Memory))
            {
                UpddateChatUI();
            }
        }
        public async void AddChatAndGetResponse(SenderTypes senderType, string message)
        {
            AddChat(senderType, message);
            List<Chat> conversationsToSend = GetPreConversations();
            string response = await ChatGPT.Chat(conversationsToSend, App.Instance.Models.Settings.Temperature, App.Instance.Models.Settings.MaxTokens);
            if (response == null)
                return;

            ChatList.Add(new Chat(SenderTypes.ASSISTANT, response));
            App.Instance.Models.SpeechRecognizer.PlayTextSound(response);
        }
        public void AddChat(SenderTypes senderType, string message)
        {
            if (string.IsNullOrEmpty(message))
                return;

            ChatList.Add(new Chat(senderType, message));
        }
        public void ClearChat()
        {
            ChatList.Clear();
        }
        private List<Chat> GetPreConversations()
        {
            List<Chat> result = ChatList
                .Where(chat => !chat.Expired)
                .TakeLast(App.Instance.Models.Settings.Memory)
                .ToList();

            Chat staticChat = App.Instance.Models.Settings.MakeSystemMessage();
            result.Insert(0, staticChat);

            return result;
        }
        private void UpddateChatUI()
        {
            for (int i = 0; i < ChatList.Count; i++)
            {
                bool expired = i < ChatList.Count - App.Instance.Models.Settings.Memory;
                if (ChatList[i].Expired != expired)
                    ChatList[i].Expired = expired;
            }
        }
    }
}
