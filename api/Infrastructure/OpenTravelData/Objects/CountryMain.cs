namespace Infrastructure.OpenTravelData.Objects;

public class CountryMain
{
    public string? Iso2CharCode { get; set; }
    public string? Iso3CharCode { get; set; }
    public string? IsoNumCode { get; set; }
    public string? Fips { get; set; }
    public string? Name { get; set; }
    public string? Capital { get; set; }
    public double? Area { get; set; }
    public long? Population { get; set; }
    public string? ContinentCode { get; set; }
    public string? Tld { get; set; }
    public string? CurrencyCode { get; set; }
    public string? CurrencyName { get; set; }
    public List<string>? TelephonePrefix { get; set; }
    public List<string>? ZipFormat { get; set; }
    public List<string>? LanguageCodes { get; set; }
    public string? GeoId { get; set; }
    public List<string>? NeighborCountryCodes { get; set; }
}