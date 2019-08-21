using System;
using System.Collections.Generic;
using System.Text;

using Dominio.EntidadesDominio;

namespace Dominio.LogicaNegocio
{
    public class CancelarServicioController
    {

        public CancelarServicioController()
        {
        }

        public LinkedList<Servicio> ListarServicios(string email)
        {
            Paciente p = new Paciente();
            p.email = email;

            return p.ListarServicios();
        }

        public Tuple<string, LinkedList<Servicio>> CancelarServicio(int idServicio)
        {
            Servicio s = new Servicio();
            s.idServicio = idServicio;

            return s.CancelarServicio();
        }
        
    }
}
