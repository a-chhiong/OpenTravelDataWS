using Application.Models.OpenTravelData;
using CrossCutting.Helpers;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Infrastructure.OpenTravelData.Converters;

public class AltNameConverter<TAltName>: DefaultTypeConverter
    where TAltName : BaseAltName, new()
{
    public override object? ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text))
            return null;

        var chunks = text.Split('=', StringSplitOptions.RemoveEmptyEntries);
        
        var result = new List<TAltName>();

        foreach (var chunk in chunks)
        {
            var tokens = chunk.Split('|', StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim())
                .Where(t => !string.IsNullOrEmpty(t))
                .ToList();

            var lang = tokens.ElementAtOrDefault(0);
            var name = tokens.ElementAtOrDefault(1);

            if (!CultureInfoHelper.IsValidCulture(lang, out var culture))
                continue;
            
            lang = culture?.Name;
            
            // Use reflection or an interface to set properties
            var altName = new TAltName();
            typeof(TAltName).GetProperty("Lang")?.SetValue(altName, lang);
            typeof(TAltName).GetProperty("Name")?.SetValue(altName, name);
            
            result.Add(altName);
        }

        return result;
    }
}