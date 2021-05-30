using OnlyFoodXamarin.Base;
using OnlyFoodXamarin.Helpers;
using OnlyFoodXamarin.Models;
using OnlyFoodXamarin.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace OnlyFoodXamarin.ViewModels
{
    public class NuevaCadenaViewModel: ViewModelBase
    {
        OnlyFoodService service;
        public NuevaCadenaViewModel(OnlyFoodService service)
        {
            //UploadService uploadService = new UploadService();
            //service = new OnlyFoodService(uploadService);
            this.service = service;
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
        //private String _Imagen;
        //public String Imagen
        //{
        //    get { return _Imagen; }
        //    set
        //    {
        //        this._Imagen = value;
        //        OnPropertyChanged("Imagen");
        //    }
        //}
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
        //private Cadena NewCadena()
        //{
        //    Cadena cadena = new Cadena();
        //    cadena.Descripcion = this.Descripcion;
        //    cadena.Imagen = this.Imagen;
        //    cadena.Nombre = this.Nombre;
        //    cadena.Web = this.Web;
        //    return cadena;
        //}
        private void Limpiar()
        {
            //this.Nombre = "";
            //this.Descripcion = "";
            //this.Imagen = "";
            //this.Web = "";
            //this.Mensaje = "";
            this.Cadena = new Cadena();
        }
        public Command CrearCadena
        {
            get{
                return new Command(async() =>
                {
                    String token = await this.service.GetApiTokenAsync("onlyfoodes@gmail.com", "Admin123");
                    //Cadena cadena = this.NewCadena();
                    await this.service.NewCadenaAsync(Cadena.Nombre, Cadena.Descripcion, null, Cadena.Web, token);
                    this.Mensaje = "Cadena creada: " + this.Cadena.Nombre;
                    //llamada a api para crearla
                    this.Limpiar();

                });
            }
        }
    }
}
