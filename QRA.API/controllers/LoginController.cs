using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QRA.Entities.Entities;
using QRA.Persistence;
using QRA.UseCases.DTOs;

namespace QRA.API.controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        public static  QRAchallengeContext _qr;
        public LoginController(QRAchallengeContext qR)
        {
            _qr = qR;
        }

        /// <summary>
        /// test method
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("dd")]
       public Tenant getData([FromBody] LoginDTO logindto)
        {
            return _qr.Tenants.FirstOrDefault();
        }
    }
}
