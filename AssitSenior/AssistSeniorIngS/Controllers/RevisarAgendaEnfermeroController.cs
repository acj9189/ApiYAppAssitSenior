using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Dominio.EntidadesDominio;
using Dominio.Services;
using AssistSeniorIngS.Models;

namespace AssistSeniorIngS.Controllers
{
    public class RevisarAgendaEnfermeroController : Controller
    {

		InterfazPD In = new Fachada();

        // GET: RevisarAgendaEnfermero
        public ActionResult vistaRevisarAgendaTurnos()
        {
			LinkedList<TurnoEnfermero> listaTurnos = new LinkedList<TurnoEnfermero>();

			listaTurnos = In.ListarAgendaEnfermero(Session["Email"] as string);

			ViewData["listaTurnos"] = listaTurnos;

			return View();
        }

        
    }
}
