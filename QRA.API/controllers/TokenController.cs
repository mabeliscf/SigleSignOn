using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QRA.JWT.interfaces;
using QRA.UseCases.DTOs;

namespace QRA.API.controllers
{
    
    [Route("api/login")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public readonly IToken itoken;
        public TokenController(IToken token)
        {
            itoken = token;
        }
        /// <summary>
        /// given a user and password validate user login
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetLogin(LoginDTO dto)
        {
            //delete 
            dto.UserName = "rcorniel";
            dto.Password = "123456";



            GlobalResponse response= itoken.validateUser(dto);

            if (response.responseNumber == 0)
            {
                return BadRequest(response.response);
                     
            }
            return Ok(response.response);

        }

    }
}
