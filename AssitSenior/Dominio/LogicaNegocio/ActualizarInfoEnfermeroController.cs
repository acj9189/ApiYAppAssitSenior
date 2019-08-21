using Dominio.EntidadesDominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.LogicaNegocio
{
    public class ActualizarInfoEnfermeroController
    {
        public bool ActualizarInfoEnfermero(string email,string telefono, string direccion)
        {
            Enfermero enfermero = new Enfermero(email, telefono, direccion);
            return enfermero.ActualizarInfoEnfermero();
        }
        public string listarInfoEnfermero(string email)
        {
            Enfermero enfermero = new Enfermero();
            enfermero.email = email;
            return enfermero.listarInfoEnfermero();
        }
    }
}
