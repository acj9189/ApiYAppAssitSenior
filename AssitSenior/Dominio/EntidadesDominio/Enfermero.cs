using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.EntidadesDominio
{
    public class Enfermero : Persona
    {
        ConexionBD Conexion = new ConexionBD();

        public string hojaVida { set; get; }
        public string certificado { set; get; }

        public LinkedList<TurnoEnfermero> listaTurnos { set; get; }

        public Enfermero()
        {
            listaTurnos = new LinkedList<TurnoEnfermero>();
        }


        public Enfermero(string email, string telefono, string direccion)
        {
            this.email = email;
            this.direccion = direccion;
            this.telefono = telefono; 
        }


        

        public Enfermero(string cedula, string nombre, string apellido, int edad, string direccion, string telefono, DateTime fechaNacimiento, char genero, string email)
        {
            this.cedula = cedula;
            this.nombre = nombre;
            this.apellido = apellido;
            this.edad = edad;
            this.direccion = direccion;
            this.telefono = telefono;
            this.fechaNacimiento = fechaNacimiento;
            this.genero = genero;
            this.email = email;
        }

        public LinkedList<Enfermero> ListarEnfermeros()
        {
            LinkedList<Enfermero> listaEnfermero = new LinkedList<Enfermero>();
            listaEnfermero = Conexion.ListarEnfermero();

            return listaEnfermero;
        }

        public LinkedList<Enfermero> ListarSolicitudesEnfermeros()
        {
            LinkedList<Enfermero> lista = new LinkedList<Enfermero>();
            lista = Conexion.ListarSolicitudesEnfermeros();

            return lista;
        }

        public string MostrarInfoEnfermero(string cedula)
        {
            return Conexion.MostrarInfoEnfermero(cedula);

        }

        //METODO QUE LISTA LOS TURNOS DEL ENFERMERO PARA EL PACIENTE SOLICITAR SERVICIO//
        public LinkedList<TurnoEnfermero> ListarTurnosEnfermero(string tipoS, int duracion)
        {
            LinkedList<string> Json = Conexion.ListarTurnosEnfermero(tipoS, this.cedula, duracion);

            foreach (string i in Json)
            {
                this.listaTurnos.AddLast(JsonConvert.DeserializeObject<TurnoEnfermero>(i));
            }

            return this.listaTurnos;
        }


        public string Registrar()
        {
            string Mensaje = Conexion.RegistrarEnfermeros(nombre, apellido, cedula, edad, direccion, telefono, fechaNacimiento, genero, email);
            return Mensaje;
        }

        public bool ActualizarInfoEnfermero()
        {
            return Conexion.ActualizarInfoEnfermero(this.email,this.telefono, this.direccion);
        
        }

        public string listarInfoEnfermero()
        {
            return Conexion.listarInfoEnfermero(this.email);
                 
        }

		public LinkedList<Enfermero> ListarEnfermerosAgTu()
		{
			return Conexion.ListarEnfermerosAgTu();
		}

		public string AgendarTurnosEnfermero(List<string> listaTurnos)
		{
			return Conexion.AgendarTurnosEnfermero(this.cedula, listaTurnos);
		}


		public LinkedList<TurnoEnfermero> ListarAgendaEnfermero()
		{
			return Conexion.ListarAgendaEnfermero(this.email);
		}
	}
}
