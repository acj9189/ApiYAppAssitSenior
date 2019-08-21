using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dominio.LogicaNegocio;
using Dominio.EntidadesDominio;


namespace CasosPrueba
{
    [TestFixture]
    class Program
    {
        static void Main(string[] args)
        {
        }


        //Caso de prueba Test login para Pacientes

        [TestCase]
        public void Login()
        {
            AutenticarseController RCU1 = new AutenticarseController();
            Assert.AreEqual("Datos Correctos, Bienvenido P", RCU1.Login("stivenza96@gmail.com", "stiven123"));
        }



        [TestCase(1)]
        [TestCase("stivenza96@gmail.com", "stiven12")]
        [TestCase("stivenza96@gmail.com", "stiven123")]
        public void Login2(string email, string pass)
        {
            AutenticarseController RCU1 = new AutenticarseController();
            Assert.AreEqual("Datos Correctos, Bienvenido P", RCU1.Login(email, pass));
        }

        //Caso de prueba para editar la información del perfil del enfermero
        [TestCase("alberto123@gmail.com", "8832443", "calle 10 #40-21")]
        [TestCase("alberto3@gmail.com", "8832443", "calle 10 #40-21")]
        public void ActualizarInfoEnfermero(string email, string telefono, string direccion)
        {
            ActualizarInfoEnfermeroController RCU12 = new ActualizarInfoEnfermeroController();
            Assert.AreEqual(true, RCU12.ActualizarInfoEnfermero(email, telefono, direccion));
        }

        //Caso de prueba para editar la información del perfil del Médico
        [TestCase("pepitoperez@gmail.com", "8832443", "calle 10 #40-22")]
        [TestCase("pepitoperz@gmail.com", "8832443", "calle 10 #40-22")]
        public void ActualizarInfoMedico(string email, string telefono, string direccion)
        {
            ActualizarInfoMedicoController RCU13 = new ActualizarInfoMedicoController();
            Assert.AreEqual(true, RCU13.ActualizarInfoMedico(email, telefono, direccion));
        }

        //Caso de prueba para editar la información del perfil del Paciente
        [TestCase("stivenza96@gmail.com", "8832443", "calle 10 #40-19")]
        [TestCase("stivena96@gmail.com", "8832443", "calle 10 #40-19")]
        public void ActualizarInfoPaciente(string email, string telefono, string direccion)
        {
            ActualizarInfoPacienteController RCU14 = new ActualizarInfoPacienteController();
            Assert.AreEqual(true, RCU14.ActualizarInfoPaciente(email, telefono, direccion));
        }

        [TestCase("1053847680", "acetaminofen", "A+", "Asma")]
        [TestCase("1053847680", 11, "A+", "Asma")]
        [TestCase(11, "Acetaminofen", "A+", "Asma")]
        public void ComplementarInfoPaciente(string cedula, string alergias, string rh, string problemasC)
        {
            ComplementarInformacionController RCU7 = new ComplementarInformacionController();
            Assert.AreEqual("Ok", RCU7.ComplementarInfoPaciente(cedula, alergias, rh, problemasC));

        }


        [TestCase("Caminar", 3, "105768909", "juanmunoz@gmail.com")]
        [TestCase("Caminar", 3, "105768909", "juaw@gmail.com")]
        public void ReservarServicio(string tipoServicio, int duracion, string enfermero, string paciente)
        {
            ReservarServicioController RCU8 = new ReservarServicioController();
            int[] listaTurnos = new int[2];
            listaTurnos[0] = 1;//Cambiar cada que se ejecuta la prueba
            listaTurnos[1] = 2;//Cambiar cada que se ejecuta la prueba

            Assert.AreEqual("Ok", RCU8.ReservarServicio(tipoServicio, duracion, enfermero, paciente, listaTurnos));
        }

        [TestCase(7)]
        [TestCase(1000)]
        public void CancelarServicio(int idServicio)
        {
            CancelarServicioController RCU10 = new CancelarServicioController();
            Tuple<string, LinkedList<Servicio>> l;
            l = RCU10.CancelarServicio(idServicio);
            Assert.AreEqual("Ok", l.Item1);
        }

        //CASOS DE PRUEBA PARA EL CASO DE USO 4 - APROBAR PACIENTES//
        [TestCase]
        public void MostrarInfoPacienteValido()
        {
            AprobarSolicitudPacienteController RCU4 = new AprobarSolicitudPacienteController();
            Assert.AreNotEqual("no existe la solicitud buscada", RCU4.MostrarInfoPaciente("1053847680"));
        }

        [TestCase]
        public void MostrarInfoPacienteInvalido()
        {
            AprobarSolicitudPacienteController RCU4 = new AprobarSolicitudPacienteController();
            Assert.AreEqual("no existe la solicitud buscada", RCU4.MostrarInfoPaciente("abc"));
        }

        [TestCase]
        public void AprobarPacienteInvalido()
        {
            bool var = true;
            AprobarSolicitudPacienteController RCU4 = new AprobarSolicitudPacienteController();
            Assert.AreNotEqual(var, RCU4.AprobarPaciente("87"));
        }

        //CASOS DE PRUEBA PARA EL CASO DE USO 5 - APROBAR ENFERMEROS//
        public void MostrarInfoEnfermeroValido()
        {
            AprobarSolicitudEnfermeroController RCU5 = new AprobarSolicitudEnfermeroController();
            Assert.AreNotEqual("no existe la solicitud buscada", RCU5.MostrarInfoEnfermero("354"));
        }

        [TestCase]
        public void MostrarInfoEnfermeroInvalido()
        {
            AprobarSolicitudEnfermeroController RCU5 = new AprobarSolicitudEnfermeroController();
            Assert.AreEqual("no existe la solicitud buscada", RCU5.MostrarInfoEnfermero("aleja"));
        }

        [TestCase]
        public void AprobarEnfermeroInvalido()
        {
            bool var = true;
            AprobarSolicitudEnfermeroController RCU5 = new AprobarSolicitudEnfermeroController();
            Assert.AreNotEqual(var, RCU5.AprobarEnfermero("hola"));
        }

        //CASOS DE PRUEBA PARA EL CASO DE USO 6 - AGENDAR CITA PACIENTE//

        [TestCase("8347384")]
        [TestCase("rocco123")]
        public void ListarTurnosMedicoTest(string cedula)
        {
            AgendarCitaPacienteController RCU6 = new AgendarCitaPacienteController();
            Assert.AreNotEqual(0, RCU6.ListarTurnosMedico(cedula).Count);
        }

        [TestCase("8347384", "8347384", 1)] //paciente invalido
        [TestCase("1053847680", "hola", 9)] //medico invalido
        [TestCase("1053847680", "8347384", 11)] //turno invalido
        [TestCase("875645", "8347384", 9)] //valido
        public void GuardarCitaTest(string paciente, string medico, int idTurno)
        {
            bool var = true;
            AgendarCitaPacienteController RCU6 = new AgendarCitaPacienteController();
            Assert.AreEqual(var, RCU6.GuardarCita(paciente, medico, idTurno));
        }

    }
}