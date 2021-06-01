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
            this.service = service;
            Task.Run(async () =>
            {
                await this.CargarOfertaAsync();
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

        public async Task CargarOfertaAsync()
        {
            this.Cadena = await this.service.GetCadenaByIdAsync(Oferta.IdCadena);
            VistaComentariosListApi comentarios = await this.service.GetComentariosPaginadosAsync(0, Oferta.Id, 0, "asc");
            this.Comentarios = comentarios;
            this.Likes = await this.service.GetLikesOfertaAsync(Oferta.Id);
            this.DisLikes = await this.service.GetDisLikesOfertaAsync(Oferta.Id);
        }
    }
}
