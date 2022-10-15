using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QRA.Entities.Entities;
using QRA.Entities.Models;
using QRA.JWT.contracts;
using QRA.UseCases.Commands;
using QRA.UseCases.contracts;
using QRA.UseCases.DTOs;
using QRA.UseCases.Helpers;

namespace QRA.API.controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public readonly IToken itoken;
        public readonly IUserService iuser;
        public readonly IModelValidation imodelValidation;
        public readonly IMapper imapper;
        public readonly IUserQuery iuserQuery;
        public readonly IGetTenantQuery itenantQuery;
        public readonly IOktaService ioktaService;


        public UserController(IOktaService oktaService, IToken token, IUserService user, IModelValidation model, IMapper mapper, IUserQuery userQuery, IGetTenantQuery tenantQuery)
        {
            itoken = token;
            iuser = user;
            imodelValidation = model;
            imapper = mapper;
            iuserQuery = userQuery;
            itenantQuery= tenantQuery;
            ioktaService = oktaService;
        }
        /// <summary>
        /// given a user and password validate user login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// this method allow anonymous because this request do not have a token
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Login([FromBody]LoginDTO model)
        {
            //check if is a valid user 
            Tenant user = iuser.Authenticate(model.UserName, model.Password);

            //if user is valid generate token 
            if (user == null) return BadRequest ("Username or password incorrect!");
           
            //generate token
            GlobalResponse response = itoken.validateUser(user);

            if (response.responseNumber == 0) return BadRequest(response.response);    
           
            //map return 
            UserLogged userLogged = imapper.Map<UserLogged>(user);
            userLogged.Token = response.response;

            return Ok(userLogged);
        }

        [HttpGet("validateUser")]
        public IActionResult validUser(long id)
        {
            //check if is a valid user 
            Tenant user = itenantQuery.GetTenantbyId(id);

            //if user is valid generate token 
            if (user == null) return BadRequest("Username or password incorrect!");

            //generate token
            GlobalResponse response = itoken.validateUser(user);

            if (response.responseNumber == 0) return BadRequest(response.response);

            //map return 
            UserLogged userLogged = imapper.Map<UserLogged>(user);
            userLogged.Token = response.response;

            return Ok(userLogged);
        }

        /// <summary>
        /// if a new user (system new), it will go directly to register, allow to create a root user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDTO model)
        {
            //if (!imodelValidation.isFieldsValid(model))
            //    return BadRequest("Please fill form correctly!");
           // var result = ioktaService.CreateUser(model);
            if (!iuser.isNewUser(model.Email))
                return BadRequest("This user exist, please log in!");

            GlobalResponse user = iuser.CreateAdmin(model);
            return Ok(user);

        }

        /// <summary>
        /// A root can create users (tenant or users under a tenant space)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("CreateUsers")]
        public IActionResult RegisterUsers([FromBody] RegisterUserDTO model)
        {
          
            if (!imodelValidation.isFieldsValid(model))
                return BadRequest("Please fill form correctly!");

            if (!iuser.isNewUser(model.Email))
                return BadRequest("This user exist, please log in!");

            GlobalResponse response = iuser.CreateUser(model);

            if (response.responseNumber == 0)
            {
                return BadRequest(response.response);
            }

            return Ok(response.response);
        }
        /// <summary>
        /// get user with rol and db acces 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("UserID")]
        public IActionResult GetUserbyId(long id)
        {
            var result = iuserQuery.GetUserInfoByID(id);

            return Ok(result);

        }
        /// <summary>
        /// get all users from a tenant space with their roles and db access
        /// </summary>
        /// <returns></returns>
        [HttpGet("TenantUsers")]
        public IActionResult GetallUserbyTenant(long id)
        {
            var result = iuserQuery.GetUsersbyTenantID(id);

            return Ok(result);

        }
        /// <summary>
        /// as an admin it will get all users 
        /// </summary>
        /// <returns></returns>
        [HttpGet("Tenants")]
        public IActionResult GetAllTenants()
        {
            
            var result =  iuserQuery.GetAllTenants();


            return Ok(result);

        }
        [AllowAnonymous]
        [HttpGet("AdminExist")]
        public bool GetAllAdmins()
        {
            return iuserQuery.GetAllAdmins();

        }
    

        [AllowAnonymous]
        [HttpGet("gettoken")]
        public OktaToken getToken()
        {
            return ioktaService.getToken();

        }
    }
}
