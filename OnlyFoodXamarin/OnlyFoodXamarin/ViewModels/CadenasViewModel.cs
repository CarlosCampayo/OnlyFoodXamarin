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
    public class CadenasViewModel : ViewModelBase
    {
        OnlyFoodService service;

        public CadenasViewModel(OnlyFoodService service)
        {
            this.service = service;
            Task.Run(async () =>
            {
                await this.LoadCadenasAsync();
            });
            if (App.ServiceLocator.SessionService.Usuario != null)
            {
                Task.Run(async () =>
                {
                    App.ServiceLocator.SessionService.Token = await service.GetApiTokenAsync(App.ServiceLocator.SessionService.Usuario.Email, App.ServiceLocator.SessionService.Password);
                    App.ServiceLocator.SessionService.Usuario = await service.GetUserByIdAsync(App.ServiceLocator.SessionService.Usuario.Id, App.ServiceLocator.SessionService.Token);
                });
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
            }
        }
        #endregion

        private ObservableCollection<Cadena> _Cadenas;
        public ObservableCollection<Cadena> Cadenas {
            get { return this._Cadenas; }
            set
            {
                this._Cadenas = value;
                OnPropertyChanged("Cadenas");
            }
        }

        private Cadena _CadenaSeleccionada;
        public Cadena CadenaSeleccionada
        {
            get { return this._CadenaSeleccionada; }
            set
            {
                this._CadenaSeleccionada = value;
                OnPropertyChanged("CadenaSeleccionada");
                Task.Run(async () =>
                {
                    await this.MostrarOfertaAsync();
                });
            }
        }

        public async Task LoadCadenasAsync()
        {
            this.ShowLoading = true;
            List<Cadena> cadenas = await this.service.GetCadenasAsync();
            this.Cadenas = new ObservableCollection<Cadena>(cadenas);
            this.ShowLoading = false;
        }

        private async Task MostrarOfertaAsync()
        {
            OfertasView view = new OfertasView();
            OfertasViewModel viewmodel = App.ServiceLocator.OfertasViewModel;

            FiltroOfertas filtroOfertas = new FiltroOfertas();
            filtroOfertas.IdCadenas = new List<int>();
            filtroOfertas.IdCadenas.Add(this.CadenaSeleccionada.Id);
            filtroOfertas.Preciomax = 100;
            filtroOfertas.Preciomin = 0;
            viewmodel.Filtro = filtroOfertas;
            await viewmodel.LoadOfertas();
            view.BindingContext = viewmodel;

            var masterDetailPage = Application.Current.MainPage as MasterDetailPage;
            masterDetailPage.Detail = new NavigationPage(view);
            masterDetailPage.IsPresented = false;
        }
    }
}
