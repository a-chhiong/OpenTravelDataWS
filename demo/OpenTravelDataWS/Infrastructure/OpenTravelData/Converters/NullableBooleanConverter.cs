using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Infrastructure.OpenTravelData.Converters;

public class NullableBoolConverter : DefaultTypeConverter
{
    public override object? ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text))
            return null;

        text = text.Trim();

        if (text == "1")
            return true;
        if (text == "0")
            return false;

        // fallback: try standard bool parsing
        if (bool.TryParse(text, out var result))
            return result;

        return null;
    }
}