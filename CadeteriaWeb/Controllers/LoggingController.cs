using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CadeteriaWeb.Models.UsuarioModels;
using Microsoft.AspNetCore.Mvc;
using CadeteriaWeb.ViewModels;

namespace CadeteriaWeb.Controllers
{
    public class LoggingController : Controller
    {
        private readonly IUsuario UsuarioRepo;

        public LoggingController(IUsuario UserRepo)
        {
            UsuarioRepo = UserRepo;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Verificar(string usuario, string pass)
        {
            Usuario user = UsuarioRepo.Logear(usuario, pass);

            if(user is not null)
            {
                HttpContext.Session.SetString("name", user.Nombre);

                if(user.Rol == RolUsuario.Administrador)
                {
                    HttpContext.Session.SetInt32("rol", 1);
                }
                if(user.Rol == RolUsuario.Cadete)
                {
                    HttpContext.Session.SetInt32("rol", 2);
                }

                HttpContext.Session.SetInt32("id_cadete", user.IDCadete);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Error", new { error = "No se ha encontrado el usuario ingresado" });
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult Error(string error)
        {
            ViewData["error"] = error;
            return View();
        }
    }
}
