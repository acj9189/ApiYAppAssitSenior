using System;
using System.Collections.Generic;
using System.Text;

namespace AssistSeniorMovil.ViewModel
{
	public class MainViewModel
	{
		private static MainViewModel instance;
		public MenuInicioViewModel MenuInicio { get; set; }
		public ConsultarTurnoViewModel ConsultarTurno { get; set; }
        public TurnoEnfermeroViewModel TurnoEnfermero { get; set; }
        public BuscarEnfermeroViewModel BuscarEnfermero { get; set; }
        
        public ValidarTurnoViewModel ValidarTurno { get; set; }

        public MainViewModel()
		{
			instance = this;

		}
		public static MainViewModel GetInstance()
		{
			if (instance==null)
			{
				return new MainViewModel();
			}

			return instance;
		}


	}
}
