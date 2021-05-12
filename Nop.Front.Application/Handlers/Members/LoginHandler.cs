using Autofac;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nop.Domain.Members;
using Nop.Domain.Models;
using Nop.Front.Common.Helpers;
using Nop.Front.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nop.Front.Application.Handlers.Members
{
    public class LoginHandler
    {
        public class Query : IRequest<Result>
        {
            public LoginModel data { get; set; }
        }

        public class Result :BaseDto
        {
            public BaseDtoGeneric<Domain.Members.MemberInfo, Query> rtdata { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result>
        {
            private readonly ILogger<LoginHandler> _logger;
            private readonly ILifetimeScope _scope;
            private IHttpService _httpService;

            public Handler(ILogger<LoginHandler> logger
                , ILifetimeScope scope
                 , IHttpService httpService)
            {
                _logger = logger;
                _scope = scope;
                _httpService = httpService;
            }
            public async Task<Result> Handle(Query req, CancellationToken cancellationToken)
            {
                Result dt = new Result();
                try
                {
                    var rt = await _httpService.Post<BaseDtoGeneric<Domain.Members.MemberInfo, Query>>(Constants.Login, req.data);
                    
                    
                    dt.rtdata = rt;
                }
                catch (Exception exc)
                {
                    dt.Success = false;
                    dt.HasError = true;
                    dt.Message = exc.Message;
                    dt.SystemMessage = ExcetionHelper.ExceptionMessage(exc);
                }
                return dt;
            }
        }
    }
}
