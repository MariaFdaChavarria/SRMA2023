using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMRA2023.Entities;
using SMRA2023.Interfaces;

namespace SMRA2023.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioModel _usuario;
        public UsuarioController(IUsuarioModel usuario)
        {
            _usuario = usuario;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult validarusuario(UsuarioEntities usuario)
        {

            var result = _usuario.validarUsuario(usuario);
            if(result != null)
            {
                long userID = result.IdUser;

                HttpContext.Session.SetInt32("IdSession",(int)userID);
                HttpContext.Session.SetString("NameSession",result.userName);
                string NameSession = HttpContext.Session.GetString("NameSession");
                ViewBag.NameSession = NameSession;

                return RedirectToAction("Index","Home");

            }else 
            {
                TempData["MensajeError"] = "Usuario incorrecto o no existe.";
                return RedirectToAction("Index", "Usuario");
            }
        }



        [HttpGet]
        public IActionResult RecoverPW()
        {
            return View();
        }

        [HttpPost]
        public IActionResult email_Recovery(UsuarioEntities usuario)
        {
            var resultado = _usuario.email_Verification(usuario);
            if (resultado != null)
            {
                _usuario.Email(usuario.email);
                TempData["ContrasenaRecuperada"] = "Su contraseña se envió al correo ingresado.";
                return RedirectToAction("RecoverPW", "Usuario");
                // Su contraseña se envió al correo ingresado.
            }
            else
            {
                TempData["ErrorCorreo"] = "No tienes una cuenta, por favor registrate.";
                // No tienes una cuenta, por favor registrate.
                return RedirectToAction("RecoverPW", "Usuario");

            }

        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterUser(UsuarioEntities usuario)
        {
            usuario.idRol = 2;
            usuario.statusU = true;
            
            var resultado= _usuario.RegisterUser(usuario);

            if (resultado != null)
            {
                return RedirectToAction("Index", "Usuario");
            }
            else
            {
                return RedirectToAction("Index", "Usuario");
            }
        }

          
        [HttpGet]
        public IActionResult Profile()
        {
           var IdUser = HttpContext.Session.GetInt32("IdSession");

            long UserId = (long)IdUser;

            var result = _usuario.ConsultAcc(UserId);

            if (result != null)
            {
                return View(result);
            }

            return NotFound(); // Maneja el caso en el que el usuario no se encuentre
        }

        [HttpPost]
        public IActionResult UpdateUser(UsuarioEntities usuario)
        {

            var IdUser = HttpContext.Session.GetInt32("IdSession");

            long UserId = (long)IdUser;

            var resultado = _usuario.UpdateUser(usuario, UserId);


            if (resultado != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Usuario");
            }
        }

        [HttpPost]
        public IActionResult RegisterProgram()
        {
            var IdUser = HttpContext.Session.GetInt32("IdSession");

            long UserId = (long)IdUser;

            var result = _usuario.RegUserProg(UserId);

            if (result != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Usuario");
            }
        }

        [HttpPost]
        public IActionResult DeleteAcc()
        {
            var IdUser = HttpContext.Session.GetInt32("IdSession");

            long UserId = (long)IdUser;

            var result = _usuario.DeleteAcc(UserId);

            if (result != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Usuario");
            }
        }


    }



}



 



