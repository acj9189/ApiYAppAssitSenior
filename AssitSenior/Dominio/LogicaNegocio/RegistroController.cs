using System;
using System.Collections.Generic;
using System.Text;

using Dominio.EntidadesDominio;

namespace Dominio.LogicaNegocio
{
    public class RegistroController
    {
        public RegistroController()
        {
        }

        public string RegistrarEnfermeros(string nombre, string apellido, string cedula, int edad, string direccion, string telefono, DateTime fechaNacimiento, char sexo, string email, string password)
        {
            string MensajeCuenta = "";
            string MensajeEnfermero = "";

            Enfermero e = new Enfermero(cedula, nombre, apellido, edad, direccion, telefono, fechaNacimiento, sexo, email);
            CuentaUsuario Cu = new CuentaUsuario(email,password, "Inactivo", 'E');

            MensajeCuenta = Cu.RegistrarCuenta();

            if (MensajeCuenta.Equals("registro cuenta OK"))
            {
                MensajeEnfermero = e.Registrar();
            }

            else
            {
                MensajeEnfermero = MensajeCuenta;
            }

            return MensajeEnfermero;
        }

        public string RegistrarPaciente(string nombre, string apellido, string cedula, int edad, string direccion, string telefono, DateTime fechaNacimiento, char sexo, string email, string password,string descripcion, string invalidez)
        {
            string MensajeCuenta = "";
            string MensajePaciente = "";

            Paciente p = new Paciente(cedula, nombre, apellido, edad, direccion, telefono, fechaNacimiento, sexo, email,descripcion,invalidez);
            CuentaUsuario Cu = new CuentaUsuario(email,password, "Inactivo", 'P');

            MensajeCuenta = Cu.RegistrarCuenta();

            if (MensajeCuenta.Equals("registro cuenta OK"))
            {
                MensajePaciente = p.Registrar();
            }

            else
            {
                MensajePaciente = MensajeCuenta;
            }

            return MensajePaciente;
        }
    }
}
