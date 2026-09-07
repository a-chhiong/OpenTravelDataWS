using Application.Models.OpenTravelData.Airport;
using CsvHelper.Configuration;
using Infrastructure.OpenTravelData.Converters;

namespace Infrastructure.OpenTravelData.ClassMaps;

public sealed class AirportRecordMap : ClassMap<AirportRecord>
{
    public AirportRecordMap()
    {
        Map(m => m.IataCode).Name("iata_code");
        Map(m => m.IcaoCode).Name("icao_code");
        Map(m => m.FaaCode).Name("faa_code");
        Map(m => m.IsGeonames).Name("is_geonames");
        Map(m => m.GeonameId).Name("geoname_id");
        Map(m => m.EnvelopeId).Name("envelope_id");
        Map(m => m.Name).Name("name");
        Map(m => m.Asciiname).Name("asciiname");
        Map(m => m.Latitude).Name("latitude");
        Map(m => m.Longitude).Name("longitude");
        Map(m => m.Fclass).Name("fclass");
        Map(m => m.Fcode).Name("fcode");
        Map(m => m.PageRank).Name("page_rank");
        Map(m => m.DateFrom).Name("date_from").TypeConverter(new NullableDateOnlyConverter());
        Map(m => m.DateUntil).Name("date_until").TypeConverter(new NullableDateOnlyConverter());
        Map(m => m.Comment).Name("comment");
        Map(m => m.CountryCode).Name("country_code");
        Map(m => m.Cc2).Name("cc2");
        Map(m => m.CountryName).Name("country_name");
        Map(m => m.ContinentName).Name("continent_name");
        Map(m => m.Adm1Code).Name("adm1_code");
        Map(m => m.Adm1NameUtf).Name("adm1_name_utf");
        Map(m => m.Adm1NameAscii).Name("adm1_name_ascii");
        Map(m => m.Adm2Code).Name("adm2_code");
        Map(m => m.Adm2NameUtf).Name("adm2_name_utf");
        Map(m => m.Adm2NameAscii).Name("adm2_name_ascii");
        Map(m => m.Adm3Code).Name("adm3_code");
        Map(m => m.Adm4Code).Name("adm4_code");
        Map(m => m.Population).Name("population");
        Map(m => m.Elevation).Name("elevation");
        Map(m => m.Gtopo30).Name("gtopo30");
        Map(m => m.Timezone).Name("timezone");
        Map(m => m.GmtOffset).Name("gmt_offset");
        Map(m => m.DstOffset).Name("dst_offset");
        Map(m => m.RawOffset).Name("raw_offset");
        Map(m => m.ModDate).Name("moddate").TypeConverter(new NullableDateOnlyConverter());
        Map(m => m.CityCodeList).Name("city_code_list").TypeConverter(new DelimitationConverter(","));
        Map(m => m.CityNameList).Name("city_name_list").TypeConverter(new DelimitationConverter("="));
        Map(m => m.CityDetailList).Name("city_detail_list").TypeConverter(new CityDetailConverter());
        Map(m => m.TvlPorList).Name("tvl_por_list").TypeConverter(new DelimitationConverter(","));
        Map(m => m.Iso31662).Name("iso31662");
        Map(m => m.LocationType).Name("location_type");
        Map(m => m.WikiLink).Name("wiki_link");
        Map(m => m.AltNames).Name("alt_name_section").TypeConverter(new AltNameConverter<AltName>());
        Map(m => m.Wac).Name("wac");
        Map(m => m.WacName).Name("wac_name");
        Map(m => m.CcyCode).Name("ccy_code");
        Map(m => m.UnlcList).Name("unlc_list").TypeConverter(new UnlcListConverter());
        Map(m => m.UicList).Name("uic_list").TypeConverter(new DelimitationConverter("|"));
        Map(m => m.GeonameLat).Name("geoname_lat");
        Map(m => m.GeonameLon).Name("geoname_lon");
    }
}
