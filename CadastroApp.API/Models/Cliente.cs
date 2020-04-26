using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CadastroApp.API.Models
{
    public class Cliente
    {   
        [RegularExpression(@"([0-9]+)", ErrorMessage = "Must be a Number.")]     
        public int Id { get; set; }

        [StringLength(80, MinimumLength = 3, ErrorMessage = "Você deve especificar um nome entre 3 e 80 caracteres")]
        public string Nome { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Você deve especificar uma cidade entre 3 e 50 caracteres")]
        public string Cidade { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(80, MinimumLength = 3, ErrorMessage = "Você deve especificar um e-mail com menos de 80 caracteres")]
        public string Email { get; set; }

        [StringLength(10, MinimumLength = 3, ErrorMessage = "Você deve especificar um nome entre 3 e 10 caracteres")]
        public string Sexo { get; set; }
    }
}
