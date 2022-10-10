using AutoMapper;
using QRA.Entities.Entities;
using QRA.UseCases.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRA.UseCases.Mapper
{
    public  class RolesProfile : Profile
    {

        public RolesProfile()
        {
            CreateMap<RolesDTO, Role>();

        }
    }
}
