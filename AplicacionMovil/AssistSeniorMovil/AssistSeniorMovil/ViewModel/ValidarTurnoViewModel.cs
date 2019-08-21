using AssistSeniorCommon.Models;
using AssistSeniorCommon.Servicios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace AssistSeniorMovil.ViewModel
{
    public class ValidarTurnoViewModel:BaseViewModel
    {
        private ObservableCollection<TurnoEnfermero> turnosEnfermeros;
        private readonly ServicioApi servicioApi;
        private string cedula;
        private DateTime fecha;
        private bool isRefreshing;
        public Enfermero enfermeros{get; set; }
    public bool Seleccionado { get; set; }


    public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }
        public string Cedula
        {
            get { return this.cedula; }
            set { this.SetValue(ref this.cedula, value); }

        }
        public DateTime Fecha
        {
            get { return this.fecha; }
            set { this.SetValue(ref this.fecha, value); }
        }
       
        public ObservableCollection<TurnoEnfermero> TurnosEnfermeros
        {
            get { return this.turnosEnfermeros; }
            set { this.SetValue(ref this.turnosEnfermeros, value); }
        }
        public ValidarTurnoViewModel(String cedula, DateTime fecha)
        {
            this.servicioApi = new ServicioApi();
            this.Cedula = cedula;
            this.Fecha = fecha;
            LoadTurnos();
        }

        private async void LoadTurnos()
        {

            this.IsRefreshing = true;
            var response = await this.servicioApi.GetListAsyn<TurnoEnfermero>(
                "http://192.168.1.65:45455",
                "/api",
                "/turnoEnfermero?cedula="+Cedula+"&fecha="+Fecha.ToString("MM/dd/yyyy"));

            this.IsRefreshing = false;

            if (!response.IsSucces)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }

            var turnoenfermeros = (List<TurnoEnfermero>)response.Result;

            this.TurnosEnfermeros = new ObservableCollection<TurnoEnfermero>(turnoenfermeros);
        }


    }
}
