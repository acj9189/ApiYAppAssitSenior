using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Dominio.Services;
using AssistSeniorIngS.Models;

namespace AssistSeniorIngS.Controllers
{
    public class RegistroEnfermeroController : Controller
    {

        InterfazPD In = new Fachada();


        // GET: RegistroEnfermero
        public ActionResult vistaRegistroEnfermero()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarEnfermeros(string nombre, string apellido, string cedula, int edad, string direccion, string telefono, DateTime fechaNacimiento,  char sexo, string email, string password, string password2)
        {
            string Mensaje = "";

            if(password == password2)
            {
                Mensaje = In.RegistrarEnfermeros(nombre, apellido, cedula, edad, direccion, telefono, fechaNacimiento, sexo, email, password);
            }
            
            else
            {
                Mensaje = "Las contraseñas no son iguales, verifique por favor";
            }


            if(Mensaje.Equals("registro enfermero OK"))
            {
                TempData["MsjRegistroEnfermero"] = "Solicitud enviada, en breve obtendra una respuesta";
                return RedirectToAction("vistaAutenticacion", "Autenticacion");
            }

            else
            {
                ViewData["MsjSolicitudRegistro"] = Mensaje;
                return View("vistaRegistroEnfermero");
            }
        }


    }
}