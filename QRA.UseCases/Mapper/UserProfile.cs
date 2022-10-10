using AutoMapper;
using QRA.Entities.Entities;
using QRA.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRA.UseCases.Mapper
{
    public  class UserProfile : Profile
    {

        public UserProfile()
        {
            CreateMap<Tenant, UserLogged>()
                .ForMember(model => model.Id, action => action.MapFrom(c => c.IdTenant));

            CreateMap<Tenant, AdminInfo>()
                .ForMember(model => model.Id, action => action.MapFrom(c => c.IdTenant));

            CreateMap<Tenant,TenantInfo>()
                .ForMember(model => model.Id, action => action.MapFrom(c => c.IdTenant));

            CreateMap<Tenant, UserInfo>()
               .ForMember(model => model.Id, action => action.MapFrom(c => c.IdTenant));


        }
    }
}
