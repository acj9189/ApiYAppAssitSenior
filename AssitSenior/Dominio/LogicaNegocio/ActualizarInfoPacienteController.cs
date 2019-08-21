using System;
using System.Collections.Generic;
using System.Text;

using Dominio.EntidadesDominio;

namespace Dominio.LogicaNegocio
{
    public class ActualizarInfoPacienteController
    {
        public bool ActualizarInfoPaciente(string email, string telefono, string direccion)
        {
            Paciente p = new Paciente(email, telefono, direccion);
            return p.ActualizarInfoPaciente();
        }

        public string listarInfoPaciente(string email)
        {
            Paciente paciente = new Paciente();
            paciente.email = email;
            return paciente.listarInfoPaciente();
        }
    }
}
