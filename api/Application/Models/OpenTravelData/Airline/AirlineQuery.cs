namespace Application.Models.OpenTravelData.Airline;

public class AirlineQuery: BaseOPTDQuery
{
    /// <summary>
    /// 機場代號：IATA（2CHAR/3CHAR）
    /// </summary>
    public string Code { get; set; }
}