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

namespace Nop.Business1.Application.Handlers.Boards
{
    public class AddBoardHandler
    {
        public class Query : IRequest<Result>
        {

        }

        public class Result : BaseDto
        {
        }
        public class Handler : IRequestHandler<Query, Result>
        {
            private readonly ILogger<AddBoardHandler> _logger;
            private readonly ILifetimeScope _scope;
            private readonly NopContext _nop;
            private readonly IMediator _mediator;


            public Handler(ILogger<AddBoardHandler> logger
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
                    dt.Message = "안녕";
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
