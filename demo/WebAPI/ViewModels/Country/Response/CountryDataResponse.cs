namespace WebAPI.ViewModels.Airport.Response;

public class CountryDataResponse
{
    public string? Iso2CharCode { get; set; }
    public string? Iso3CharCode { get; set; }
    public string? IsoNumCode { get; set; }
    public string? Fips { get; set; }
    public string? Name { get; set; }
    public string? Capital { get; set; }
    public double? Area { get; set; }          // km²
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
    public Schengen? Schengen { get; set; }
    public Region? Region { get; set; }
    public Subregion? Subregion { get; set; }
}

public class Schengen
{
    public bool? IsSchengen { get; set; }
    public DateOnly? SchengenFrom { get; set; }
    public DateOnly? SchengenTo { get; set; }
}

public class Region
{
    public string? UnWtoCode { get; set; }
    public int? UnWtoNumeric { get; set; }
    public string? UnWtoName { get; set; }
    public string? IataSsimCode { get; set; }
    public string? IataSsimName { get; set; }
    public string? IataWatsCode { get; set; }
    public string? IataWatsName { get; set; }
}

public class Subregion
{
    public string? UnWtoCode { get; set; }
    public string? UnWtoName { get; set; }
}