using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Mobile.Converters
{
    internal class ImageByGenderConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo info)
        {
            if (value != null && !string.IsNullOrEmpty(value.ToString()) && value.ToString() == "Female")
                return "female.jpg";
            else
                return "male.jpg";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
