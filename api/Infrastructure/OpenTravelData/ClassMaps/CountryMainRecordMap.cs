using CsvHelper.Configuration;
using Infrastructure.OpenTravelData.Converters;
using Infrastructure.OpenTravelData.Objects;

namespace Infrastructure.OpenTravelData.ClassMaps;

public class CountryMainMap : ClassMap<CountryMain>
{
    public CountryMainMap()
    {
        Map(m => m.Iso2CharCode).Name("iso_2char_code");
        Map(m => m.Iso3CharCode).Name("iso_3char_code");
        Map(m => m.IsoNumCode).Name("iso_num_code");
        Map(m => m.Fips).Name("fips");
        Map(m => m.Name).Name("name");
        Map(m => m.Capital).Name("cptl");
        Map(m => m.Area).Name("area");
        Map(m => m.Population).Name("pop");
        Map(m => m.ContinentCode).Name("cont_code");
        Map(m => m.Tld).Name("tld");
        Map(m => m.CurrencyCode).Name("ccy_code");
        Map(m => m.CurrencyName).Name("ccy_name");
        Map(m => m.TelephonePrefix).Name("tel_pfx").TypeConverter(new DelimitationConverter("and"));
        Map(m => m.ZipFormat).Name("zip_fmt").TypeConverter(new DelimitationConverter("|"));
        Map(m => m.LanguageCodes).Name("lang_code_list").TypeConverter(new DelimitationConverter("="));
        Map(m => m.GeoId).Name("geo_id");
        Map(m => m.NeighborCountryCodes).Name("ngbr_ctry_code_list").TypeConverter(new DelimitationConverter("="));
    }
}