using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SMRA2023.Entities;
using SMRA2023.Interfaces;

namespace SMRA2023.Models
{
    public class UsuarioModel : IUsuarioModel
    {
        private readonly IConfiguration _configuration;
        public UsuarioModel(IConfiguration configuration){ 
            _configuration = configuration;
}

        public UsuarioEntities? validarUsuario(UsuarioEntities usuario) {

            using(var connection= new MySqlConnection(_configuration.GetConnectionString("defaultconnection"))){
                return connection.Query<UsuarioEntities>("user_Validation",
                    new {usuario.email, usuario.passwordU},
                    commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public UsuarioEntities? RegisterUser(UsuarioEntities usuario)
        {

            using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
            {
                 connection.Execute("RegisterUser",
                    new { usuario.userName, usuario.lastName, usuario.Id,usuario.email, usuario.cellphone, usuario.statusU,usuario.passwordU, usuario.idRol},
                    commandType: System.Data.CommandType.StoredProcedure);

                return usuario ;
                
            }
        }

        public UsuarioEntities? UpdateUser(UsuarioEntities usuario)
        {

            using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
            {
                connection.Execute("RegisterUser",
                   new { usuario.userName, usuario.lastName, usuario.Id, usuario.email, usuario.cellphone, usuario.statusU, usuario.passwordU, usuario.idRol },
                   commandType: System.Data.CommandType.StoredProcedure);

                return usuario;

            }
        }

    }
}

