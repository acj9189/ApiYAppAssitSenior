using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio.EntidadesDominio;
using Dominio.Services;
using Newtonsoft.Json;
using AssistSeniorIngS.Models;

namespace AssistSeniorIngS.Controllers
{
    public class ActualizarInfoMedicoController : Controller
    {
        InterfazPD inter = new Fachada();
        //GET::
        public ActionResult vistaActualizarInfoMedico()
        {
			string email = Session["Email"] as String;
            Medico medico = listarInfoMedico(email);
            ViewData["infoActualizarMedico"] = medico;
            return View();

        }

        private Medico listarInfoMedico(string email)
        {
            string json = inter.listarInfoMedico(email);
            Medico medico = JsonConvert.DeserializeObject<Medico>(json);
            return medico;
        }

        public ActionResult ActualizarInfoMedico(string email, string telefono, string direccion)
        {
            string mensaje = "";
            bool bandera = inter.ActualizarInfoMedico(email, telefono, direccion);
            if (bandera)
            {
                mensaje = "Datos actualizados correctamente";
            }
            else
            {
                mensaje = "Error al actualizar los datos";
            }

            TempData["MensajeActualizacionDatos"] = mensaje;

			return RedirectToAction("vistaPerfilMedico", "PerfilMedico");
        }
    }
}