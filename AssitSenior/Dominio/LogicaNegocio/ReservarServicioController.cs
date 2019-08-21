using System;
using System.Collections.Generic;
using System.Text;

using Dominio.EntidadesDominio;

namespace Dominio.LogicaNegocio
{
    public class ReservarServicioController
    {
        public ReservarServicioController()
        {
        }

        public LinkedList<Enfermero> ListarEnfermeros()
        {
            Enfermero enfermero = new Enfermero();
            return enfermero.ListarEnfermeros();
        }

        public LinkedList<TurnoEnfermero> ListarTurnosEnfermero(string tipoS, string cedulaEnfermero, int duracion)
        {
            Enfermero e = new Enfermero();
            e.cedula = cedulaEnfermero;

            return e.ListarTurnosEnfermero(tipoS, duracion);           
        }

        public string ReservarServicio(string tipoServicio, int duracion, string enfermero, string paciente, int[] listaTurnos)
        {
            Servicio s = new Servicio();
            s.tipoServicio = tipoServicio;
            s.duracion = duracion;

            return s.ReservarServicio(enfermero, paciente, listaTurnos);
        }

        
    }
}
