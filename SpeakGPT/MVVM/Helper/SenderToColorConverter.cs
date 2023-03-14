using Microsoft.Maui.Controls;
using System.Globalization;

namespace SpeakGPT.MVVM.Helper
{
    public class SenderToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color result = Colors.Gray;
            if (value is bool expired)
            {
                bool isDarkTheme = App.Current.RequestedTheme == AppTheme.Dark;
                if (expired)
                    result = isDarkTheme ? Colors.Gray : Colors.Gray;
                else
                    result = isDarkTheme ? Colors.White : Colors.Black;
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
