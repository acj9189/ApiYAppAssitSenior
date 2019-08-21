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
    public class ReservarServicioController : Controller
    {
        InterfazPD In = new Fachada();
        public static int[] lista;

        public ActionResult vistaReservarServicio()
        {
            return View();
        }


        [HttpPost]
        public ActionResult vistaReservarServicio(string tipoS, string listaEnfermeros, int duracion)
        {
            LinkedList<TurnoEnfermero> listaTurnos = new LinkedList<TurnoEnfermero>();

            listaTurnos = In.ListarTurnosEnfermero(tipoS, listaEnfermeros, duracion);

            ViewBag.tipoS = tipoS;
            ViewBag.duracion = duracion;
            ViewBag.enfermero = listaEnfermeros; // solo es la cedula

            ViewData["listaTurnos"] = listaTurnos;

            return View();
        }

        [HttpPost]
        public ActionResult RecibirInfoServicio(List<int> listaIds)
        {
            lista = listaIds.ToArray();

            return View("vistaReservarServicio");
        }

        [HttpPost] 
        public ActionResult ReservarServicio(string tipoServicio, int duracion, string enfermero, string paciente)
        {
            int[] listaEnvio = lista;

            string Mensaje = In.ReservarServicio(tipoServicio, duracion, enfermero, paciente, listaEnvio);

            if(Mensaje.Equals("Ok"))
            {
                ViewData["MensajeServicio"] = "Servicio Reservado con Exito !";
                return RedirectToAction("vistaPerfilPaciente", "PerfilPaciente");
            }

            else
            {
                ViewData["MensajeServicio"] = Mensaje;
                return RedirectToAction("vistaTipoServicio", "SeleccionarServicio");
            }
            
        }
    }

    

}