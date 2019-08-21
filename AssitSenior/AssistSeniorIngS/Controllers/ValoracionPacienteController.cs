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
    public class ValoracionPacienteController : Controller
    {
        InterfazPD In = new Fachada();

        // GET: ValoracionPaciente
        public ActionResult vistaValoracionPaciente()
        {
            return View();
        }

        [HttpPost]
        public ActionResult vistaValoracionPaciente(string cedula, string emailMedico)
        {
            Paciente p = ConsultarPaciente(cedula, emailMedico);

            if(p != null)
            {
				if (p.nombre.Equals("CS01"))
				{
					ViewBag.NoHayCita = "El paciente que busca no tiene cita asignada con usted";
				}

				else
				{
					ViewData["InfoPaciente"] = p;
				}
            }

            else
            {
                ViewData["InfoError"] = "Error con la cedula ingresada o el paciente ya se encuentra Activo";
            }

            return View("vistaValoracionPaciente");
        }

        private Paciente ConsultarPaciente(string cedula, string email)
        {
            string Json = In.ConsultarPaciente(cedula, email);
			Paciente p;

			if(Json.Equals("No hay cita"))
			{
				p = new Paciente();
				p.nombre = "CS01";
			}

			else
			{
				p = JsonConvert.DeserializeObject<Paciente>(Json);		
			}

			return p;
		}

        [HttpPost]
        public ActionResult ComplementarInfoPaciente(string cedula, string alergias, string rh, string problemasC)
        {
            string mensaje = In.ComplementarInfoPaciente(cedula, alergias, rh, problemasC);

            if(mensaje.Equals("Ok"))
            {
                TempData["mensajeComplementar"] = "Los datos se actualizaron correctamente, El Paciente ahora esta activo";
            }

            else
            {
                TempData["mensajeComplementar"] = mensaje;
            }

            return RedirectToAction("vistaPerfilMedico", "PerfilMedico");
        }
    }
}