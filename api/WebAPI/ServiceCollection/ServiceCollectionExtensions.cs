using Application.Interfaces;
using Application.Models.OpenTravelData.Airline;
using Application.Models.OpenTravelData.Airport;
using Application.Models.OpenTravelData.Country;
using Application.Services;
using Application.Validators;
using Application.Validators.OpenTravelData;
using Infrastructure.OpenTravelData.Adapters;

namespace WebAPI.ServiceCollection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<AirportQuery>, AirportValidator>();
        services.AddScoped<IValidator<CountryQuery>, CountryValidator>();
        services.AddScoped<IValidator<AirlineQuery>, AirlineValidator>();
        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IOpenTravelDataService, OpenTravelDataService>();
        return services;
    }
    
    public static IServiceCollection AddOpenTravelDataAdapters(this IServiceCollection services)
    {
        services.AddScoped<IOpenTravelDataAdapter<AirportRecord, AirportQuery>, AirportOPTDAdapter>();
        services.AddScoped<IOpenTravelDataAdapter<CountryRecord, CountryQuery>, CountryOPTDAdapter>();
        services.AddScoped<IOpenTravelDataAdapter<AirlineRecord, AirlineQuery>, AirlineOPTDAdapter>();
        return services;
    }
}
