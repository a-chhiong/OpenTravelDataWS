namespace Application.Models.OpenTravelData;

public abstract class BaseAltName
{
    /// <summary>
    /// 語系代號
    /// </summary>
    public string? Lang { get; set; }
    
    /// <summary>
    /// 對應名稱
    /// </summary>
    public string? Name { get; set; }
}