using AssistSeniorMovil.View;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AssistSeniorMovil.ViewModel
{
    public class BuscarEnfermeroViewModel : BaseViewModel
    {
        private string cedulaSeleccionada;
        private DateTime fechaSeleccionada;
        private DateTime fechaMinima = DateTime.Now.AddDays(1);
        private string numeroDeHoras;

        public DateTime FechaSeleccionada
        {
            get { return this.fechaSeleccionada; }
            set
            {
                this.fechaSeleccionada = value;
                OnPropertyChanged("FechaSeleccionada");
            }
        }
        public string NumeroDeHoras
        {
            get { return this.numeroDeHoras; }
            set
            {
                this.numeroDeHoras = value;
                OnPropertyChanged("NumerosDeHoras");
            }
        }
        public DateTime FechaMinima
        {
            get { return this.fechaMinima; }
            set
            {
                this.SetValue(ref this.fechaMinima, value);

            }
        }
        public string CedulaSeleccionada
        {
            get { return this.cedulaSeleccionada; }
            set
            {
                this.cedulaSeleccionada = value;
                OnPropertyChanged("CedulaSeleccionada");
            }
        }


        public ICommand BuscarCommand => new RelayCommand(BuscarTurnoEnfermero);

        private async void BuscarTurnoEnfermero()
        {
             MainViewModel.GetInstance().ValidarTurno = new ValidarTurnoViewModel(CedulaSeleccionada,FechaSeleccionada);
            await Application.Current.MainPage.Navigation.PushAsync(new ValidarTurnoView());
        }
    }
}