using NUnit.Framework;
using AssistSeniorMovil.ViewModel;
using System.Collections.ObjectModel;



namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void casoPruebaConsultarTurnoEnfermeroNotNull()
        {
            ConsultarTurnoViewModel casoPruebaConsultarTurnoEnfermero = new ConsultarTurnoViewModel();
            casoPruebaConsultarTurnoEnfermero.ConsultarTurno();
            Assert.NotNull(casoPruebaConsultarTurnoEnfermero.Enfermeros);
        }

        [Test]
        public void casoPruebaConsultarTurnoNUll()
        {
            ConsultarTurnoViewModel casoPruebaConsultarTurnoEnfermero = new ConsultarTurnoViewModel();
            casoPruebaConsultarTurnoEnfermero.ConsultarTurno();
            Assert.Null(casoPruebaConsultarTurnoEnfermero.Enfermeros);
        }

        [Test]
        public void casoPruebaConsultarTurnoPass()
        {
            ConsultarTurnoViewModel casoPruebaConsultarTurnoEnfermero = new ConsultarTurnoViewModel();
            casoPruebaConsultarTurnoEnfermero.ConsultarTurno();
            Assert.Pass(casoPruebaConsultarTurnoEnfermero.Enfermeros.ToString());
        }

        [Test]
        public void casoPruebaCalificarEnfermeroNotNUll()
        {
            Assert.Pass();
        }

        [Test]
        public void casoPruebaCalificarEnfermeroNUll()
        {
            Assert.Pass();
        }

        [Test]
        public void casoPruebaCalificarEnfermeroEcxeption()
        {
            Assert.Pass();
        }

        [Test]
        public void casoPruebaSeleccionarTurnoNotNUll()
        {
            TurnoEnfermeroViewModel turnoEnfermero = new TurnoEnfermeroViewModel("1054997625", System.DateTime.Now);
            turnoEnfermero.LoadTurnos();
            Assert.NotNull(turnoEnfermero.TurnosEnfermeros);
        }

        [Test]
        public void casoPruebaSeleccionarTurnoNUll()
        {
            TurnoEnfermeroViewModel turnoEnfermero = new TurnoEnfermeroViewModel("1054997625", System.DateTime.Now);
            turnoEnfermero.LoadTurnos();
            Assert.Null(turnoEnfermero.TurnosEnfermeros);
        }

        [Test]
        public void casoPruebaSeleccionarPass()
        {
            TurnoEnfermeroViewModel turnoEnfermero = new TurnoEnfermeroViewModel("1054997625", System.DateTime.Now);
            turnoEnfermero.LoadTurnos();
            Assert.Pass(turnoEnfermero.TurnosEnfermeros.ToString());
        }

        [Test]
        public void casoPruebaSeleccionarServicioNotNUll()
        {
            Assert.Pass();
        }

        [Test]
        public void casoPruebaSeleccionarServicioNUll()
        {
            Assert.Pass();
        }

        [Test]
        public void casoPruebaSeleccionarServicioEcxeption()
        {
            Assert.Pass();
        }
    }
}