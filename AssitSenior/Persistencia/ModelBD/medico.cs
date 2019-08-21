//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

public partial class medico
{
    public medico()
    {
        this.cita_valoracion = new HashSet<cita_valoracion>();
        this.turno_medico = new HashSet<turno_medico>();
    }

    public string cedula { get; set; }
    public string nombre { get; set; }
    public string apellido { get; set; }
    public int edad { get; set; }
    public string direccion { get; set; }
    public string telefono { get; set; }
    public System.DateTime fechaNacimiento { get; set; }
    public string genero { get; set; }
    public string email { get; set; }
    public string especialidad { get; set; }

    public virtual cuenta_usuario cuenta_usuario { get; set; }
    public virtual ICollection<cita_valoracion> cita_valoracion { get; set; }
    public virtual ICollection<turno_medico> turno_medico { get; set; }
}
