using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BuildingService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildingService.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signIn;
        private readonly IPasswordHasher<AppUser> hasher;


        public AuthController(UserManager<AppUser> _userManager, SignInManager<AppUser> _signIn, IPasswordHasher<AppUser> _hasher)
        {
            userManager = _userManager;
            signIn = _signIn;
            hasher = _hasher;
        }




        [HttpPost]
        [Route("api/auth/login")]
        public async Task<IActionResult> Login(LoginModel model)
        {

            var result = await signIn.PasswordSignInAsync(model.UserName, model.Password, false, false);
            if (result.Succeeded)
            {
                return Ok("Successful login");
            }


            return BadRequest("Failed to login");
        }

        [HttpPost]
        [Route("api/auth/signup")]
        public async Task<IActionResult> Register(Credential model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    CreatedAt = model.Created,
                    IsSuperUser = model.IsSuperUser
                };

                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return Ok($"Successful request, User: {user.UserName} has been created");
                }
            }
            else
            {
                return BadRequest("Failed");
            }

            return Ok();
        }


        [HttpPost]
        [Route("api/auth/token")]
        public async Task<IActionResult> GenerateToken(Credential model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            if(user != null)
            {
                if(hasher.VerifyHashedPassword(user,user.PasswordHash,model.Password)== PasswordVerificationResult.Success)
                {
                    var myClaims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                         new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName+user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("jqdkjasdkgh1e7621863e812equdsqgv"));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        issuer: "https://localhost:44378/",
                        audience: "https://localhost:44378/",
                        claims: myClaims,
                        expires: DateTime.UtcNow.AddDays(1),
                        signingCredentials: creds
                        );

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });
                }
            }

            return BadRequest("Failed to create token");

        }
    }
}
