using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AssistSeniorIngS.Models;
using Dominio.Services;
using Dominio.EntidadesDominio;

using Newtonsoft.Json;

namespace AssistSeniorIngS.Controllers
{
    public class AprobarPacientesController : Controller
    {
        InterfazPD In = new Fachada();
        private static LinkedList<Paciente> listaPacientes;
      

        // GET: VistaAprobarSolicitudesPacientes
        public ActionResult vistaAprobarSolicitudesPacientes()
        {

			if (TempData["MensajeApDe"] != null)
			{
				ViewBag.MensajeApDe = TempData["MensajeApDe"];
			}

			listaPacientes = new LinkedList<Paciente>();
            listaPacientes= In.ListarSolicitudesPacientes();
            if (listaPacientes.LongCount() != 0)
            {
                ViewData["listaPacientes"] = listaPacientes;
                return View();
            }
            else
            {
                TempData["NoHaySolPacientes"] = "No hay solicitudes de Pacientes actualmente";
                return RedirectToAction("vistaPerfilAdministrador", "PerfilAdministrador");
            }
        }

        [HttpPost]
        public ActionResult vistaAprobarSolicitudesPacientes(string cedula)
        {
            Paciente p = ConsultarPaciente(Convert.ToString(cedula));

            if (p != null)
            {
                ViewData["InfoPaciente"] = p;
                return View();
            }

            else
            {
				TempData["ExcepcionP"] = "Error con la cedula ingresada";
                return RedirectToAction("vistaPerfilAdministrador", "PerfilAdministrador");
            }
        }

        private Paciente ConsultarPaciente(string cedula)
        {
            string Json = In.MostrarInfoPaciente(cedula);
            Paciente p = JsonConvert.DeserializeObject<Paciente>(Json);
            return p;
        }

        public ActionResult estado(string estado, string cedula)
        {
            Boolean var;
            if (estado == "Aprobar")
            {
                var = In.AprobarPaciente(cedula);
                if (var)
                {
					TempData["MensajeApDe"] = "Paciente aprobado con exito, Por favor Asigne la cita pronto";
                    return RedirectToAction("vistaAprobarSolicitudesPacientes");
                }
            }
            else
            {
                if (estado == "Desaprobar")
                {
                    var = In.DesaprobarPaciente(cedula);
                    if (var)
                    {
						TempData["MensajeApDe"] = "Paciente rechazado con exito";
						return RedirectToAction("vistaAprobarSolicitudesPacientes");
                    }
                }
                else
                {
					TempData["ExcepcionP"] = "Error con el estado";
                    return RedirectToAction("vistaPerfilAdministrador", "PerfilAdministrador");
                }
            }

			TempData["ExcepcionP"] = "Error con el cambio";
            return RedirectToAction("vistaPerfilMedico", "PerfilMedico");
        }

    }
}