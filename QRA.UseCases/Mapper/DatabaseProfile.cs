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
    public class DatabaseProfile : Profile
    {

        public DatabaseProfile()
        {
            CreateMap<DatabaseDTO, Db>();

        }
    }
}
