using System;
using System.Collections.Generic;
using System.Text;

using Dominio.EntidadesDominio; 

namespace Dominio.LogicaNegocio
{
    public class AutenticarseController
    {
        public string Login(string Email, string Pass)
        {
            string Mensaje = "";

            CuentaUsuario CuentaU= new CuentaUsuario();
            CuentaU.email= Email;
            CuentaU.password= Pass;

            Mensaje= CuentaU.Loguear();

            return Mensaje;
            
            
        }
    }
}
