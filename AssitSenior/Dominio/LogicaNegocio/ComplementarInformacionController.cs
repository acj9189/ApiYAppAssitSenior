using System;
using System.Collections.Generic;
using System.Text;

using Dominio.EntidadesDominio;

namespace Dominio.LogicaNegocio
{
    public class ComplementarInformacionController
    {
        public ComplementarInformacionController()
        {
        }

        public string ConsultarPaciente(string cedula, string email)
        {
            Paciente p = new Paciente();
            p.cedula = cedula;
            return p.ConsultarPaciente(email);
        }

        public void DesaprobarPaciente(string cedula)
        {

        }

        public string ComplementarInfoPaciente(string cedula, string alergias, string rh, string problemasC)
        {
            Paciente p = new Paciente(cedula, alergias, rh, problemasC);
            return p.ActualizarDatosPaciente();
        }
    }
}
