namespace WebAPI.ViewModels.Airline.Request;

public class AirlineDataRequest
{
    /// <summary>
    /// 航司代碼（IATA/ICAO）
    /// </summary>
    public string Code { get; set; } = "";
}