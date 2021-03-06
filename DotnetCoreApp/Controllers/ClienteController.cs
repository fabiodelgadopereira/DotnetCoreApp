using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroApp.API.Data;
using CadastroApp.API.Helpers;
using CadastroApp.API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace cadastro.Controllers {
    [Route ("api/[controller]")]
    [Authorize ()]
    [ApiController]
    public class ClienteController : Controller {
        private readonly ClienteRepository _repository;

        public ClienteController (ClienteRepository repository) {
            this._repository = repository ??
                throw new ArgumentNullException (nameof (repository));
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Object>>> Get ([FromQuery] ClienteParams param) {

             return  await _repository.GetAll (param);
  
        }

        // GET api/values/5
        [HttpGet ("{id}")]
        public async Task<ActionResult<Cliente>> Get (int id) {
            var response = await _repository.GetById (id);
            if (response == null) { return NotFound (); }
            return response;
        }

        // POST api/values
        [HttpPost]
        public async Task Post ([FromBody] Cliente value) {
            await _repository.Insert (value);
        }

        // PUT api/values/5
        [HttpPut]
        public async Task  Put ( [FromBody] Cliente value) {
            await _repository.Update(value); 
         }

        // DELETE api/values/5
        [HttpDelete ("{id}")]
        public async Task Delete (int id) {
            await _repository.DeleteById (id);
        }
    }
}