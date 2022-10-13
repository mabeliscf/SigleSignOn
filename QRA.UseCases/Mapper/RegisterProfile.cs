using AutoMapper;
using QRA.Entities.Entities;
using QRA.Entities.Models;
using QRA.Entities.oktaModels;
using QRA.UseCases.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRA.UseCases.Mapper
{
    public class RegisterProfile : AutoMapper.Profile
    {
        public RegisterProfile()
        {
            CreateMap<RegisterDTO, Register>();
            CreateMap<RegisterDTO, Tenant>()
                 .ForMember(model => model.Fullname, action => action.MapFrom(c => c.FirstName +" "+ c.Lastname));
            CreateMap<RegisterDTO, TenantsLogin>()
               .ForMember(model => model.Administrator, action => action.MapFrom(c => c.IsAdmin));
              



            CreateMap<RegisterUserDTO, Tenant>();


            CreateMap<RegisterUserDTO, TenantsLogin>()
               .ForMember(model => model.Administrator, action => action.MapFrom(c => c.IsAdmin))
               .ForMember(model => model.TenantFather, action => action.MapFrom(c => c.IdTenantFather));


            CreateMap<RegisterDTO, OktaUser>();
             //.ForMember(model => model.profile.FirstName, action => action.MapFrom(c => c.FirstName))
             //.ForMember(model => model.profile.LastName, action => action.MapFrom(c => c.Lastname))
             //.ForMember(model => model.profile.Email, action => action.MapFrom(c => c.Email))
             //.ForMember(model => model.profile.Login, action => action.MapFrom(c => c.Username))
             //.ForMember(model => model.profile.MobilePhone, action => action.MapFrom(c => c.Phone))
             //.ForMember(model => model.credentials.password.value, action => action.MapFrom(c => c.Password));

            CreateMap<RegisterDTO, Entities.oktaModels.Profile>()
             .ForMember(model => model.FirstName, action => action.MapFrom(c => c.FirstName))
             .ForMember(model => model.LastName, action => action.MapFrom(c => c.Lastname))
             .ForMember(model => model.Email, action => action.MapFrom(c => c.Email))
             .ForMember(model => model.Login, action => action.MapFrom(c => c.Username))
             .ForMember(model => model.MobilePhone, action => action.MapFrom(c => c.Phone));
            CreateMap<RegisterDTO, Credentials>();
            CreateMap<RegisterDTO, Password>()
           .ForMember(model => model.value, action => action.MapFrom(c => c.Password));

        }



    }
}
