using OnlyFoodXamarin.Base;
using OnlyFoodXamarin.Helpers;
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
    public class NuevaOfertaViewModel:ViewModelBase
    {
        OnlyFoodService service;
        public NuevaOfertaViewModel(OnlyFoodService service)
        {
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
            //UploadService uploadService = new UploadService();
            //service = new OnlyFoodService(uploadService);
            //this.Cadenas = new ObservableCollection<Cadena>();
            this.service = service;
            Task.Run(async () =>
            {
                await this.LoadCadenas();
            });
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
        //private String _Titulo;
        //public String Titulo
        //{
        //    get { return _Titulo; }
        //    set
        //    {
        //        this._Titulo = value;
        //        OnPropertyChanged("Titulo");
        //    }
        //}
        //private String _Descripcion;
        //public String Descripcion
        //{
        //    get { return _Descripcion; }
        //    set
        //    {
        //        this._Descripcion = value;
        //        OnPropertyChanged("Descripcion");
        //    }
        //}
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
        //private String _Web;
        //public String Web
        //{
        //    get { return _Web; }
        //    set
        //    {
        //        this._Web = value;
        //        OnPropertyChanged("Web");
        //    }
        //}
        //private String _Precio;
        //public String Precio
        //{
        //    get { return _Precio; }
        //    set
        //    {
        //        this._Precio = value;
        //        OnPropertyChanged("Precio");
        //    }
        //}
        //private String _Codigo;
        //public String Codigo
        //{
        //    get { return _Codigo; }
        //    set
        //    {
        //        this._Codigo = value;
        //        OnPropertyChanged("Codigo");
        //    }
        //}
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
        private async Task LoadCadenas()
        {
            List<Cadena> cadenas = await this.service.GetCadenasAsync();
            this.Cadenas = new ObservableCollection<Cadena>(cadenas);
        }
        private void NewOferta()
        {
            //Oferta oferta = new Oferta();
            //oferta.Codigo = this.CadenaNueva;
            //oferta.Descripcion = this.Descripcion;
            //oferta.IdCadena = int.Parse(this.CadenaSeleccionada.);
            //oferta.Imagen = this.Imagen;
            //if (Precio != null)
            //{
            //    oferta.Precio = int.Parse(this.Precio);
            //}
            //oferta.Titulo = this.Titulo;
            //oferta.Web = this.Web;
            this.Oferta.IdCadena = this.CadenaSeleccionada.Id;
        }
        private void Limpiar()
        {
            this.Oferta = new Oferta();
        }
        public Command CrearOferta
        {
            get{
                return new Command(async() =>
                {
                    this.Mensaje = "Oferta creada: " + this.Oferta.Titulo;
                    //llamada a api para crearla
                    await this.service.NewOfertaAsync(Oferta.IdCadena,Oferta.Titulo,Oferta.Descripcion,null,Oferta.Web,Oferta.Codigo,Oferta.Precio,1,"");
                    this.Limpiar();
                });
            }
            
        }
    }
}
