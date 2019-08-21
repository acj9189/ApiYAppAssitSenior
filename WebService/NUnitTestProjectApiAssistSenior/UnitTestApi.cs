using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AssistSenior.Controllers;
using AssistSenior.Models;
using Persistencia.Modelos;

namespace Tests
{
    public class Tests
    {


        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void cadoPruebaGetEnfermeroApiEqual()
        {
            EnfermeroController enfermeroController = new EnfermeroController();
            Assert.Equals(enfermeroController.Get(), consultaGETEnfermero());
        }
            

        [Test]
        public void cadoPruebaGetEnfermeroApiNotlNull()
        {
            EnfermeroController enfermeroController = new EnfermeroController();
            Assert.NotNull(enfermeroController.Get());
        }

        [Test]
        public void cadoPruebaGetEnfermeroApiException()
        {
            EnfermeroController enfermeroController = new EnfermeroController();
            Assert.That(enfermeroController.Get(), Throws.ArgumentException.With.Property("Message"));

        }

        [Test]
        public void cadoPruebaPutEnfermeroApiEqual()
        {
            turnoEnfermeroController turnoController = new turnoEnfermeroController();
            Assert.Equals(turnoController.Get("1054997625", System.DateTime.Now), consultaGETTurno("1054997625", System.DateTime.Now));
        }

        [Test]
        public void cadoPruebaPutEnfermeroApiEqualNotNull()
        {
            turnoEnfermeroController turnoController = new turnoEnfermeroController();
            Assert.NotNull(turnoController.Get("1054997625", System.DateTime.Now));
        }

        [Test]
        public void cadoPruebaPutEnfermeroApiNUll()
        {
            turnoEnfermeroController turnoController = new turnoEnfermeroController();
            Assert.Null(turnoController.Get("10549975", System.DateTime.Now));
        }



        private IEnumerable<Enfermero> consultaGETEnfermero()
        {
            assistseniorEntities BD = new assistseniorEntities();
            var enfermeros = (from e in BD.enfermero
                              join t in BD.turno_enfermero
                              on e.cedula equals t.ced_enfermero
                              where t.fecha > System.DateTime.Today
                              select new Enfermero() { nombre = e.nombre.ToUpper(), apellido = e.apellido.ToUpper(), cedula = e.cedula }).Distinct().ToList();


            return enfermeros;

        }

        private IEnumerable<turnoEnfermero> consultaGETTurno(string cedula, DateTime fecha)
        {
            assistseniorEntities BD = new assistseniorEntities();
            var turnosEnfermeros = BD.turno_enfermero
                .Where(enferm => enferm.estado == "Disponible")
                .Where(enferm => enferm.fecha == fecha)
                .Where(enferm => enferm.ced_enfermero == cedula).Select
                (e => new turnoEnfermero()
                {
                    fecha = e.fecha,
                    horaInicial = e.horaInicial,
                    horaFinal = e.horaFinal,
                    cedEnfermero = e.ced_enfermero
                })
                .ToList();

            return turnosEnfermeros;


        }

    }
}
