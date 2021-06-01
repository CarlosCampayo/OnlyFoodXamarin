using OnlyFoodXamarin.Base;
using OnlyFoodXamarin.Helpers;
using OnlyFoodXamarin.Models;
using OnlyFoodXamarin.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OnlyFoodXamarin.ViewModels
{
    public class NuevaOfertaViewModel : ViewModelBase
    {
        OnlyFoodService service;
        public NuevaOfertaViewModel(OnlyFoodService service)
        {
            this.service = service;
            this.Oferta = new Oferta();
            Task.Run(async () =>
            {
                await this.LoadCadenasAsync();
            });
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

        private Cadena _CadenaSeleccionada;
        public Cadena CadenaSeleccionada
        {
            get { return _CadenaSeleccionada; }
            set
            {
                this._CadenaSeleccionada = value;
                OnPropertyChanged("CadenaSeleccionada");
            }
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
        
        private String _Mensaje;
        public String Mensaje
        {
            get { return _Mensaje; }
            set
            {
                this._Mensaje = value;
                OnPropertyChanged("Mensaje");
            }
        }

        private async Task LoadCadenasAsync()
        {
            List<Cadena> cadenas = await this.service.GetCadenasAsync();
            this.Cadenas = new ObservableCollection<Cadena>(cadenas);
        }

        private void Limpiar()
        {
            this.Oferta = new Oferta();
        }


        private async Task CrearOfertaAsync()
        {
            //String token = await this.service.GetApiTokenAsync("onlyfoodes@gmail.com", "Admin123");
            String token = App.ServiceLocator.SessionService.Token;

            int idUsaurio = App.ServiceLocator.SessionService.Usuario.Id;

            this.Oferta.IdCadena = this.CadenaSeleccionada.Id;
            this.Oferta.IdUsuario = idUsaurio;

            this.Mensaje = $"Oferta creada: {this.Oferta.Titulo}";
            await this.service.NewOfertaAsync(Oferta.IdCadena, Oferta.Titulo, Oferta.Descripcion,
                this.Imagen, Oferta.Web, Oferta.Codigo, Oferta.Precio, idUsaurio, token);
        }

        public Command CrearOferta
        {
            get{
                return new Command(async() =>
                {
                    await this.CrearOfertaAsync();
                    this.Limpiar();
                });
            }
            
        }
    }
}
