using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.EntidadesDominio
{
    public class Paciente : Persona
    {
        ConexionBD Conexion = new ConexionBD();

        public string invalidez { set; get; }
        public string alergias { set; get; }
        public string rh { set; get; }
        public string problemasC { set; get; }
        public string descripcion { set; get; }

        public LinkedList<Servicio> listaServicios;

        public Paciente()
        {
            this.listaServicios = new LinkedList<Servicio>();
        }

        public Paciente(string cedula, string nombre, string apellido,  int edad, string direccion, string telefono, DateTime fechaNacimiento, char sexo, string email,  string descripcion, string invalidez)
        {
            this.cedula = cedula;
            this.nombre = nombre;
            this.apellido = apellido;
            this.edad = edad;
            this.direccion = direccion;
            this.telefono = telefono;
            this.fechaNacimiento = fechaNacimiento;
            this.genero = sexo;
            this.email = email;
            this.descripcion = descripcion;
            this.invalidez = invalidez;
        }

        public Paciente(string cedula, string alergias, string rh, string problemasC)
        { 
            this.cedula = cedula;
            this.alergias = alergias;
            this.rh = rh;
            this.problemasC = problemasC;
        }

        public Paciente(string telefono, string direccion, string email)
        {
            this.telefono = telefono;
            this.direccion = direccion;
            this.email = email;
        }

        public string ConsultarPaciente(string emailMedico)
        {
            return Conexion.ConsultarPaciente(this.cedula, emailMedico);
        }

        public LinkedList<Paciente> ListarSolicitudesPacientes()
        {
            LinkedList<Paciente> lista = new LinkedList<Paciente>();
            lista = Conexion.ListarSolicitudesPacientes();

            return lista;
        }

        public string MostrarInfoPaciente(string cedula)
        {
            return Conexion.MostrarInfoPaciente(cedula);

        }

        public LinkedList<Paciente> ListarPacientesPendientes()
        {
            LinkedList<Paciente> lista = new LinkedList<Paciente>();
            lista = Conexion.ListarPacientesPendientes();

            return lista;
        }

        //METODO QUE REGISTRA PACIENTE
        public string Registrar()
        {
            string Mensaje = Conexion.RegistrarPaciente(nombre, apellido, cedula, edad, direccion, telefono, fechaNacimiento, genero, email, descripcion,invalidez);
            return Mensaje;
        }

        // ACTUALIZA LOS DATOS DEL PACIENTE QUE EL MEDICO CONSIDERA PERTINENTES EN LA CITA//
        public string ActualizarDatosPaciente()
        {
            return Conexion.ComplementarInfoPaciente(this.cedula, this.alergias, this.rh, this.problemasC);
        }
        
        public string listarInfoPaciente()
        {
            return Conexion.listarInfoPaciente(this.email);
        }

        //ACTUALIZA LOS DATOS DEL PACIENTE, POR EL PACIENTE //
        public bool ActualizarInfoPaciente()
        {
            return Conexion.ActualizarInfoPaciente(this.telefono, this.direccion, this.email);
        }

        // METODO QUE LISTA LOS SERVICIOS A CANCELAR DE UN PACIENTE //
        public LinkedList<Servicio> ListarServicios()
        {
            listaServicios = Conexion.ListarServicios(this.email);

            return listaServicios;
        }

    }
}
