using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nop.Business1.Application.Handlers
{
    public class StartHandler
    {
        public class Query : IRequest<Result>
        {

        }

        public class Result  
        {
        }
        public class Handler : IRequestHandler<Query, Result>
        { 
            public Handler()
            {    
            }

            public async Task<Result> Handle(Query req, CancellationToken cancellationToken)
            {
                Result dt = new Result();
                
                return dt;
            }
        }

    }
}
