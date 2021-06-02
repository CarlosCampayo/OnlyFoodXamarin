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

        public OfertasViewModel(OnlyFoodService service)
        {
            this.service = service;
            if (Filtro == null)
            {
                this.Filtro = new FiltroOfertas();
                this.Filtro.Preciomax = 100;
                this.Filtro.Preciomin = 0;
            }
        }

        #region ACTIVITY INDICATOR
        private bool _ShowLoading;
        public bool ShowLoading
        {
            get { return this._ShowLoading; }
            set
            {
                this._ShowLoading = value;
                OnPropertyChanged("ShowLoading");
                this.ShowContent = !this.ShowLoading;
            }
        }

        private bool _ShowContent;
        public bool ShowContent
        {
            get { return this._ShowContent; }
            set
            {
                this._ShowContent = value;
                OnPropertyChanged("ShowContent");
            }
        }
        #endregion

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
                //Task.Run(async () =>
                //{
                //    await this.MostrarDetalleOfertaAsync();
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

        public async Task LoadOfertas()
        {
            this.ShowLoading = true;
            OfertasListApi ofertas = await this.service.GetOfertasPaginadosAsync(this.Filtro,0,4);
            this.Ofertas = new ObservableCollection<Oferta>(ofertas.Ofertas);
            this._Ultimo = ofertas.Ultimo;
            this.ShowLoading = false;
        }

        public async Task CargarMasOfertas()
        {
            int posicion = 0;
            if (this.Ofertas != null)
            {
                posicion = this.Ofertas.Count;
            }
            OfertasListApi ofertas = await this.service.GetOfertasPaginadosAsync(this.Filtro, posicion, 4);
            ObservableCollection<Oferta> nuevas = this.Ofertas;
            foreach(Oferta o in ofertas.Ofertas)
            {
                nuevas.Add(o);
            }
            this.Ofertas = nuevas;
            this._Ultimo = ofertas.Ultimo;
        }

        public async Task<DetalleOfertaView> MostrarDetalleOfertaAsync()
        {
            DetalleOfertaView view = new DetalleOfertaView();
            DetalleOfertaViewModel viewmodel = App.ServiceLocator.DetalleOfertaViewModel;
            viewmodel.Oferta=this.OfertaSeleccionada;
            await viewmodel.CargarOfertaAsync();
            view.BindingContext = viewmodel;
            return view;
            //var masterDetailPage = Application.Current.MainPage as MasterDetailPage;
            //masterDetailPage.Detail = new NavigationPage(view);
            //masterDetailPage.IsPresented = false;
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
    }
}
