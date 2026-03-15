using Application.Models.OpenTravelData.Airline;
using Application.Services;
using CrossCutting.JSON;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ViewModels.Airline.Request;
using WebAPI.ViewModels.Airline.Response;

namespace WebAPI.Controllers;

/// <summary>
/// 提供開源資料的查詢
/// </summary>
public class AirlineController : BaseController
{
    private readonly ILogger<AirlineController> _logger;
    private readonly IJsonMapper _mapper;
    private readonly IOpenTravelDataService _service;

    public AirlineController(
        ILogger<AirlineController> logger,
        IJsonMapper mapper,
        IOpenTravelDataService service)
    {
        _logger = logger;
        _mapper = mapper;
        _service = service;
    }

    /// <summary>
    /// 查詢航司資訊
    /// </summary>
    /// <remarks>
    /// # 使用方式
    /// ----------
    /// 1. 提供全世界航司的基礎資料
    /// 2. 可用航司的2碼或3碼Code做搜索
    /// </remarks>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet("Airline")]
    [Consumes("application/json")]
    public async Task<AirlineDataResponse> GetCountry(
        [FromQuery] AirlineDataRequest request)
    {
        var inputModel = _mapper.Map<AirlineDataRequest, AirlineQuery>(request);

        var outputModel = await _service.Handle(inputModel);

        var response = _mapper.Map<AirlineRecord, AirlineDataResponse>(outputModel);
        
        return response;
    }
}