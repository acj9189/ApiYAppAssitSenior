using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Dominio.EntidadesDominio
{
    public class CuentaUsuario
    {

        public string email { set; get; }
        public string password { set; get; }
        public string estado { set; get; }
        public char tipoCuenta { set; get; }


        public CuentaUsuario()
        {
        }

        public CuentaUsuario(string email, string password, string estado, char tipoCuenta)
        {
            this.email = email;
            this.password = password;
            this.estado = estado;
            this.tipoCuenta = tipoCuenta;
        }


        public string CompararPassEncriptada()
        {
            byte[] BtClearBytes;
            BtClearBytes = new UnicodeEncoding().GetBytes(password);
            byte[] BtHashedBytes = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(BtClearBytes);

            this.password = BitConverter.ToString(BtHashedBytes);

            return password;

        }

        public string EncriptarPass()
        {
            byte[] BtClearBytes;
            BtClearBytes = new UnicodeEncoding().GetBytes(password);
            byte[] BtHashedBytes = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(BtClearBytes);

            password = BitConverter.ToString(BtHashedBytes);

            return password;
        }

        public string Loguear()
        {
            ConexionBD Conexion = new ConexionBD();
            CompararPassEncriptada(); // ENCRIPTA LA CONTRASEÑA Y LA REEMPLAZA EN LA MISMA VARIABLE PASS

            String Mensaje = Conexion.Loguear(email, password);

            return Mensaje;
        }

        public string RegistrarCuenta()
        {
            ConexionBD Conexion = new ConexionBD();
            string Mensaje = Conexion.RegistrarCuenta(email, EncriptarPass(), estado, tipoCuenta);

            return Mensaje;
        }

        public Boolean CambiarEstadoCuentaEnfermero(string estado, string cedula)
        {
            ConexionBD Conexion = new ConexionBD();
            return Conexion.CambiarEstadoCuentaEnfermero(estado, cedula);
        }

        public Boolean CambiarEstadoCuentaPaciente(string estado, string cedula)
        {
            ConexionBD Conexion = new ConexionBD();
            return Conexion.CambiarEstadoCuentaPaciente(estado, cedula);
        }
    }
}
