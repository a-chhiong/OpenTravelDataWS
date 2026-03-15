using CsvHelper.Configuration;
using Infrastructure.OpenTravelData.Converters;
using Infrastructure.OpenTravelData.Objects;

namespace Infrastructure.OpenTravelData.ClassMaps;

public class CountrySubMap : ClassMap<CountrySub>
{
    public CountrySubMap()
    {
        Map(m => m.CountryCode).Name("country_code");
        Map(m => m.CountryName).Name("country_name");
        References<SchengenMap>(m => m.Schengen);
        References<RegionMap>(m => m.Region);
        References<SubregionMap>(m => m.Subregion);
    }
}

public sealed class SchengenMap : ClassMap<Schengen>
{
    public SchengenMap()
    {
        Map(m => m.IsSchengen).Name("schengen").TypeConverter(new NullableBoolConverter());
        Map(m => m.SchengenFrom).Name("schengen_from").TypeConverter(new NullableDateOnlyConverter());
        Map(m => m.SchengenTo).Name("schengen_to").TypeConverter(new NullableDateOnlyConverter());
    }
}

public sealed class RegionMap : ClassMap<Region>
{
    public RegionMap()
    {
        Map(m => m.UnWtoCode).Name("region_unwto_code");
        Map(m => m.UnWtoNumeric).Name("region_unwto_numeric");
        Map(m => m.UnWtoName).Name("region_unwto_name");
        Map(m => m.IataSsimCode).Name("region_iatassim_code");
        Map(m => m.IataSsimName).Name("region_iatassim_name");
        Map(m => m.IataWatsCode).Name("region_iatawats_code");
        Map(m => m.IataWatsName).Name("region_iatawats_name");
    }
}

public sealed class SubregionMap : ClassMap<Subregion>
{
    public SubregionMap()
    {
        Map(m => m.UnWtoCode).Name("subregion_unwto_code");
        Map(m => m.UnWtoName).Name("subregion_unwto_name");
    }
}