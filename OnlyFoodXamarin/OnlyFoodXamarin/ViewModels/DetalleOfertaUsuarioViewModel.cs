﻿using OnlyFoodXamarin.Base;
using OnlyFoodXamarin.Models;
using OnlyFoodXamarin.Services;
using OnlyFoodXamarin.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OnlyFoodXamarin.ViewModels
{
    public class DetalleOfertaUsuarioViewModel : ViewModelBase
    {
        OnlyFoodService service;

        public DetalleOfertaUsuarioViewModel(OnlyFoodService service)
        {
            this.service = service;
            Task.Run(async () =>
            {
                await this.CargarCadenaAsync();
            });
        }

        private Oferta _Oferta;
        public Oferta Oferta
        {
            get { return _Oferta; }
            set
            {
                this._Oferta = value;
                OnPropertyChanged("Oferta");
            }
        }

        private Cadena _Cadena;
        public Cadena Cadena
        {
            get { return _Cadena; }
            set
            {
                this._Cadena = value;
                OnPropertyChanged("Cadena");
            }
        }

        private async Task CargarCadenaAsync()
        {
            this.Cadena = await this.service.GetCadenaByIdAsync(this.Oferta.IdCadena);
        }

        private async Task EditarAsync()
        {
            EditarOfertaView view = new EditarOfertaView();
            EditarOfertaViewModel viewmodel = App.ServiceLocator.EditarOfertaViewModel;
            viewmodel.Oferta = this.Oferta;
            viewmodel.CadenaSeleccionada = this.Cadena;
            view.BindingContext = viewmodel;
            await Application.Current.MainPage.Navigation.PushModalAsync(view);
        }

        public Command Editar
        {
            get
            {
                return new Command(async () =>
                {
                    await this.EditarAsync();
                });
            }
        }

        private async Task EliminarAsync()
        {
            String token = await this.service.GetApiTokenAsync("onlyfoodes@gmail.com", "Admin123");
            await this.service.DeleteOfertaAsync(this.Oferta.Id, token);
            await Application.Current.MainPage.Navigation.PushModalAsync(new OfertasUsuarioView());
        }
        public Command Eliminar
        {
            get
            {
                return new Command(async () =>
                {
                    await this.EliminarAsync();
                });
            }
        }
    }
}
