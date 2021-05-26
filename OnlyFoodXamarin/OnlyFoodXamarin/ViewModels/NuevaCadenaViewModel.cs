using OnlyFoodXamarin.Base;
using OnlyFoodXamarin.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlyFoodXamarin.ViewModels
{
    public class NuevaCadenaViewModel: ViewModelBase
    {
        private String _Nombre;
        public String Nombre
        {
            get { return _Nombre; }
            set
            {
                this._Nombre = value;
                OnPropertyChanged("Nombre");
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
        public Cadena NewCadena()
        {
            Cadena cadena = new Cadena()
            {
                Descripcion = this.Descripcion,
                Imagen = this.Imagen,
                Nombre = this.Nombre,
                Web = this.Web
            };
            return cadena;
        }
    }
}
