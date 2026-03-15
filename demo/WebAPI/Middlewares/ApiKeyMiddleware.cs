using CrossCutting.Constants;

namespace WebAPI.Middlewares;

using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _apiKey;

    public ApiKeyMiddleware(
        RequestDelegate next, IConfiguration config)
    {
        _next = next;
        _apiKey = config.GetSection("Authorization:ApiKey").Value ?? "";
    }

    public async Task InvokeAsync(HttpContext context, IConfiguration configuration)
    {
        // Skip if endpoint allows anonymous
        var endpoint = context.GetEndpoint();
        if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
        {
            await _next(context);
            return;
        }

        // Check header
        if (!context.Request.Headers.TryGetValue(CustomHeaderNames.API_KEY, out var extractedApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("API Key was not provided.");
            return;
        }

        if (!_apiKey.Equals(extractedApiKey))
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Unauthorized client.");
            return;
        }

        await _next(context);
    }
}