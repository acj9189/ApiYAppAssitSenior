using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.EntidadesDominio
{
    public class TurnoMedico
    {
        public int idTurno { get; set; }
        public DateTime fecha { get; set; }
        public TimeSpan horaInicial { get; set; }
        public TimeSpan horaFinal { get; set; }
        public string estado { get; set; }
        public string ced_medico { get; set; }

        ConexionBD Conexion = new ConexionBD();

        public TurnoMedico(){

        }

        public LinkedList<TurnoMedico> ListarTurnosMedico(String cedulaMedico)
        {
            LinkedList<TurnoMedico> turnos = new LinkedList<TurnoMedico>();
            turnos = Conexion.ListarTurnosMedico(cedulaMedico);

            return turnos;
        }
    }
}
