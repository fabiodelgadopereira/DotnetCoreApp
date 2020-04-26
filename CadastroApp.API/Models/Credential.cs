using System.ComponentModel.DataAnnotations.Schema;

namespace CadastroApp.API.Models {
    [Table ("Usuarios")]
    public class Credential {
        public string tenantId { get; set; }
        public string clientId { get; set; }
        public string secretId { get; set; }
    }
}