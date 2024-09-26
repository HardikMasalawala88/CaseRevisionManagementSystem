using CMS.Data.ContextModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CMS.API.Extension
{
    public static class ServiceExtension
    {
        #region Swagger Impementation
        public static void IntegrateSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CMS.API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
            });
        }
        #endregion

        #region Identity
        public static void IdentityImplementation(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();
        }
        #endregion

        #region Authorization
        public static void AuthorizationHandler(this IServiceCollection services)
        {
            services.AddAuthorization(cfg =>
            {
                cfg.AddPolicy("SuperAdmin", policy => policy.RequireClaim(ClaimTypes.Role, "SuperAdmin"));
                cfg.AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
                //cfg.AddPolicy("User", policy => policy.RequireClaim("type", "User"));
            });

        }
        #endregion
    }
}
