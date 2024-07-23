using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeroKero.Converters
{
    public class DirectionToAlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value == true) return LayoutOptions.Start; // Chatbot messages are displayed on the left
            else return LayoutOptions.End; // User responses are displayed on the right
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException(); // This should never be used
        }
    }
}
