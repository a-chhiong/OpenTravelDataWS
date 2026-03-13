namespace Application.Models.OpenTravelData.Country;

public class CountryQuery : BaseOPTDQuery
{
    /// <summary>
    /// 國家ISO代號：2CHAR/3CHAR
    /// </summary>
    public string Code { get; set; }
}