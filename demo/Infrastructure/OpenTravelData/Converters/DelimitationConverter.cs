using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Infrastructure.OpenTravelData.Converters;

public class DelimitationConverter : DefaultTypeConverter
{
    private readonly string _delimiter;

    public DelimitationConverter(string delimiter)
    {
        _delimiter = delimiter;
    }

    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text))
            return new List<string>();

        return text.Split(_delimiter, StringSplitOptions.RemoveEmptyEntries)
            .Select(t => t.Trim())
            .Where(t => !string.IsNullOrEmpty(t))
            .ToList();
    }
}