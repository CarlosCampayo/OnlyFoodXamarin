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
                    await this.MostrarOfertasFunction();
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

        private async Task MostrarOfertasFunction()
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

            // LISTA NULA
            //viewmodel.Filtro.IdCadenas.Add(this.CadenaSeleccionada.Id);
            //SOBRA, AL CAMBIAR EL FILTRO SE ACTUALIZA
            //await viewmodel.LoadOfertas(); 
            //view.BindingContext = viewmodel;
            //await Application.Current.MainPage.Navigation.PushModalAsync(view);
            //await App.Current.MainPage.Navigation.PushAsync(view);
            //this.CadenaSeleccionada = null;

            var masterDetailPage = Application.Current.MainPage as MasterDetailPage;
            masterDetailPage.Detail = new NavigationPage(
                (Page)Activator.CreateInstance(typeof(OfertasView)))
            {
                BarBackgroundColor = Color.FromHex("#e41b23")
            };
            masterDetailPage.Detail = new NavigationPage(view);
            masterDetailPage.IsPresented = false;
        }

        public Command MostrarOfertas
        {
            get
            {
                return new Command(async() =>
                {
                    await this.MostrarOfertasFunction();
                });
            }
        }
    }
}
