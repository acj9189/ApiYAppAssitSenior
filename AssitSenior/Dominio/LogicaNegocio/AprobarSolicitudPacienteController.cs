using System;
using System.Collections.Generic;
using System.Text;

using Dominio.EntidadesDominio;

namespace Dominio.LogicaNegocio
{
    public class AprobarSolicitudPacienteController
    {
        public LinkedList<Paciente> ListarSolicitudesPacientes()
        {
            Paciente p = new Paciente();
            LinkedList<Paciente> lista = new LinkedList<Paciente>();

            lista = p.ListarSolicitudesPacientes();

            return lista;
        }

        public string MostrarInfoPaciente(string cedula)
        {
            Paciente p = new Paciente();
            return p.MostrarInfoPaciente(cedula);
        }

        public Boolean AprobarPaciente(string cedula)
        {
            return CambiarEstadoCuentaPaciente("Pendiente", cedula);
        }

        public Boolean DesaprobarPaciente(string cedula)
        {
            return CambiarEstadoCuentaPaciente("Rechazado", cedula);
        }

        public Boolean CambiarEstadoCuentaPaciente(string estado, string cedula)
        {
            CuentaUsuario c = new CuentaUsuario();
            return c.CambiarEstadoCuentaPaciente(estado, cedula);
        }
    }
}
