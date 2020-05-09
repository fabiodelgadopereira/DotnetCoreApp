using System.ComponentModel.DataAnnotations.Schema;

namespace CadastroApp.API.Models {
    [Table ("Usuarios")]
    public class Credential {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}