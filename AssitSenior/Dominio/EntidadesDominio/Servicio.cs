using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.EntidadesDominio
{
    public class Servicio
    {
        ConexionBD Conexion = new ConexionBD();

        public int idServicio { set; get; }
        public string tipoServicio { set; get; }
        public int duracion { set; get; }
        public string cedula_Enfermero { set; get; }
        public string cedula_Paciente { set; get; }
        public DateTime fecha { set; get; }
        public TimeSpan hora { set; get; }
        public string estado { set; get; }

        public Servicio()
        {
        }

		public Servicio(int idServicio, string tipoServicio, int duracion, string cedE, string cedP, DateTime fecha, TimeSpan hora, string estado)
		{
			this.idServicio = idServicio;
			this.tipoServicio = tipoServicio;
			this.duracion = duracion;
			this.cedula_Enfermero = cedE;
			this.cedula_Paciente = cedP;
			this.fecha = fecha;
			this.hora = hora;
			this.estado = estado;
		}
        
        public string ReservarServicio(string enfermero, string paciente, int[] listaTurnos)
        {
            return Conexion.ReservarServicio(this.tipoServicio, this.duracion, enfermero, paciente, listaTurnos);
        }

        public Tuple<string, LinkedList<Servicio>> CancelarServicio()
        {
            return Conexion.CancelarServicio(this.idServicio);
        }
    }
}
