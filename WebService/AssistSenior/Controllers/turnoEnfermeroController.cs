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
	public class turnoEnfermeroController : ApiController
	{
	   
		assistseniorEntities BD = new assistseniorEntities();
	

	   [HttpGet]
		public IEnumerable<turnoEnfermero> Get(string cedula,DateTime fecha)
		{
			var turnosEnfermeros = BD.turno_enfermero
				.Where(enferm=>enferm.estado=="Disponible")
				.Where(enferm=>enferm.fecha==fecha)
				.Where(enferm=>enferm.ced_enfermero==cedula).Select
				(e => new turnoEnfermero()
				{ fecha = e.fecha,
					horaInicial =e.horaInicial,
					horaFinal =e.horaFinal,
					cedEnfermero =e.ced_enfermero})
				.ToList();  
				   
			return turnosEnfermeros;


		}
	}
}
  