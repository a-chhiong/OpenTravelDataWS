namespace WebAPI.ViewModels.Seat.Map.Request;

public class SeatMapRequest
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
    /// 艙等代號：C、W、Y
    /// ----------
    /// 1. 必須指定要查詢的艙等
    /// 2. 同一 PNR 內的每一名旅客應爲同一個艙等
    /// 3. 若有例外、請洽詢華信
    /// </summary>
    public string? CabinCode { get; set; }

    /// <summary>
    /// 旅客資訊
    /// </summary>
    public List<Traveller>? Travellers { get; set; }
}

public class Traveller
{
    /// <summary>
    /// 旅客姓
    /// </summary>
    public string? Surname { get; set; }
    
    /// <summary>
    /// 旅客名
    /// </summary>
    public string? GivenName { get; set; }
    
    /// <summary>
    /// 旅客種類
    /// ----------
    /// - A: Adult
    /// - C: Child
    /// - IN: Infant
    /// - Z: NCP
    /// - N: NCPACP
    /// - B: CBBG
    /// - E: EXST
    /// - 0: Noname
    /// - 767: Infant with Seat
    /// </summary>
    public string? Type  { get; set; }
    
    /// <summary>
    /// 產品識別：DID(Departure)
    /// </summary>
    public string? DID { get; set; }
}