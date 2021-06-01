using OnlyFoodXamarin.Base;
using OnlyFoodXamarin.Helpers;
using OnlyFoodXamarin.Models;
using OnlyFoodXamarin.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OnlyFoodXamarin.ViewModels
{
    public class NuevaCadenaViewModel: ViewModelBase
    {
        OnlyFoodService service;

        public NuevaCadenaViewModel(OnlyFoodService service)
        {
            this.service = service;
            this.Cadena = new Cadena();
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

        private void Limpiar()
        {
            this.Cadena = new Cadena();
        }

        private async Task CrearCadenaAsync()
        {
            //String token = await this.service.GetApiTokenAsync("onlyfoodes@gmail.com", "Admin123");
            String token = App.ServiceLocator.SessionService.Token;
            await this.service.NewCadenaAsync(Cadena.Nombre, Cadena.Descripcion, this.Imagen, Cadena.Web, token);
            this.Mensaje = $"Cadena creada: {this.Cadena.Nombre}";
        }

        public Command CrearCadena
        {
            get{
                return new Command(async() =>
                {
                    await this.CrearCadenaAsync();
                    this.Limpiar();
                });
            }
        }
    }
}
