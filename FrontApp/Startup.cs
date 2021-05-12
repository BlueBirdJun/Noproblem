using FrontApp.Data;
using FrontApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Nop.Front.Application.Services;
using Nop.Front.Common.Services;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly string NopPolicy = "policy";
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        } 

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddRazorPages();
            services.AddServerSideBlazor().AddHubOptions(o =>
            {
                o.MaximumReceiveMessageSize = 10 * 1024 * 1024;
            });
            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<ThemeState>();
            services.AddScoped<MenuService>();
            services.AddScoped<DialogService>();
            services.AddScoped<NotificationService>();
            services.AddScoped<TooltipService>();
            services.AddScoped<ContextMenuService>();
            services.AddScoped<IAccountService,AccountService>();

            services.AddScoped<ILocalStorageService, LocalStorageService>();
            services.AddScoped(x =>
            {
                var apiUrl = new Uri(Configuration.GetValue<string>("apiUrl"));
                // use fake backend if "fakeBackend" is "true" in appsettings.json
                if (Configuration.GetValue<string>("fakeBackend") == "true")
                {
                    var fakeBackendHandler = new FakeBackendHandler(x.GetService<ILocalStorageService>());
                    return new HttpClient(fakeBackendHandler) { BaseAddress = apiUrl };
                }
                return new HttpClient() { BaseAddress = apiUrl };
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: NopPolicy,
                    builder =>
                    {
                        builder.WithOrigins(Configuration.GetValue<string>("apiUrl")
                                            )
                               .WithHeaders(HeaderNames.ContentType, "x-custom-header")
                               .WithMethods("PUT", "DELETE", "GET", "OPTIONS");
                    });
            });
            //services.AddControllersWithViews(options =>
            //{
            //    options.Filters.Add(typeof(ExceptionLogFilter));
            //    options.Filters.Add(typeof(ValidateModelAttribute));
            //}).AddNewtonsoftJson();

            services.Configure<Microsoft.AspNetCore.Http.Features.FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = long.MaxValue;
            });
            services.AddLocalization();
            services.AddHealthChecks();
            services.AddMetrics();

            //services.AddHostedService<StartAsyncService>();

            Nop.Front.Application.BootStrap boot = new Nop.Front.Application.BootStrap().SetContainerBuilder(services).RegMediator();

            //services.BuildServiceProvider().GetRequiredService<IAccountService>().Initialize().ConfigureAwait().GetAwaiter();
            //var scopeFactory = services
            //        .BuildServiceProvider()
            //        .GetRequiredService<IAccountService>();
            //await scopeFactory.Initialize();
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
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors(NopPolicy);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
