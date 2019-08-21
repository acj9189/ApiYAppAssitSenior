using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Dominio.EntidadesDominio;
using Dominio.LogicaNegocio;
using Dominio.Services;

namespace AssistSeniorIngS.Models
{
    public class Fachada : InterfazPD
    {
        
        AutenticarseController RCU1 = new AutenticarseController();
		CrudMedicoController RCU2 = new CrudMedicoController();
		RegistroController RCU3 = new RegistroController();
		AprobarSolicitudPacienteController RCU4 = new AprobarSolicitudPacienteController();
		AprobarSolicitudEnfermeroController RCU5 = new AprobarSolicitudEnfermeroController();
        AgendarCitaPacienteController RCU6 = new AgendarCitaPacienteController();
        ComplementarInformacionController RCU7 = new ComplementarInformacionController();
        ReservarServicioController RCU8 = new ReservarServicioController();
		ConsultarTurnoController RCU9 = new ConsultarTurnoController();
        CancelarServicioController RCU10 = new CancelarServicioController();
		AgendarHorariosController RCU11 = new AgendarHorariosController();
        ActualizarInfoEnfermeroController RCU12 = new ActualizarInfoEnfermeroController();
		ActualizarInfoMedicoController RCU13 = new ActualizarInfoMedicoController();
		ActualizarInfoPacienteController RCU14 = new ActualizarInfoPacienteController();


		// METODO LOGIN //
		public string Login(string Email, string Pass)
        {
            string Mensaje = RCU1.Login(Email, Pass);
            return Mensaje;
        }
        //METODO QUE REGISTRA ENFERMEROS //
        public string RegistrarEnfermeros(string nombre, string apellido, string cedula, int edad, string direccion, string telefono, DateTime fechaNacimiento, char sexo, string email, string password)
        {
            string Mensaje = RCU3.RegistrarEnfermeros(nombre, apellido, cedula, edad, direccion, telefono, fechaNacimiento, sexo, email, password);
            return Mensaje;
        }
        //METODO QUE REGISTRA PACIENTE //
        public string RegistrarPaciente(string nombre, string apellido, string cedula, int edad, string direccion, string telefono, DateTime fechaNacimiento, char sexo, string email, string password, string descripcion, string invalidez)
        {
            string Mensaje = RCU3.RegistrarPaciente(nombre, apellido, cedula, edad, direccion, telefono, fechaNacimiento, sexo, email, password, descripcion, invalidez);
            return Mensaje;
        }

		//METODO QUE PERMITE LISTAR MEDICOS POR EL ADMINISTRADOR PARA CRUD//
		public LinkedList<Medico> ListarMedicosCrud()
		{
			LinkedList<Medico> listaMedico = new LinkedList<Medico>();
			listaMedico = RCU2.ListarMedicosCrud();

			return listaMedico;
		}

		//METODO QUE PERMITE CREAR UN MEDICO
		public string CrearMedico(string cedula, string nombre, string apellido, int edad, string direccion, string telefono, DateTime fechaNacimiento, char genero, string email, string especialidad, string password, string estado, char tipocuenta)
		{
			return RCU2.CrearMedico(cedula, nombre, apellido, edad, direccion, telefono, fechaNacimiento, genero, email, especialidad, password, estado, tipocuenta);
		}

		public string ActualizarMedico(string cedula, string nombre, string apellido, int edad, string direccion, string telefono, DateTime fechaNacimiento, char genero, string especialidad)
		{
			return RCU2.ActualizarMedico(cedula, nombre, apellido, edad, direccion, telefono, fechaNacimiento, genero, especialidad);
		}

		public string EliminarMedico(string cedula)
		{
			return RCU2.EliminarMedico(cedula);
		}


		// METODO QUE LISTA LOS ENFERMEROS PARA LA VISTA TIPO SERVICIO //
		public LinkedList<Enfermero> ListarEnfermeros()
        {
            LinkedList<Enfermero> listaEnfermeros = new LinkedList<Enfermero>();
            listaEnfermeros = RCU8.ListarEnfermeros();

            return listaEnfermeros;
        }

        //METODO QUE LISTA LOS SERVICIOS DE UN PACIENTE PARA LA CANCELACION //
        public LinkedList<Servicio> ListarServicios(string email)
        {
            LinkedList<Servicio> listaServicios = new LinkedList<Servicio>();
            listaServicios = RCU10.ListarServicios(email);

            return listaServicios;
        }

        //METODO PARA CANCELAR UN SERVICIO POR PARTE DEL PACIENTE //
        public Tuple<string, LinkedList<Servicio>> CancelarServicio(int idServicio)
        {
            return RCU10.CancelarServicio(idServicio);
        }

        //METODO QUE RETORNA LA INFO DEL PACIENTE DADA LA CEDULA RCU7 //
        public string ConsultarPaciente(string cedula, string email)
        {
            return RCU7.ConsultarPaciente(cedula, email);
        }


        //METODO QUE LISTA LA INFORMACION DEL ENFERMERO PARA ACTUALIZAR
        public string listarInfoEnfermero(string email)
        {
            return RCU12.listarInfoEnfermero(email);
        }

        //METODO QUE ACTUALIZA LA INFORMACION DEL ENFERMERO
        public bool ActualizarInfoEnfermero(string email, string telefono, string direccion)
        {
            return RCU12.ActualizarInfoEnfermero(email, telefono, direccion); 
        }

        //METODO PARA ACTUALIZAR LA INFORMACION DE UN PACIENTE EN CITA//
        public string ComplementarInfoPaciente(string cedula, string alergias, string rh, string problemasC)
        {
            return RCU7.ComplementarInfoPaciente(cedula, alergias, rh, problemasC);
        }

        // METODO QUE LISTA LOS ENFERMEROS PARA PEDIR SERVICIO //
        public LinkedList<TurnoEnfermero> ListarTurnosEnfermero(string tipoS, string cedulaEnfermero, int duracion)
        {
            return RCU8.ListarTurnosEnfermero(tipoS, cedulaEnfermero, duracion);
        }

        //METODO PARA RESERVAR SERVICIOS //
        public string ReservarServicio(string tipoServicio, int duracion, string enfermero, string paciente, int[] listaTurnos)
        {
            return RCU8.ReservarServicio(tipoServicio, duracion, enfermero, paciente, listaTurnos);
        }

		//METODO QUE LISTA LAS SOLICITUDES DE LOS ENFERMEROS//
		public LinkedList<Enfermero> ListarSolicitudesEnfermeros()
		{
			LinkedList<Enfermero> listaSolicitudesEnfermeros = new LinkedList<Enfermero>();
			listaSolicitudesEnfermeros = RCU5.ListarSolicitudesEnfermeros();

			return listaSolicitudesEnfermeros;
		}

		//METODO QUE MUESTRA LA INFO DE UNA SOLICITUD DE ENFERMERO//
		public string MostrarInfoEnfermero(string cedula)
		{
			return RCU5.MostrarInfoEnfermero(cedula);
		}

		//METODO QUE APRUEBA UNA SOLICITUD DE ENFERMERO//
		public Boolean AprobarEnfermero(string cedula)
		{
			return RCU5.AprobarEnfermero(cedula);
		}

		//METODO QUE RECHAZA UNA SOLICITUD DE ENFERMERO//
		public Boolean DesaprobarEnfermero(string cedula)
		{
			return RCU5.DesaprobarEnfermero(cedula);
		}

		//METODO QUE LISTA LAS SOLICITUDES DE LOS PACIENTES//
		public LinkedList<Paciente> ListarSolicitudesPacientes()
		{
			LinkedList<Paciente> listaSolicitudesPacientes = new LinkedList<Paciente>();
			listaSolicitudesPacientes = RCU4.ListarSolicitudesPacientes();

			return listaSolicitudesPacientes;
		}

		//METODO QUE MUESTRA LA INFO DE UNA SOLICITUD DE PACIENTE//
		public string MostrarInfoPaciente(string cedula)
		{
			return RCU4.MostrarInfoPaciente(cedula);
		}

		//METODO QUE APRUEBA UNA SOLICITUD DE PACIENTE//
		public Boolean AprobarPaciente(string cedula)
		{
			return RCU4.AprobarPaciente(cedula);
		}

		//METODO QUE RECHAZA UNA SOLICITUD DE PACIENTE//
		public Boolean DesaprobarPaciente(string cedula)
		{
			return RCU4.DesaprobarPaciente(cedula);
		}

        public LinkedList<Paciente> ListarPacientesPendientes()
        {
            return RCU6.ListarPacientesPendientes();
        }

        public LinkedList<Medico> ListarMedicos()
        {
            return RCU6.ListarMedicos();
        }

        public LinkedList<TurnoMedico> ListarTurnosMedico(string medico)
        {
            return RCU6.ListarTurnosMedico(medico);
        }

        public Boolean GuardarCita(string paciente, string medico, string idTurno)
        {
            int id = Convert.ToInt32(idTurno);
            return RCU6.GuardarCita(paciente, medico, id);
        }

		//METODO QUE LISTA LA INFORMACION DEL MEDICO PARA ACTUALIZAR
		public string listarInfoMedico(string email)
		{
			return RCU13.listarInfoMedico(email);
		}

		//MEOTOD QUE ACTUALIZA LA INFORMACION DEL MEDICO
		public bool ActualizarInfoMedico(string email, string telefono, string direccion)
		{
			return RCU13.ActualizarInfoMedico(email, telefono, direccion);
		}

		//METODO QUE LISTA LA INFORMACION DEL PACIENTE PARA ACTUALIZAR
		public string listarInfoPaciente(string email)
		{
			return RCU14.listarInfoPaciente(email);
		}
		//METODO QUE ACTUALIZA LA INFORMACION DEL PACIENTE
		public bool ActualizarInfoPaciente(string email, string telefono, string direccion)
		{
			return RCU14.ActualizarInfoPaciente(email, telefono, direccion);
		}
		public LinkedList<Medico> ListarMedicosAgTu()
		{
			return RCU11.ListarMedicosAgTu();
		}

		public LinkedList<Enfermero> ListarEnfermerosAgTu()
		{
			return RCU11.ListarEnfermerosAgTu();
		}

		public string AgendarTurnosMedico(string cedula, List<string> listaTurnos)
		{
			return RCU11.AgendarTurnosMedico(cedula, listaTurnos);
		}

		public string AgendarTurnosEnfermero(string cedula, List<string> listaTurnos)
		{
			return RCU11.AgendarTurnosEnfermero(cedula, listaTurnos);
		}

        public string ConsultarMedico(string cedula)
        {
            return RCU2.ConsultarMedico(cedula);
        }

		public LinkedList<TurnoEnfermero> ListarAgendaEnfermero(string email)
		{
			return RCU9.ListarAgendaEnfermero(email);
		}

	}
}