using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using DatingApp.Dtos.User;
using DatingApp.Models;
using DatingApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.Controllers
{

    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        public AuthController(IUserRepository userRepository, IConfiguration config)
        {
            _config = config;
            _userRepository = userRepository;

        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto user)
        {
            // if(!ModelState.IsValid) return BadRequest(ModelState); // NOT NEEDED 'Cause ApiController
            if (await _userRepository.UserExists(user.UserName)) return BadRequest("user already esists");
            await _userRepository.Register(new User
            {
                Username = user.UserName
            }, user.Password);
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto user)
        {
            var user_to_login = await _userRepository.Login(user.Username, user.Password);
            if (user_to_login == null) return Unauthorized();

            var claims = new[]{
                new Claim(ClaimTypes.NameIdentifier , user_to_login.Id.ToString()),
                new Claim(ClaimTypes.Name , user_to_login.Username)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var token_descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature)
            };

            var token_handler = new JwtSecurityTokenHandler();
            var token = token_handler.CreateToken(token_descriptor);

            return Ok(new
            {
                token = token_handler.WriteToken(token)
            });
        }
    }
}