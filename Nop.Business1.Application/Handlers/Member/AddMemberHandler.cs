using Autofac;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using Nop.Common.Helpers;
using Nop.Common.Models;
using Nop.Domain.Members;
using Nop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nop.Business1.Application.Handlers.Member
{
    public class AddMemberHandler
    { 
        public class Query : IRequest<Result>
        {
            public LoginModel login { get; set; }
        }

        public class Result : BaseDto
        {
        }

        public class Validator: AbstractValidator<LoginModel>
        {
            public Validator()
            {
                CascadeMode = CascadeMode.StopOnFirstFailure;
             

                RuleFor(x => x.UserId).NotEmpty().WithMessage("아이디를 입력해주세요.");
                RuleFor(x => x.PassWord).NotEmpty().WithMessage("비밀번호를 입력해주세요.");
            }
            
            public override ValidationResult Validate(ValidationContext<LoginModel> context)
            {
                return context.InstanceToValidate == null
                ? new ValidationResult(new[] { new ValidationFailure(nameof(context), "Object cannot be null") })
                : base.Validate(context);
            }
        }

        public class Handler : IRequestHandler<Query, Result>
        {
            private readonly ILogger<AddMemberHandler> _logger;
            private readonly ILifetimeScope _scope;
            private readonly NopContext _nop;
            private readonly IMediator _mediator;


            public Handler(ILogger<AddMemberHandler> logger
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
                    Validator valid = new Validator();
                    ValidationResult results = valid.Validate(req.login);
                    
                    if(!results.IsValid)
                    {
                        dt.HasAlert = true;
                        foreach(var p in results.Errors)
                        {
                           dt.Message += $"{p.ErrorMessage}|";
                        }
                        

                    }

                     
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
