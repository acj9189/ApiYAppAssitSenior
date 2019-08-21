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
    public class ActualizarInfoEnfermeroController : Controller
    {
        InterfazPD inter = new Fachada();


        //GET::
        public ActionResult vistaActualizarInfoEnfermero()
        {
            string email = Session["Email"] as String;
            Enfermero enfermero = listarInfoEnfermero(email);
            ViewData["infoActualizarEnfermero"] = enfermero;
            return View();

        }

        private Enfermero listarInfoEnfermero(string email)
        {
            string json = inter.listarInfoEnfermero(email);
            Enfermero enfermero = JsonConvert.DeserializeObject<Enfermero>(json);
            return enfermero;
        }

        public ActionResult ActualizarInfoEnfermero(string email, string telefono, string direccion)
        {
            string mensaje = "";
            bool bandera = inter.ActualizarInfoEnfermero(email, telefono, direccion);
            if (bandera)
            {
                mensaje = "Datos actualizados correctamente";
            }
            else
            {
                mensaje = "Error al actualizar los datos";
            }

            TempData["MensajeActualizacionDatos"] = mensaje;

            return RedirectToAction("vistaPerfilEnfermero", "PerfilEnfermero");
        }
    }
   
}