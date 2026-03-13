namespace WebAPI.ViewModels.Seat.Allocation.Request;

public class SeatAllocRequest
{
    /// <summary>
    /// 出發日期：DDMMYY
    /// </summary>
    public DateOnly? DepartureDate { get; set; }
    
    /// <summary>
    /// 出發地（機場代號）
    /// </summary>
    public string? Departure {get; set;}
    
    /// <summary>
    /// 抵達地（機場代號）
    /// </summary>
    public string? Arrival {get; set;}

    /// <summary>
    /// 航空公司代碼
    /// </summary>
    public string? MarketingCarrier { get; set; }

    /// <summary>
    /// 班機號碼
    /// </summary>
    public string? FlightNumber { get; set; }

    /// <summary>
    /// 航段清單
    /// ----------
    /// by 人 / by 航段
    /// </summary>
    public List<SeatRequest>? SeatRequests { get; set; }
}

public class SeatRequest
{
    /// <summary>
    /// 產品識別：DID(Departure)
    /// </summary>
    public string? DID { get; set; }

    /// <summary>
    /// 座位號碼
    /// </summary>
    public string? SeatNumber  { get; set; }
}
