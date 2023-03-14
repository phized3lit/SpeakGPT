using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeakGPT.MVVM.Model
{
    public class Settings : BaseModel
    {
        private int _maxTokens;
        public int MaxTokens { get { return _maxTokens; } set { _maxTokens = value; NotifyPropertyChanged(); } }
        private double _temperature;
        public double Temperature { get { return _temperature; } set { _temperature = value; NotifyPropertyChanged(); } }
        private int _memory;
        public int Memory { get { return _memory; } set { _memory = value; NotifyPropertyChanged(); } }

        private bool _rolePlay;
        public bool RolePlay { get { return _rolePlay; } set { _rolePlay = value; NotifyPropertyChanged(); } }
        private string _userRole;
        public string UserRole { get { return _userRole; } set { _userRole = value; NotifyPropertyChanged(); } }
        private string _aIRole;
        public string AIRole { get { return _aIRole; } set { _aIRole = value; NotifyPropertyChanged(); } }
        private string _situation;
        public string Situation { get { return _situation; } set { _situation = value; NotifyPropertyChanged(); } }

        public Settings()
        {
            Initialize();
        }
        private void Initialize()
        {
            MaxTokens = 50;
            Temperature = 0.8;
            Memory = 7;

            UserRole = "User";
            AIRole = "Assistant";
            Situation = "You are a helpful assistant";
        }
        public void Reset()
        {
            Initialize();
        }
        internal Chat MakeSystemMessage()
        {
            SenderTypes senderTYpe = SenderTypes.SYSTEM;
            string systemMessage = "Give a short answer of 3 sentences or less. ";
            if (RolePlay)
            {
                senderTYpe = SenderTypes.USER;
                systemMessage += string.Format($"I want to role play to study conversation. I am the {UserRole} and you are the {AIRole}. Suppose {Situation}.");
            }
            else
            {
                systemMessage += "You are a helpful assistant.";
            }
            return new Chat(senderTYpe, systemMessage);
        }
    }
}
