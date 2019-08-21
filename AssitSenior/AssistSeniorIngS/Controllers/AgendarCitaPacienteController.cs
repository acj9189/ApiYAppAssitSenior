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
    public class AgendarCitaPacienteController : Controller
    {
        InterfazPD In = new Fachada();
        private static LinkedList<Paciente> Pacientes;
        private static LinkedList<Medico> Medicos;
        private static LinkedList<TurnoMedico> turnos;
        private string paciente = "";
        private string medico = "";


        // GET: VistaAgendarCitaPaciente
        public ActionResult vistaAgendarCitaPaciente()
        {
            
            Pacientes = new LinkedList<Paciente>();
            Pacientes = In.ListarPacientesPendientes();

            if (Pacientes.LongCount() != 0)
            {
                ViewData["listaPacientes"] = Pacientes;
            }

            else
            {
                TempData["NoHayPacientes"] = "No hay Pacientes sin cita actualmente";
                return RedirectToAction("vistaPerfilAdministrador", "PerfilAdministrador");
            }

            Medicos = new LinkedList<Medico>();
            Medicos = In.ListarMedicos();

            if (Medicos.LongCount() != 0)
            {
                ViewData["listaMedicos"] = Medicos;
            }

            else
            {
				TempData["NoHayMedicoConTurno"] = "No hay medicos disponibles";
				return RedirectToAction("vistaPerfilAdministrador", "PerfilAdministrador");
			}

			return View();
            
        }

		[HttpPost]
		public ActionResult vistaAgendarCitaPaciente(string cedula, string cedPaciente, string nombre, string tipo)
		{
			if (tipo == "M")
			{
                this.medico = cedula;
                ViewBag.Cpaciente = cedPaciente;				
				ViewBag.medico = this.medico;

				turnos = new LinkedList<TurnoMedico>();
				turnos = In.ListarTurnosMedico(cedula);
				ViewData["listaTurnos"] = turnos;
				return View();
			}
            else
			{
				this.paciente = cedula;
				ViewBag.Cpaciente = cedula;
				ViewBag.Npaciente = nombre;
                ViewData["listaPacientes"] = Pacientes;				
			    ViewData["listaMedicos"] = Medicos;
				return View();
			}
		}

        [HttpPost]
        public ActionResult GuardarCita(string idTurno, string cedulaP, string cedulaM)
        {
            Boolean var;           
            var = In.GuardarCita(cedulaP, cedulaM, idTurno);

            if (var)
            {
                TempData["MensajeAgendaC"] = "Cita guardada con exito";
				return RedirectToAction("vistaPerfilAdministrador", "PerfilAdministrador");
			}      			
            else
            {                   
                ViewBag.ErrorGuardandoCita = "Error guardando la cita";
				return View("vistaAgendarCitaPaciente");
            }           
        }
    }
}