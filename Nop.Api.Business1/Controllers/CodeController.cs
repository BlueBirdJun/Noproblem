using Autofac;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nop.Business1.Application.Handlers.Code;
using Nop.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Api.Business1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeController : ExtensionContoroller
    {
        #region field 
        private readonly ILogger<CodeController> _logger;
        private readonly ILifetimeScope _scope;
        private readonly IMediator _mediator;        
        #endregion

        public CodeController(ILogger<CodeController> logger,
            ILifetimeScope scope,IMediator mediator
            )
        {
            _logger = logger;
            _scope = scope;
            _mediator = mediator; 
        }
        [HttpGet]
        public async Task<IActionResult> GetCodeList()
        {
            GetCodeHandler.Query data = new GetCodeHandler.Query();
            var rt = await _mediator.Send(data);
            return Ok(rt);
        }
    }
}
