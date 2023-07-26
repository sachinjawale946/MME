using MME.Model.Lookups;
using MME.Model.Response;
using System.Globalization;

namespace MME.Mobile.Converters
{
    internal class ParticipationEventOptionConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo info)
        {
            var event_type_id = Convert.ToInt16(value);
            if (event_type_id != 0 && event_type_id == EventType_Lookup.Participation)
                return true;
            else
                return false;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    internal class CharityEventOptionConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo info)
        {
            var event_type_id = Convert.ToInt16(value);
            if (event_type_id != 0 && event_type_id == EventType_Lookup.Charity)
                return true;
            else
                return false;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    internal class FundraiserEventOptionConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo info)
        {
            var event_type_id = Convert.ToInt16(value);
            if (event_type_id != 0 && event_type_id == EventType_Lookup.Fundraiser)
                return true;
            else
                return false;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    internal class ConferenceEventOptionConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo info)
        {
            var event_type_id = Convert.ToInt16(value);
            if (event_type_id != 0 && event_type_id == EventType_Lookup.Conferences)
                return true;
            else
                return false;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
