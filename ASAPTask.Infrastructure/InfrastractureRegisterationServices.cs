using ASAPTask.Applications.Common.Interfaces;
using ASAPTask.Domain.Entities.Identity;
using ASAPTask.Infrastructure.Seeding;
using ASAPTask.SharedKernal.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Infrastructure
{
    public static class InfrastructureRegistrationServices
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {

            services.AddDbContext<ASAPTaskContext>(BuildDbContextOptions);

            services.AddScoped<IASAPTaskDbContext, ASAPTaskContext>();

            // Add Identity Services
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ASAPTaskContext>()
                .AddDefaultTokenProviders();

            // Add Authorization and Authentication Middleware
            services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };
                });

            services.AddAuthorization();

            services.AddTransient<AppInitializer>();
            services.Configure<JWT>(configuration.GetSection("JWT"));
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", o =>
                {
                    o.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });
            return services;
        }

        private static void BuildDbContextOptions(IServiceProvider serviceProvider, DbContextOptionsBuilder options)
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var connString = configuration.GetConnectionString("DefaultConnection");
            options.LogTo(s => System.Diagnostics.Debug.WriteLine(s));
            options.UseSqlServer(connString);
        }
    }
}
