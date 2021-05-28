using OnlyFoodXamarin.Base;
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
    public class EliminarCadenaViewModel:ViewModelBase
    {
        OnlyFoodService service;
        public EliminarCadenaViewModel(OnlyFoodService service)
        {
            this.service = service;
            Task.Run(async () =>
            {
                await this.LoadCadenas();
            });
            //Cadena cadena = new Cadena()
            //{
            //    Id = 1,
            //    Nombre = "Burguer King",
            //    Descripcion = "Burger King, también conocida como BK, ​ es una cadena de establecimientos de comida rápida estadounidense con sede central en Miami, fundada por James McLamore y David Edgerton, presente a nivel internacional y especializada principalmente en la elaboración de hamburguesas.",
            //    Imagen = "https://onlyfood.blob.core.windows.net/imagenes/bklogo%20-%20copia.png",
            //    Web = "https://www.burgerking.es/"
            //};
            //List<Cadena> listaCadenas = new List<Cadena>();
            //listaCadenas.Add(cadena);
            //this.Cadenas = new ObservableCollection<Cadena>(listaCadenas);
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
        public async Task LoadCadenas()
        {
            List<Cadena> cadenas = await this.service.GetCadenasAsync();
            this.Cadenas = new ObservableCollection<Cadena>(cadenas);
        }
        public Command EliminarCadena
        {
            get
            {
                return new Command(async() =>
                {
                    //recoger el id de la cadena seleccionada
                    this.Mensaje = "Cadena eliminada";
                    //llamada a api para eliminarla
                    String token = await this.service.GetApiTokenAsync("onlyfoodes@gmail.com", "Admin123");
                    await this.service.DeleteCadenaAsync(this.CadenaSeleccionada.Id,token);
                    await this.LoadCadenas();
                });
            }
        }
    }
}
