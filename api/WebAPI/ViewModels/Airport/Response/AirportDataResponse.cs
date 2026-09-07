namespace WebAPI.ViewModels.Airport.Response;

public class AirportDataResponse
{
    public string? IataCode { get; set; }
    public string? IcaoCode { get; set; }
    public string? FaaCode { get; set; }
    public string? IsGeonames { get; set; }
    public string? GeonameId { get; set; }
    public string? EnvelopeId { get; set; }
    public string? Name { get; set; }
    public string? Asciiname { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string? Fclass { get; set; }
    public string? Fcode { get; set; }
    public double? PageRank { get; set; }
    public DateOnly? DateFrom { get; set; }
    public DateOnly? DateUntil { get; set; }
    public string? Comment { get; set; }
    public string? CountryCode { get; set; }
    public string? Cc2 { get; set; }
    public string? CountryName { get; set; }
    public string? ContinentName { get; set; }
    public string? Adm1Code { get; set; }
    public string? Adm1NameUtf { get; set; }
    public string? Adm1NameAscii { get; set; }
    public string? Adm2Code { get; set; }
    public string? Adm2NameUtf { get; set; }
    public string? Adm2NameAscii { get; set; }
    public string? Adm3Code { get; set; }
    public string? Adm4Code { get; set; }
    public long? Population { get; set; }
    public int? Elevation { get; set; }
    public int? Gtopo30 { get; set; }
    public string? Timezone { get; set; }
    public double? GmtOffset { get; set; }
    public double? DstOffset { get; set; }
    public double? RawOffset { get; set; }
    public DateOnly? ModDate { get; set; }
    public List<string>? CityCodeList { get; set; }
    public List<string>? CityNameList { get; set; }
    public List<CityDetail>? CityDetailList { get; set; }
    public List<string>? TvlPorList { get; set; }
    public string? Iso31662 { get; set; }
    public string? LocationType { get; set; }
    public string? WikiLink { get; set; }
    public List<AltName>? AltNames { get; set; }
    public string? Wac { get; set; }
    public string? WacName { get; set; }
    public string? CcyCode { get; set; }
    public List<string>? UnlcList { get; set; }
    public List<string>? UicList { get; set; }
    public double? GeonameLat { get; set; }
    public double? GeonameLon { get; set; }
}

public class AltName
{
    public string? Lang { get; set; }
    public string? Name { get; set; }
}

public class CityDetail
{
    public string? CityCode { get; set; }
    public string? GeonameId { get; set; }
    public string? NameUtf { get; set; }
    public string? NameAscii { get; set; }
    public string? CountryCode { get; set; }
}