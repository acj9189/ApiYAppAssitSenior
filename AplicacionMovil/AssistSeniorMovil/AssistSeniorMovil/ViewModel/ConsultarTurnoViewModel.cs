namespace AssistSeniorMovil.ViewModel
{
	using AssistSeniorCommon.Models;
	using AssistSeniorCommon.Servicios;
    using AssistSeniorMovil.View;
    using GalaSoft.MvvmLight.Command;
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Windows.Input;
	using Xamarin.Forms;
    public class ConsultarTurnoViewModel : BaseViewModel
    {
        private ObservableCollection<Enfermero> enfermeros;
        private readonly ServicioApi servicioApi;
        private string cedula;
        private System.DateTime fechaSeleccionada;
       private DateTime fechaMinima=DateTime.Now;
        private Enfermero selectedEnfermero { get; set; }
        private bool isRefreshing;
        public ObservableCollection<Enfermero> Enfermeros {
            get { return this.enfermeros; }
            set { this.SetValue(ref this.enfermeros, value); }
        }
    
        public DateTime FechaSeleccionada
        {
            get { return this.fechaSeleccionada; }
            set {
                this.fechaSeleccionada= value;
                OnPropertyChanged("FechaSeleccionada");
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
        public Enfermero SelectedEnfermero
        {
            get { return selectedEnfermero; }
            set
            {
                selectedEnfermero = value;
                OnPropertyChanged("SelectedEnfermero");

            }
        }
       

        public bool IsRefreshing		{
			get { return this.isRefreshing; }
			set { this.SetValue(ref this.isRefreshing, value); }
		}
        public string Cedula
        {
            get { return this.cedula; }
            set { this.SetValue(ref this.cedula, value); }
        }
        public ConsultarTurnoViewModel()
		{
			this.servicioApi = new ServicioApi();
			this.LoadEnfermeros();

           
			
		}
		private async void LoadEnfermeros()
		{
			this.IsRefreshing = true;
			var response =await this.servicioApi.GetListAsyn<Enfermero>(
				"http://192.168.1.65:45455",
				"/api",
				"/Enfermero");
			this.IsRefreshing = false;
			if (!response.IsSucces)
			{
				await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
				return;
			}
			
			var enfermero = (List<Enfermero>)response.Result;
			
			this.Enfermeros = new ObservableCollection<Enfermero>(enfermero);
		}
		public ICommand consultarTurnoCommand => new RelayCommand(ConsultarTurno);
		public async void ConsultarTurno()
		{
           
            MainViewModel.GetInstance().TurnoEnfermero = new TurnoEnfermeroViewModel(SelectedEnfermero.Cedula.ToString(), FechaSeleccionada);
         
            await Application.Current.MainPage.Navigation.PushAsync(new TurnoView());
		}



	}
}
