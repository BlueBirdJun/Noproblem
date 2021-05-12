using Autofac;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nop.Business1.Application.Handlers.Code;
using Nop.Business1.Application.Handlers.Member;
using Nop.Common.Extensions;
using Nop.Domain.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Api.Business1.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ExtensionContoroller
    {
        #region field 
        private readonly ILogger<MemberController> _logger;
        private readonly ILifetimeScope _scope;
        private readonly IMediator _mediator;
        #endregion

        public MemberController(ILogger<MemberController> logger,
            ILifetimeScope scope, IMediator mediator
            )
        {
            _logger = logger;
            _scope = scope;
            _mediator = mediator;

        }
        [HttpGet]
        public async Task<IActionResult> LoginTest()
        {
            AddMemberHandler.Query data = new AddMemberHandler.Query();
            //data.login = new Domain.Members.LoginModel();

            var rt = await _mediator.Send(data);
            return Ok(rt);
        }

        [HttpPost]
        public async Task<AddMemberHandler.Result> AddMember([FromBody] MemberInfo data)
        {
            AddMemberHandler.Query senddata = new AddMemberHandler.Query();
            senddata.data = data;
            var rt = await _mediator.Send(senddata);
            return rt;
        }

        [HttpPost("login")]
        public async Task<LoginHandler.Result> LoginMember([FromBody] LoginModel  data)
        {
            LoginHandler.Query senddata = new LoginHandler.Query();
            senddata.data = data;
            var rt = await _mediator.Send(senddata);
            return rt;
        }

    }
}
