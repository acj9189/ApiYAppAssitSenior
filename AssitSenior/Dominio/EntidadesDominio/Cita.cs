using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.EntidadesDominio
{
    public class Cita
    {
        public int idCita { get; set; }
        public string ced_paciente { get; set; }
        public string ced_medico { get; set; }
        public DateTime fecha { get; set; }
        public TimeSpan hora {get; set; }

        ConexionBD Conexion = new ConexionBD();

        public Cita()
        {
        }

        public Boolean GuardarCita(string paciente,string medico,int idTurno)
        {
            return Conexion.GuardarCita(paciente,medico,idTurno);
        }
        
    }
}
