using OnlyFoodXamarin.Base;
using OnlyFoodXamarin.Models;
using OnlyFoodXamarin.Services;
using OnlyFoodXamarin.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OnlyFoodXamarin.ViewModels
{
    public class OfertasViewModel : ViewModelBase
    {
        OnlyFoodService service;

        private ObservableCollection<Oferta> _Ofertas;
        public ObservableCollection<Oferta> Ofertas
        {
            get { return this._Ofertas; }
            set
            {
                this._Ofertas = value;
                OnPropertyChanged("Ofertas");
            }
        }
        private Oferta _OfertaSeleccionada;
        public Oferta OfertaSeleccionada
        {
            get { return this._OfertaSeleccionada; }
            set
            {
                this._OfertaSeleccionada = value;
                OnPropertyChanged("OfertaSeleccionada");
                this.MostrarDetalleOferta.Execute(1);
                //Task.Run(async () =>
                //{
                //    await this.MostrarDetalleOfertaFunction();
                //});
            }
        }
        private FiltroOfertas _Filtro;
        public FiltroOfertas Filtro
        {
            get { return this._Filtro; }
            set
            {
                this._Filtro = value;
                OnPropertyChanged("Filtro");
            }
        }
        private int _Ultimo;
        public OfertasViewModel(OnlyFoodService service)
        {
            this.service = service;
            if (Filtro == null)
            {
                this.Filtro = new FiltroOfertas();
                this.Filtro.Preciomax = 100;
                this.Filtro.Preciomin = 0;
            }
            if (this.Ofertas == null)
            {
                Task.Run(async () =>
                {
                    await this.LoadOfertas();
                });
            }

            //List<Oferta> ofertaslocal = new List<Oferta>()
            //{
            //    new Oferta()
            //    {
            //        Id = 1,
            //        Codigo = "D105",
            //        Descripcion="Cono de nata por 0,50€",
            //        IdCadena=1,
            //        IdUsuario=1,
            //        Imagen= "https://onlyfood.s3.amazonaws.com/longchickenbk.jpg",
            //        Precio=5.49,
            //        Titulo="Menu mediano: Hamburguesa long chicken con patatas y bebida medianas",
            //        Web= "https://www.burgerking.es/"
            //    },
            //    new Oferta()
            //    {
            //        Id = 2,
            //        Codigo = "B110",
            //        Descripcion="Cono de nata por 0,50€",
            //        IdCadena=2,
            //        IdUsuario=1,
            //        Imagen= "https://onlyfood.s3.amazonaws.com/B110.jpg",
            //        Precio=24.99,
            //        Titulo="8 TIRAS DE PECHUGA + 6 PIEZAS DE POLLO + 4 BEBIDAS + 4 PATATAS por 24,99€",
            //        Web= "https://www.burgerking.es/"
            //    },
            //    new Oferta()
            //    {
            //        Id = 3,
            //        Codigo = "D105",
            //        Descripcion="Cono de nata por 0,50€",
            //        IdCadena=3,
            //        IdUsuario=1,
            //        Imagen= "https://onlyfood.s3.amazonaws.com/cbo.jpg",
            //        Precio=5.45,
            //        Titulo="Menú CBO: Hamburguesa CBO, bebida y patatas pequeñas",
            //        Web= "https://www.burgerking.es/"
            //    }
            //};

            //if (idcadena != null)
            //{
            //    this.Ofertas = new ObservableCollection<Oferta>
            //    (ofertaslocal.Where(x => x.IdCadena == idcadena.Value));
            //}
            //else
            //{
            //    this.Ofertas = new ObservableCollection<Oferta>
            //    (ofertaslocal);
            //}
            //this.Ofertas = new ObservableCollection<Oferta>(
            //    (idcadena != null)
            //    ? ofertaslocal.Where(x => x.IdCadena == idcadena.Value)
            //    : ofertaslocal);
        }
        public async Task LoadOfertas()
        {
            OfertasListApi ofertas = await this.service.GetOfertasPaginadosAsync(this.Filtro,0,4);
            this.Ofertas = new ObservableCollection<Oferta>(ofertas.Ofertas);
            this._Ultimo = ofertas.Ultimo;
        }
        public async Task CargarMasOfertas()
        {
            OfertasListApi ofertas = await this.service.GetOfertasPaginadosAsync(this.Filtro, this._Ultimo, 4);
            ObservableCollection<Oferta> nuevas = this.Ofertas;
            foreach(Oferta o in ofertas.Ofertas)
            {
                nuevas.Add(o);
            }
            this.Ofertas = nuevas;
            this._Ultimo = ofertas.Ultimo;
        }
        private async Task MostrarDetalleOfertaFunction()
        {
            DetalleOfertaView view = new DetalleOfertaView();
            DetalleOfertaViewModal viewmodel = App.ServiceLocator.DetalleOfertaViewModal;
            viewmodel.Oferta=this.OfertaSeleccionada;
            await viewmodel.CargarOferta();
            view.BindingContext = viewmodel;
            Application.Current.MainPage.Navigation.PushModalAsync(view);
        }
        public Command MostrarMasOfertas
        {
            get
            {
                return new Command(async () =>
                {
                    await this.CargarMasOfertas();
                });
            }
        }
        public Command MostrarDetalleOferta
        {
            get
            {
                return new Command(async () =>
                {
                    await this.MostrarDetalleOfertaFunction();
                });
            }
        }
    }
}
