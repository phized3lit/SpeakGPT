using SpeakGPT.MVVM.Model;
using SpeakGPT.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;

namespace SpeakGPT;

public partial class MainPage : ContentPage
{
    public MainPageViewModel ViewModel { get { return BindingContext as MainPageViewModel; } }
    public MainPage()
    {
        InitializeComponent();

        App.Instance.Models.Conversation.ChatList.CollectionChanged += OnChatListChanged;
    }

    private void OnChatListChanged(object sender, NotifyCollectionChangedEventArgs args)
    {
        try
        {
            IEnumerable<Chat> items = (IEnumerable<Chat>)listView_conversation.ItemsSource;
            if (items.Any() == false)
                return;

            //listView_conversation.ScrollTo(items?.Last(), ScrollToPosition.End, true);
            listView_conversation.ScrollTo(items?.Last(), ScrollToPosition.MakeVisible, true);
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine($"An InvalidOperationException occurred: {e.Message}");
        }
        catch (TargetInvocationException e)
        {
            Console.WriteLine($"A TargetInvocationException occurred: {e.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"An exception occurred: {e.Message}");
        }
    }
}

