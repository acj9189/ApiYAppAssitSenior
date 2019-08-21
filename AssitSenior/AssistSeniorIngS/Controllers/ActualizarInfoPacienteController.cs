using Dominio.EntidadesDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio.Services;
using AssistSeniorIngS.Models;
using Newtonsoft.Json;


namespace AssistSeniorIngS.Controllers
{
    public class ActualizarInfoPacienteController : Controller
    {
        InterfazPD inter = new Fachada();


        //GET::
        public ActionResult vistaActualizarInfoPaciente()
        {
			string email = Session["Email"] as String;
            Paciente paciente = listarInfoPaciente(email);
            ViewData["infoActualizarPaciente"] = paciente;
            return View();

        }

        private Paciente listarInfoPaciente(string email)
        {
            string json = inter.listarInfoPaciente(email);
            Paciente paciente= JsonConvert.DeserializeObject<Paciente>(json);
            return paciente;
        }

        public ActionResult ActualizarInfoPaciente(string email, string telefono, string direccion)
        {
            string mensaje = "";
            bool bandera = inter.ActualizarInfoPaciente(email, telefono, direccion);
            if (bandera)
            {
                mensaje = "Datos actualizados correctamente";
            }
            else
            {
                mensaje = "Error al actualizar los datos";
            }

            TempData["MensajeActualizacionDatos"] = mensaje;

			return RedirectToAction("vistaPerfilPaciente", "PerfilPaciente");
        }
    }

}