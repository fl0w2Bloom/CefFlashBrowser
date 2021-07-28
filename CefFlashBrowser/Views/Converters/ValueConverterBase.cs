﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CefFlashBrowser.Views.Converters
{
    public abstract class ValueConverterBase<SourceType, TargetType> : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert((SourceType)value, parameter, culture);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertBack((TargetType)value, parameter, culture);
        }

        public abstract TargetType Convert(SourceType value, object parameter, CultureInfo culture);

        public abstract SourceType ConvertBack(TargetType value, object parameter, CultureInfo culture);
    }
}
