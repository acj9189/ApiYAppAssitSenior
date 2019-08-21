using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
namespace AssistSeniorCommon.Models
{
	public class Enfermero
	{
		
			[JsonProperty("nombre")]
			public string Nombre { get; set; }

			[JsonProperty("cedula")]

			public long Cedula { get; set; }

			[JsonProperty("apellido")]
			public string Apellido { get ; set; }
         
        
	}
}
