using Autofac;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nop.Common.Helpers;
using Nop.Common.Models;
using Nop.Domain.Members;
using Nop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nop.Domain.Models;
using System.Threading;
using System.Threading.Tasks;
using Database = Nop.Repository.NopModels;
using Domain = Nop.Domain;

namespace Nop.Business1.Application.Handlers.Member
{
    public class LoginHandler
    {
        public class Query : IRequest<Result>
        {
            public LoginModel data { get; set; }
        }

        public class Result : BaseDtoGeneric<Domain.Members.MemberInfo, Query>
        {
        }
        public class Validator : AbstractValidator<LoginModel>
        {
            public Validator()
            {
                //CascadeMode = CascadeMode.StopOnFirstFailure; 
                RuleFor(x => x.UserId).NotEmpty().WithMessage("아이디를 입력해주세요.");
                RuleFor(x => x.Password).NotEmpty().WithMessage("비밀번호를 입력해주세요.");
                
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
            private readonly ILogger<LoginHandler> _logger;
            private readonly ILifetimeScope _scope;
            private readonly NopContext _nop;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;

            public Handler(ILogger<LoginHandler> logger
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
                    Validator valid = new Validator();
                    ValidationResult results = valid.Validate(req.data);
                    if (!results.IsValid)
                    {
                        dt.Success = false;
                        dt.HasAlert = true;
                        foreach (var p in results.Errors)
                        {
                            dt.ListMessage.Add($"{p.PropertyName}:{p.ErrorMessage}");
                        }
                        return dt;
                    }                    
                    if (!_nop.Member.AsNoTracking().Where(p => p.UserId == req.data.UserId).Any())
                    {
                        dt.Success = false;
                        dt.HasAlert = true;
                        dt.Message = "가입되지 않은 계정입니다.";
                        return dt;
                    }

                    var userinfo =await _nop.Member.AsNoTracking().Where(p => p.UserId == req.data.UserId).SingleOrDefaultAsync();
                    if(userinfo.Password != req.data.Password)
                    {
                        dt.Success = false;
                        dt.HasAlert = true;
                        dt.Message = "비밀번호가 잘못됬습니다.";
                        return dt;
                    }
                    
                    dt.Success = true;
                    dt.OutPutValue = _mapper.Map<Domain.Members.MemberInfo>(userinfo);
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
