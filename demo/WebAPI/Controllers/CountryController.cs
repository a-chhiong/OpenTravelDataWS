using Application.Models.OpenTravelData.Country;
using Application.Services;
using CrossCutting.JSON;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ViewModels.Airport.Request;
using WebAPI.ViewModels.Airport.Response;

namespace WebAPI.Controllers;

/// <summary>
/// 提供開源資料的查詢
/// </summary>
public class CountryController : BaseController
{
    private readonly ILogger<CountryController> _logger;
    private readonly IJsonMapper _mapper;
    private readonly IOpenTravelDataService _service;

    public CountryController(
        ILogger<CountryController> logger,
        IJsonMapper mapper,
        IOpenTravelDataService service)
    {
        _logger = logger;
        _mapper = mapper;
        _service = service;
    }
    
    /// <summary>
    /// 查詢國家資訊
    /// </summary>
    /// <remarks>
    /// # 使用方式
    /// ----------
    /// 1. 提供全世界國家的基礎資料
    /// 2. 可用國家的2碼或3碼Code做搜索
    /// </remarks>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet("Country")]
    [Consumes("application/json")]
    public async Task<CountryDataResponse> GetCountry(
        [FromQuery] CountryDataRequest request)
    {
        var inputModel = _mapper.Map<CountryDataRequest, CountryQuery>(request);

        var outputModel = await _service.Handle(inputModel);

        var response = _mapper.Map<CountryRecord, CountryDataResponse>(outputModel);
        
        return response;
    }
}