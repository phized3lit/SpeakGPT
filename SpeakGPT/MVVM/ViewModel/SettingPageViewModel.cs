using SpeakGPT.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeakGPT.MVVM.ViewModel
{
    public class SettingPageViewModel : BaseViewModel
    {
        public Settings Settings { get { return App.Instance.Models.Settings; } }
    }
}
