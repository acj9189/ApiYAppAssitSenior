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
    public class AprobarEnfermerosController : Controller
    {
        InterfazPD In = new Fachada();
        private static LinkedList<Enfermero> listaEnfermeros;
        private Enfermero e;

        // GET: VistaAprobarSolicitudesEnfermeros
        public ActionResult vistaAprobarSolicitudesEnfermeros()
        {

			if(TempData["MensajeApDe"] != null)
			{
				ViewBag.MensajeApDe = TempData["MensajeApDe"];
			}

            listaEnfermeros = new LinkedList<Enfermero>();
            listaEnfermeros = In.ListarSolicitudesEnfermeros();

            if (listaEnfermeros.LongCount() != 0)
            {
                ViewData["listaEnfermeros"] = listaEnfermeros;
                return View();
            }
            else
            {
                ViewBag.Excepcion = "No hay solicitudes de Enfermeros actualmente";
                return RedirectToAction("vistaPerfilAdministrador", "PerfilAdministrador");
            }
        }

        [HttpPost]
        public ActionResult vistaAprobarSolicitudesEnfermeros(string cedula)
        {
            Enfermero e = ConsultarEnfermero(Convert.ToString(cedula));

			if (e != null)
			{
				ViewData["InfoEnfermero"] = e;
				return View();
			}

			else
			{
				TempData["ExcepcionE"] = "Error con la cedula ingresada" +cedula;
                return RedirectToAction("vistaPerfilAdministrador", "PerfilAdministrador");
            }

            
        }

        private Enfermero ConsultarEnfermero(string cedula)
        {
            string Json = In.MostrarInfoEnfermero(cedula);
            Enfermero e = JsonConvert.DeserializeObject<Enfermero>(Json);
            return e;
        }

        public ActionResult estado(string estado,string cedula)
        {
           Boolean var;
           if(estado == "Aprobar"){
               var = In.AprobarEnfermero(cedula);
                if (var)
                {
					TempData["MensajeApDe"] = "Enfermero aprobado con exito";
                    return RedirectToAction("vistaAprobarSolicitudesEnfermeros");
                }
           }
           else{
                if(estado == "Desaprobar"){
                   var = In.DesaprobarEnfermero(cedula);
                    if (var)
                    {
						TempData["MensajeApDe"] = "Enfermero rechazado con exito";
						return RedirectToAction("vistaAprobarSolicitudesEnfermeros");
                    }
                }
                else{
					TempData["ExcepcionE"] = "Error con el estado" ;
                    return RedirectToAction("vistaPerfilAdministrador", "PerfilAdministrador");
                }
           }

			TempData["ExcepcionE"] = "Error con el cambio";
            return RedirectToAction("vistaPerfilAdministrador", "PerfilAdministrador");
        }
                
    }
}