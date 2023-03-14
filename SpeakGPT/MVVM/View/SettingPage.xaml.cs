namespace SpeakGPT.MVVM.View;

public partial class SettingPage : ContentPage
{
    public SettingPage()
    {
        InitializeComponent();
    }

    private void MaxTokens_Slider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        Slider slider = sender as Slider;
        if (slider == null)
            return;

        int roundedValue = (int)Math.Round(e.NewValue / 10.0, MidpointRounding.AwayFromZero) * 10;
        if (slider.Value != roundedValue)
            slider.Value = roundedValue;
    }

    private void Temperature_Slider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        Slider slider = sender as Slider;
        if (slider == null)
            return;

        double roundedValue = Math.Round(e.NewValue * 10.0) / 10.0;
        if (slider.Value != roundedValue)
            slider.Value = roundedValue;
    }
}