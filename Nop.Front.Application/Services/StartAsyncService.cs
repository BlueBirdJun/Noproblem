using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nop.Front.Application.Services
{
    public class StartAsyncService: IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        public StartAsyncService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                // Example of getting a service you registered in the startup
                var accountservice = scope.ServiceProvider.GetRequiredService<IAccountService>();
                await accountservice.Initialize();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
