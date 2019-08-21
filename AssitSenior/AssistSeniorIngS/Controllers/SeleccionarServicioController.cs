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
    public class SeleccionarServicioController : Controller
    {
        InterfazPD In = new Fachada();

        // GET: SeleccionarServicio
        public ActionResult vistaTipoServicio()
        {
     
            LinkedList<Enfermero> listaEnfermero = new LinkedList<Enfermero>();
            listaEnfermero = In.ListarEnfermeros();
            ViewBag.listaEnfermeros = new SelectList(listaEnfermero, "cedula", "nombre");
            return View();
        }
    }
}