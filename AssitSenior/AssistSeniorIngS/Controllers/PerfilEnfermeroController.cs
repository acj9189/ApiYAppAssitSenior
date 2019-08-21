using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssistSeniorIngS.Controllers
{
    public class PerfilEnfermeroController : Controller
    {
        // GET: PerfilEnfermero
        public ActionResult vistaPerfilEnfermero()
        {
			if(TempData["MensajeActualizacionDatos"] != null)
			{
				ViewBag.MensajeActualizarDatos = TempData["MensajeActualizacionDatos"];
			}
		
            return View();
        }
    }
}