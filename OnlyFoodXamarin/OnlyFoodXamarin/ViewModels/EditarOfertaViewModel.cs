using Microsoft.AspNetCore.Http;
using OnlyFoodXamarin.Base;
using OnlyFoodXamarin.Helpers;
using OnlyFoodXamarin.Models;
using OnlyFoodXamarin.Services;
using OnlyFoodXamarin.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OnlyFoodXamarin.ViewModels
{
    public class EditarOfertaViewModel : ViewModelBase
    {
        OnlyFoodService service;

        public EditarOfertaViewModel(OnlyFoodService service)
        {
            this.service = service;
            Task.Run(async () =>
            {
                await this.CargarCadenas();
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

        private Cadena _CadenaSeleccionada;
        public Cadena CadenaSeleccionada
        {
            get { return _CadenaSeleccionada; }
            set
            {
                this._CadenaSeleccionada = value;
                OnPropertyChanged("Cadena");
            }
        }

        private ObservableCollection<Cadena> _Cadenas;
        public ObservableCollection<Cadena> Cadenas
        {
            get { return _Cadenas; }
            set
            {
                this._Cadenas = value;
                OnPropertyChanged("Cadenas");
            }
        }

        private String _Imagen;
        public String Imagen
        {
            get { return _Imagen; }
            set
            {
                this._Imagen = value;
                OnPropertyChanged("Imagen");
            }
        }

        private Stream _ResultImagen;
        public Stream ResultImagen
        {
            get { return _ResultImagen; }
            set
            {
                this._ResultImagen = value;
                OnPropertyChanged("ResultImagen");
            }
        }

        private async Task CargarCadenas()
        {
            List<Cadena> cadenas = await this.service.GetCadenasAsync();
            this.Cadenas = new ObservableCollection<Cadena>(cadenas);
        }

        private async Task EditarOfertaFunction()
        {
            String token = await this.service.GetApiTokenAsync("onlyfoodes@gmail.com", "Admin123");
            int idUsaurio = 2;

            //this.ResultImagen.
            //await this.service.EditOfertaAsync(
            //    Oferta.Id, this.CadenaSeleccionada.Id, Oferta.Titulo,
            //    Oferta.Descripcion, this.ResultImagen, Oferta.Web, Oferta.Codigo, Oferta.Precio, 
            //    idUsaurio, token); 
        }

        public Command EditarOferta
        {
            get
            {
                return new Command(async () => 
                {
                    await this.EditarOfertaFunction();
                    //EditarOfertaViewModel vm = this;
                    await Application.Current.MainPage.Navigation.PushModalAsync
                        (new OfertasUsuarioView());
                });
            }
        }
    }
}
