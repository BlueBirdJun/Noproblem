using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.RootApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            string appjson = "appsettings.json";
            string conjson = "values.json";
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").ToLower() == "development")
            {
                appjson = $"appsettings.{environmentName}.json";
                conjson = $"values.{environmentName}.json";
            }
            var configuration = new ConfigurationBuilder()
                    .AddJsonFile(appjson)
                    .AddJsonFile(conjson)
                    .Build();
            Serilog.ILogger logger = Log.Logger = new LoggerConfiguration()
           .ReadFrom.Configuration(configuration)
           .CreateLogger();
            try
            {
                Log.Warning("Start API");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception exception)
            {
                Log.Fatal(exception.Message);
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            } 
        }

        public static Microsoft.Extensions.Hosting.IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
              /*.ConfigureMetricsWithDefaults(builder =>
                 {
                     builder.Report.ToTextFile("c:\\logs", TimeSpan.FromSeconds(2));
                 })
                 .ConfigureAppMetricsHostingConfiguration(options =>
                 {
                     // options.AllEndpointsPort = 3333;
                     options.EnvironmentInfoEndpoint = "/my-env";
                     options.EnvironmentInfoEndpointPort = 1111;
                     options.MetricsEndpoint = "/my-metrics";
                     options.MetricsEndpointPort = 2222;
                     options.MetricsTextEndpoint = "/my-metrics-text";
                     options.MetricsTextEndpointPort = 3333;
                 })*/
              .UseServiceProviderFactory(new AutofacServiceProviderFactory())
              .ConfigureWebHostDefaults(webBuilder =>
              {
                  webBuilder.UseStartup<Startup>();
              }).UseSerilog();//.UseMetrics();
    }
}
