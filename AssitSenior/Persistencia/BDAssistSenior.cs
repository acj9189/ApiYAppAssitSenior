using System;
using System.Collections.Generic;
using System.Configuration;

using Newtonsoft.Json;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Persistencia
{
    public class BDAssistSenior : IRepositorioAssistSenior
    {

        assistseniorEntities db = new assistseniorEntities();

        public string Loguear(string Email, string Password)
        {
            string Mensaje = "";

            try
            {
				cuenta_usuario cuentaSql = db.cuenta_usuario.Where(c => c.email == Email).First();

                if (cuentaSql.password == Password)
                {
                    if (!cuentaSql.estado.Equals("Inactivo"))
                    {
                        Mensaje = "Datos Correctos, Bienvenido " + cuentaSql.tipo;
                    }

                    else
                    {
                        Mensaje = "Su Solicitud Esta en Proceso, Agradecemos su espera";
                    }
                }

                else
                {
                    Mensaje = "Contrasena Incorrecta, Verifique Por Favor";
                }
            }

            catch (Exception e)
            {
                Mensaje = "La cuenta ingresada no existe verifique por favor " + e.Message;
            }

            return Mensaje;
        }

        // METODO QUE LISTA LOS ENFERMEROS PARA EL PACIENTE PEDIR EL SERVICIO //
        public LinkedList<Tuple<int, string>> ListarEnfermeros()
        {
            string Mensaje = "";

            LinkedList<Tuple<int, string>> listaEnfermeros = new LinkedList<Tuple<int, string>>();

            try
            {
                var enfermeros = (from e in db.enfermero
                                  join turno in db.turno_enfermero on e.cedula equals turno.ced_enfermero
                                  where turno.fecha.Day >= DateTime.Now.Day
                                  select new
                                  {
                                      cedula = e.cedula,
                                      nombre = e.nombre,
                                      apellido = e.apellido

                                  }).Distinct();


                foreach (var i in enfermeros)
                {
                    var Tupla = new Tuple<int, string>(Convert.ToInt32(i.cedula), i.nombre + " " + i.apellido);
                    listaEnfermeros.AddLast(Tupla);
                }

            }

            catch (Exception e)
            {
                Mensaje = "Error listando los enfermeros ";
            }

            return listaEnfermeros;
        }

        //METODO QUE LISTA LOS TURNOS DEL ENFERMERO ESCODIGO //
        public LinkedList<string> ListarTurnosEnfermero(string tipoS, string cedulaEnfermero, int duracion)
        {
            LinkedList<string> OutputJson = new LinkedList<string>();

            try
            {
                int horaComparar = Convert.ToInt32(DateTime.Now.Hour);
                horaComparar = horaComparar + 3;

                var listaTurnos = from turnoe in db.turno_enfermero
								  where turnoe.ced_enfermero == cedulaEnfermero
								  orderby turnoe.horaInicial
                                  select turnoe;

                if (listaTurnos != null)
                {
                    foreach (turno_enfermero i in listaTurnos)
                    {
                        if (i.fecha.ToShortDateString().Equals(DateTime.Now.ToShortDateString()))
                        {
                            if (horaComparar <= (int)i.horaInicial.Hours)
                            {
                                var obj = new
                                {
                                    idTurno = i.idTurno,
                                    fecha = i.fecha,
                                    horaInicial = i.horaInicial,
                                    horaFinal = i.horaFinal,
                                    estado = i.estado,
                                    ced_enfermero = i.ced_enfermero
                                };

                                OutputJson.AddLast(JsonConvert.SerializeObject(obj));
                            }
                        }

                        else
                        {
                            var obj = new
                            {
                                idTurno = i.idTurno,
                                fecha = i.fecha,
                                horaInicial = i.horaInicial,
                                horaFinal = i.horaFinal,
                                estado = i.estado,
                                ced_enfermero = i.ced_enfermero
                            };

                            OutputJson.AddLast(JsonConvert.SerializeObject(obj));
                        }


                    }
                }
            }

            catch (Exception e)
            {
                string Mensaje = "ERROR";
            }

            return OutputJson;
        }

        //METODO QUE LISTA LOS TURNOS DISPONIBLES DE UN MEDICO //
        public LinkedList<string> ListarTurnosMedico(string cedulaMedico)
        {
            LinkedList<string> OutputJson = new LinkedList<string>();
            string Mensaje = "";

            try
            {
                var listaTurnos = from turnoM in db.turno_medico
								  where turnoM.ced_medico == cedulaMedico
								  orderby turnoM.fecha,turnoM.horaInicial
                                  select turnoM;
                
                if (listaTurnos != null)
                {
                    foreach (turno_medico i in listaTurnos)
                    {
                        var obj = new
                        {
                            idTurno = i.idTurno,
                            fecha = i.fecha,
                            horaInicial = i.horaInicial,
                            horaFinal = i.horaFinal,
                            estado = i.estado,
                            ced_enfermero = i.ced_medico
                        };
                        OutputJson.AddLast(JsonConvert.SerializeObject(obj));
                    }
                }
                else
                {
                    Mensaje = "turnos de medico nulos";
                }

            }

            catch (Exception e)
            {
                Mensaje = "no existen turnos para este medico";
            }
            return OutputJson;
        }

        //METODO QUE AGENDA UNA VALORACION DE PRIMERA VEZ//
        public Boolean GuardarCita(string paciente, string medico, int idTurno)
        {
            Boolean mensaje = true;
			try
			{
                paciente pac = db.paciente.Where(p => p.cedula == paciente).First();
                if (pac != null)
                {
                    medico med = db.medico.Where(m => m.cedula == medico).First();
                    if (med != null)
                    {
                        turno_medico tur = db.turno_medico.Where(t => t.idTurno == idTurno).First();
                        if( tur.estado == "Disponible"){
                            tur.estado = "Ocupado";
                            db.SaveChanges();

                            string idCita = db.cita_valoracion.Max(t => t.idCita);

                            int num = Convert.ToInt32(idCita);
                            num = num + 1;

                            cita_valoracion cita = new cita_valoracion()
                            {
                                idCita = Convert.ToString(num),
                                ced_paciente = paciente,
                                ced_medico = medico,
                                fecha = tur.fecha,
                                hora = tur.horaInicial
                            };

							db.cita_valoracion.Add(cita);
							db.SaveChanges();
                        }
                        else
                        {
                            mensaje = false;
                        }
                    }
                    else
                    {
                        mensaje = false;
                    }
                }
                else
                {
                    mensaje = false;
                }

                return mensaje;
            }

			catch(Exception e)
			{
				return false;
			}
           
        }
               
        //METODO REGISTRAR EN LA CUENTA //
        public string RegistrarCuenta(string email, string pass, string estado, char tipoCuenta)
        {
            string Mensaje = "";

            cuenta_usuario cu = new cuenta_usuario()
            {
                email = email,
                password = pass,
                estado = estado,
                tipo = Convert.ToString(tipoCuenta)
            };

            try
            {
                db.cuenta_usuario.Add(cu);
                db.SaveChanges();
                Mensaje = "registro cuenta OK";
            }

            catch (DbUpdateException e)
            {
                Mensaje = "Error al registrar la cuenta";
            }

            return Mensaje;
        }

        //METODO REGISTRAR ENFERMERO //
        public string RegistrarEnfermeros(string nombre, string apellido, string cedula, int edad, string direccion, string telefono, DateTime fechaNacimiento, char sexo, string email)
        {
            string Mensaje = "";

            enfermero e = new enfermero()
            {
                nombre = nombre,
                apellido = apellido,
                cedula = cedula,
                edad = edad,
                direccion = direccion,
                telefono = telefono,
                fechaNacimiento = fechaNacimiento,
                genero = Convert.ToString(sexo),
                email = email
            };

            try
            {
                db.enfermero.Add(e);
                db.SaveChanges();
                Mensaje = "registro enfermero OK";
            }

            catch (DbUpdateException error)
            {
                Mensaje = "Error registrando el enfermero, verifique por favor";
            }

            return Mensaje;
        }

		//METODO REGISTRAR PACIENTE //
		public string RegistrarPaciente(string nombre, string apellido, string cedula, int edad, string direccion, string telefono, DateTime fechaNacimiento, char sexo, string email, string descripcion, string invalidez)
		{
			string Mensaje = "";

			paciente p = new paciente()
			{
				cedula = cedula,
				nombre = nombre,
				apellido = apellido,
				edad = edad,
				direccion = direccion,
				telefono = telefono,
				fecha_nacimiento = fechaNacimiento,
				genero = Convert.ToString(sexo),
				email = email,
				descripcion_Paciente = descripcion,
				invalidez = invalidez

			};

			try
			{
				db.paciente.Add(p);
				db.SaveChanges();
				Mensaje = "registro paciente OK";
			}

			catch (Exception e)
			{
				Mensaje = "Error registrando el paciente, verifique por favor";
			}

			return Mensaje;
		}

        //METODO PARA LISTAR LA INFO DEL ENFERMERO PARA ACTUALIZAR
        public string listarInfoEnfermero(string email)
        {
            string json = "";

            try
            {
                var e = (from en in db.enfermero
                         where en.email == email
                         select en).First();

                var obj = new
                {
                    cedula = e.cedula,
                    nombre = e.nombre,
                    apellido = e.apellido,
                    edad = e.edad,
                    direccion = e.direccion,
                    telefono = e.telefono,
                    fechaNacimiento = e.fechaNacimiento,
                    genero = e.genero,
                    email = e.email,

                };
                json = JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
            }

            return json;
        }

        //METODO QUE ACTUALIZA LA INFORMACION DEL ENFERMERO//
        public bool ActualizarInfoEnfermero(string email, string telefono, string direccion)
        {
            bool bandera = false;
            
            try
            {
                var enfermero = db.enfermero.SingleOrDefault(e => e.email == email);
                if (enfermero != null)
                {
                    //se actualizan los campos necesarios para enviar a la BD //
                    enfermero.telefono = telefono;
                    enfermero.direccion = direccion;
                    

                    db.SaveChanges();

                    bandera = true;
                }
                else
                {
                    bandera = false;
                }


            }
            catch (Exception e)
            {
                bandera = false;
            }
            return bandera;
        }
        //METODO QUE LISTA LA INFORMACION DEL MEDICO PARA ACTUALIZAR
        public string listarInfoMedico(string email)
        {
            string json = "";

            try
            {
                var e = (from en in db.medico
                         where en.email == email
                         select en).First();

                var obj = new
                {
                    cedula = e.cedula,
                    nombre = e.nombre,
                    apellido = e.apellido,
                    edad = e.edad,
                    direccion = e.direccion,
                    telefono = e.telefono,
                    fechaNacimiento = e.fechaNacimiento,
                    genero = e.genero,
                    email = e.email,

                };
                json = JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
            }

            return json;
        }


        //METODO QUE ACTUALIZA LA INFORMACION DEL MEDICO//
        public bool ActualizarInfoMedico(string email, string telefono, string direccion)
        {
            bool bandera = false;
            try
            {
                var medico = db.medico.SingleOrDefault(m => m.email == email);
                if (medico != null)
                {
                    //se actualizan los campos necesarios para enviar a la BD //
                    medico.telefono = telefono;
                    medico.direccion = direccion;

                    db.SaveChanges();

                    bandera = true;
                }
                else
                {
                    bandera = false;
                }


            }
            catch (Exception e)
            {
              
            }
            return bandera;
        }
        //METODO QUE LISTA LA INFORMACION DEL PACIENTE PARA ACTUALIZAR
        public string listarInfoPaciente(string email)
        {
            string json = "";

            try
            {
                var e = (from en in db.paciente 
                         where en.email == email
                         select en).First();

                var obj = new
                {
                    cedula = e.cedula,
                    nombre = e.nombre,
                    apellido = e.apellido,
                    edad = e.edad,
                    direccion = e.direccion,
                    telefono = e.telefono,
                    genero = e.genero,
                    email = e.email,

                };
                json = JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
            }

            return json;
        }


        //METODO QUE ACTUALIZA LA INFORMACION DEL PACIENTE//
        public bool ActualizarInfoPaciente(string email, string telefono, string direccion)
        {
            bool bandera= false;
            try
            {
                var paciente = db.paciente.SingleOrDefault(p => p.email == email);
                if (paciente != null)
                {
                    //se actualizan los campos necesarios para enviar a la BD //
                    paciente.telefono = telefono;
                    paciente.direccion = direccion;
                    db.SaveChanges();

                    bandera = true;
                }
                else
                {
                    bandera = false;
                }
            }
            catch (Exception e)
            {
            }
            return bandera;
        }

        //METODO QUE CONSULTA LA INFO DEL PACIENTE DADA LA CEDULA RCU7//
        public string ConsultarPaciente(string cedula, string email)
        {
            string OutputJson = "";
            string Mensaje = "";

			try
			{
				try
				{
					var cedMedico = db.medico.SingleOrDefault(c => c.email == email).cedula;

					cita_valoracion hayCita = db.cita_valoracion.Where(c => (c.ced_medico == cedMedico))
											  .Where(c => (c.ced_paciente == cedula)).First();
				}
				
				catch(Exception e)
				{
					OutputJson = "No hay cita";
					return OutputJson;
				}

			
				var pa = (from p in db.paciente
						  join cu in db.cuenta_usuario on p.email equals cu.email
						  where p.cedula == cedula && cu.estado.Trim() == "Agendado"
						  select p).First();


				var obj = new
				{
					cedula = pa.cedula,
					nombre = pa.nombre,
					apellido = pa.apellido,
					edad = pa.edad,
					direccion = pa.direccion,
					telefono = pa.telefono,
					fechaNacimiento = pa.fecha_nacimiento,
					genero = pa.genero,
					email = pa.email,
					invalidez = pa.invalidez,
					alergias = pa.alergias,
					rh = pa.rh,
					problemasC = pa.p_Coronarios,
					descripcion = pa.descripcion_Paciente
				};

				OutputJson = JsonConvert.SerializeObject(obj);
			}

            catch (Exception e)
            {
                Mensaje = "Error en la consulta";
            }

            return OutputJson;
        }

        //METODO QUE ACTUALIZA LA INFORMACION DEL PACIENTE EN LA CITA //
        public string ComplementarInfoPaciente(string cedula, string alergias, string rh, string problemasC)
        {
            string Mensaje = "";
				try
				{
					var paciente = db.paciente.SingleOrDefault(p => p.cedula == cedula);

					if (paciente != null)
					{
						//se actualizan los campos necesarios para enviar a la BD //
						paciente.alergias = alergias;
						paciente.rh = rh;
						paciente.p_Coronarios = problemasC;

						try
						{
							var cuenta = db.cuenta_usuario.SingleOrDefault(cu => cu.email == paciente.email);
							cuenta.estado = "Activo";
							db.SaveChanges();
							Mensaje = "Ok";
						}
						catch (Exception e)
						{
							Mensaje = "problemas al actualizar la cuenta del paciente ";
							return Mensaje;
						}
					}
					else
					{
						Mensaje = "cedula no existe";
					}
				}

				catch (Exception e)
				{
					Mensaje = "problemas al actualizar paciente ";
					return Mensaje;
				}
			return Mensaje;

        }

        //METODO QUE LISTA LAS SOLICITUDES DE ENFERMEROS //
        public LinkedList<string> ListarSolicitudesEnfermeros()
        {

            LinkedList<string> OutputJson = new LinkedList<string>();
            string Mensaje = "";

            try
            {
                var solEnfermeros = from e in db.enfermero
                                    join cu in db.cuenta_usuario on e.email equals cu.email
                                    where cu.estado == "Inactivo"
                                    select e;

                if (solEnfermeros != null)
                {
                    foreach (enfermero i in solEnfermeros)
                    {
                        var obj = new
                        {
                            cedula = i.cedula,
                            nombre = i.nombre,
                            apellido = i.apellido,
                            edad = i.edad,
                            direccion = i.direccion,
                            telefono = i.telefono,
                            fechaNacimiento = i.fechaNacimiento,
                            genero = i.genero,
                            email = i.email
                        };

                        OutputJson.AddLast(JsonConvert.SerializeObject(obj));
                    }
                }
                else
                {
                    Mensaje = "enfermeros nulos";
                }

            }

            catch (Exception e)
            {
                Mensaje = "no existen enfermeros inactivos";
            }

            return OutputJson;
        }

        //METODO QUE LISTA LAS SOLICITUDES DE PACIENTES//
        public LinkedList<string> ListarSolicitudesPacientes()
        {

            LinkedList<string> OutputJson = new LinkedList<string>();
            string Mensaje = "";

            try
            {
                var solPacientes = from p in db.paciente
                                   join cu in db.cuenta_usuario on p.email equals cu.email
                                   where cu.estado == "Inactivo"
                                   select p;

                foreach (paciente i in solPacientes)
                {
                    var obj = new
                    {
                        cedula = i.cedula,
                        nombre = i.nombre,
                        apellido = i.apellido,
                        edad = i.edad,
                        direccion = i.direccion,
                        telefono = i.telefono,
                        fechaNacimiento = i.fecha_nacimiento,
                        genero = i.genero,
                        email = i.email,
						invalidez = i.invalidez
                    };

                    OutputJson.AddLast(JsonConvert.SerializeObject(obj));
                }
            }

            catch (Exception e)
            {
                Mensaje = "no existen pacientes inactivos";
            }

            return OutputJson;
        }
        
        //METODO QUE BUSCA LA INFORMACION DE UNA SOLICITUD DE PACIENTE EN PARTICULAR //
        public string MostrarInfoPaciente(string cedula)
        {
            string OutputJson = "";            

            try
            {
                var paciente = db.paciente.SingleOrDefault(p => p.cedula == cedula);
                if (paciente != null)
                {
					var obj = new
					{
						cedula = paciente.cedula,
						nombre = paciente.nombre,
						apellido = paciente.apellido,
						edad = paciente.edad,
						direccion = paciente.direccion,
						telefono = paciente.telefono,
						fechaNacimiento = paciente.fecha_nacimiento,
						genero = paciente.genero,
						email = paciente.email,
						invalidez = paciente.invalidez,
						descripcion = paciente.descripcion_Paciente
                    };

                    OutputJson = JsonConvert.SerializeObject(obj);
                }
                else
                {
                    OutputJson = "no existe la solicitud buscada";
                }
            }
            catch (Exception e)
            {
                OutputJson = "Error en la consulta" + e.Message;
            }
            return OutputJson;
        }

        //METODO QUE BUSCA LA INFORMACION DE UNA SOLICITUD DE ENFERMERO EN PARTICULAR //
        public string MostrarInfoEnfermero(string cedula)
        {
            string OutputJson = "";            

            try
            {
                var enfermero = db.enfermero.SingleOrDefault(e => e.cedula == cedula);
                if (enfermero != null)
                {
                    var obj = new
                    {
                        cedula = enfermero.cedula,
                        nombre = enfermero.nombre,
                        apellido = enfermero.apellido,
                        edad = enfermero.edad,
                        direccion = enfermero.direccion,
                        telefono = enfermero.telefono,
                        fechaNacimiento = enfermero.fechaNacimiento,
                        genero = enfermero.genero,
                        email = enfermero.email
                    };

                    OutputJson = JsonConvert.SerializeObject(obj);
                }
                else
                {
                    OutputJson = "no existe la solicitud buscada";
                }
            }
            catch (Exception e)
            {
                OutputJson = "Error en la consulta" + e.Message;
            }
            return OutputJson;
        }

        //CAMBIA EL ESTADO DE UNA SOLICITUD DE ENFERMERO A APROBADA O A RECHAZADA //
        public Boolean CambiarEstadoCuentaEnfermero(string estado, string cedula)
        {
            string mensaje = "";
            try
            {
                var enfermero = db.enfermero.SingleOrDefault(e => e.cedula == cedula);
                if (enfermero != null)
                {
                    var cuenta = db.cuenta_usuario.SingleOrDefault(cu => cu.email == enfermero.email);
                    cuenta.estado = estado;
                    db.SaveChanges();
                }
                else
                {
                    mensaje = "cedula invalida";
                    return false;
                }

            }
            catch (Exception e)
            {
                mensaje = "Error cambiando estado de enfermero" + e.Message;
                return false;
            }

            return true;
        }

        //CAMBIA EL ESTADO DE UNA SOLICITUD DE PACIENTE A PENDIENTE O A RECHAZADA
        public Boolean CambiarEstadoCuentaPaciente(string estado, string cedula)
        {
            string mensaje = "";
            try
            {
                var paciente = db.paciente.SingleOrDefault(p => p.cedula == cedula);
                if (paciente != null)
                {
                    var cuenta = db.cuenta_usuario.SingleOrDefault(cu => cu.email == paciente.email);
                    cuenta.estado = estado;
                    db.SaveChanges();
                }
                else
                {
                    mensaje = "cedula invalida";
                    return false;
                }

            }
            catch (Exception e)
            {
                mensaje = "Error cambiando estado de paciente" + e.Message;
                return false;
            }

            return true;
        }

        //METODO QUE LISTA LOS PACIENTES SIN VALORACION DE PRIMERA VEZ//
        public LinkedList<string> ListarPacientesPendientes()
        {
            LinkedList<string> OutputJson = new LinkedList<string>();
            string Mensaje = "";

            try
            {
                var solPacientes = from p in db.paciente
                                   join cu in db.cuenta_usuario on p.email equals cu.email
                                   where cu.estado == "Pendiente"
                                   select p;

                foreach (paciente i in solPacientes)
                {
                    var obj = new
                    {
                        cedula = i.cedula,
                        nombre = i.nombre,
                        apellido = i.apellido,
                        edad = i.edad,
                        direccion = i.direccion,
                        telefono = i.telefono,
                        fechaNacimiento = i.fecha_nacimiento,
                        genero = i.genero,
                        email = i.email,
                        invalidez = i.invalidez
                    };

                    OutputJson.AddLast(JsonConvert.SerializeObject(obj));
                }
            }

            catch (Exception e)
            {
                Mensaje = "no existen pacientes Pendientes de valoración";
            }

            return OutputJson;
        }

        //METODO QUE LISTA LOS MEDICOS CON AL MENOS UN TURNO DISPONIBLE//
        public LinkedList<string> ListarMedicos()
        {
            string Mensaje = "";

            LinkedList<string> listaMedicos = new LinkedList<string>();

            try
            {

                var medicos = (from m in db.medico
                              join t in db.turno_medico on m.cedula equals t.ced_medico
                              where t.estado == "Disponible" 
							  && t.fecha.Day > DateTime.Now.Day
							  select m).Distinct();


                if (medicos != null)
                {
                    foreach (medico i in medicos)
                    {
                        var obj = new
                        {
                            cedula= i.cedula,
                            nombre= i.nombre,
                            apellido= i.apellido,
                            edad = i.edad,
                            direccion= i.direccion,
                            telefono= i.telefono,
                            fechaNacimiento= i.fechaNacimiento,
                            genero= i.genero,
                            email= i.email
                        };
                        listaMedicos.AddLast(JsonConvert.SerializeObject(obj));
                    }
                }
                else
                {
                    Mensaje = "No hay medicos";
                }

            }

            catch (Exception e)
            {
                Mensaje = "Error listando medicos ";
            }

            return listaMedicos;
        }
               
        public string ActualizarMedico(string cedula, string nombre, string apellido, int edad, string direccion, string telefono, DateTime fechaNacimiento, char genero, string especialidad)
        {
            string mensaje = "";
            try
            {
                var medico = db.medico.SingleOrDefault(m => m.cedula == cedula);
                if (medico != null)
                {
                    //se actualizan los campos necesarios para enviar a la BD //
                    medico.telefono = telefono;
                    medico.direccion = direccion;
                    medico.nombre = nombre;
                    medico.apellido = apellido;
                    medico.edad = edad;
                    medico.fechaNacimiento = fechaNacimiento;
                    medico.genero = Convert.ToString(genero);
                    medico.especialidad = especialidad;





                    db.SaveChanges();

                    mensaje = "datos actualizados correctamente";
                }
                else
                {
                    mensaje = "no existe el enfermero";
                }


            }
            catch (Exception e)
            {
                mensaje = "Error en la consulta" + e.Message;
            }
            return mensaje;
        }

        public string CrearMedico(string cedula, string nombre, string apellido, int edad, string direccion, string telefono, DateTime fechaNacimiento, char genero, string email, string especialidad)
        {
            string Mensaje = "";
            medico m = new medico()

            {
                nombre = nombre,
                apellido = apellido,
                cedula = cedula,
                edad = edad,
                direccion = direccion,
                telefono = telefono,
                fechaNacimiento = fechaNacimiento,
                genero = Convert.ToString(genero),
                email = email,
                especialidad = especialidad
            };

            try
            {
                db.medico.Add(m);
                db.SaveChanges();
                Mensaje = "registro Medico OK";
            }

            catch (DbUpdateException error)
            {
                Mensaje = "Error registrando el Medico, verifique por favor";
            }

            return Mensaje;
        }

        public string EliminarMedico(string cedula)
        {

            string Mensaje;

            try
            {
                var cus = (from cu in db.cuenta_usuario
                           join m in db.medico on cu.email equals m.email
                           where m.cedula == cedula
                           select cu).First();

                if (cus != null)
                {
                    cus.estado = "Inactivo";
                    db.SaveChanges();

                    Mensaje = "el usuario se elimino";
                }

                else
                {
                    Mensaje = "El medico" + cedula + "no existe";
                }

            }
            catch (Exception e)
            {

                Mensaje = "Error con";

            }
            return Mensaje;
        }

        public string ConsultarMedico(string cedula)
        {


            string OutputJson = "";

            var me = db.medico.SingleOrDefault(m => m.cedula == cedula);
            var obj = new
            {
                cedula = me.cedula,
                nombre = me.nombre,
                apellido = me.apellido,
                edad = me.edad,
                direccion = me.direccion,
                telefono = me.telefono,
                fechaNacimiento = me.fechaNacimiento,
                genero = me.genero,
                email = me.email,
                especialidad = me.especialidad
            };

            OutputJson = JsonConvert.SerializeObject(obj);

            return OutputJson;
        }

        //METODO QUE RESERVA SERVICIOS POR PARTE DEL PACIENTE //
        public string ReservarServicio(string tipoServicio, int duracion, string enfermero, string paciente, int[] listaTurnos)
        {

            string Mensaje = "";
            TimeSpan horaMenor = new TimeSpan(20, 00, 00);

            turno_enfermero Turno = new turno_enfermero();

            try
            {
                for (int i = 0; i < listaTurnos.Length; i++)
                {
                    var x = listaTurnos[i];
                    Turno = db.turno_enfermero.Where(t => t.idTurno == x).First();
                    Turno.estado = "Pedido";

                    if (Turno.horaInicial < horaMenor)
                    {
                        horaMenor = Turno.horaInicial;
                    }

                    db.SaveChanges(); // Cambia el estado del turno a pedido  
                }


                int id = db.servicio.Count();

                var Paciente = db.paciente.SingleOrDefault(p => p.email == paciente);
				if (Paciente != null)
				{

					servicio Servicio = new servicio()
					{
						idServicio = Convert.ToString(id + 1), // Asocia el ultimo id mas 1 para el consecutivo
						tipoServicio = tipoServicio,
						duracion = duracion,
						cedula_Enfermero = enfermero,
						cedula_Paciente = Paciente.cedula,
						fecha = Convert.ToDateTime(Turno.fecha),
						hora = horaMenor,
						estado = "Pendiente"
					};

					try
					{
						db.servicio.Add(Servicio);
						db.SaveChanges();

						Mensaje = "Ok";
					}

					catch (Exception e)
					{
						Mensaje = "Error con la informacion del servicio por favor intentelo de nuevo" + e;
					}
				}
				else
				{
					Mensaje = "Paciente incorrecto";
				}
            }

            catch (Exception e)
            {
                Mensaje = "Error con los turnos, por favor intentelo de nuevo" + e;
            }

            return Mensaje;
        }

        //METODO QUE LISTA LOS SERVICIOS PARA CANCELAR //
        public LinkedList<string> ListarServicios(string email)
        {

            LinkedList<string> listaServicios = new LinkedList<string>();
            string OutputJson = "";

            try
            {
                var Paciente = db.paciente.SingleOrDefault(p => p.email == email);
                var cedula = Paciente.cedula;

                var lista = from serv in db.servicio
                            where serv.cedula_Paciente == cedula && serv.estado == "Pendiente"
                            select serv;


                foreach (var i in lista)
                {
                    var Servicio = new servicio()
                    {
                        idServicio = i.idServicio,
                        tipoServicio = i.tipoServicio,
                        duracion = i.duracion,
                        cedula_Enfermero = i.cedula_Enfermero,
                        cedula_Paciente = i.cedula_Paciente,
                        fecha = i.fecha,
                        hora = i.hora,
                        estado = i.estado
                    };

                    OutputJson = JsonConvert.SerializeObject(Servicio);
                    listaServicios.AddLast(OutputJson);
                }

            }

            catch (Exception e)
            {
                OutputJson = "Error";
            }

            return listaServicios;
        }

        //METODO PARA CANCELAR SERVICIOS //
        public Tuple<string, LinkedList<string>> CancelarServicio(int idServicio)
        {
			Tuple<string, LinkedList<string>> resultadoConsulta;
			LinkedList<string> listaServicios = new LinkedList<string>();
			string Json = "";

            try
            {
                string idComparar = Convert.ToString(idServicio);
                servicio serv = db.servicio.Where(s => s.idServicio == idComparar).First();

				string cedulaEnfermero = serv.cedula_Enfermero;
				TimeSpan horaFinal = new TimeSpan(Convert.ToInt32(serv.duracion), 00, 00);


				var turno =  from t in db.turno_enfermero
							 where t.ced_enfermero == cedulaEnfermero &&
							 t.fecha == serv.fecha && t.horaInicial >= serv.hora && 
							 t.horaFinal.Hours <= serv.hora.Value.Hours + horaFinal.Hours
							 select t;


                if(serv != null)
                {
					serv.estado = "Cancelado"; // Actualiza el estado del servicio

					foreach(turno_enfermero t in turno)
					{
						t.estado = "Disponible";						
					}

					db.SaveChanges();
				}


				var lista = from s in db.servicio
							where s.cedula_Paciente == serv.cedula_Paciente && s.estado == "Pendiente"
							select s;


				foreach (var i in lista)
				{
					var Servicio = new servicio()
					{
						idServicio = i.idServicio,
						tipoServicio = i.tipoServicio,
						duracion = i.duracion,
						cedula_Enfermero = i.cedula_Enfermero,
						cedula_Paciente = i.cedula_Paciente,
						fecha = i.fecha,
						hora = i.hora,
						estado = i.estado
					};

					Json = JsonConvert.SerializeObject(Servicio);
					listaServicios.AddLast(Json);
				}

				resultadoConsulta = new Tuple<string, LinkedList<string>>("Ok", listaServicios);
			}

            catch(Exception e)
            {
				resultadoConsulta = new Tuple<string, LinkedList<string>>("Error", listaServicios);
			}

            return resultadoConsulta;
        }

		//METODO QUE LISTA LOS MEDICOS PARA HACER EL CRUD //
		public LinkedList<Tuple<int, string>> ListarMedicosCrud()
		{
			string Mensaje = "";

			LinkedList<Tuple<int, string>> listaMedicos = new LinkedList<Tuple<int, string>>();

			try
			{
				var medicos = from medico in db.medico
							  join cu in db.cuenta_usuario on medico.email equals cu.email
							  where cu.estado.Equals("Activo")
							  select medico;

				foreach (medico i in medicos)
				{
					var Tupla = new Tuple<int, string>(Convert.ToInt32(i.cedula), i.nombre + " " + i.apellido);
					listaMedicos.AddLast(Tupla);
				}
			}

			catch (Exception e)
			{
				Mensaje = "Error listando los enfermeros ";
			}

			return listaMedicos;
		}


		//METODO QUE LISTA LOS MEDICOS PARA AGENDAR TURNOS A UNO SELECCIONADO //
		public LinkedList<string> ListarMedicosAgTu()
		{
			LinkedList<string> listaMedicos = new LinkedList<string>();

			try
			{
				var listaM = from m in db.medico
							 join cu in db.cuenta_usuario on m.email equals cu.email
							 where cu.estado.Equals("Activo")
							 select m;


				if (listaM != null)
				{
					foreach (medico i in listaM)
					{
						var obj = new
						{
							cedula = i.cedula,
							nombre = i.nombre + " " + i.apellido,
							apellido = i.apellido,
							edad = i.edad,
							direccion = i.direccion,
							telefono = i.telefono,
							fechaNacimiento = i.fechaNacimiento,
							genero = i.genero,
							email = i.email
						};

						listaMedicos.AddLast(JsonConvert.SerializeObject(obj));
					}
				}

				else
				{
					return listaMedicos;
				}

			}

			catch(Exception e)
			{
				return listaMedicos;
			}


			return listaMedicos;
		}


		//METODO QUE LISTA LOS ENFERMEROS PARA AGENDAR TURNOS A UNO SELECCIONADO //
		public LinkedList<string> ListarEnfermerosAgTu()
		{
			LinkedList<string> listaEnfermeros = new LinkedList<string>();

			try
			{
				var listaE = from e in db.enfermero
							 join cu in db.cuenta_usuario on e.email equals cu.email
							 where cu.estado.Equals("Activo")
							 select e;


				if (listaE != null)
				{
					foreach (enfermero i in listaE)
					{
						var obj = new
						{
							cedula = i.cedula,
							nombre = i.nombre + " " + i.apellido,
							apellido = i.apellido,
							edad = i.edad,
							direccion = i.direccion,
							telefono = i.telefono,
							fechaNacimiento = i.fechaNacimiento,
							genero = i.genero,
							email = i.email
						};

						listaEnfermeros.AddLast(JsonConvert.SerializeObject(obj));
					}
				}

				else
				{
					return listaEnfermeros;
				}

			}

			catch (Exception e)
			{
				return listaEnfermeros;
			}


			return listaEnfermeros;
		}

		public string AgendarTurnosMedico(string cedula, List<string> listaTurnos)
		{
			string Mensaje = "";

			foreach (string i in listaTurnos)
			{
				var idMax = db.turno_medico.Count();

				string[] info = i.Split(',');

				string temp = Convert.ToString(info[1]);
				DateTime fecha = Convert.ToDateTime(temp);
				TimeSpan horaI = new TimeSpan(Convert.ToInt32(info[2]), 00, 00);
				TimeSpan horaAux = new TimeSpan(horaI.Hours, 30, 00);
				TimeSpan horaF = new TimeSpan(Convert.ToInt32(info[3]), 00, 00);

				turno_medico tm = new turno_medico()
				{
					idTurno = idMax + 1,
					fecha = fecha,
					horaInicial = horaI,
					horaFinal = horaAux,
					estado = "Disponible",
					ced_medico = cedula
				};

				try
				{
					db.turno_medico.Add(tm);
					db.SaveChanges();

					turno_medico tm2 = new turno_medico()
					{
						idTurno = idMax + 2,
						fecha = fecha,
						horaInicial = horaAux,
						horaFinal = horaF,
						estado = "Disponible",
						ced_medico = cedula
					};

					try
					{
						db.turno_medico.Add(tm2);
						db.SaveChanges();

						Mensaje = "Agenda Registrada con exito";
					}

					catch (Exception e)
					{
						Mensaje = "Error al registrar la agenda 2";
					}
				}

				catch (Exception e)
				{
					Mensaje = "Error al registrar la agenda";
				}
			}
			return Mensaje;
		}

		public string AgendarTurnosEnfermero(string cedula, List<string> listaTurnos)
		{
			string Mensaje = "";

			foreach (string i in listaTurnos)
			{
				var idMax = db.turno_enfermero.Count();

				string[] info = i.Split(',');

				string temp = Convert.ToString(info[1]);
				DateTime fecha = Convert.ToDateTime(temp);
				TimeSpan horaI = new TimeSpan(Convert.ToInt32(info[2]), 00, 00);
				TimeSpan horaF = new TimeSpan(Convert.ToInt32(info[3]), 00, 00);

				turno_enfermero te = new turno_enfermero()
				{
					idTurno = idMax + 1,
					fecha = fecha,
					horaInicial = horaI,
					horaFinal = horaF,
					estado = "Disponible",
					ced_enfermero = cedula
				};

				try
				{
					db.turno_enfermero.Add(te);
					db.SaveChanges();

					Mensaje = "Agenda Registrada con exito";
				}

				catch (Exception e)
				{
					Mensaje = "Error al registrar la agenda";
				}
			}
			return Mensaje;
		}
		public LinkedList<string> ListarAgendaEnfermero(string email)
		{
			LinkedList<string> OutputJson = new LinkedList<string>();

			try
			{
				var cedulaEnfermero = db.enfermero.SingleOrDefault(x => x.email == email);

				var listaTurnos = from turnoe in db.turno_enfermero
								  where turnoe.ced_enfermero.Equals(cedulaEnfermero.cedula) 
								  orderby turnoe.horaInicial
								  select turnoe;

				if (listaTurnos != null)
				{
					foreach (turno_enfermero i in listaTurnos)
					{
						var obj = new
						{
							idTurno = i.idTurno,
							fecha = i.fecha,
							horaInicial = i.horaInicial,
							horaFinal = i.horaFinal,
							estado = i.estado,
							ced_enfermero = i.ced_enfermero
						};

						OutputJson.AddLast(JsonConvert.SerializeObject(obj));
					}
				}
			}

			catch (Exception e)
			{
				string Mensaje = "ERROR";
			}

			return OutputJson;
		}

	}
}