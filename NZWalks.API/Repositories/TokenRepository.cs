using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using System;

namespace NZWalks.API.Repositories
{
	public class TokenRepository: ITokenRepository
	{
		private readonly IConfiguration configuration;
		public TokenRepository(IConfiguration configuration)
		{
			this.configuration = configuration;
		}
		public string createJWTToken(IdentityUser user, List<string> Roles)
		{
            //Create Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
            };
            foreach (var role in Roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
				configuration["Jwt:issuer"],
				configuration["Jwt:audience"],
				claims,
				notBefore: DateTime.Now.AddMinutes(1),
                expires: DateTime.Now.AddMinutes(2345),
				credentials);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}

