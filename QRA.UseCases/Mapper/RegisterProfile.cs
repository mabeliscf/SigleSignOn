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
    public class RegisterProfile : Profile
    {
        public RegisterProfile()
        {
            CreateMap<RegisterDTO, Register>();
            CreateMap<RegisterDTO, Tenant>();
            CreateMap<RegisterDTO, TenantsLogin>()
               .ForMember(model => model.Administrator, action => action.MapFrom(c => c.IsAdmin));


            CreateMap<RegisterUserDTO, Tenant>();


            CreateMap<RegisterUserDTO, TenantsLogin>()
               .ForMember(model => model.Administrator, action => action.MapFrom(c => c.IsAdmin))
               .ForMember(model => model.TenantFather, action => action.MapFrom(c => c.IdTenantFather));






        }



    }
}
