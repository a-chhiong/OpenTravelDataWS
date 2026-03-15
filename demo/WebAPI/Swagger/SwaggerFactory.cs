using System.Reflection;
using CrossCutting.Constants;
using CrossCutting.Extensions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using WebAPI.Middlewares;
using WebAPI.Swagger.Filters.Document;
using WebAPI.Swagger.Filters.Operation;
using WebAPI.Swagger.Filters.Schema;

namespace WebAPI.Swagger;

/// <summary>
/// Swagger 打造工廠
/// </summary>
public static class SwaggerFactory
{
    /// <summary>
    /// SwaggerGen
    /// </summary>
    /// <param name="options"></param>
    /// <param name="versionInfo"></param>
    public static void Config(SwaggerGenOptions options, string versionInfo)
    {
        var descriptionFilePath = Path.Combine(AppContext.BaseDirectory, "Swagger/docs", "Description.md");
        var descriptionText = File.Exists(descriptionFilePath) 
            ? File.ReadAllText(descriptionFilePath) 
            : "T.C. Lee API";
        options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "T.C. Lee",
            Version = versionInfo.SafeFirst(7),
            Description = descriptionText,
            Contact = new OpenApiContact
            {
                Name = "T.C. Lee",
                Url = new Uri("https://www.unilarm.com/"),
            }
        });
    
        // THIS ENSURE NOTHING WILL BREAK ON SWAGGER DOC-GEN
        options.CustomSchemaIds(SchemaIdBuilder);   
        
        // Get all loaded assemblies in the current AppDomain
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        foreach (var assembly in assemblies)
        {
            var xmlFile = $"{assembly.GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            if (File.Exists(xmlPath))
            {
                // Register XML comments for this assembly
                options.IncludeXmlComments(xmlPath, true);
            }
        }
        
        // options.UseInlineDefinitionsForEnums();
        
        options.AddSecurityDefinition(CustomHeaderNames.API_KEY, new OpenApiSecurityScheme
        {
            Description = "API Key needed to access the endpoints",
            Name = CustomHeaderNames.API_KEY,
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey
        });
        
        options.OperationFilter<ApiResponseFilter>();
        options.OperationFilter<AuthHeaderFilter>();
        options.OperationFilter<HeaderRegistryFilter>();
        options.DocumentFilter<SchemaRegistryFilter>();
        options.SchemaFilter<TitleFilter>();
        options.SchemaFilter<DateTimeFilter>();
        options.SchemaFilter<EnumFilter>();
        options.SchemaFilter<DescriptionFilter>();
    }

    /// <summary>
    /// SwaggerUI
    /// </summary>
    /// <param name="options"></param>
    public static void Config(SwaggerUIOptions options)
    {
        options.DocumentTitle = "OpenTravelData - Swagger UI";
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "OpenTravelData");
    }

    private static string SchemaIdBuilder(Type type)
    {
        var baseName = BuildName(type);
        var npName = type.Namespace?.Replace(".", "") ?? "Global";
        return $"{baseName}_{npName}";

        string BuildName(Type t)
        {
            if (!t.IsGenericType)
                return t.Name;
            
            var genericName = t.GetGenericTypeDefinition().Name;
            genericName = genericName[..genericName.IndexOf('`')]; // strip `1, `2, etc.
            var genericArgs = string.Join("_", t.GetGenericArguments().Select(BuildName));
            return $"{genericName}_{genericArgs}";
        }
    }
}