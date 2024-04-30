using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using NZWalks.API.Repositories;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NZWalks.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var IdentityUser = new IdentityUser
            {
                UserName = registerRequestDTO.Username,
                Email = registerRequestDTO.Username
            };

            var identityResult = await userManager.CreateAsync(IdentityUser, registerRequestDTO.Password);
            if (identityResult.Succeeded)
            {
                //Add Roles to this user
                if(registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(IdentityUser, registerRequestDTO.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered! Please login.");
                    }
                }
                
            }
            return BadRequest("Something went wrong");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] AuthRequestDto authRequestDto)
        {
            var user = await userManager.FindByEmailAsync(authRequestDto.Username);
            if(user != null)
            {
                var checkPassword = await userManager.CheckPasswordAsync(user, authRequestDto.Password);
                if (checkPassword)
                {
                    //Get roles for this user
                    var roles = await userManager.GetRolesAsync(user);
                    //create token
                    if(roles != null)
                    {
                        var jwtToken = tokenRepository.createJWTToken(user, roles.ToList());
                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };
                        return Ok(response);
                    }                                     
                } else
                {
                    return BadRequest("Password is incorrect");
                }
            }
            return BadRequest("Username not found");
        }
    }
}

