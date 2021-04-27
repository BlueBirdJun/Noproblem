using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Nop.Common.Filters;
using Nop.Repository;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Pomelo.EntityFrameworkCore.MySql.Storage;


namespace Nop.RootApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly string NopPolicy = "policy";
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        public Startup(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _environment = environment;
            Configuration = configuration;
            
           
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        }  

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        { 
            
            //services.AddAutoMapper(typeof(Startup))
            

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

            services.AddCors(options =>
            {
                options.AddPolicy(name: NopPolicy,
                    builder =>
                    {
                        builder.WithOrigins("http://example.com",
                                            "http://www.contoso.com",
                                            "https://cors1.azurewebsites.net",
                                            "https://cors3.azurewebsites.net",
                                            "https://localhost:44398",
                                            "https://localhost:5001")
                               .WithHeaders(HeaderNames.ContentType, "x-custom-header")
                               .WithMethods("PUT", "DELETE", "GET", "OPTIONS");
                    });
            });
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(typeof(ExceptionLogFilter));
                options.Filters.Add(typeof(ValidateModelAttribute));
            }).AddNewtonsoftJson();
              
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Nop.RootApi", Version = "v1" });
            });

            var valuesString = File.ReadAllText(Configuration.GetValue<string>("ConnectionStringPaths:values"));
            var valuesJson = JsonConvert.DeserializeObject<Dictionary<string, string>>(valuesString);

            services.AddDbContext<NopContext>(options =>
            options.UseMySql(valuesJson["NOP"],
            mySqlOptionsAction: options =>
            {
                options.CharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8).CharSetBehavior(CharSetBehavior.AppendToAllColumns);
                //DB 연결에 실패할 경우 재시도 설정
                options.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
            }));

            services.AddAutoMapper(Assembly.GetAssembly(typeof(Api.Business1.MapperProfile)));
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            services.AddHealthChecks();
            services.AddMetrics();

            Nop.Api.Business1.BootStrap boot = new Api.Business1.BootStrap().SetContainerBuilder(services).RegMediator();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nop.RootApi v1"));
            }
            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseCors(NopPolicy);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
