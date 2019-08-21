using System;
using System.Collections.Generic;
using System.Text;

using Dominio.EntidadesDominio;

namespace Dominio.LogicaNegocio
{
    public class AgendarCitaPacienteController
    {
        public LinkedList<Paciente> ListarPacientesPendientes()
        {
            Paciente p = new Paciente();
            LinkedList<Paciente> lista = new LinkedList<Paciente>();
            lista = p.ListarPacientesPendientes();
            return lista;
        }

        public LinkedList<Medico> ListarMedicos()
        {
            Medico m = new Medico();
            LinkedList<Medico> listaMedico = new LinkedList<Medico>();
            listaMedico = m.ListarMedicos();
            return listaMedico;
        }

        public LinkedList<TurnoMedico> ListarTurnosMedico(string cedulaMedico)
        {
            TurnoMedico t = new TurnoMedico();
            LinkedList<TurnoMedico> turnos = new LinkedList<TurnoMedico>();

            turnos = t.ListarTurnosMedico(cedulaMedico);

            return turnos;
        }
               
        public Boolean GuardarCita(string paciente, string medico, int idTurno )
        {
            Cita cita = new Cita();
            CuentaUsuario c = new CuentaUsuario();

            bool var1 = cita.GuardarCita(paciente, medico, idTurno);

			if (var1)
			{
				bool var2 = c.CambiarEstadoCuentaPaciente("Agendado", paciente);

				if (var2)
				{
					return true;
				}

				else
				{
					return false;
				}
			}

			else
			{
				return false;
			}
            
        }
  
    }
}
