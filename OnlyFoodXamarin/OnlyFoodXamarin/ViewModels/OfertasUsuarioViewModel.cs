using OnlyFoodXamarin.Base;
using OnlyFoodXamarin.Models;
using OnlyFoodXamarin.Services;
using OnlyFoodXamarin.Views;
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
            }
        }

        public OfertasUsuarioViewModel(OnlyFoodService service)
        {
            this.service = service;
            this.CargarMisOfertasFunction.Execute(1);
        }

        private async Task MostrarDetalleOfertaFunction()
        {
            DetalleOfertaUsuarioView view = new DetalleOfertaUsuarioView();
            DetalleOfertaUsuarioViewModel viewmodel = 
                App.ServiceLocator.DetalleOfertaUsuarioViewModel;
            viewmodel.Oferta = this.OfertaSeleccionada;
            view.BindingContext = viewmodel;
            var masterDetailPage = Application.Current.MainPage as MasterDetailPage;
            masterDetailPage.Detail = new NavigationPage(view);
            masterDetailPage.IsPresented = false;
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

        public async Task CargarMisOfertas()
        {
            String token = await this.service.GetApiTokenAsync("onlyfoodes@gmail.com", "Admin123");
            int idUsuario = 2;
            this.Ofertas = new ObservableCollection<Oferta>
                (await this.service.GetOfertasByIdUserAsync(idUsuario, token));
        }

        public Command CargarMisOfertasFunction
        {
            get
            {
                return new Command(async () =>
                {
                    await this.CargarMisOfertas();
                });
            }
        }
    }
}
