using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssistSeniorCommon.Models
{
	public class TurnoEnfermero
	{
        [JsonProperty("fecha")]
        public System.DateTime Fecha { get; set; }

        [JsonProperty("horaInicial")]
        public System.TimeSpan HoraInicial { get; set; }

        [JsonProperty("horaFinal")]
        public System.TimeSpan HoraFinal { get; set; }

        [JsonProperty("cedEnfermero")]
        public string CedEnfermero { get; set; }
        public string FechaCompleta
        {
            get { return $"{this.Fecha.ToString("MM/dd/yyyy")}      {this.HoraInicial.ToString()} {"-"} {this.HoraFinal.ToString()}"; }
            
        }
    }
}
