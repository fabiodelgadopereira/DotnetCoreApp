using CadastroApp.API.Models;
using CadastroApp.API.Dto;
using System.Threading.Tasks;

namespace CadastroApp.API.Data
{

    public interface IAuthRepository
    {
         Task<UserForRegisterDto> Register(UserForRegisterDto user);
         Task<bool> Login(string username, string password);
         Task<bool> UserExists(string username);
    }

}