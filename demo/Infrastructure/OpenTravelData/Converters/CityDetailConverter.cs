using Application.Models.OpenTravelData.Airport;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Infrastructure.OpenTravelData.Converters;

public class CityDetailConverter : DefaultTypeConverter
{
    public override object? ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text))
            return null;
        
        var chunks = text.Split('=', StringSplitOptions.RemoveEmptyEntries);
        
        var result = new List<CityDetail>();

        foreach (var chunk in chunks)
        {
            var tokens = chunk.Split('|', StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim())
                .Where(t => !string.IsNullOrEmpty(t))
                .ToList();

            result.Add(new CityDetail
            {
                CityCode = tokens.ElementAtOrDefault(0) ?? "",
                GeonameId = tokens.ElementAtOrDefault(1) ?? "",
                NameUtf = tokens.ElementAtOrDefault(2) ?? "",
                NameAscii = tokens.ElementAtOrDefault(3) ?? "",
                CountryCode = tokens.ElementAtOrDefault(4) ?? "",
            });
        }
        
        return  result;
    }
}