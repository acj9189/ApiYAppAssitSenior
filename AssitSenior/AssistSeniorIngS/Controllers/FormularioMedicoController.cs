using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssistSeniorIngS.Models;
using Dominio.Services;
using Newtonsoft.Json;
using Dominio.EntidadesDominio;
namespace AssistSeniorIngS.Controllers
{
	public class FormularioMedicoController : Controller
	{
		InterfazPD In = new Fachada();


		[HttpPost]
		public ActionResult vistaformularioMedico()
		{
            ViewBag.crear = "para crear";
			return View();
		}

		[HttpPost]
		public ActionResult Modificar()
		{
			
            ViewBag.modificar = "para modificar";

			return View("vistaformularioMedico");
		}

		[HttpPost]
		public ActionResult CrearMedico(string cedula, string nombre, string apellido, int edad, string direccion, string telefono, DateTime fechaNacimiento, char genero, string email, string especialidad, string password, string password2)
		{
			string Mensaje = "";

			if (password == password2)
			{
				Mensaje = In.CrearMedico(cedula,nombre, apellido, edad, direccion, telefono, fechaNacimiento, genero, email,especialidad, password,"Activo",'M');		
			}

			else
			{
				Mensaje = "Las contraseñas no son iguales, verifique por favor";
			}


			if (Mensaje.Equals("registro Medico OK"))
			{
				TempData["MsjRegistroMedico"] = "Medico Registrado con exito";
				return RedirectToAction("vistaPerfilAdministrador", "PerfilAdministrador");
			}

			else
			{
				TempData["MsjSolicitudRegistro"] = Mensaje;
                return RedirectToAction("vistaPerfilAdministrador", "PerfilAdministrador");
            }
		}
		

		private Medico MostrarMedico(string cedula)
		{
			string json = In.listarInfoMedico(cedula);
			Medico medico = JsonConvert.DeserializeObject<Medico>(json);
			return medico;

		}
		[HttpPost]
		public ActionResult ActualizarMedico(string cedula, string nombre, string apellido, int edad, string direccion, string telefono, DateTime fechaNacimiento, char genero, string especialidad)
		{   string Mensaje = "";
			 Mensaje = In.ActualizarMedico( cedula,nombre, apellido, edad, direccion, telefono,  fechaNacimiento, genero,  especialidad);

			TempData["MsjModificarMedico"] = Mensaje;
			return RedirectToAction("vistaPerfilAdministrador", "PerfilAdministrador");
			
		}




	}
}
    
