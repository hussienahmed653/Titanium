using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Titanium2.Application;
using Titanium2.Application.Interfaces.CategoryInterfaces;
using Titanium2.Application.Interfaces.ImageInterface;
using Titanium2.Application.Interfaces.JwtInterfaces;
using Titanium2.Application.Interfaces.ProductInterfaces;
using Titanium2.Application.Interfaces.StockInterface;
using Titanium2.Application.Services;
using Titanium2.Application.Services.JwtRgistrationAndLoginRepo;
using Titanium2.Domain.UserRepo;
using Titanium2.Infrastructure.AppDbContext;
using Titanium2.Infrastructure.CategoryRepo;
using Titanium2.Infrastructure.ImageRepo;
using Titanium2.Infrastructure.JwtServices;
using Titanium2.Infrastructure.ProductRepo;
using Titanium2.Infrastructure.StockRepo;
using Titanium2.Infrastructure.UserRepo;

namespace Titanium2.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IJwtServices, JwtServics>();
            services.AddScoped<IUserRepo, UserRepository>();
            services.AddScoped<JwtRegistrationRepo>();
            services.AddScoped<JwtRemoveRoleFromUser>();
            services.AddScoped<JwtLoginRepo>();
            services.AddScoped<ICategoryInterface, CategoryRepository>();
            services.AddScoped<CategoryServices>();
            services.AddScoped<IImageInterface, ImageReposetory>();
            services.AddScoped<ImageServices>();
            services.AddScoped<IproductInterface, ProductRepository>();
            services.AddScoped<ProductServices>();
            services.AddScoped<ISockInterface, StockRepository>();
            services.AddScoped<StockServices>();


            var connection = configuration.GetConnectionString("Defaultconnection");
            services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseNpgsql(connection);
            });

            services.Configure<Jwt>(configuration.GetSection("Jwt"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var key = Encoding.ASCII.GetBytes(configuration.GetSection("Jwt:Key").Value);

                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = true
                };
            });




            return services;
        }
    }
}
