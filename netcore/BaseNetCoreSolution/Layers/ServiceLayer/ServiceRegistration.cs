using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.Configurations;
using CoreLayer.IRepositories;
using CoreLayer.IServices;
using CoreLayer.IUnitOfWorks;
using CoreLayer.Models.IdentityModels;
using DataLayer;
using DataLayer.Repositories;
using DataLayer.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceLayer.Mapping;
using ServiceLayer.Services;

namespace ServiceLayer
{
    public static class ServiceRegistration
    {
        
        public static void ApiServiceRegistration(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(Mapper));

            SqlServerConnection(services, configuration);
            IdentityConfiguration(services,configuration);
            //Configure
            services.Configure<List<JwtClient>>(configuration.GetSection("Clients"));
            services.Configure<TokenOption>(configuration.GetSection("TokenOption"));

            //UnitOfWorks
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Repository
            services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            //Service
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITokenService, TokenService>();
        }

        public static void ClientServiceRegistration(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(Mapper));

            SqlServerConnection(services, configuration);
            IdentityConfiguration(services, configuration);
        }

        public static void SqlServerConnection(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly("DataLayer");
                });
            });
            
        }

        public static void IdentityConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<AppUser, AppRole>(opt =>
                {
                    opt.User.RequireUniqueEmail = true;
                    opt.Password.RequireDigit = false;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequiredLength = 6;
                    opt.Password.RequiredUniqueChars = 3;

                })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}
