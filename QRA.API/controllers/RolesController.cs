using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using QRA.Entities.Entities;
using QRA.UseCases.contracts;
using AutoMapper;
using QRA.UseCases.DTOs;

namespace QRA.API.controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RolesController : ControllerBase
    {
        public readonly IRolesService iroles;
        public readonly IGetRolesQuery irolesQuery;
        public readonly IMapper imapper;


        public RolesController( IMapper mapper, IRolesService roles, IGetRolesQuery rolesQuery)
        {
            imapper = mapper;
            iroles = roles;
            irolesQuery = rolesQuery;
        }
      
        //get
        [HttpGet("listRole")]
        public List<Role> GetRoles()
        {
           return irolesQuery.GetRoles();
        }
        [HttpGet("getRole")]
        public Role GetRolesbyId(long id)
        {
            return irolesQuery.GetRolesbyID(id);
        }

        //create
        [HttpPost("createRole")]
        public long CreateRoles(RolesDTO roles)
        {
            Role role = imapper.Map<Role>(roles);
            long idrole=  iroles.create(role);

            return idrole;
        }


        //update 
        [HttpPost("updateRole")]
        public bool UpdateRoles(RolesDTO roles)
        {
            Role role = imapper.Map<Role>(roles);
            return iroles.update(role);
        }


        //delete 
        [HttpDelete("deleteRole")]
        public bool DeleteRoles(RolesDTO roles)
        {
            Role role = imapper.Map<Role>(roles);
            return iroles.delete(role);
        }



    }
}
