using AutoMapper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dest = Nop.Repository.NopModels;
using Source = Nop.Domain.Commons;

namespace Nop.Api.Business1
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        { 
            //CreateMap<Source.CommonCodes, Dest.CommonCodes>();
            CreateMap<Dest.CommonCodes, Source.CommonCodes>();
        }
    }
}
