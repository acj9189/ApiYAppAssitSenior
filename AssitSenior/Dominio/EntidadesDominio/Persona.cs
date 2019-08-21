using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Dominio.EntidadesDominio
{
    public abstract class Persona
    {
        public string cedula { set; get; }
        public string nombre { set; get; }
        public string apellido { set; get; }
        public int edad { set; get; }
        public string direccion { set; get; }
        public string telefono { set; get; }
        public DateTime fechaNacimiento { set; get; }
        public char genero { set; get; }
        public string email { set; get; }

        public Persona()
        {
        }

    }
}
