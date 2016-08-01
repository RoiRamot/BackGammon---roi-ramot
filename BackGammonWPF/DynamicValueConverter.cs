using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BackGammonWPF
{
    public class DynamicValueConverter  : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
               var intValue = (int)value;
               if (intValue % 2 == 0)
               {
                   return 1;
               }
            }
            return 0;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class IdToColumOrientation : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                var intValue = (int)value;
                if (intValue>11)
                {
                    return 1;
                }
            }
            return 0;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

}