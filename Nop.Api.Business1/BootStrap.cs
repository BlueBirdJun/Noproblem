using Autofac;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Nop.Business1.Application.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Api.Business1
{
    public class BootStrap
    {
        private ContainerBuilder _builder;
        private IServiceCollection _services;

        public BootStrap SetContainerBuilder(ContainerBuilder builder)
        {
            this._builder = builder;
            return this;
        }

        public BootStrap SetContainerBuilder(IServiceCollection services)
        {
            this._services = services;
            return this;
        }
        public BootStrap RegService()
        {
            return this;
        }
        public BootStrap RegRepsitory()
        { 
            //_services.AddHostedService<NsqService>();
            return this;
        }

        public BootStrap RegMediator()
        {
            _services.AddMediatR(typeof(StartHandler.Query).Assembly);
            return this;
        }

        public BootStrap RegAutoMapper()
        {
            
            return this;
        }
    }
}
