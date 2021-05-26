using OnlyFoodXamarin.Base;
using OnlyFoodXamarin.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace OnlyFoodXamarin.ViewModels
{
    public class EliminarCadenaViewModel:ViewModelBase
    {
        public EliminarCadenaViewModel()
        {
            Cadena cadena = new Cadena()
            {
                Id = 1,
                Nombre = "Burguer King",
                Descripcion = "Burger King, también conocida como BK, ​ es una cadena de establecimientos de comida rápida estadounidense con sede central en Miami, fundada por James McLamore y David Edgerton, presente a nivel internacional y especializada principalmente en la elaboración de hamburguesas.",
                Imagen = "https://onlyfood.blob.core.windows.net/imagenes/bklogo%20-%20copia.png",
                Web = "https://www.burgerking.es/"
            };
            List<Cadena> listaCadenas = new List<Cadena>();
            listaCadenas.Add(cadena);
            this.Cadenas = new ObservableCollection<Cadena>(listaCadenas);
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
    }
}
