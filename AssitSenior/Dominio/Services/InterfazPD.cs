using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dominio.EntidadesDominio;

namespace Dominio.Services
{
    public interface InterfazPD
    {
        string Login(string Email, string Pass);
        string RegistrarEnfermeros(string nombre, string apellido, string cedula, int edad, string direccion, string telefono, DateTime fechaNacimiento, char sexo, string email, string password);
        string RegistrarPaciente(string nombre, string apellido, string cedula, int edad, string direccion, string telefono, DateTime fechaNacimiento, char sexo, string email, string password, string descripcion, string invalidez);
		string CrearMedico(string cedula, string nombre, string apellido, int edad, string direccion, string telefono, DateTime fechaNacimiento, char genero, string email, string especialidad, string password, string estado, char tipocuenta);
		string EliminarMedico(string cedula);
		string ConsultarMedico(string cedula);
		LinkedList<Enfermero> ListarEnfermeros();
        LinkedList<Servicio> ListarServicios(string email);
		Tuple<string, LinkedList<Servicio>> CancelarServicio(int idServicio);
        string ConsultarPaciente(string cedula, string email);
        string ComplementarInfoPaciente(string cedula, string alergias, string rh, string problemasC);
        LinkedList<TurnoEnfermero> ListarTurnosEnfermero(string tipoS, string cedulaEnfermero, int duracion);
        string ReservarServicio(string tipoServicio, int duracion, string enfermero, string paciente, int[] listaTurnos);
        string listarInfoEnfermero(string email);
        bool ActualizarInfoEnfermero(string email, string telefono, string direccion);
        string listarInfoMedico(string email);
		string ActualizarMedico(string cedula, string nombre, string apellido, int edad, string direccion, string telefono, DateTime fechaNacimiento, char genero, string especialidad);
        bool ActualizarInfoMedico(string email, string telefono, string direccion);
        string listarInfoPaciente(string email);
        bool ActualizarInfoPaciente(string email, string telefono, string direccion);
        LinkedList<Enfermero> ListarSolicitudesEnfermeros();
        LinkedList<Paciente> ListarSolicitudesPacientes();
        LinkedList<Paciente> ListarPacientesPendientes();
        LinkedList<Medico> ListarMedicos();
		LinkedList<Medico> ListarMedicosCrud();
		LinkedList<Medico> ListarMedicosAgTu();
		LinkedList<Enfermero> ListarEnfermerosAgTu();
		LinkedList<TurnoMedico> ListarTurnosMedico(string medico);
        Boolean GuardarCita(string paciente, string medico, string idTurno);
        string MostrarInfoEnfermero(string cedula);
        string MostrarInfoPaciente(string cedula);
        Boolean AprobarEnfermero(string cedula);
        Boolean DesaprobarEnfermero(string cedula);
        Boolean AprobarPaciente(string cedula);
        Boolean DesaprobarPaciente(string cedula);
		string AgendarTurnosMedico(string cedula, List<string> listaTurnos);
		string AgendarTurnosEnfermero(string cedula, List<string>listaTurnos);
		LinkedList<TurnoEnfermero> ListarAgendaEnfermero(string email);
	}

}
