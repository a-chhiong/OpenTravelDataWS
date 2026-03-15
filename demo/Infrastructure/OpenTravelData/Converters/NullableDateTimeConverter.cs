using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Infrastructure.OpenTravelData.Converters;

public class NullableDateTimeConverter: DefaultTypeConverter
{
    public override object? ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData) 
    {
        if (string.IsNullOrEmpty(text)) return null;
        
        var formatProvider = (IFormatProvider?)memberMapData.TypeConverterOptions.CultureInfo?.GetFormat(typeof(DateTimeFormatInfo)) ?? memberMapData.TypeConverterOptions.CultureInfo;
        var dateTimeStyle = memberMapData.TypeConverterOptions.DateTimeStyle ?? DateTimeStyles.None;

        DateTime dateTime;
        var success = memberMapData.TypeConverterOptions.Formats == null || memberMapData.TypeConverterOptions.Formats.Length == 0
            ? DateTime.TryParse(text, formatProvider, dateTimeStyle, out dateTime)
            : DateTime.TryParseExact(text, memberMapData.TypeConverterOptions.Formats, formatProvider, dateTimeStyle, out dateTime);

        return success ? dateTime : null;
    }
}