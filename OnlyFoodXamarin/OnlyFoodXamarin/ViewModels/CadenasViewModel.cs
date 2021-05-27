using OnlyFoodXamarin.Base;
using OnlyFoodXamarin.Models;
using OnlyFoodXamarin.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace OnlyFoodXamarin.ViewModels
{
    public class CadenasViewModel : ViewModelBase
    {
        public CadenasViewModel()
        {
            List<Cadena> cadenasLocal = new List<Cadena>()
            {
                new Cadena()
                {
                    Id = 1,
                    Nombre = "Burguer King",
                    Descripcion = "Burger King, también conocida como BK, es una cadena de establecimientos de comida rápida estadounidense con sede central en Miami, fundada por James McLamore y David Edgerton, presente a nivel internacional y especializada principalmente en la elaboración de hamburguesas.",
                    Imagen = "https://onlyfood.blob.core.windows.net/imagenes/bklogo%20-%20copia.png",
                    Web = "https://www.burgerking.es/"
                },
                new Cadena()
                {
                    Id = 2,
                    Nombre = "KFC",
                    Descripcion = "Burger King, también conocida como BK, es una cadena de establecimientos de comida rápida estadounidense con sede central en Miami, fundada por James McLamore y David Edgerton, presente a nivel internacional y especializada principalmente en la elaboración de hamburguesas.",
                    Imagen = "https://onlyfood.s3.amazonaws.com/kfc.png",
                    Web = "https://www.burgerking.es/"
                },
                new Cadena()
                {
                    Id = 3,
                    Nombre = "McDonald's",
                    Descripcion = "Burger King, también conocida como BK, es una cadena de establecimientos de comida rápida estadounidense con sede central en Miami, fundada por James McLamore y David Edgerton, presente a nivel internacional y especializada principalmente en la elaboración de hamburguesas.",
                    Imagen = "https://onlyfood.s3.amazonaws.com/McDonaldsEmblema.png",
                    Web = "https://www.burgerking.es/"
                },
            };

            this.Cadenas = new ObservableCollection<Cadena>(cadenasLocal);
        }

        public ObservableCollection<Cadena> Cadenas { get; set; }
        private Cadena _CadenaSeleccionada;
        public Cadena CadenaSeleccionada
        {
            get { return this._CadenaSeleccionada; }
            set
            {
                this._CadenaSeleccionada = value;
                // Solo usa el parametro si el command lo tiene declarado
                this.MostrarOfertas.Execute(1);  
                OnPropertyChanged("CadenaSeleccionada");
            }
        }

        public Command MostrarOfertas
        {
            get
            {
                return new Command(() =>
                {
                    int idcadena = this.CadenaSeleccionada.Id;
                    OfertasView view = new OfertasView();
                    view.BindingContext = new OfertasViewModel(idcadena);
                    Application.Current.MainPage.Navigation.PushModalAsync(view);
                });
            }
        }
    }
}
