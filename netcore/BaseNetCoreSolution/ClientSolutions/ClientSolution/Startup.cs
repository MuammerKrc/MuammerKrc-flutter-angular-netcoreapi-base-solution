using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CoreLayer.Models.IdentityModels;
using DataLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.StaticFiles.Infrastructure;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Mapping;

namespace ClientSolution
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Mapper));
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly("DataLayer");
                });
            });
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

            services.ConfigureApplicationCookie(opt =>
            {
                opt.AccessDeniedPath = "/Account/AccessDenied";
                opt.LoginPath = "/account/login";
                opt.LogoutPath = "/Home/Index";
                opt.Cookie = new CookieBuilder()
                {
                    SameSite = SameSiteMode.Strict,
                    SecurePolicy = CookieSecurePolicy.SameAsRequest,
                    Name = "IdentityStructureModelCookie",
                    HttpOnly = false,
                };
                opt.ExpireTimeSpan = TimeSpan.FromDays(15);
                opt.SlidingExpiration = true;
            });

            services.AddControllersWithViews(cfg =>
            {
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {

                    //var allowedIps = Configuration["AllowedIPList"].Split(';').ToList();
                    //var allowedPaths = Configuration["AllowedPathList"].Split(';').ToList();
                    //if (!allowedPaths.Any(i => ctx.Context.Request.Path.StartsWithSegments(i))
                    //    && !allowedIps.Any(i => i.Equals(ctx.Context.Connection.RemoteIpAddress.ToString().TrimStart('{').TrimEnd('}'))))
                    //{
                    //    ctx.Context.Response.Headers.Add("Cache-Control", "no-store");
                    //    if (!ctx.Context.User.Identity.IsAuthenticated)
                    //    {
                    //        // respond HTTP 401 Unauthorized with empty body.
                    //        ctx.Context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    //        ctx.Context.Response.ContentLength = 0;
                    //        ctx.Context.Response.Body = Stream.Null;
                    //        ctx.Context.Response.Redirect("/account/login?returnUrl=" + ctx.Context.Request.Path.Value);
                    //    }
                    //}
                }
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
