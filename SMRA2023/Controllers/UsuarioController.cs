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

            var resultado = _usuario.validarUsuario(usuario);
            if(resultado != null)
            {
                return RedirectToAction("Index","Home");
            }else 
            {
                TempData["MensajeError"] = "Usuario incorrecto o no existe.";
                return RedirectToAction("Index", "Usuario");
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
            return View();
        }

        [HttpPost]
        public IActionResult UpdateUser(UsuarioEntities usuario)
        {
            usuario.idRol = 2;
            usuario.statusU = true;

            var resultado = _usuario.UpdateUser(usuario);

            if (resultado != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

    }

}

 



