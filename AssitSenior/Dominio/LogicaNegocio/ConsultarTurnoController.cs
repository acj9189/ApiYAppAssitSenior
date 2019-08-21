using System;
using System.Collections.Generic;
using System.Text;

using Dominio.EntidadesDominio;

namespace Dominio.LogicaNegocio
{
	public class ConsultarTurnoController
	{
		public LinkedList<TurnoEnfermero> ListarAgendaEnfermero(string email)
		{
			Enfermero e = new Enfermero();
			e.email = email;

			return e.ListarAgendaEnfermero();
		}
	}
}
