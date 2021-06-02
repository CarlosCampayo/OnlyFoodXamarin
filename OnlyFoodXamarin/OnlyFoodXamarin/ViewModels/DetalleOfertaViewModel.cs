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
    public class DetalleOfertaViewModel:ViewModelBase
    {
        OnlyFoodService service;
        public DetalleOfertaViewModel(OnlyFoodService service)
        {
            this.service = service;
            //this.Comentarios = new ObservableCollection<VistaComentarios>();
            //Task.Run(async () =>
            //{
            //    await this.CargarOfertaAsync();
            //});
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
        private String _LikesImagen;
        public String LikesImagen
        {
            get { return _LikesImagen; }
            set
            {
                this._LikesImagen = value;
                OnPropertyChanged("LikesImagen");
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
        private String _DisLikesImagen;
        public String DisLikesImagen
        {
            get { return _DisLikesImagen; }
            set
            {
                this._DisLikesImagen = value;
                OnPropertyChanged("DisLikesImagen");
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
        public async Task CargarComentarios()
        {
            int posicion = 0;
            if (this.Comentarios != null)
            {
                posicion=this.Comentarios.Count;
                VistaComentariosListApi comentarios = await this.service.GetComentariosPaginadosAsync(posicion, Oferta.Id, 4, "desc");
                foreach(VistaComentarios comentario in comentarios.VistaComentarios)
                {
                    this.Comentarios.Add(comentario);
                }
            }
            else
            {
                VistaComentariosListApi comentarios = await this.service.GetComentariosPaginadosAsync(posicion, Oferta.Id, 4, "desc");
                this.Comentarios = new ObservableCollection<VistaComentarios>(comentarios.VistaComentarios);
            }
        }
        public async Task CargarOfertaAsync()
        {
            this.Cadena = await this.service.GetCadenaByIdAsync(Oferta.IdCadena);
            await this.CargarComentarios();
            this.Likes = await this.service.GetLikesOfertaAsync(Oferta.Id);
            this.DisLikes = await this.service.GetDisLikesOfertaAsync(Oferta.Id);
            if (App.ServiceLocator.SessionService.Usuario != null)
            {
                Votacion voto=await this.service.GetVotacionByIdUsuarioAsync(
                    App.ServiceLocator.SessionService.Usuario.Id,
                    this.Oferta.Id, App.ServiceLocator.SessionService.Token);
                if (voto == null)
                {
                    this.LikesImagen = "yellowlike_116080.png";
                    this.DisLikesImagen = "yellowunlike_116069.png";
                }
                else
                {
                    if (voto.Voto==("like"))
                    {
                        this.LikesImagen = "bluelike_116108.png";
                        this.DisLikesImagen = "yellowunlike_116069.png";
                    }
                    else
                    {
                        this.LikesImagen = "yellowlike_116080.png";
                        this.DisLikesImagen = "pinkunlike_116084.png";
                    }
                }
            }
            else
            {
                this.LikesImagen = "yellowlike_116080.png";
                this.DisLikesImagen = "yellowunlike_116069.png";
            }
        }
        public Command CargarMasComentarios
        {
            get
            {
                return new Command(async(voto) =>
                {
                    await this.CargarComentarios();
                });
            }
        }
        public Command Votar
        {
            get
            {
                return new Command(async (voto) =>
                {
                    if (App.ServiceLocator.SessionService.Usuario != null)
                    {
                        int idUsaurio = App.ServiceLocator.SessionService.Usuario.Id;
                        await this.service.VotarAsync(idUsaurio, this.Oferta.Id, voto.ToString(), App.ServiceLocator.SessionService.Token);
                        await this.CargarOfertaAsync();
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert
                        ("OnlyFood", "" + "Tienes que iniciar sesión para poder votar", "Ok");
                    }
                    
                });
            }
        }
        public Command Comentar
        {
            get
            {
                return new Command(async (voto) =>
                {
                    if (App.ServiceLocator.SessionService.Usuario != null)
                    {
                        int idUsaurio = App.ServiceLocator.SessionService.Usuario.Id;
                        await this.service.NewComentarioAsync(this.Oferta.Id, idUsaurio, this.Mensaje, 0, App.ServiceLocator.SessionService.Token);
                        this.Comentarios = null;
                        await this.CargarOfertaAsync();
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert
                        ("OnlyFood", "" + "Tienes que iniciar sesión para poder comentar", "Ok");
                    }
                });
            }
        }
    }
}
