using Microsoft.AspNetCore.Mvc;
using QRA.Entities.Entities;
using QRA.Persistence;

namespace QRA.API.controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

       [HttpGet]
       public Tenant getData()
        {
            return _qr.Tenants.FirstOrDefault();
        }
    }
}
