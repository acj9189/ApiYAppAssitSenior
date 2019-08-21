using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AssistSeniorIngS.Models;
using Dominio.Services;

namespace AssistSeniorIngS.Controllers
{
    public class AutenticacionController : Controller
    {
        InterfazPD In = new Fachada();

        // GET: Autenticacion
        public ActionResult vistaAutenticacion()
        {
            ViewData["MsjRegistroE"] = TempData["MsjRegistroEnfermero"];
            ViewData["MsjRegistroP"] = TempData["MsjRegistroPaciente"];
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Email, string Pass)
        {
            string Mensaje = "";

            Mensaje= In.Login(Email, Pass);
            
            if (Mensaje.Equals("Datos Correctos, Bienvenido P"))
            {
                Session["Email"] = Email;
                return RedirectToAction("vistaPerfilPaciente", "PerfilPaciente");
            }
            if (Mensaje.Equals("Datos Correctos, Bienvenido M"))
            {
                Session["Email"] = Email;
                return RedirectToAction("vistaPerfilMedico", "PerfilMedico");
            }
            if (Mensaje.Equals("Datos Correctos, Bienvenido E"))
            {
                Session["Email"] = Email;
                return RedirectToAction("vistaPerfilEnfermero", "PerfilEnfermero");
            }
            if (Mensaje.Equals("Datos Correctos, Bienvenido A"))
            {
                Session["Email"] = Email;
                return RedirectToAction("vistaPerfilAdministrador", "PerfilAdministrador");
            }

            else
            {
                ViewData["MensajeLogueo"] = Mensaje;
                return View("vistaAutenticacion");
            }
        }

        public ActionResult Logout()
        {
            Session.Remove("Email");
            return View("vistaAutenticacion");
        }
    }
}