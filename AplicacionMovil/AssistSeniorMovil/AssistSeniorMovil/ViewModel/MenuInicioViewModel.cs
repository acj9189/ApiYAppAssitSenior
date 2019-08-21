using AssistSeniorMovil.View;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AssistSeniorMovil.ViewModel
{
	public class MenuInicioViewModel
	{
		public ICommand consultarEnfermeroCommand => new RelayCommand(ConsultarTurnoEnfermero);
        public ICommand SolicitarServicioCommand => new RelayCommand(SolicitarServicio);
        private async void ConsultarTurnoEnfermero()
		{
			MainViewModel.GetInstance().ConsultarTurno = new ConsultarTurnoViewModel();
			await Application.Current.MainPage.Navigation.PushAsync(new ConsultarTurnoView());
		}
        private async void SolicitarServicio()
        {
            MainViewModel.GetInstance().BuscarEnfermero = new BuscarEnfermeroViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new BuscarEnfermero());
        }
    }
}
