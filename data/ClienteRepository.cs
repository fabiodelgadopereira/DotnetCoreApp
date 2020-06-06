using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CadastroApp.API.Helpers;
using CadastroApp.API.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace CadastroApp.API.Data {
    public class ClienteRepository {
        private readonly string _connectionString;

        public ClienteRepository (IConfiguration configuration) {
            _connectionString = configuration.GetConnectionString ("defaultConnection");
        }

        public async Task<JObject> GetAll (ClienteParams param) {
            int PageIndex = param.PageIndex;
            int PageSize = param.PageSize;

            using (SqlConnection sql = new SqlConnection (_connectionString)) {
                using (SqlCommand cmd = new SqlCommand ("sp_GetClientesPageWise", sql)) {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add (new SqlParameter ("@PageIndex", PageIndex));
                    cmd.Parameters.Add (new SqlParameter ("@PageSize", PageSize));
                    cmd.Parameters.Add (new SqlParameter ("@RecordCount", SqlDbType.Int) { Direction = ParameterDirection.Output, Value = -1 });
                    var response = new List<Cliente> ();
                    await sql.OpenAsync ();

                    using (var reader = await cmd.ExecuteReaderAsync ()) {
                        while (await reader.ReadAsync ()) {
                            response.Add (MapToValue (reader));
                        }
                    }
                    //pega valor da output da consulta SQL
                    //esse valor é o count de linhas totais da tabela sendo paginada
                    cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;

                    //retorna count + dados
                    JObject getAll = JObject.FromObject (new {
                        count = (int) cmd.Parameters["@RecordCount"].Value,
                            data = response
                    });
                    return getAll;
                }
            }
        }

        private Cliente MapToValue (SqlDataReader reader) {
            return new Cliente () {
                Id = (int) reader["Id"],
                    Nome = reader["Nome"].ToString (),
                    Cidade = reader["Cidade"].ToString (),
                    Email = reader["Email"].ToString (),
                    Sexo = reader["Sexo"].ToString ()
            };
        }

        public async Task<Cliente> GetById (int Id) {
            using (SqlConnection sql = new SqlConnection (_connectionString)) {
                using (SqlCommand cmd = new SqlCommand ("sp_Clientes_GetValueById", sql)) {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add (new SqlParameter ("@Id", Id));
                    Cliente response = null;
                    await sql.OpenAsync ();

                    using (var reader = await cmd.ExecuteReaderAsync ()) {
                        while (await reader.ReadAsync ()) {
                            response = MapToValue (reader);
                        }
                    }

                    return response;
                }
            }
        }

        public async Task Insert (Cliente value) {
            using (SqlConnection sql = new SqlConnection (_connectionString)) {
                using (SqlCommand cmd = new SqlCommand ("sp_Clientes_InsertValue", sql)) {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add (new SqlParameter ("@Nome", value.Nome));
                    cmd.Parameters.Add (new SqlParameter ("@Cidade", value.Cidade));
                    cmd.Parameters.Add (new SqlParameter ("@Email", value.Email));
                    cmd.Parameters.Add (new SqlParameter ("@Sexo", value.Sexo));
                    await sql.OpenAsync ();
                    await cmd.ExecuteNonQueryAsync ();
                    return;
                }
            }
        }
                public async Task Update (Cliente value) {
            using (SqlConnection sql = new SqlConnection (_connectionString)) {
                using (SqlCommand cmd = new SqlCommand ("sp_Clientes_UpdateValue", sql)) {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add (new SqlParameter ("@Id", value.Id));
                    cmd.Parameters.Add (new SqlParameter ("@Nome", value.Nome));
                    cmd.Parameters.Add (new SqlParameter ("@Cidade", value.Cidade));
                    cmd.Parameters.Add (new SqlParameter ("@Email", value.Email));
                    cmd.Parameters.Add (new SqlParameter ("@Sexo", value.Sexo));
                    await sql.OpenAsync ();
                    await cmd.ExecuteNonQueryAsync ();
                    return;
                }
            }
        }

        public async Task DeleteById (int Id) {
            using (SqlConnection sql = new SqlConnection (_connectionString)) {
                using (SqlCommand cmd = new SqlCommand ("sp_Clientes_DeleteValue", sql)) {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add (new SqlParameter ("@Id", Id));
                    await sql.OpenAsync ();
                    await cmd.ExecuteNonQueryAsync ();
                    return;
                }
            }
        }
    }
}