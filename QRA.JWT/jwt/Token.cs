using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QRA.Entities.Entities;
using QRA.Entities.Models;
using QRA.JWT.contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QRA.JWT.jwt
{
    public class Token : IToken
    {
        public IConfiguration iconfiguration;

        public Token( IConfiguration configuration)
        {
            iconfiguration = configuration;
        }

        /// <summary>
        /// validate user credentials across database and generates a valid token
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public GlobalResponse validateUser(Tenant userDB)
        {
            GlobalResponse response = new GlobalResponse();
          
            
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
                expires: DateTime.UtcNow.AddDays(3),
                signingCredentials: signIn);

            //return token
            //TODO: return expire time and token 
            response.response = new JwtSecurityTokenHandler().WriteToken(token);
            response.responseNumber = 1;
            return response;
        }
    
    
        //public GlobalResponse validateUSerOkta()
        //{
        //    DateTime expires;
        //    var idToken = await context.GetTokenAsync("id_token");
        //    var expiresToken = await context.GetTokenAsync("expires_at");
        //    var accessToken = await context.GetTokenAsync("access_token");
        //    var refreshToken = await context.GetTokenAsync("refresh_token");

        //    if (refreshToken != null && (DateTime.TryParse(expiresToken, out expires)))
        //    {
        //        if (expires < DateTime.Now) //Token is expired, let's refresh
        //        {
        //            var client = new HttpClient();
        //            var tokenResult = client.RequestRefreshTokenAsync(new RefreshTokenRequest
        //            {
        //                Address = "https://yourOrg.okta.com/oauth2/v1/token",
        //                ClientId = "---yourclientid---",
        //                ClientSecret = "---yourclientsecret---",
        //                RefreshToken = refreshToken
        //            }).Result;


        //            if (!tokenResult.IsError)
        //            {
        //                var oldIdToken = idToken;
        //                var newAccessToken = tokenResult.AccessToken;
        //                var newRefreshToken = tokenResult.RefreshToken;
        //                idToken = tokenResult.IdentityToken;

        //                var tokens = new List<AuthenticationToken>
        //        {
        //            new AuthenticationToken {Name = OpenIdConnectParameterNames.IdToken, Value = tokenResult.IdentityToken},
        //            new AuthenticationToken
        //            {
        //                Name = OpenIdConnectParameterNames.AccessToken,
        //                Value = newAccessToken
        //            },
        //            new AuthenticationToken
        //            {
        //                Name = OpenIdConnectParameterNames.RefreshToken,
        //                Value = newRefreshToken
        //            }
        //        };

        //                var expiresAt = DateTime.Now + TimeSpan.FromSeconds(tokenResult.ExpiresIn);
        //                tokens.Add(new AuthenticationToken
        //                {
        //                    Name = "expires_at",
        //                    Value = expiresAt.ToString("o", CultureInfo.InvariantCulture)
        //                });

        //                var result = await context.AuthenticateAsync();
        //                result.Properties.StoreTokens(tokens);

        //                await context.SignInAsync(result.Principal, result.Properties);
        //            }
        //        }
        //    }

        //    await next.Invoke();
        //}
    
    }
}
