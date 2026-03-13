using System.Globalization;
using Application.Interfaces;
using Application.Models.OpenTravelData.Airline;
using Application.Models.OpenTravelData.Airport;
using CsvHelper;
using CsvHelper.Configuration;
using Infrastructure.OpenTravelData.ClassMaps;
using Infrastructure.OpenTravelData.Readers;

namespace Infrastructure.OpenTravelData.Adapters;

public class AirlineOPTDAdapter: IOpenTravelDataAdapter<AirlineRecord, AirlineQuery>
{
    private static readonly string FilePath = Path.Combine(AppContext.BaseDirectory, "OpenTravelData", "CSV", "optd_airlines.csv");
    
    private static readonly Lazy<List<AirlineRecord>> Records = new(() => 
        CsvReaderFactory.Load(FilePath, new AirlineRecordMap()));

    public Task<AirlineRecord?> Fetch(AirlineQuery request)
    {
        var record = Records.Value.FirstOrDefault(r => 
            string.Equals(r.IataCode, request.Code, StringComparison.OrdinalIgnoreCase) 
            || string.Equals(r.IcaoCode, request.Code, StringComparison.OrdinalIgnoreCase));

        return Task.FromResult(record);
    }
}