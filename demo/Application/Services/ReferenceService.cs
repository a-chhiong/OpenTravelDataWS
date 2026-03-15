using Application.Interfaces;
using Application.Models.OpenTravelData.Airline;
using Application.Models.OpenTravelData.Airport;
using Application.Models.OpenTravelData.Country;
using Application.Validators;
using CrossCutting.Enums;
using CrossCutting.Guarder;

namespace Application.Services;

public interface IOpenTravelDataService
{
    Task<AirportRecord?> Handle(AirportQuery input);
    Task<CountryRecord?> Handle(CountryQuery input);
    Task<AirlineRecord?> Handle(AirlineQuery input);
}

public class OpenTravelDataService: IOpenTravelDataService
{
    private readonly IOpenTravelDataAdapter<AirportRecord, AirportQuery> _airportAdapter;
    private readonly IOpenTravelDataAdapter<CountryRecord, CountryQuery> _countryAdapter;
    private readonly IOpenTravelDataAdapter<AirlineRecord, AirlineQuery> _airlineAdapter;
    private readonly IValidator<AirportQuery> _airportValidator;
    private readonly IValidator<CountryQuery> _countryValidator;
    private readonly IValidator<AirlineQuery> _airlineValidator;
    
    public OpenTravelDataService(
        IOpenTravelDataAdapter<AirportRecord, AirportQuery> airportAdapter,
        IOpenTravelDataAdapter<CountryRecord, CountryQuery> countryAdapter,
        IOpenTravelDataAdapter<AirlineRecord, AirlineQuery> airlineAdapter,
        IValidator<AirportQuery> airportValidator,
        IValidator<CountryQuery> countryValidator,
        IValidator<AirlineQuery> airlineValidator)
    {
        this._airportAdapter = airportAdapter;
        this._countryAdapter = countryAdapter;
        this._airlineAdapter = airlineAdapter;
        this._airportValidator = airportValidator;
        this._countryValidator = countryValidator;
        this._airlineValidator = airlineValidator;
    }

    public async Task<AirportRecord?> Handle(AirportQuery input)
    {
        var result = _airportValidator.Validate(input);
        
        Guarder.Throw(!result.IsValid, ErrorCodeEnum.InvalidRequest, result.ErrorMessage);

        var record = await _airportAdapter.Fetch(input);
        
        Guarder.Throw(record == null, ActionCodeEnum.NoDataFound);

        return record;
    }

    public async Task<CountryRecord?> Handle(CountryQuery input)
    {
        var result = _countryValidator.Validate(input);
        
        Guarder.Throw(!result.IsValid, ErrorCodeEnum.InvalidRequest, result.ErrorMessage);
        
        var record = await _countryAdapter.Fetch(input);
        
        Guarder.Throw(record == null, ActionCodeEnum.NoDataFound);

        return record;
    }

    public async Task<AirlineRecord?> Handle(AirlineQuery input)
    {
        var result = _airlineValidator.Validate(input);
        
        Guarder.Throw(!result.IsValid, ErrorCodeEnum.InvalidRequest, result.ErrorMessage);
        
        var record = await _airlineAdapter.Fetch(input);
        
        Guarder.Throw(record == null, ActionCodeEnum.NoDataFound);

        return record;
    }
}