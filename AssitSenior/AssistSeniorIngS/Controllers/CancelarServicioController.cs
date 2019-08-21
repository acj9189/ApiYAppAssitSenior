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
	public class CancelarServicioController : Controller
	{
		InterfazPD In = new Fachada();
		private static LinkedList<Servicio> listaServicios;

		// GET: CancelarServicio
		public ActionResult vistaCancelarServicio()
		{
			listaServicios = new LinkedList<Servicio>();

			string email = Convert.ToString(Session["Email"]);
			listaServicios = In.ListarServicios(email);

			if (listaServicios.LongCount() != 0)
			{
				ViewData["listaServicios"] = listaServicios;
				return View();
			}

			else
			{
				TempData["Excepcion"] = "No tiene servicios pendientes que pueda cancelar";
				return RedirectToAction("vistaPerfilPaciente", "PerfilPaciente");
			}
		}

		public ActionResult CancelarServicio(int idServicio, DateTime fecha, TimeSpan hora)
		{
			Tuple<string, LinkedList<Servicio>> resultadoCancelacion;

			string fechaSinHora = fecha.ToShortDateString();
			int horaComparar = hora.Hours;

			if (DateTime.Now.Day < fecha.Day)
			{
				
				resultadoCancelacion = In.CancelarServicio(idServicio);

				if (resultadoCancelacion.Item1.Equals("Ok"))
				{
					ViewBag.Excepcion = "Servicio Cancelado Exitosamente";
				}

				else
				{
					ViewBag.Excepcion = "Error al intentar cancelar el servicio, intentelo de nuevo Porfavor";
				}

				ViewData["listaServicios"] = resultadoCancelacion.Item2;
				return View("vistaCancelarServicio");
			}

			else if (DateTime.Now.Day == fecha.Day)
			{

				if (horaComparar >= (DateTime.Now.Hour + 3))
				{
					resultadoCancelacion = In.CancelarServicio(idServicio);

					if (resultadoCancelacion.Item1.Equals("Ok"))
					{
						ViewBag.Excepcion = "Servicio Cancelado Exitosamente";
					}

					else
					{
						ViewBag.Excepcion = "Error al intentar cancelar el servicio, intentelo de nuevo Porfavor";
					}

					ViewData["listaServicios"] = resultadoCancelacion.Item2;
				}

				else
				{
					ViewBag.Excepcion = "No puede cancelar un servicio con menos de 3 horas de anticipacion";
					ViewData["listaServicios"] = listaServicios;
				}				
			}

			return View("vistaCancelarServicio");
		}
	}
}