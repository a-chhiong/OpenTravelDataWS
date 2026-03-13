using Application.Models.OpenTravelData.Airline;
using CsvHelper.Configuration;
using Infrastructure.OpenTravelData.Converters;

namespace Infrastructure.OpenTravelData.ClassMaps;

public sealed class AirlineRecordMap : ClassMap<AirlineRecord>
{
    public AirlineRecordMap()
    {
        Map(m => m.Pk).Name("pk");
        Map(m => m.EnvId).Name("env_id");
        Map(m => m.ValidityFrom).Name("validity_from").TypeConverter(new NullableDateOnlyConverter());
        Map(m => m.ValidityTo).Name("validity_to").TypeConverter(new NullableDateOnlyConverter());
        Map(m => m.IataCode).Name("3char_code");
        Map(m => m.IcaoCode).Name("2char_code");
        Map(m => m.NumCode).Name("num_code");
        Map(m => m.Name).Name("name");
        Map(m => m.Name2).Name("name2");
        Map(m => m.AllianceCode).Name("alliance_code");
        Map(m => m.AllianceStatus).Name("alliance_status");
        Map(m => m.Type).Name("type");
        Map(m => m.WikiLink).Name("wiki_link");
        Map(m => m.FlightFrequency).Name("flt_freq");
        Map(m => m.AltNames).Name("alt_names").TypeConverter(new AltNameConverter<AltName>());
        Map(m => m.Bases).Name("bases").TypeConverter(new DelimitationConverter("="));
        Map(m => m.Key).Name("key");
        Map(m => m.Version).Name("version");
        Map(m => m.ParentPkList).Name("parent_pk_list").TypeConverter(new PkListConverter());
        Map(m => m.SuccessorPkList).Name("successor_pk_list").TypeConverter(new PkListConverter());
    }
}