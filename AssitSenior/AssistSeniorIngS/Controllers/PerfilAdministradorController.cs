using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssistSeniorIngS.Controllers
{
    public class PerfilAdministradorController : Controller
    {
        // GET: PerfilAdministrador
        public ActionResult vistaPerfilAdministrador()
        {
            if (TempData["ExcepcionE"] != null)
            {
                ViewBag.ExcepcionE = TempData["ExcepcionE"];
            }

            else if (TempData["ExcepcionP"] != null)
            {
                ViewBag.ExcepcionP = TempData["ExcepcionP"];
            }

            else if (TempData["NoHaySolPacientes"] != null)
            {
                ViewBag.NoHaySolPacientes = TempData["NoHaySolPacientes"];
            }

            else if (TempData["NoHayCitasPorAgendar"] != null)
            {
                ViewBag.NoHayCitasPorAgendar = TempData["NoHayCitasPorAgendar"];
            }

            else if (TempData["NoHayMedicoConTurno"] != null)
            {
                ViewBag.NoHayMedicosConTurno = TempData["NoHayMedicoConTurno"];
            }

            else if (TempData["NoHayPacientes"] != null)
            {
                ViewBag.NoHayPacientes = TempData["NoHayPacientes"];
            }

            else if (TempData["MensajeAgendaC"] != null)
            {
                ViewBag.MensajeAgendaC = TempData["MensajeAgendaC"];
            }
            else if (TempData["MsjEliminar"] != null)
            {
                ViewBag.MsjEliminar = TempData["MsjEliminar"];
            }

            else if (TempData["MsjRegistroMedico"] != null)
            {
                ViewBag.MsjRegistroMedico = TempData["MsjRegistroMedico"];
            }

            else if(TempData["MsjSolicitudRegistro"] != null)
            {
                ViewBag.MsjSolicitudRegistro = TempData["MsjSolicitudRegistro"];
            }
			else if (TempData["MsjModificarMedico"] != null)
			{
				ViewBag.MsjSolicitudRegistro = TempData["MsjModificarMedico"];
			}
			return View();
        }
    }
}