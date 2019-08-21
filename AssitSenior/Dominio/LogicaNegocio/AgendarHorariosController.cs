using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dominio.EntidadesDominio;

namespace Dominio.LogicaNegocio
{
	public class AgendarHorariosController
	{

		public LinkedList<Medico> ListarMedicosAgTu()
		{
			Medico m = new Medico();
			LinkedList<Medico> lista = new LinkedList<Medico>();

			lista = m.ListarMedicosAgTu();

			return lista;
		}

		public LinkedList<Enfermero> ListarEnfermerosAgTu()
		{
			Enfermero e = new Enfermero();
			LinkedList<Enfermero> lista = new LinkedList<Enfermero>();

			lista = e.ListarEnfermerosAgTu();

			return lista;
		}

		public string AgendarTurnosMedico(string cedula, List<string> listaTurnos)
		{
			Medico m = new Medico();
			m.cedula = cedula;

			return m.AgendarTurnosMedico(listaTurnos);
		}

		public string AgendarTurnosEnfermero(string cedula, List<string> listaTurnos)
		{
			Enfermero e = new Enfermero();
			e.cedula = cedula;

			return e.AgendarTurnosEnfermero(listaTurnos);
		}
	}
}
