using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Infrastructure.OpenTravelData.Converters;

public class PkListConverter : DefaultTypeConverter
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text))
            return default;

        var chunks = text.Split('=', StringSplitOptions.RemoveEmptyEntries);
        
        var result = new List<string>();

        foreach (var chunk in chunks)
        {
            var tokens = chunk.Split('|', StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim())
                .Where(t => !string.IsNullOrEmpty(t))
                .ToList();

            var second = tokens.ElementAtOrDefault(1);

            if (!string.IsNullOrEmpty(second))
            {
                result.Add(second);   
            }
        }

        return result;
    }
}