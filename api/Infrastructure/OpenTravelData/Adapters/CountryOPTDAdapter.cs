using System.Globalization;
using Application.Interfaces;
using Application.Models.OpenTravelData.Country;
using CsvHelper;
using CsvHelper.Configuration;
using Infrastructure.OpenTravelData.ClassMaps;
using Infrastructure.OpenTravelData.Objects;
using Infrastructure.OpenTravelData.Readers;
using Region = Application.Models.OpenTravelData.Country.Region;
using Schengen = Application.Models.OpenTravelData.Country.Schengen;
using Subregion = Application.Models.OpenTravelData.Country.Subregion;

namespace Infrastructure.OpenTravelData.Adapters;

public class CountryOPTDAdapter: IOpenTravelDataAdapter<CountryRecord, CountryQuery>
{
    private static readonly string FilePathMain = Path.Combine(AppContext.BaseDirectory, "OpenTravelData", "CSV", "optd_countries.csv");
    private static readonly string FilePathSub = Path.Combine(AppContext.BaseDirectory, "OpenTravelData", "CSV", "optd_country_region_info.csv");
    
    private static readonly Lazy<List<CountryRecord>> Records = new(() =>
        CsvReaderFactory.Load(
            FilePathMain, new CountryMainMap(),
            FilePathSub, new CountrySubMap(),
            main => main.Iso2CharCode,
            sub => sub.CountryCode,
            Mapper)
        );

    private static CountryRecord Mapper(CountryMain main, CountrySub? sub)
    {
        return new CountryRecord
        {
            Iso2CharCode = main.Iso2CharCode,
            Iso3CharCode = main.Iso3CharCode,
            IsoNumCode = main.IsoNumCode,
            Fips = main.Fips,
            Name = main.Name,
            Capital = main.Capital,
            Area = main.Area,
            Population = main.Population,
            ContinentCode = main.ContinentCode,
            Tld = main.Tld,
            CurrencyCode = main.CurrencyCode,
            CurrencyName = main.CurrencyName,
            TelephonePrefix = main.TelephonePrefix,
            ZipFormat = main.ZipFormat,
            LanguageCodes = main.LanguageCodes,
            GeoId = main.GeoId,
            NeighborCountryCodes = main.NeighborCountryCodes,
            Schengen = sub?.Schengen?.IsSchengen == null
                ? null
                : new Schengen
                {
                    IsSchengen = sub?.Schengen?.IsSchengen,
                    SchengenFrom = sub?.Schengen?.SchengenFrom,
                    SchengenTo = sub?.Schengen?.SchengenTo,
                },
            Region = sub?.Region == null
                ? null
                : new Region
                {
                    UnWtoCode = sub?.Region?.UnWtoCode,
                    UnWtoName = sub?.Region?.UnWtoName,
                    UnWtoNumeric = sub?.Region?.UnWtoNumeric,
                    IataSsimCode = sub?.Region?.IataSsimCode,
                    IataSsimName = sub?.Region?.IataSsimName,
                    IataWatsCode = sub?.Region?.IataWatsCode,
                    IataWatsName = sub?.Region?.IataWatsName,
                },
            Subregion = sub?.Subregion == null
                ? null
                : new Subregion
                {
                    UnWtoCode = sub?.Subregion?.UnWtoCode,
                    UnWtoName = sub?.Subregion?.UnWtoName,
                }
        };
    }

    public Task<CountryRecord?> Fetch(CountryQuery request)
    {
        var record = Records.Value.FirstOrDefault(r => 
            string.Equals(r.Iso2CharCode, request.Code, StringComparison.OrdinalIgnoreCase) 
            || string.Equals(r.Iso3CharCode, request.Code, StringComparison.OrdinalIgnoreCase)
            || string.Equals(r.Fips, request.Code, StringComparison.OrdinalIgnoreCase));

        return Task.FromResult(record);
    }
}