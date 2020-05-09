using System.Data.SqlClient;
using System.Threading.Tasks;
using CadastroApp.API.Models;
using CadastroApp.API.Dto;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;




namespace CadastroApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
   
   private readonly string _connectionString;

        public AuthRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("defaultConnection");
        }

       public async Task<bool> Login(string username, string password)
        {
             byte[] passwordHash, passwordSalt;
            

             using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_User_Login", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Username ", username));

                  await sql.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {       
                          passwordHash= (byte[])reader["PasswordHash"];
                          passwordSalt= (byte[])reader["PasswordSait"];
                            return VerifyPasswordHash(password,  passwordHash,  passwordSalt);
                        }
                    }
                    return false;
                }
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
                return true;
            }
        }

        public async Task<UserForRegisterDto> Register(UserForRegisterDto cred)
        {
            

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(cred.Password, out passwordHash, out passwordSalt);

             using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_User_Register", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Username ", cred.Username));
                    cmd.Parameters.Add(new SqlParameter("@PasswordHash", passwordHash));
                    cmd.Parameters.Add(new SqlParameter("@PasswordSait", passwordSalt));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return cred;
                }
            }
            }



       

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            } 
        }

        public async Task<bool> UserExists(string username)
        {
            
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_User_Exists", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Username", username));
                    await sql.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {       
                           if ((int)reader["result"]==0) return false;
                        }
                    }
                    return true;
                }
            }
        }



    }
   
} 
    
