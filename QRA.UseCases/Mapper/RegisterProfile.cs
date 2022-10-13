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

            CreateMap<RegisterUserDTO, OktaUserGroup>()
             .ForMember(model => model.groupIds, action => action.MapFrom(c => c.Group));


            CreateMap<RegisterUserDTO, Entities.oktaModels.Profile>()
             .ForMember(model => model.firstName, action => action.MapFrom(c => c.FirstName))
             .ForMember(model => model.lastName, action => action.MapFrom(c => c.Lastname))
             .ForMember(model => model.email, action => action.MapFrom(c => c.Email))
             .ForMember(model => model.login, action => action.MapFrom(c => c.Username))
             .ForMember(model => model.mobilePhone, action => action.MapFrom(c => c.Phone));

            CreateMap<RegisterUserDTO, Credentials>();

            CreateMap<RegisterUserDTO, Password>()
           .ForMember(model => model.value, action => action.MapFrom(c => c.Password));



            CreateMap<RegisterDTO, OktaUser>();
          
            CreateMap<RegisterDTO, Entities.oktaModels.Profile>()
             .ForMember(model => model.firstName, action => action.MapFrom(c => c.FirstName))
             .ForMember(model => model.lastName, action => action.MapFrom(c => c.Lastname))
             .ForMember(model => model.email, action => action.MapFrom(c => c.Email))
             .ForMember(model => model.login, action => action.MapFrom(c => c.Username))
             .ForMember(model => model.mobilePhone, action => action.MapFrom(c => c.Phone));

            CreateMap<RegisterDTO, Credentials>();

            CreateMap<RegisterDTO, Password>()
           .ForMember(model => model.value, action => action.MapFrom(c => c.Password));



            CreateMap<RegisterUserDTO, oktaGroup>();

            CreateMap<RegisterUserDTO, ProfileGroup>()
             .ForMember(model => model.name, action => action.MapFrom(c => c.Group))
             .ForMember(model => model.description, action => action.MapFrom(c => c.Group));
            

        }



    }
}
