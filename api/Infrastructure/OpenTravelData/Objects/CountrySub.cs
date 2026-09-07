namespace Infrastructure.OpenTravelData.Objects;

public class CountrySub
{
    public string? CountryCode { get; set; }
    public string? CountryName { get; set; }
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