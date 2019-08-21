using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Dominio.Services;
using AssistSeniorIngS.Models;

namespace AssistSeniorIngS.Controllers
{
    public class RegistroPacienteController : Controller
    {
        InterfazPD In = new Fachada();

        // GET: RegistroPaciente
        public ActionResult vistaRegistroPaciente()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarPaciente(string nombre, string apellido, string cedula, int edad, string direccion, string telefono, DateTime fechaNacimiento, char sexo, string email, string password, string password2, string descripcion,string invalidez)
        {
            string Mensaje = "";

            if (password == password2)
            {
                Mensaje = In.RegistrarPaciente( nombre,  apellido,  cedula, edad,  direccion,  telefono, fechaNacimiento,  sexo,  email,  password,  descripcion,  invalidez);
            }

            else
            {
                Mensaje = "Las contraseñas no son iguales, verifique por favor";
            }


            if (Mensaje.Equals("registro paciente OK"))
            {
                TempData["MsjRegistroPaciente"] = "Solicitud enviada, en breve obtendra una respuesta";
                return RedirectToAction("vistaAutenticacion", "Autenticacion");
            }

            else
            {
                ViewData["MsjSolicitudRegistro"] = Mensaje;
                return View("vistaRegistroPaciente");
            }
        }


    }
}