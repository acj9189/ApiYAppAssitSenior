using System;
using System.Collections.Generic;
using Persistencia;
using Newtonsoft.Json;

namespace Dominio.EntidadesDominio

{
    public class ConexionBD
    {
        private IRepositorioAssistSenior repositorioConexion;

        public ConexionBD()
        {
            repositorioConexion = new BDAssistSenior();
        }



        public string Loguear(string Email, string Pass)
        {
            return repositorioConexion.Loguear(Email, Pass);
        }



        //METODO QUE REGISTRA ENFERMEROS //
        public string RegistrarEnfermeros(string nombre, string apellido, string cedula, int edad, string direccion, string telefono, DateTime fechaNacimiento, char sexo, string email)
        {
            return repositorioConexion.RegistrarEnfermeros(nombre, apellido, cedula, edad, direccion, telefono, fechaNacimiento, sexo, email);
        }

        //METODO QUE REGISTRA LA INFORMACION DE LA CUENTA
        public string RegistrarCuenta(string email, string pass, string estado, char tipoCuenta)
        {
            return repositorioConexion.RegistrarCuenta(email, pass, estado, tipoCuenta);
        }

        //METODO QUE REGISTRA PACIENTES
        public string RegistrarPaciente(string nombre, string apellido, string cedula, int edad, string direccion, string telefono, DateTime fechaNacimiento, char sexo, string email, string descripcion, string invalidez)
        {
            return repositorioConexion.RegistrarPaciente(nombre, apellido, cedula, edad, direccion, telefono, fechaNacimiento, sexo, email, descripcion, invalidez);
        }

        //METODO QUE ACTUALIZA LA INFORMACIÓN DEL ENFERMERO
        public bool ActualizarInfoEnfermero(string email, string telefono, string direccion)
        {
            return repositorioConexion.ActualizarInfoEnfermero(email, telefono, direccion);
        }

        //METODO QUE ACTUALIZA LA INFORMACION DEL MEDICO//
        public bool ActualizarInfoMedico(string email, string telefono, string direccion)
        {
            return repositorioConexion.ActualizarInfoMedico(email, telefono, direccion);
        }
        //METODO QUE ACTUALIZA LA INFORMACION DEL PACIENTE//
        public bool ActualizarInfoPaciente(string email, string telefono, string direccion)
        {
            return repositorioConexion.ActualizarInfoPaciente(email, telefono, direccion);
        }

        // METODO QUE LISTA LOS ENFERMEROS PARA EL TIPO DE SERVICIO //
        public LinkedList<Enfermero> ListarEnfermero()
        {
            LinkedList<Enfermero> listaEnfermero = new LinkedList<Enfermero>();

            foreach (Tuple<int, string> i in repositorioConexion.ListarEnfermeros())
            {
                Enfermero e = new Enfermero();
                e.cedula = Convert.ToString(i.Item1);
                e.nombre = i.Item2;
                listaEnfermero.AddLast(e);
            }

            return listaEnfermero;
        }

        // METODO QUE LISTA LOS SERVICIOS PARA LA CANCELACION //
        public LinkedList<Servicio> ListarServicios(string email)
        {
            LinkedList<Servicio> listaServicios = new LinkedList<Servicio>();

            foreach (string i in repositorioConexion.ListarServicios(email))
            {
                Servicio s = JsonConvert.DeserializeObject<Servicio>(i);  
                listaServicios.AddLast(s);
            }

            return listaServicios;
        }

        //METODO QUE CONSULTA LA INFO DEL PACIENTE DADA LA CEDULA RCU7//
        public string ConsultarPaciente(string cedula, string email)
        {
            return repositorioConexion.ConsultarPaciente(cedula, email);
        }


        //METODO QUE ACTUALIZA LA INFORMACION DEL PACIENTE //
        public string ComplementarInfoPaciente(string cedula, string alergias, string rh, string problemasC)
        {
            return repositorioConexion.ComplementarInfoPaciente(cedula, alergias, rh, problemasC);
        }
        //METODO QUE CREA UN MEDICO
        public string CrearMedico(string cedula, string nombre, string apellido, int edad, string direccion, string telefono, DateTime fechaNacimiento, char genero, string email, string especialidad)
        {
            return repositorioConexion.CrearMedico(cedula, nombre, apellido, edad, direccion, telefono, fechaNacimiento, genero, email, especialidad);

        }
        //METODO QUE ELIMINA UN MEDICO
        public string EliminarMedico(string cedula)
        {
            return repositorioConexion.EliminarMedico(cedula);
        }

        public string ActualizarMedico(string cedula, string nombre, string apellido, int edad, string direccion, string telefono, DateTime fechaNacimiento, char genero, string especialidad)
        {
            return repositorioConexion.ActualizarMedico(cedula, nombre, apellido, edad, direccion, telefono, fechaNacimiento, genero,  especialidad);
        }


        //METODO QUE LISTA LOS MEDICOS
        public LinkedList<Medico> ListarMedicos()
        {
            LinkedList<Medico> lista = new LinkedList<Medico>();
            LinkedList<string> Json = repositorioConexion.ListarMedicos();
            foreach ( string i in Json)
            {
                lista.AddLast(JsonConvert.DeserializeObject<Medico>(i));
            }
            return lista;
            
        }

		//METODO QUE LISTA LOS MEDICOS PARA EL CRUD //
		public LinkedList<Medico> ListarMedicosCrud()
		{
			LinkedList<Medico> lista = new LinkedList<Medico>();

			foreach (Tuple<int, string> i in repositorioConexion.ListarMedicosCrud())
			{
				Medico m = new Medico();
				m.cedula = Convert.ToString(i.Item1);
				m.nombre = i.Item2;
				lista.AddLast(m);
			}
			return lista;
		}


		//METODO QUE CONSULTA UN MEDICO
		public string ConsultarMedico(string cedula)
        {
           
            string Json = repositorioConexion.ConsultarMedico(cedula);
            
            return Json;
        }


        public LinkedList<string> ListarTurnosEnfermero(string tipoS, string cedulaEnfermero, int duracion)
        {
            return repositorioConexion.ListarTurnosEnfermero(tipoS, cedulaEnfermero, duracion);
        }

        public LinkedList<TurnoMedico> ListarTurnosMedico(string cedulaMedico)
        {
            LinkedList<TurnoMedico> turnos = new LinkedList<TurnoMedico>();
            
            LinkedList<string> Json = repositorioConexion.ListarTurnosMedico(cedulaMedico);

            foreach (string i in Json)
            {
                turnos.AddLast(JsonConvert.DeserializeObject<TurnoMedico>(i));
            }

            return turnos;
        }

        //METODO QUE LISTA LAS SOLICITUDES DE CUENTA DE LOS ENFERMEROS //
        public LinkedList<Enfermero> ListarSolicitudesEnfermeros()
        {
            LinkedList<Enfermero> lista = new LinkedList<Enfermero>();
            
            LinkedList<string> Json = repositorioConexion.ListarSolicitudesEnfermeros();

            foreach (string i in Json)
            {
                lista.AddLast(JsonConvert.DeserializeObject<Enfermero>(i));
            }

            return lista;
        }

        //METODO QUE LISTA LAS SOLICITUDES DE CUENTA DE LOS PACIENTES //
        public LinkedList<Paciente> ListarSolicitudesPacientes()
        {
            LinkedList<Paciente> lista = new LinkedList<Paciente>();
            Paciente e = new Paciente();

            LinkedList<string> Json = repositorioConexion.ListarSolicitudesPacientes();

            foreach (string i in Json)
            {
                lista.AddLast(JsonConvert.DeserializeObject<Paciente>(i));
            }

            return lista;
        }

        public LinkedList<Paciente> ListarPacientesPendientes()
        {
            LinkedList<Paciente> lista = new LinkedList<Paciente>();
            Paciente e = new Paciente();

            LinkedList<string> Json = repositorioConexion.ListarPacientesPendientes();

            foreach (string i in Json)
            {
                lista.AddLast(JsonConvert.DeserializeObject<Paciente>(i));
            }

            return lista;
        }

        //MUESTRA LA INFORMACION DE UNA SOLICITUD DE ALGUN ENFERMERO//
        public string MostrarInfoEnfermero(string cedula)
        {
            return repositorioConexion.MostrarInfoEnfermero(cedula);
        }

        //CAMBIA EL ESTADO DE UNA CUENTA VINCULADA A UNA SOLICITUD DE ENFERMERO//
        public Boolean CambiarEstadoCuentaEnfermero(string estado, string cedula)
        {
            return repositorioConexion.CambiarEstadoCuentaEnfermero(estado, cedula);
        }

        //MUESTRA LA INFORMACION DE UNA SOLICITUD DE ALGUN PACIENTE//
        public string MostrarInfoPaciente(string cedula)
        {
            return repositorioConexion.MostrarInfoPaciente(cedula);
        }

        //CAMBIA EL ESTADO DE UNA CUENTA VINCULADA A UNA SOLICITUD DE PACIENTE//
        public Boolean CambiarEstadoCuentaPaciente(string estado, string cedula)
        {
            return repositorioConexion.CambiarEstadoCuentaPaciente(estado, cedula);
        }

        //GUARDA UNA VALORACION DE PRIMERA VEZ//
        public Boolean GuardarCita(string paciente, string medico, int idTurno)
        {
            return repositorioConexion.GuardarCita(paciente,medico,idTurno);
        }

        //METODO QUE RESERVA SERVICIO POR PARTE DE LOS PACIENTES //
        public string ReservarServicio(string tipoServicio, int duracion, string enfermero, string paciente, int[] listaTurnos)
        {
            return repositorioConexion.ReservarServicio(tipoServicio, duracion, enfermero, paciente, listaTurnos);
        }

        //METODO PARA CANCELAR SERVICIOS POR PARTE DEL PACIENTE //
        public Tuple<string, LinkedList<Servicio>> CancelarServicio(int idServicio)
        {
			Tuple<string, LinkedList<Servicio>> Tupla;
			LinkedList<Servicio> listaServicios = new LinkedList<Servicio>();

			Tuple<string, LinkedList<string>> i = repositorioConexion.CancelarServicio(idServicio);
			
			foreach(string j in i.Item2)
			{
				Servicio s = JsonConvert.DeserializeObject<Servicio>(j);
				listaServicios.AddLast(s);
			}

			Tupla = new Tuple<string, LinkedList<Servicio>>(i.Item1, listaServicios);

			return Tupla;
		}

        //METODO PARA LISTAR LA INFORMACION DE UN ENFERMERO PARA ACTUALIZAR

        public string listarInfoEnfermero(string email)
        {
            return repositorioConexion.listarInfoEnfermero(email);
        }

        //METODO PARA LISTAR LA INFORMACION DE UN MEDICO PARA ACTUALIZAR

        public string listarInfoMedico(string email)
        {
            return repositorioConexion.listarInfoMedico(email);
        }

        //METODO PARA LISTAR LA INFORMACION DE UN PACIENTE PARA ACTUALIZAR

        public string listarInfoPaciente(string email)
        {
            return repositorioConexion.listarInfoPaciente(email);
        }

		public LinkedList<Medico> ListarMedicosAgTu()
		{
			LinkedList<Medico> listaMedicos = new LinkedList<Medico>();

			foreach(string i in repositorioConexion.ListarMedicosAgTu())
			{
				Medico m = JsonConvert.DeserializeObject<Medico>(i);
				listaMedicos.AddLast(m);
			}

			return listaMedicos;
		}

		public LinkedList<Enfermero> ListarEnfermerosAgTu()
		{
			LinkedList<Enfermero> listaEnfermeros = new LinkedList<Enfermero>();

			foreach (string i in repositorioConexion.ListarEnfermerosAgTu())
			{
				Enfermero e = JsonConvert.DeserializeObject<Enfermero>(i);
				listaEnfermeros.AddLast(e);
			}

			return listaEnfermeros;
		}

		public string AgendarTurnosMedico(string cedula, List<string> listaTurnos)
		{
			return repositorioConexion.AgendarTurnosMedico(cedula, listaTurnos);
		}

		public string AgendarTurnosEnfermero(string cedula, List<string> listaTurnos)
		{
			return repositorioConexion.AgendarTurnosEnfermero(cedula, listaTurnos);
		}

		public LinkedList<TurnoEnfermero> ListarAgendaEnfermero(string email)
		{
			LinkedList<TurnoEnfermero> listaTurnos = new LinkedList<TurnoEnfermero>();

			foreach (string i in repositorioConexion.ListarAgendaEnfermero(email))
			{
				TurnoEnfermero te = JsonConvert.DeserializeObject<TurnoEnfermero>(i);
				listaTurnos.AddLast(te);
			}

			return listaTurnos;
		}
	}
}
  