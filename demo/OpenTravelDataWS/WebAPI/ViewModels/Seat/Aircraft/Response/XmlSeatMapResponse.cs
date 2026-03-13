using WebAPI.ViewModels.Seat.Map.Response;

namespace WebAPI.ViewModels.Seat.Aircraft.Response;

public class XmlSeatMapResponse
{
    /// <summary>
    /// 機型資訊
    /// </summary>
    public EquipmentInfo? EquipmentInfo { get; set; }

    /// <summary>
    /// 機艙配置
    /// </summary>
    public List<Compartment>? Compartments { get; set; }
}