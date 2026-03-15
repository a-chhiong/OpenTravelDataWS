namespace Application.Models.OpenTravelData.Airport;

public class AirportQuery: BaseOPTDQuery
{
    /// <summary>
    /// 機場代號：IATA（3CHAR）
    /// </summary>
    public string Code { get; set; }
}