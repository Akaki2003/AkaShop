using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;

namespace AkaShop.Infrastructure.Extensions
{
    public static class SwaggerClientExtension
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services, IConfiguration config)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "AkaShopApi",
                    Version = "v1",
                    Description = "AkaShopApi",
                    Contact = new OpenApiContact
                    {
                        Email = "akaki.ujarashvili.1@btu.edu.ge",
                        Name = "Shop Api",
                    }
                });
                option.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                    Description = "Basic Authorization header using the Bearer scheme."
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "basic"
                                }
                            },
                            new string[] {}
        }
                });
                option.CustomSchemaIds(type => type.ToString());
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine($"{AppContext.BaseDirectory}", xmlFile);

                option.IncludeXmlComments(xmlPath);
                option.ExampleFilters();
            });
            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.DocumentTitle = "AkaShop API";
                c.DocExpansion(DocExpansion.None);
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
                c.OAuthScopeSeparator(" ");
                c.RoutePrefix = string.Empty;
            });

            return app;
        }

    }
}
