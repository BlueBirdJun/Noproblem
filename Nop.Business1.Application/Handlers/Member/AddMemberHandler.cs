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
using Nop.Domain.Models;
using Nop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Database = Nop.Repository.NopModels;
using Domain = Nop.Domain;


namespace Nop.Business1.Application.Handlers.Member
{
    public class AddMemberHandler
    { 
        public class Query : IRequest<Result>
        {
            public MemberInfo data { get; set; }
        }

        public class Result : BaseDtoGeneric<Domain.Members.MemberInfo,Query>
        {
        }

        public class Validator: AbstractValidator<MemberInfo>
        {
            public Validator()
            {
                //CascadeMode = CascadeMode.StopOnFirstFailure; 
                RuleFor(x => x.UserId).NotEmpty().WithMessage("아이디를 입력해주세요.");
                RuleFor(x => x.Password).NotEmpty().WithMessage("비밀번호를 입력해주세요.");
                RuleFor(x => x.Name).NotEmpty().WithMessage("이름을 입력해주세요.");
                RuleFor(x => x.Name).MaximumLength(10).WithMessage("이름은 10자 까지입니다.");
                RuleFor(x => x.Password).MaximumLength(20).WithMessage("비밀번호는 20자 까지입니다.");
                RuleFor(x => x.UserId).MaximumLength(20).WithMessage("아이디는 20자 까지입니다.");
                //RuleFor(x => x.Password).NotEmpty().WithMessage("비밀번호를 입력해주세요.");

            }
            
            public override ValidationResult Validate(ValidationContext<MemberInfo> context)
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
            private readonly IMapper _mapper;

            public Handler(ILogger<AddMemberHandler> logger
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
                    if(!results.IsValid)
                    {
                        dt.Success = false;
                        dt.HasAlert = true;
                        foreach(var p in results.Errors)
                        {
                            dt.ListMessage.Add($"{p.PropertyName}:{p.ErrorMessage}");                           
                        }
                        return dt;
                    }
                      
                    var mem = _mapper.Map<Database.Member>(req.data);
                    if( _nop.Member.AsNoTracking().Where(p=>p.UserId == mem.UserId).Any())
                    {
                        dt.Success = false;
                        dt.Message = "중복된 아이디입니다.";
                        return dt;
                    }
                    _nop.Member.Add(mem);
                    await _nop.SaveChangesAsync();
                    dt.Success = true;
                    dt.OutPutValue = _mapper.Map<Domain.Members.MemberInfo>(mem);
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
