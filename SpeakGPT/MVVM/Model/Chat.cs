using Microsoft.Maui.ApplicationModel;

namespace SpeakGPT.MVVM.Model
{
    public enum SenderTypes { UNKNOWN, SYSTEM, USER, ASSISTANT };
    public class Chat : BaseModel
    {
        private SenderTypes _senderType;
        public SenderTypes SenderType { get { return _senderType; } set { _senderType = value; NotifyPropertyChanged(); } }
        public string ImageUrl { get { return SenderType == SenderTypes.ASSISTANT ? "dotnet_bot.png" : "user_icon_2.png"; } }
        public string DisplayName
        {
            get
            {
                switch (SenderType)
                {
                    case SenderTypes.SYSTEM:
                        return "SYSTEM";
                    case SenderTypes.USER:
                        return "ME";
                    case SenderTypes.ASSISTANT:
                        return "GPT";
                    default:
                        return "UNKNOWN";
                }
            }
        }
        private string _message;
        public string Message { get { return _message; } set { _message = value; NotifyPropertyChanged(); } }
        private bool _expired;
        public bool Expired { get { return _expired; } set { _expired = value; NotifyPropertyChanged(); } }

        public Chat(SenderTypes senderType, string message)
        {
            SenderType = senderType;
            Message = message;
            Expired = false;
        }

        public override string ToString()
        {
            return string.Format($"{SenderType}:{Message}");
        }
    }
}
