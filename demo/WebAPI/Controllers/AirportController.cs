using Application.Models.OpenTravelData.Airport;
using Application.Services;
using CrossCutting.JSON;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ViewModels.Airport.Request;
using WebAPI.ViewModels.Airport.Response;

namespace WebAPI.Controllers;

/// <summary>
/// 提供開源資料的查詢
/// </summary>
public class AirportController : BaseController
{
    private readonly ILogger<AirportController> _logger;
    private readonly IJsonMapper _mapper;
    private readonly IOpenTravelDataService _service;

    public AirportController(
        ILogger<AirportController> logger,
        IJsonMapper mapper,
        IOpenTravelDataService service)
    {
        _logger = logger;
        _mapper = mapper;
        _service = service;
    }
    
    /// <summary>
    /// 查詢機場資訊
    /// </summary>
    /// <remarks>
    /// # 使用方式
    /// ----------
    /// 1. 提供全世界機場的基礎資料
    /// 2. 可用機場的3碼Code做搜索
    /// </remarks>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet("Airport")]
    [Consumes("application/json")]
    public async Task<AirportDataResponse> GetAirport(
        [FromQuery] AirportDataRequest request)
    {
        var inputModel = _mapper.Map<AirportDataRequest, AirportQuery>(request);

        var outputModel = await _service.Handle(inputModel);

        var response = _mapper.Map<AirportRecord, AirportDataResponse>(outputModel);
        
        return response;
    }
}