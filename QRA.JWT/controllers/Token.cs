using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QRA.Entities.contracts;
using QRA.JWT.interfaces;
using QRA.UseCases.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QRA.JWT.controllers
{
    public class Token : IToken
    {
        public IConfiguration iconfiguration;
        public readonly ITenantService itenant;

        public Token(ITenantService tenant, IConfiguration configuration)
        {
            itenant = tenant;
            iconfiguration = configuration;
        }

        /// <summary>
        /// validate user credentials across database and generates a valid token
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public GlobalResponse validateUser(LoginDTO login)
        {
            GlobalResponse response = new GlobalResponse();
            if (login == null && login.UserName == null && login.Password == null)
            {
                response.response = "No user info";
                response.responseNumber = 0;
                return response;
            }


            //validate user exist en DB 
            TeantLoginDTO userDB = (TeantLoginDTO)itenant.GetUser(login.UserName, login.Password, 1);

           
            if (userDB == null)
            {
                response.response = "No valid Credentials";
                response.responseNumber = 0;
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, iconfiguration["JWT:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("UserId", userDB.IdTenant.ToString()),
                new Claim("DisplayName", userDB.Fullname),
                new Claim("UserName", userDB.Username),
                new Claim("Email", userDB.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(iconfiguration["JWT:key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: iconfiguration["JWT.Issuer"],
                audience: iconfiguration["JWT:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn);

            //return token
            response.response = new JwtSecurityTokenHandler().WriteToken(token);
            response.responseNumber = 1;
            return response;
        }
    }
}
