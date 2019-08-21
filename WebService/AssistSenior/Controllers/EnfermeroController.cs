using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Persistencia.Modelos;
using AssistSenior.Models;

namespace AssistSenior.Controllers
{
    public class EnfermeroController : ApiController
    {
		assistseniorEntities BD = new assistseniorEntities();
		[HttpGet]
		public IEnumerable<Enfermero> Get()
		{
			var enfermeros = (from e in BD.enfermero
							 join t in BD.turno_enfermero
							 on e.cedula equals t.ced_enfermero where  t.fecha > System.DateTime.Today
							 select new Enfermero() { nombre = e.nombre.ToUpper(), apellido=e.apellido.ToUpper(), cedula = e.cedula }).Distinct().ToList();


			return enfermeros;

		}

	}
}
