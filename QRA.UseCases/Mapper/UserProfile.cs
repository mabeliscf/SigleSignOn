using AutoMapper;
using QRA.Entities.Entities;
using QRA.Entities.Models;
using QRA.UseCases.DTOs;
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
                .ForMember(model => model.Id, action => action.MapFrom(c => c.IdTenant))
                .ForMember(model => model.FullName, action => action.MapFrom(c =>  string.Concat(
                    c.FirstName.Split(" ", System.StringSplitOptions.None).Count() > 0 ? c.FirstName.Split(" ", System.StringSplitOptions.None)[0] : c.FirstName, 
                    c.LastName.Split(" ", System.StringSplitOptions.None).Count() > 0 ? c.LastName.Split(" ", System.StringSplitOptions.None)[0] : c.LastName))
                            );

            CreateMap<Tenant, AdminInfo>()
                .ForMember(model => model.Id, action => action.MapFrom(c => c.IdTenant));

            CreateMap<Tenant,TenantInfo>()
                .ForMember(model => model.Id, action => action.MapFrom(c => c.IdTenant));
            CreateMap<TenantsLogin, TenantInfo>()
        .ForMember(model => model.LoginType, action => action.MapFrom(c => c.LoginType))
        .ForMember(model => model.IsAdmin, action => action.MapFrom(c => c.Administrator))
        .ForMember(model => model.TenantFather, action => action.MapFrom(c => c.TenantFather))
        .ForMember(model => model.isUser, action => action.MapFrom(c => c.TenantFather != 0 ? true : false))
        .ForMember(model => model.isTenant, action => action.MapFrom(c => (c.TenantFather == 0 && c.Administrator==false ) ? true : false));





            CreateMap<Tenant, UserInfo>()
               .ForMember(model => model.Id, action => action.MapFrom(c => c.IdTenant));

            CreateMap<UserInfo, RegisterUserDTO>();



        }
    }
}
