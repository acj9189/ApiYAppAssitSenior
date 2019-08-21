using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio.Services;
using AssistSeniorIngS.Models;
using Dominio.EntidadesDominio;
namespace AssistSeniorIngS.Controllers
{
    public class CrudMedicoController : Controller
    {
		InterfazPD In = new Fachada();
		// GET: CrudMedico

		public ActionResult vistaListarMedico()
        {
			LinkedList<Medico> listaMedicos = new LinkedList<Medico>();
			listaMedicos = In.ListarMedicosCrud();
			ViewBag.listaMedicos = new SelectList(listaMedicos, "cedula", "nombre");
			
			return View();
		}

		[HttpPost]
		public ActionResult EliminarMedico(string  listaMedicos)
		{
			string Mensaje = "";
			Mensaje=In.EliminarMedico(listaMedicos);
			TempData["MsjEliminar"] = Mensaje;
			return RedirectToAction("vistaPerfilAdministrador", "PerfilAdministrador");
		}

		[HttpPost]
		public ActionResult ModificarMedico()
		{
			return RedirectToAction("vistaPerfilAdministrador", "PerfilAdministrador");
		}

	}
}