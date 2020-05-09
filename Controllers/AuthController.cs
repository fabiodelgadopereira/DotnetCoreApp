using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CadastroApp.API.Data;
using CadastroApp.API.Dto;
using CadastroApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CadastroApp.API.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class AuthController : Controller {

        private readonly AuthRepository _repository;

        private readonly IConfiguration _config;

        public AuthController (IConfiguration config, AuthRepository repository) {
            this._repository = repository ??
                throw new ArgumentNullException (nameof (repository));
            _config = config;
        }

        //  public async Task<ActionResult<Cliente>> Get(int id)

        [HttpPost ("register")]
        public async Task Post ([FromBody] UserForRegisterDto value) {
            if (_repository.UserExists (value.Username).Result == false) {

                await _repository.Register (value);
            }

            /*  if (result.Succeeded)
             {
                 return CreatedAtRoute("GetUser", 
                     new { controller = "Users", id = userToCreate.Id }, userToReturn);
             }

             return BadRequest(result.Errors);*/

        }

        [HttpPost ("login")]
        public async Task<IActionResult> Post ([FromBody] UserForLoginDto value) {

            var result = await _repository.Login (value.Username, value.Password);

            var userToCreate = new Credential {
                Username = value.Username,
                    Id = 1
            };
            if (result == true) {
                return Ok (new {
                    token = GenerateJwtToken (userToCreate)
                });
            }

            return Unauthorized ();

        }

        private async Task<string> GenerateJwtToken (Credential user) {
            var claims = new List<Claim> {
                new Claim (ClaimTypes.NameIdentifier, user.Id.ToString ()),
                new Claim (ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey (Encoding.UTF8
                .GetBytes (_config.GetSection ("SecurityKey").Value));

            var creds = new SigningCredentials (key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity (claims),
                Expires = DateTime.Now.AddDays (1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler ();

            var token = tokenHandler.CreateToken (tokenDescriptor);

            return tokenHandler.WriteToken (token);
        }
    }
}