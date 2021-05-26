using OnlyFoodXamarin.Base;
using OnlyFoodXamarin.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace OnlyFoodXamarin.ViewModels
{
    public class DetalleOfertaViewModal:ViewModelBase
    {
        public DetalleOfertaViewModal()
        {
            Oferta oferta = new Oferta()
            {
                Id = 1,
                Codigo = "D105",
                Descripcion="Cono de nata por 0,50€",
                IdCadena=1,
                IdUsuario=1,
                Imagen= "https://onlyfood.blob.core.windows.net/imagenes/bk-d15.jpg",
                Precio=0.5,
                Titulo="Cono helado",
                Web= "https://www.burgerking.es/"
            };
            Cadena cadena = new Cadena()
            {
                Id = 1,
                Nombre = "Burguer King",
                Descripcion = "Burger King, también conocida como BK, ​ es una cadena de establecimientos de comida rápida estadounidense con sede central en Miami, fundada por James McLamore y David Edgerton, presente a nivel internacional y especializada principalmente en la elaboración de hamburguesas.",
                Imagen = "https://onlyfood.blob.core.windows.net/imagenes/bklogo%20-%20copia.png",
                Web = "https://www.burgerking.es/"
            };
            this.Oferta = oferta;
            this.Cadena = cadena;
            this.Comentarios = new ObservableCollection<VistaComentarios>
                (new List<VistaComentarios>() 
                { new VistaComentarios { Mensaje = "Comentario de prueba" , Username="Carlos"} });
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
        private ObservableCollection<VistaComentarios> _Comentarios;
        public ObservableCollection<VistaComentarios> Comentarios
        {
            get { return _Comentarios; }
            set
            {
                this._Comentarios = value;
                OnPropertyChanged("Comentarios");
            }
        }
    }
}
