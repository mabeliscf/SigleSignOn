using Microsoft.AspNetCore.Identity;
using System;

using Microsoft.Extensions.Configuration;

namespace QRA.JWT.Auth
{
    public class Actions
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public Actions(    UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;

        }

        //public async Task<IActionResult> Login(Loginmodel model)
        //{
        //    var user = await _userManager.FindByNameAsync(model.us)
        //}
            

    }
}
