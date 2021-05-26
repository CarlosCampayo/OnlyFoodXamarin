using OnlyFoodXamarin.Base;
using OnlyFoodXamarin.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace OnlyFoodXamarin.ViewModels
{
    public class NuevaOfertaViewModel:ViewModelBase
    {
        public NuevaOfertaViewModel()
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
        private String _IdCadena;
        public String IdCadena
        {
            get { return _IdCadena; }
            set
            {
                this._IdCadena = value;
                OnPropertyChanged("IdCadena");
            }
        }
        private String _Titulo;
        public String Titulo
        {
            get { return _Titulo; }
            set
            {
                this._Titulo = value;
                OnPropertyChanged("Titulo");
            }
        }
        private String _Descripcion;
        public String Descripcion
        {
            get { return _Descripcion; }
            set
            {
                this._Descripcion = value;
                OnPropertyChanged("Descripcion");
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
        private String _Web;
        public String Web
        {
            get { return _Web; }
            set
            {
                this._Web = value;
                OnPropertyChanged("Web");
            }
        }
        private String _Precio;
        public String Precio
        {
            get { return _Precio; }
            set
            {
                this._Precio = value;
                OnPropertyChanged("Precio");
            }
        }
        private String _Codigo;
        public String Codigo
        {
            get { return _Codigo; }
            set
            {
                this._Codigo = value;
                OnPropertyChanged("Codigo");
            }
        }

        public Oferta NewOferta()
        {
            Oferta oferta = new Oferta()
            {
                Codigo = this.Codigo,
                Descripcion = this.Descripcion,
                IdCadena = int.Parse(this.IdCadena),
                Imagen = this.Imagen,
                Precio = int.Parse(this.Precio),
                Titulo = this.Titulo,
                Web = this.Web
                //IdUsuario=usuariodesesion
            };
            return oferta;
        }
    }
}
