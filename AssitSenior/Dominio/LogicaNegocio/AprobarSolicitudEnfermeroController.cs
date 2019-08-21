using System;
using System.Collections.Generic;
using System.Text;

using Dominio.EntidadesDominio;

namespace Dominio.LogicaNegocio
{
    public class AprobarSolicitudEnfermeroController
    {
        public LinkedList<Enfermero> ListarSolicitudesEnfermeros()
        {
            Enfermero e = new Enfermero();
            LinkedList<Enfermero> lista = new LinkedList<Enfermero>();

            lista = e.ListarSolicitudesEnfermeros();

            return lista;
        }

        public string MostrarInfoEnfermero(string cedula)
        {
            Enfermero e = new Enfermero();
            return e.MostrarInfoEnfermero(cedula);
        }

        public Boolean AprobarEnfermero(string cedula)
        {
            return CambiarEstadoCuentaEnfermero("activo", cedula);
        }

        public Boolean DesaprobarEnfermero(string cedula)
        {
            return CambiarEstadoCuentaEnfermero("Rechazado", cedula);
        }

        public Boolean CambiarEstadoCuentaEnfermero(string estado, string cedula)
        {
            CuentaUsuario c = new CuentaUsuario();
            return c.CambiarEstadoCuentaEnfermero(estado, cedula);
        }



    }
}
