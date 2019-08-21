using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.EntidadesDominio
{
    public class TurnoEnfermero
    {
        public int idTurno { get; set; }
        public DateTime fecha { get; set; }
        public TimeSpan horaInicial { get; set; }
        public TimeSpan horaFinal { get; set; }
        public string estado { get; set; }
        public string ced_enfermero { get; set; }

        public Enfermero enfermero { get; set; }

        public static explicit operator TurnoEnfermero(LinkedList<turno_enfermero> v)
        {
            throw new NotImplementedException();
        }
    }
}
