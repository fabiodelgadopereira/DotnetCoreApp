using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using cadastro.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace cadastro.Controllers {
    [Route ("api/[controller]")]
    public class TokenController : Controller {
        private readonly IConfiguration _configuration;
        public TokenController (IConfiguration configuration) {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult RequestToken ([FromBody] Usuario request) {
            if (request.Nome == "user" && request.Senha == "qwerty") {
                var claims = new [] {
                new Claim (ClaimTypes.Name, request.Nome)
                };

                //recebe uma instancia da classe SymmetricSecurityKey 
                //armazenando a chave de criptografia usada na criação do token
                var key = new SymmetricSecurityKey (
                    Encoding.UTF8.GetBytes (_configuration["SecurityKey"]));

                //recebe um objeto do tipo SigninCredentials contendo a chave de 
                //criptografia e o algoritmo de segurança empregados na geração 
                // de assinaturas digitais para tokens
                var creds = new SigningCredentials (key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken (
                    issuer: "aplicacao",
                    audience: "canal",
                    claims : claims,
                    expires : DateTime.Now.AddMinutes (30),
                    signingCredentials : creds);

                return Ok (new {
                    token = new JwtSecurityTokenHandler ().WriteToken (token)
                });
            }
            return BadRequest ("Credenciais inválidas...");
        }
    }
}