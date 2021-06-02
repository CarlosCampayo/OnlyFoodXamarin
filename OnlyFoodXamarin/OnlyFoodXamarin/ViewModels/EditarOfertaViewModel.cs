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
                this.CadenaSeleccionadaAux = this.CadenaSeleccionada;
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

        private Cadena _CadenaSeleccionadaAux;
        public Cadena CadenaSeleccionadaAux
        {
            get { return _CadenaSeleccionadaAux; }
            set
            {
                this._CadenaSeleccionadaAux = value;
                OnPropertyChanged("CadenaSeleccionadaAux");
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

        private String _NewImagen;
        public String NewImagen
        {
            get { return _NewImagen; }
            set
            {
                this._NewImagen = value;
                OnPropertyChanged("NewImagen");
            }
        }

        private async Task CargarCadenas()
        {
            List<Cadena> cadenas = await this.service.GetCadenasAsync();
            this.Cadenas = new ObservableCollection<Cadena>(cadenas);
        }

        public async Task EditarOfertaAsync()
        {
            //String token = await this.service.GetApiTokenAsync("onlyfoodes@gmail.com", "Admin123");
            //int idUsaurio = 2;
            String token = App.ServiceLocator.SessionService.Token;
            int idUsaurio = App.ServiceLocator.SessionService.Usuario.Id;
            if (this.CadenaSeleccionada == null)
            {
                this.CadenaSeleccionada = this.CadenaSeleccionadaAux;
            }
            await this.service.EditOfertaAsync(
                Oferta.Id, this.CadenaSeleccionada.Id, Oferta.Titulo,
                Oferta.Descripcion, this.NewImagen, Oferta.Web, Oferta.Codigo, Oferta.Precio,
                idUsaurio, token);
            //await Application.Current.MainPage.Navigation.PushModalAsync
            //    (new OfertasUsuarioView());
        }

        public Command EditarOferta
        {
            get
            {
                return new Command(async () => 
                {
                    await this.EditarOfertaAsync();
                    //EditarOfertaViewModel vm = this;
                });
            }
        }
    }
}
