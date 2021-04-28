using Autofac;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nop.Common.Helpers;
using Nop.Common.Models;
using Nop.Domain.Commons;
using Nop.Repository;
///using Nop.Repository.NopModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nop.Business1.Application.Handlers.Code
{
    public class GetCodeHandler
    {
        public class Query : IRequest<Result>
        {

        }
        public class Result : BaseDtoGeneric<List<CommonCodes>, Query>
        {
        }

        public class Handler : IRequestHandler<Query, Result>
        {
            private readonly ILogger<AddCodeHandler> _logger;
            private readonly ILifetimeScope _scope;
            private readonly NopContext _nop;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;

            public Handler(ILogger<AddCodeHandler> logger
                , ILifetimeScope scope
                , NopContext nop
                , IMediator mediator
                , IMapper mapper
                )
            {
                _logger = logger;
                _scope = scope;
                _nop = nop;
                _mediator = mediator;
                _mapper = mapper;
            }

            public async Task<Result> Handle(Query req, CancellationToken cancellationToken)
            {
                Result dt = new Result();
                try
                {
                    var lst = await _nop.Commoncodes.ToListAsync();
                        
                    dt.OutPutValue = _mapper.Map<List<Domain.Commons.CommonCodes>>(lst);
                    dt.Success = true;
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
