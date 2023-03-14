using SpeakGPT.MVVM.Model;
using System.Globalization;

namespace SpeakGPT.MVVM.Helper
{
    public class SenderToLayoutOptionsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            LayoutOptions result = LayoutOptions.Start;
            if (value is SenderTypes sender)
            {
                switch (sender)
                {
                    case SenderTypes.SYSTEM:
                        result = LayoutOptions.Center;
                        break;
                    case SenderTypes.USER:
                        result = LayoutOptions.End;
                        break;
                    case SenderTypes.ASSISTANT:
                        result = LayoutOptions.Start;
                        break;
                }
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
