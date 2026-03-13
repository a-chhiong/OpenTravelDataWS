using CrossCutting.JSON;
using CrossCutting.Logger;
using WebAPI.Swagger;
using NLog.Web;
using WebAPI.Attributes;
using WebAPI.Middlewares;
using WebAPI.ServiceCollection;

var builder = WebApplication.CreateBuilder(args);

// Load version.info written by MSBuild target
var versionInfo = "unknown";
try
{
    var path = Path.Combine(AppContext.BaseDirectory, "version.info");
    if (File.Exists(path))
    {
        versionInfo = File.ReadAllText(path).Trim();
    }
}
catch (Exception ex)
{
    versionInfo = $"error: {ex.Message}";
}
// Make it available via configuration as well singleton
builder.Configuration["VersionInfo"] = versionInfo;

// CorsPolicy
var corsOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins")
    .Get<string[]>()?.Select(o => o.Trim().TrimEnd('/')).ToArray() ?? [];   // Clean the origins (remove trailing slashes and whitespace)
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        if (corsOrigins.Any())
        {
            if (corsOrigins.First() == "*") // 'Wildcard' mode
            {
                policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            }
            else
            {
                policy.WithOrigins(corsOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .SetPreflightMaxAge(TimeSpan.FromMinutes(10));
            }
        }
    });
});

// Clear default providers and plug in NLog
builder.Logging.ClearProviders();
// Environment‑specific minimum level
if (builder.Environment.IsDevelopment())
{
    builder.Logging.SetMinimumLevel(LogLevel.Trace);       // everything for debugging
}
else if (builder.Environment.IsStaging())
{
    builder.Logging.SetMinimumLevel(LogLevel.Debug);       // detailed but less noisy
}
else if (builder.Environment.IsProduction())
{
    builder.Logging.SetMinimumLevel(LogLevel.Information); // clean, readable logs
}
builder.Host.UseNLog();

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiResponseFilterAttribute>();
    
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new EnumConverterFactory());
    options.JsonSerializerOptions.Converters.Add(new DateTimeConverterFactory());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => SwaggerFactory.Config(x, versionInfo));

// Register Adapter (DI resolves correct generic interface)
builder.Services.AddSingleton<IJsonMapper, JsonMapper>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddLogging();

builder.Services.AddApplicationValidators();
builder.Services.AddOpenTravelDataAdapters();

builder.Services.AddApplicationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI(SwaggerFactory.Config);
}

app.UseHttpsRedirection();

app.UseMiddleware<ApiKeyMiddleware>();
app.UseMiddleware<TraceMiddleware>(); 

app.MapControllers();

// Initialize AppLog once with a resolver, and resolver reads from AsyncLocal
AppLog.Initialize(
    app.Services.GetRequiredService<ILoggerFactory>(),
    () => TraceContextHolder.CurrentTraceId.Value);

app.Run();

// Ensure NLog flushes/shuts down on exit
NLog.LogManager.Shutdown();