using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Infrastructure.OpenTravelData.Converters;

public class NullableDateOnlyConverter: DefaultTypeConverter
{
    public override object? ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData) 
    {   
        if (string.IsNullOrEmpty(text)) return null;

        var formatProvider = (IFormatProvider?)memberMapData.TypeConverterOptions.CultureInfo?.GetFormat(typeof(DateTimeFormatInfo)) ?? memberMapData.TypeConverterOptions.CultureInfo;
        var dateTimeStyle = memberMapData.TypeConverterOptions.DateTimeStyle ?? DateTimeStyles.None;
 
        DateOnly dateOnly;
        var success = memberMapData.TypeConverterOptions.Formats == null || memberMapData.TypeConverterOptions.Formats.Length == 0
            ? DateOnly.TryParse(text, formatProvider, dateTimeStyle, out dateOnly)
            : DateOnly.TryParseExact(text, memberMapData.TypeConverterOptions.Formats, formatProvider, dateTimeStyle, out dateOnly);
        
        return success ? dateOnly : null;
    }
}