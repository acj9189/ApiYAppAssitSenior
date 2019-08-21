using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio.Services;
using Dominio.EntidadesDominio;
using AssistSeniorIngS.Models;

namespace AssistSeniorIngS.Controllers
{
	public class AgendarTurnosController : Controller
	{
		InterfazPD In = new Fachada();
		private static List<string> listaT;

		// GET: AgendarTurnos
		public ActionResult vistaAgendarTurnos()
		{
			return View();
		}

		//Metodo para listar los medicos activos en el sistema 
		[HttpPost]
		public ActionResult ListarMedicos()
		{
			LinkedList<Medico> listaMedicos = new LinkedList<Medico>();
			listaMedicos= In.ListarMedicosAgTu();
            ViewBag.listaMedicos = new SelectList(listaMedicos, "cedula", "nombre");

			return View("vistaAgendarTurnos");
		}


		//Metodo para listar los medicos activos en el sistema 
		[HttpPost]
		public ActionResult ListarEnfermeros()
		{
			LinkedList<Enfermero> listaEnfermeros = new LinkedList<Enfermero>();
			listaEnfermeros = In.ListarEnfermerosAgTu();
			ViewBag.listaEnfermeros = new SelectList(listaEnfermeros, "cedula", "nombre");

			return View("vistaAgendarTurnos");
		}


		[HttpPost]
		public ActionResult MostrarTablaTurnosM(string listaMedicos)
		{
			ViewData["mostrarTabla"] = listaMedicos; // se envia de nuevo la cedula para tener control siempre
			ViewBag.tipo = "M";
			return View("vistaAgendarTurnos");
		}

		[HttpPost]
		public ActionResult MostrarTablaTurnosE(string listaEnfermeros)
		{
			ViewData["mostrarTabla"] = listaEnfermeros; // se envia de nuevo la cedula para tener control siempre
			ViewBag.tipo = "E";
			return View("vistaAgendarTurnos");
		}


		[HttpPost]
		public ActionResult RecibirInfoAgendar(List<string> listaTurnos)
		{
			listaT = listaTurnos;

			return View("vistaAgendarTurnos");
		}

		[HttpPost]
		public ActionResult AgendarTurnos(string cedulaEnvio, string tipoEnvio) 
		{
			string Mensaje = "";

			if (tipoEnvio.Equals("M"))
			{
				Mensaje = In.AgendarTurnosMedico(cedulaEnvio, listaT);
			}

			else if (tipoEnvio.Equals("E"))
			{
				Mensaje = In.AgendarTurnosEnfermero(cedulaEnvio, listaT);
			}

			ViewBag.MensajeAgenda = Mensaje;

			return View("vistaAgendarTurnos");

		}
	}
}