using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using Org.BouncyCastle.Math;
using SMRA2023.Entities;
using SMRA2023.Interfaces;
using System.Data;
using System.Numerics;
using MimeKit;


namespace SMRA2023.Models
{
    public class UsuarioModel : IUsuarioModel
    {
        private readonly IConfiguration _configuration;
        public UsuarioModel(IConfiguration configuration){ 
            _configuration = configuration;
        }

        public UsuarioEntities validarUsuario(UsuarioEntities usuario)
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
            {
                var result = connection.Query<UsuarioEntities>("user_Validation",
                    new { usuario.email, usuario.passwordU },
                    commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

                return result;
            }
        }

        public UsuarioEntities? email_Verification(UsuarioEntities usuario)
        {

            using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
            {
                return connection.Query<UsuarioEntities>("email_ExistenceVerification",
                    new { usuario.email },
                    commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }



        private string? email_Recovery(string p_Email)
        {

            using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
            {
                return connection.Query<string>("email_Recovery",
                    new { p_Email },
                    commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public void Email(string correo)
        {
            //aqui se consulta la contraseña
            var respuesta = email_Recovery(correo);

            //declaro las variables para llenar lo necesario del mailkit
            var ServerEmail = _configuration.GetSection("EmailConfiguration:ServerEmail").Value;
            var ServerPassword = _configuration.GetSection("EmailConfiguration:ServerPassword").Value;
            var Host = _configuration.GetSection("EmailConfiguration:Host").Value;
            var Port = _configuration.GetSection("EmailConfiguration:Port").Value;

            //rellenamos lo solicitado
            var mensaje = new MimeMessage();
            mensaje.From.Add(MailboxAddress.Parse(ServerEmail)); //el parse es solo para poner correos y evitar poner nombres
            mensaje.To.Add(MailboxAddress.Parse(correo));
            mensaje.Subject = "Su contraseña es la siguiente: ";
            mensaje.Body = new TextPart(MimeKit.Text.TextFormat.Html) //ponemos que el texto tendra formato html para darle valor al correo
            {
                Text = "<h1>Estimado/a " + correo + ",<h1>" +
                     "<p>Hemos recibido su solicitud de recuperación de contraseña para su cuenta</p>" +
                     "<p><strong>Correo electrónico : </strong>" + correo + "</p>" +
                     "<p><strong>Contraseña : </strong>" + respuesta + "</p>"
            };
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(Host, int.Parse(Port), false);
            smtp.Authenticate(ServerEmail, ServerPassword);
            smtp.Send(mensaje);
            smtp.Disconnect(true);

        }


        public UsuarioEntities? RegisterUser(UsuarioEntities usuario)
        {
            if (usuario != null)
            {
                using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
            {
                connection.Execute("RegisterUser",
                   new { usuario.userName, usuario.lastName, usuario.Id, usuario.email, usuario.cellphone, usuario.statusU, usuario.passwordU, usuario.idRol },
                   commandType: System.Data.CommandType.StoredProcedure);

                return usuario;

            }
            }
            else
            {

                return null;
            }
        }

        public UsuarioEntities? ConsultAcc(long qIdUser)

        {
             using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
                {
                  var user = connection.Query<UsuarioEntities>("ConsultAcc",
                       new { IdUser=qIdUser},
                       commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

                if (user != null)
                {

                    var usuarioViewModel = new UsuarioEntities
                    {
                        IdUser = user.IdUser,
                        userName = user.userName,
                        lastName = user.lastName,
                        Id = user.Id,
                        cellphone = user.cellphone,
                        email = user.email,
                        passwordU = user.passwordU,

                    };

                    return usuarioViewModel;
                }

                return null;
            }
        }

        public UsuarioEntities? UpdateUser(UsuarioEntities usuario)
        {
            if (usuario != null)
            {
                using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
                {
                    var result = connection.Execute("EditAcc",
                       new
                       {usuario.IdUser,usuario.userName,usuario.lastName,usuario.cellphone,usuario.email,usuario.passwordU},
                       commandType: CommandType.StoredProcedure); ;

                    return usuario;
                }
            }
            else
            {
                return null;
            }
        }

        
        public UsuarioEntities? RegUserProg(long IdUser)

        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
            {
                var user = connection.Query<UsuarioEntities>("ConsultAcc",
                     new { IdUser },
                     commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

                if (user != null)
                {

                    connection.Execute("RegUserProg",
                       new { IdUser },
                       commandType: System.Data.CommandType.StoredProcedure); ;

                    return user;
                }

                return null;
            }
        }



    }

}

