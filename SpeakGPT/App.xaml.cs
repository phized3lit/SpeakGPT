using SpeakGPT.MVVM.Model;
using System.ComponentModel;

namespace SpeakGPT;

public partial class App : Application
{
    internal static App Instance { get; private set; }
    internal AppModels Models { get; private set; }
	public App()
	{
        InitializeComponent();
        Instance = this;

        Models = new AppModels();
        MainPage = new AppShell();
    }
}
