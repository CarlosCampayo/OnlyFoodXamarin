using OnlyFoodXamarin.Base;
using OnlyFoodXamarin.Models;
using OnlyFoodXamarin.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace OnlyFoodXamarin.ViewModels
{
    public class DetalleOfertaViewModel:ViewModelBase
    {
        OnlyFoodService service;
        public DetalleOfertaViewModel(OnlyFoodService service)
        {
            //Oferta oferta = new Oferta()
            //{
            //    Id = 1,
            //    Codigo = "D105",
            //    Descripcion="Cono de nata por 0,50€",
            //    IdCadena=1,
            //    IdUsuario=1,
            //    Imagen= "https://onlyfood.blob.core.windows.net/imagenes/bk-d15.jpg",
            //    Precio=0.5,
            //    Titulo="Cono helado",
            //    Web= "https://www.burgerking.es/"
            //};
            //Cadena cadena = new Cadena()
            //{
            //    Id = 1,
            //    Nombre = "Burguer King",
            //    Descripcion = "Burger King, también conocida como BK, ​ es una cadena de establecimientos de comida rápida estadounidense con sede central en Miami, fundada por James McLamore y David Edgerton, presente a nivel internacional y especializada principalmente en la elaboración de hamburguesas.",
            //    Imagen = "https://onlyfood.blob.core.windows.net/imagenes/bklogo%20-%20copia.png",
            //    Web = "https://www.burgerking.es/"
            //};
            this.service = service;
            Task.Run(async () =>
            {
                await this.CargarOferta();
            });
            //this.Oferta = ;
            //this.Cadena = cadena;
            //this.Comentarios = new ObservableCollection<VistaComentarios>
            //    (new List<VistaComentarios>() 
            //    { new VistaComentarios { Mensaje = "Comentario de prueba" , Username="Carlos"} });
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
        private VistaComentariosListApi _Comentarios;
        public  VistaComentariosListApi Comentarios
        {
            get { return _Comentarios; }
            set
            {
                this._Comentarios = value;
                OnPropertyChanged("Comentarios");
            }
        }
        private int _Likes;
        public int Likes
        {
            get { return _Likes; }
            set
            {
                this._Likes = value;
                OnPropertyChanged("Likes");
            }
        }
        private int _DisLikes;
        public int DisLikes
        {
            get { return _DisLikes; }
            set
            {
                this._DisLikes = value;
                OnPropertyChanged("DisLikes");
            }
        }
        public async Task CargarOferta()
        {
            this.Cadena = await this.service.GetCadenaByIdAsync(Oferta.IdCadena);
            VistaComentariosListApi comentarios = await this.service.GetComentariosPaginadosAsync(0, Oferta.Id, 0, "asc");
            this.Comentarios = comentarios;
            this.Likes = await this.service.GetLikesOfertaAsync(Oferta.Id);
            this.DisLikes = await this.service.GetDisLikesOfertaAsync(Oferta.Id);
        }
    }
}
