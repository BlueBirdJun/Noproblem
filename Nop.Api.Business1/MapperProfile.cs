using AutoMapper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dest = Nop.Repository.NopModels;
using Source = Nop.Domain;

namespace Nop.Api.Business1
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        { 
            //CreateMap<Source.CommonCodes, Dest.CommonCodes>();
            CreateMap<Dest.CommonCodes, Source.Commons.CommonCodes>().ForSourceMember(x => x.CreateDate, x => x.DoNotValidate())
                .ForSourceMember(x => x.UpdateDate, x => x.DoNotValidate());

            CreateMap< Dest.Member, Source.Members.MemberInfo>().ForSourceMember(x=>x.CreateDate,x=>x.DoNotValidate())
                .ForSourceMember(x => x.UpdateDate, x => x.DoNotValidate());
            CreateMap< Source.Members.MemberInfo, Dest.Member>().ForSourceMember(x => x.CreateDate, x => x.DoNotValidate())
                .ForSourceMember(x => x.UpdateDate, x => x.DoNotValidate());

            //CreateMap<Dest.Member, Source.Members.LoginModel>().ForSourceMember(x => x.CreateDate, x => x.DoNotValidate())
            //   .ForSourceMember(x => x.UpdateDate, x => x.DoNotValidate()); 
        }
    }
}
