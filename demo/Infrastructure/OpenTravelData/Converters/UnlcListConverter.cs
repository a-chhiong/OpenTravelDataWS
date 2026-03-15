using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Infrastructure.OpenTravelData.Converters;

public class UnlcListConverter : DefaultTypeConverter
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

            var first = tokens.ElementAtOrDefault(0);

            if (!string.IsNullOrEmpty(first))
            {
                result.Add(first);   
            }
        }

        return result;
    }
}