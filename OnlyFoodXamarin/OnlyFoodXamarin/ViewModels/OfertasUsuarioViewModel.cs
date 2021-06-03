using OnlyFoodXamarin.Base;
using OnlyFoodXamarin.Models;
using OnlyFoodXamarin.Services;
using OnlyFoodXamarin.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OnlyFoodXamarin.ViewModels
{
    public class OfertasUsuarioViewModel : ViewModelBase
    {
        OnlyFoodService service;

        public OfertasUsuarioViewModel(OnlyFoodService service)
        {
            this.service = service;
            Task.Run(async() =>
            {
                await this.CargarMisOfertasAsync();
            });
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
                //Task.Run(async() => await this.MostrarDetalleOfertaAsync());
            }
        }
        private bool _Imagen;
        public bool Imagen
        {
            get { return _Imagen; }
            set
            {
                this._Imagen = value;
                OnPropertyChanged("Imagen");
            }
        }
        private bool _Mensaje;
        public bool Mensaje
        {
            get { return _Mensaje; }
            set
            {
                this._Mensaje = value;
                OnPropertyChanged("Mensaje");
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
        public  async Task<DetalleOfertaUsuarioView> MostrarDetalleOfertaAsync()
        {
            DetalleOfertaUsuarioView view = new DetalleOfertaUsuarioView();
            DetalleOfertaUsuarioViewModel viewmodel =
                App.ServiceLocator.DetalleOfertaUsuarioViewModel;
            viewmodel.Oferta = this.OfertaSeleccionada;
            view.BindingContext = viewmodel;
            return view;
            //var masterDetailPage = Application.Current.MainPage as MasterDetailPage;
            //masterDetailPage.Detail = new NavigationPage(view);
            //masterDetailPage.IsPresented = false;
        }

        public async Task CargarMisOfertasAsync()
        {
            this.ShowLoading = true;
            //String token = await this.service.GetApiTokenAsync("onlyfoodes@gmail.com", "Admin123");
            //int idUsuario = 2;
            String token = App.ServiceLocator.SessionService.Token;
            int idUsuario = App.ServiceLocator.SessionService.Usuario.Id;
            this.Ofertas = new ObservableCollection<Oferta>
                (await this.service.GetOfertasUserByFiltroAsync(idUsuario, this.Filtro));
            if (this.Ofertas.Count == 0)
            {
                this.Mensaje = true;
                this.Imagen = true;
            }
            else
            {
                this.Mensaje = false;
                this.Imagen = false;
            }
            this.ShowLoading = false;
        }
        public Command Buscar
        {
            get
            {
                return new Command(async () =>
                {
                    FiltroPopupPage view = new FiltroPopupPage();
                    FiltroPopUpViewModel viewModel = App.ServiceLocator.FiltroPopUpViewModel;
                    viewModel.Filtro = this.Filtro;
                    viewModel.Pagina = 2;
                    await viewModel.LoadCadenasAsync();
                    view.BindingContext = viewModel;
                    await PopupNavigation.Instance.PushAsync(view);
                });
            }
        }
    }
}
