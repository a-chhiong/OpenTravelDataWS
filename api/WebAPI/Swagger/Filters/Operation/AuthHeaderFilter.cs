using CrossCutting.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebAPI.Swagger.Filters.Operation;

public class AuthHeaderFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Check for [AllowAnonymous] on the method
        var hasAllowAnonymousOnMethod = context.MethodInfo
            .GetCustomAttributes(true)
            .OfType<AllowAnonymousAttribute>()
            .Any();

        // Check for [AllowAnonymous] on the controller class itself
        var hasAllowAnonymousOnClass = context.MethodInfo.DeclaringType?
            .GetCustomAttributes(true)
            .OfType<AllowAnonymousAttribute>()
            .Any() ?? false;

        if (hasAllowAnonymousOnMethod || hasAllowAnonymousOnClass)
        {
            // Skip adding security requirement
            // Explicitly clear any inherited security requirements
            operation.Security.Clear();
            return;
        }

        // Add Swagger UI Authorization header requirement
        operation.Security = new List<OpenApiSecurityRequirement>
        {
            new OpenApiSecurityRequirement
            {
                [
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = CustomHeaderNames.API_KEY
                        }
                    }
                ] = Array.Empty<string>()
            }
        };
    }
}