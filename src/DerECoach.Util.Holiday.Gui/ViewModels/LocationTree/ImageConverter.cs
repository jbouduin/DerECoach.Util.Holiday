using System;
using System.Windows.Data;
using System.Windows.Media;

namespace DerECoach.Util.Holiday.Gui.ViewModels.LocationTree
{
    class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                if (value is string && !string.IsNullOrEmpty(value.ToString()))
                {
                    if (targetType == typeof(ImageSource))
                        return new ImageSourceConverter().ConvertFromString(value.ToString()) as ImageSource;
                    return null;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
