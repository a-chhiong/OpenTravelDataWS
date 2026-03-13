namespace WebAPI.ViewModels.Seat.Map.Response;

public class SeatMapResponse
{
    /// <summary>
    /// 座位圖清單
    /// </summary>
    public List<SeatMap>? SeatMaps { get; set; }
}

public class SeatMap
{
    /// <summary>
    /// 航班資料
    /// </summary>
    public FlightInfo? FlightInfo { get; set; }
    
    /// <summary>
    /// 機型資訊
    /// </summary>
    public EquipmentInfo? EquipmentInfo { get; set; }

    /// <summary>
    /// 機艙配置
    /// </summary>
    public List<Compartment>? Compartments { get; set; }
    
    /// <summary>
    /// 同行旅客座位
    /// </summary>
    public List<CustomerSeat>? CustomerSeats { get; set; }   
    
    /// <summary>
    /// 旅客座位價錢
    /// ---
    /// 1. 找總額：typeQualifier = `T`
    /// 2. 找定價規則：PricingRuleAttribute != `null`
    /// </summary>
    public List<CustomerSeatPrice>? CustomerSeatPrices { get; set; }
}

public class CustomerSeat
{
    /// <summary>
    /// 座位號碼
    /// </summary>
    public string? SeatNumber  { get; set; }
    
    /// <summary>
    /// 旅客姓
    /// </summary>
    public string? Surname { get; set; }
    
    /// <summary>
    /// 旅客名
    /// </summary>
    public string? GivenName { get; set; }
    
    /// <summary>
    /// 旅客姓(中文)
    /// </summary>
    public string? NativeSurname { get; set; }

    /// <summary>
    /// 旅客名(中文)
    /// </summary>
    public string? NativeGivenName  { get; set; }

    /// <summary>
    /// 旅客識別
    /// </summary>
    public string? UCI { get; set; }

    /// <summary>
    /// 產品識別：DID(Departure)
    /// </summary>
    public string? DID { get; set; }
}

public class CustomerSeatPrice
{
    /// <summary>
    /// 旅客識別
    /// </summary>
    public string? UCI { get; set; }

    /// <summary>
    /// 產品識別：DID(Departure)
    /// </summary>
    public string? DID { get; set; }

    /// <summary>
    /// 座位價錢
    /// </summary>
    public List<SeatPrice>? SeatPrices { get; set; }
}

public class SeatPrice
{
    /// <summary>
    /// 金額
    /// </summary>
    public decimal? Amount { get; set; }
    
    /// <summary>
    /// 幣別
    /// </summary>
    public string? Currency { get; set; }
    
    /// <summary>
    /// 座位排數
    /// </summary>
    public List<string>? SeatRowNumbers { get; set; }
    
    /// <summary>
    /// 定價規則
    /// </summary>
    public List<PricingRule>? PricingRules { get; set; }
}

public class PricingRule
{
    /// <summary>
    /// 規則種類
    /// </summary>
    public string? Type { get; set; }
    
    /// <summary>
    /// 規則描述
    /// </summary>
    public string? Description { get; set; }
}

public class FlightInfo
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
}

public class EquipmentInfo
{
    /// <summary>
    /// 機型代號(IATA)
    /// </summary>
    public string? IATACode { get; set; }
    
    /// <summary>
    /// 機型代號(航司)
    /// </summary>
    public string? ACVCode { get; set; }
    
    /// <summary>
    /// 機型描述(序列號)
    /// </summary>
    public List<string>? ACVDescription { get; set; }
}

public class Compartment
{
    /// <summary>
    /// 艙等代號
    /// </summary>
    public string? CabinCode { get; set; }
    
    /// <summary>
    /// 座位行代號清單
    /// </summary>
    public List<string>? Columns { get; set; }
    
    /// <summary>
    /// 機位列配置
    /// </summary>
    public List<RowDetail>? RowDetails { get; set; }
}

public class RowDetail
{
    /// <summary>
    /// 座位列數
    /// ----------
    /// 若缺值或標定爲『0』者，可推論爲非旅客座位之列、即可能爲走道或者設備
    /// </summary>
    public string? Number { get; set; }
    
    /// <summary>
    /// 座位列特徵
    /// ----------
    /// E：Exit row（緊急出口）
    /// K：Overwing row（機翼）
    /// </summary>
    public string? Characteristic { get; set; }
    
    /// <summary>
    /// 座位詳情清單
    /// </summary>
    public List<SeatDetail>? SeatDetails { get; set; }
}

public class SeatDetail
{
    /// <summary>
    /// 座位索引值、從『0』 開始
    /// ---------
    /// 1. 拿索引值去找『Compartment』的『Columns』的『對應值』
    /// 2. 若『對應值』爲『數字』、可推論爲非旅客座位、即爲走道（isle）
    /// </summary>
    public int? ColumnIndex { get; set; }

    /// <summary>
    /// 座位佔位
    /// ----------
    /// - B: Advanced boarding pass seat
    /// - C: Check-in reserved seat
    /// - D: Seat blocked for/with deadload
    /// - E: Extra seat
    /// - F: Seat is free
    /// - G: Seat for group pre-allocation
    /// - H: Courtesy reserved seat
    /// - I: Seat is not available for interline through check-in
    /// - M: seat occupied for medical reasons
    /// - N: Seat not designated for RBD specified in request
    /// - O: Seat is occupied
    /// - P: Protected seat
    /// - Q: No seat here
    /// - S: Seat protected for code sharing
    /// - T: Transit passenger - seat occupied by a transit passenger or load
    /// - U: Upline protected seat
    /// - V: Downline protected seats
    /// - X: Seat is not available for partner airlines use
    /// - Y: Advanced seat selection seat
    /// - Z: Seat blocked for other reasons
    /// ----------
    /// 若爲『F』且被推論爲『座位』者、即爲『可選座位』，其他代號預設不可選｛除非華信有其他定義｝
    /// </summary>
    public string? Occupation { get; set; }
    
    /// <summary>
    /// 座位特徵
    /// ----------
    /// - 8: 走道（isle）
    /// - ... 請參考原廠文件
    /// </summary>
    public List<string>? Characteristics { get; set; }
}