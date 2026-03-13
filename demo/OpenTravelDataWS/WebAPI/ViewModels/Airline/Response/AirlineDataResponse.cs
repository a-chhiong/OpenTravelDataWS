namespace WebAPI.ViewModels.Airline.Response;

public class AirlineDataResponse{
    public string? Pk { get; set; }
    public string? EnvId { get; set; }
    public DateOnly? ValidityFrom { get; set; }
    public DateOnly? ValidityTo { get; set; }
    public string? IataCode { get; set; }
    public string? IcaoCode { get; set; }
    public int? NumCode { get; set; }
    public string? Name { get; set; }
    public string? Name2 { get; set; }
    public string? AllianceCode { get; set; }
    public string? AllianceStatus { get; set; }
    public string? Type { get; set; }
    public string? WikiLink { get; set; }
    public int? FlightFrequency { get; set; }
    public List<AltName>? AltNames { get; set; }
    public List<string>? Bases { get; set; }
    public string? Key { get; set; }
    public int? Version { get; set; }
    public List<string>? ParentPkList { get; set; }
    public List<string>? SuccessorPkList { get; set; }
}

public class AltName
{
    public string? Lang { get; set; }
    public string? Name { get; set; }
}
