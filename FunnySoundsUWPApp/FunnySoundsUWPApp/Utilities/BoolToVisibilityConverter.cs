using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace FunnySoundsUWPApp
{
    class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is bool))
                return Visibility.Collapsed;
            bool objValue = (bool)value;
            if (objValue)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            try
            {
                if ((Visibility)value == Visibility.Visible)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
<<<<<<< Updated upstream
                throw new InvalidOperationException("That operation is not valid!");
            }
            return Visibility.Collapsed;
=======
                throw new InvalidOperationException("Invalid convertion!");
            }
            //return Visibility.Collapsed;
>>>>>>> Stashed changes
        }
    }
}
