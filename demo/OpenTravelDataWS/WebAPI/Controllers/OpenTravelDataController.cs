using Application.Models.OpenTravelData.Airline;
using Application.Models.OpenTravelData.Airport;
using Application.Models.OpenTravelData.Country;
using Application.Services;
using CrossCutting.JSON;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ViewModels.Airline.Request;
using WebAPI.ViewModels.Airline.Response;
using WebAPI.ViewModels.Airport.Request;
using WebAPI.ViewModels.Airport.Response;

namespace WebAPI.Controllers;

/// <summary>
/// 提供開源資料的查詢
/// </summary>
public class OpenTravelDataController : BaseController
{
    private readonly ILogger<OpenTravelDataController> _logger;
    private readonly IJsonMapper _mapper;
    private readonly IOpenTravelDataService _service;

    public OpenTravelDataController(
        ILogger<OpenTravelDataController> logger,
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
    /// - 此功能可能會做在後台維護，不一定需要呼叫此API
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
    
    /// <summary>
    /// 查詢國家資訊
    /// </summary>
    /// <remarks>
    /// # 使用方式
    /// ----------
    /// 1. 提供全世界國家的基礎資料
    /// 2. 可用國家的2碼或3碼Code做搜索
    /// - 此功能可能會做在後台維護，不一定需要呼叫此API
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
    
    
    /// <summary>
    /// 查詢航司資訊
    /// </summary>
    /// <remarks>
    /// # 使用方式
    /// ----------
    /// 1. 提供全世界航司的基礎資料
    /// 2. 可用航司的2碼或3碼Code做搜索
    /// - 此功能可能會做在後台維護，不一定需要呼叫此API
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