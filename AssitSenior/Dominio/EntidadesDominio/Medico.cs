using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.EntidadesDominio
{
	public class Medico : Persona
	{
		public List<Medico> ListaMedico { get; set; }

        ConexionBD Conexion = new ConexionBD();
        private string especialidad { set; get; }
		public LinkedList<TurnoMedico> listaTurnos { set; get; }

		public Medico()
        {

        }

        public Medico(string email, string telefono, string direccion)
        {
            this.telefono = telefono;
            this.direccion = direccion;
            this.email = email;
        }

        public Medico(string cedula, string nombre, string apellido, int edad, string direccion, string telefono, DateTime fechaNacimiento, char genero, string email, string especialidad)
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
            this.especialidad = especialidad;
        }



        public bool ActualizarInfoMedico()
        {
            return Conexion.ActualizarInfoMedico(this.email, this.telefono, this.direccion);
            
        }
 
		public string CrearMedico(string cedula, string nombre, string apellido, int edad, string direccion, string telefono, DateTime fechaNacimiento, char genero, string email, string especialidad)
		{
			return Conexion.CrearMedico(cedula, nombre, apellido, edad, direccion, telefono, fechaNacimiento, genero, email, especialidad);
		}

		public string EliminarMedico(string cedula)
		{
			return Conexion.EliminarMedico(cedula);
		}

		public string ActualizarMedico(string cedula, string nombre, string apellido, int edad, string direccion, string telefono, DateTime fechaNacimiento, char genero, string especialidad)
		{
			return Conexion.ActualizarMedico( cedula,  nombre,  apellido,  edad, direccion, telefono, fechaNacimiento,  genero, especialidad); 
		}

		public LinkedList<Medico> ListarMedicos()
		{
			LinkedList<Medico> listaMedico = new LinkedList<Medico>();
			listaMedico = Conexion.ListarMedicos();
			return listaMedico;
		}

		public LinkedList<Medico> ListarMedicosCrud()
		{
			LinkedList<Medico> listaMedico = new LinkedList<Medico>();
			listaMedico = Conexion.ListarMedicosCrud();
			return listaMedico;
		}

		public string ConsultarMedico(string cedula)
		{
			return Conexion.ConsultarMedico(cedula);
			
		}

        public string listarInfoMedico()
        {
            return Conexion.listarInfoMedico(this.email);
        }

		public LinkedList<Medico> ListarMedicosAgTu()
		{
			return Conexion.ListarMedicosAgTu();
		}

		public string AgendarTurnosMedico(List<string> listaTurnos)
		{
			return Conexion.AgendarTurnosMedico(this.cedula, listaTurnos);
		}
	}
}
