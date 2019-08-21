using Dominio.EntidadesDominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.LogicaNegocio
{
    public class ActualizarInfoMedicoController
    {
        public bool ActualizarInfoMedico(string email, string telefono, string direccion)
        {
            Medico medico = new Medico(email, telefono, direccion);
            return medico.ActualizarInfoMedico();
        }

        public string listarInfoMedico(string email)
        {
            Medico medico= new Medico();
            medico.email = email;
            return medico.listarInfoMedico();
        }
    }
}
