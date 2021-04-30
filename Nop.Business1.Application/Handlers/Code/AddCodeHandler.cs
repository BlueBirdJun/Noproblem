using Autofac;
using MediatR;
using Microsoft.Extensions.Logging;
using Nop.Common.Helpers;
using Nop.Common.Models;
using Nop.Domain.Models;
using Nop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nop.Business1.Application.Handlers.Code
{
    public class AddCodeHandler
    {
        public class Query : IRequest<Result>
        {

        }

        public class Result : BaseDto
        {
        }
        public class Handler : IRequestHandler<Query, Result>
        {
            private readonly ILogger<AddCodeHandler> _logger;
            private readonly ILifetimeScope _scope;            
            private readonly NopContext _nop; 
            private readonly IMediator _mediator;
            

            public Handler(ILogger<AddCodeHandler> logger
                , ILifetimeScope scope
                , NopContext nop                
                , IMediator mediator
                )
            {
                _logger = logger;
                _scope = scope;
                _nop = nop;
                _mediator = mediator;
            }

            public async Task<Result> Handle(Query req, CancellationToken cancellationToken)
            {
                Result dt = new Result();
                try
                {
                    

                    Repository.NopModels.CommonCodes cm = new Repository.NopModels.CommonCodes()
                    { Code = "user",Group="Member",Name="일반유저",UserId="System",Description="" };
                    Repository.NopModels.CommonCodes cm1 = new Repository.NopModels.CommonCodes()
                    { Code = "admin", Group = "Member", Name = "관리자", UserId = "System", Description = "" };
                    Repository.NopModels.CommonCodes cm2 = new Repository.NopModels.CommonCodes()
                    { Code = "super", Group = "Member", Name = "수퍼유저", UserId = "System", Description = "" };
                    Repository.NopModels.CommonCodes cm3 = new Repository.NopModels.CommonCodes()
                    { Code = "bad", Group = "Member", Name = "불량유저", UserId = "System", Description = "" };
                    _nop.Commoncodes.Add(cm);
                    _nop.Commoncodes.Add(cm1);
                    _nop.Commoncodes.Add(cm2);
                    _nop.Commoncodes.Add(cm3);
                    await _nop.SaveChangesAsync();
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
