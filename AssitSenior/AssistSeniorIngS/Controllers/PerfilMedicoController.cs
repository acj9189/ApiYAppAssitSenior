using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssistSeniorIngS.Controllers
{
    public class PerfilMedicoController : Controller
    {
        // GET: PerfilMedico
        public ActionResult vistaPerfilMedico()
        {
			if(TempData["mensajeComplementar"] != null)
			{
				ViewData["mensajeComplementar"] = TempData["mensajeComplementar"];
			}

			if (TempData["MensajeActualizacionDatos"] != null)
			{
				ViewBag.MensajeActualizarDatos = TempData["MensajeActualizacionDatos"];
			}

			return View();
        }
    }
}