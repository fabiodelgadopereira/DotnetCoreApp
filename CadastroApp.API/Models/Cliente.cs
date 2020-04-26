using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cadastro.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cidade { get; set; }
        public string Email { get; set; }
        public string Sexo { get; set; }
    }
}
