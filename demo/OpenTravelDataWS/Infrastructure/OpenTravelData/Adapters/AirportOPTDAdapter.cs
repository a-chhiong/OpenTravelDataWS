using System.Globalization;
using Application.Interfaces;
using Application.Models.OpenTravelData.Airport;
using CsvHelper;
using CsvHelper.Configuration;
using Infrastructure.OpenTravelData.ClassMaps;
using Infrastructure.OpenTravelData.Readers;

namespace Infrastructure.OpenTravelData.Adapters;

public class AirportOPTDAdapter: IOpenTravelDataAdapter<AirportRecord, AirportQuery>
{
    private static readonly string FilePath = Path.Combine(AppContext.BaseDirectory, "OpenTravelData", "CSV", "optd_por_public.csv");
    
    private static readonly Lazy<List<AirportRecord>> Records = new(() => 
        CsvReaderFactory.Load(FilePath, new AirportRecordMap()));

    public Task<AirportRecord?> Fetch(AirportQuery request)
    {
        var record = Records.Value.Where(r =>
            string.Equals(r.IataCode, request.Code, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(r => r.ModDate.HasValue)
            .ThenByDescending(r => r.ModDate ?? DateOnly.MinValue)
            .FirstOrDefault();

        return Task.FromResult(record);
    }
}