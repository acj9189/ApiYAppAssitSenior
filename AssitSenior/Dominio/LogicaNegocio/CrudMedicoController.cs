using System;
using System.Collections.Generic;
using System.Text;

using Dominio.EntidadesDominio;

namespace Dominio.LogicaNegocio
{
	public class CrudMedicoController
	{
		Medico medico = new Medico();
		
		public string CrearMedico(string cedula, string nombre, string apellido, int edad, string direccion, string telefono, DateTime fechaNacimiento, char genero, string email, string especialidad,string password,string estado,char tipocuenta)
		{
			CuentaUsuario cuenta = new CuentaUsuario(email, password, estado, tipocuenta);
			string x= cuenta.RegistrarCuenta();
			string mensaje;



			if (x .Equals( "registro cuenta OK"))
			{
				mensaje = medico.CrearMedico(cedula, nombre, apellido, edad, direccion, telefono, fechaNacimiento, genero, email, especialidad);
				
			}
			else
			{
				mensaje = x;
			}
			return mensaje;

		}
		
		public string EliminarMedico(string cedula)
		{

			return medico.EliminarMedico(cedula);
		}
		public string ActualizarMedico(string cedula, string nombre, string apellido, int edad, string direccion, string telefono, DateTime fechaNacimiento, char genero, string especialidad)
		{

			return medico.ActualizarMedico( cedula, nombre,  apellido, edad,  direccion,  telefono,  fechaNacimiento,  genero, especialidad);
		}
		public LinkedList<Medico> ListarMedicosCrud()
		{
			LinkedList<Medico> listaMedico = new LinkedList<Medico>();
			listaMedico= medico.ListarMedicosCrud();
			return listaMedico;
		}
		public string ConsultarMedico(string cedula)
		{
			
		      return medico.ConsultarMedico(cedula);
		
		}

	}
	}
