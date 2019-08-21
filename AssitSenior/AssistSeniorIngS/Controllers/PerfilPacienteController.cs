using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssistSeniorIngS.Controllers
{
    public class PerfilPacienteController : Controller
    {
        // GET: PerfilPaciente
        public ActionResult vistaPerfilPaciente()
        {
			// MENSAJE QUE SE RETORNA DESDE CANCELAR SERVICIO //
			if(TempData["Excepcion"] != null)
			{
				ViewBag.Excepcion = TempData["Excepcion"];
			}

			if (TempData["MensajeActualizacionDatos"] != null)
			{
				ViewBag.MensajeActualizarDatos = TempData["MensajeActualizacionDatos"];
			}

			return View();
        }
    }
}