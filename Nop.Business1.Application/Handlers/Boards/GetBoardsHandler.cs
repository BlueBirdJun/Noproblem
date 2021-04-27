using MediatR;
using Nop.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Business1.Application.Handlers.Boards
{
    public class GetBoardsHandler
    {
        public class Query : IRequest<Result>
        {

        }
        public class Result : BaseDto
        {
        }

    }
}
