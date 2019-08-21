using System;
using System.Collections.Generic;
using System.Text;


namespace Persistencia
{
    public interface IRepositorioAssistSenior
    {
        string Loguear(string Email, string Pass);
        string RegistrarEnfermeros(string nombre, string apellido, string cedula, int edad, string direccion, string telefono, DateTime fechaNacimiento, char sexo, string email);
        string RegistrarPaciente(string nombre, string apellido, string cedula, int edad, string direccion, string telefono, DateTime fechaNacimiento, char sexo, string email, string descripcion, string invalidez);
        string RegistrarCuenta(string email, string pass, string estado, char tipoCuenta);
        bool ActualizarInfoEnfermero(string email, string telefono, string direccion);
		bool ActualizarInfoMedico(string email, string telefono, string direccion);
		bool ActualizarInfoPaciente(string email, string telefono, string direccion);
		LinkedList<Tuple<int, string>> ListarEnfermeros();
        LinkedList<string> ListarServicios(string email);
        Tuple<string, LinkedList<string>> CancelarServicio(int idServicio);
        string ConsultarPaciente(string cedula, string email);
        string ComplementarInfoPaciente(string cedula, string alergias, string rh, string problemasC);
        string ConsultarMedico(string cedula);
        LinkedList<string> ListarMedicos();
		LinkedList<Tuple<int, string>> ListarMedicosCrud();
		LinkedList<string> ListarMedicosAgTu();
		LinkedList<string> ListarEnfermerosAgTu();
		string EliminarMedico(string cedula);
        string ActualizarMedico(string cedula, string nombre, string apellido, int edad, string direccion, string telefono, DateTime fechaNacimiento, char genero, string especialidad);
        string CrearMedico(string cedula, string nombre, string apellido, int edad, string direccion, string telefono, DateTime fechaNacimiento, char genero, string email, string especialidad);
        LinkedList<string> ListarSolicitudesEnfermeros();
        LinkedList<string> ListarSolicitudesPacientes();
        string MostrarInfoEnfermero(string cedula);
        string MostrarInfoPaciente(string cedula);
        Boolean CambiarEstadoCuentaEnfermero(String estado, string cedula);
        Boolean CambiarEstadoCuentaPaciente(String estado, string cedula);
        LinkedList<string> ListarTurnosEnfermero(string tipoS, string cedulaEnfermero, int duracion);
        LinkedList<string> ListarTurnosMedico(string cedulaMedico);
        Boolean GuardarCita(string paciente, string medico, int idTurno);
        string ReservarServicio(string tipoServicio, int duracion, string enfermero, string paciente, int[] listaTurnos);
        string listarInfoEnfermero(string email);
		string listarInfoMedico(string email);
		string listarInfoPaciente(string email);
		LinkedList<string> ListarPacientesPendientes();
		string AgendarTurnosMedico(string cedula, List<string> listaTurnos);
		string AgendarTurnosEnfermero(string cedula, List<string> listaTurnos);
		LinkedList<string> ListarAgendaEnfermero(string email);



	}
}
