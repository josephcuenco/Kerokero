using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeroKero.Converters
{
    public class DirectionToShapeConverter : IValueConverter
    {
        IShape BotMsgShape = new RoundRectangle() { CornerRadius = new CornerRadius(10,10, 0,10) };
        IShape UsrMsgShape = new RoundRectangle() { CornerRadius = new CornerRadius(10,10,10, 0) };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value == true) return BotMsgShape; // Chatbot messages are displayed on the left
            else return UsrMsgShape; // User responses are displayed on the right
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException(); // This should never be used
        }
    }
}
