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
            this._ShowLoading = true;
            Task.Run(async () =>
            {
                await this.LoadCadenasAsync();
                this.ShowLoading = false;
            });
        }

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
                //Task.Run(async () =>
                //{
                //    await this.MostrarOfertasFunction();
                //});
                this.MostrarOfertas.Execute(1);
            }
        }

        public async Task LoadCadenasAsync()
        {
            List<Cadena> cadenas = await this.service.GetCadenasAsync();
            this.Cadenas = new ObservableCollection<Cadena>(cadenas);
            this._ShowLoading = false;

        }

        private async Task MostrarOfertasFunction()
        {
            OfertasView view = new OfertasView();

            OfertasViewModel viewmodel = App.ServiceLocator.OfertasViewModel;

            FiltroOfertas filtroOfertas = new FiltroOfertas();
            filtroOfertas.IdCadenas.Add(this.CadenaSeleccionada.Id);
            viewmodel.Filtro = filtroOfertas;
            view.BindingContext = viewmodel;

            // LISTA NULA
            //viewmodel.Filtro.IdCadenas.Add(this.CadenaSeleccionada.Id);
            //SOBRA, AL CAMBIAR EL FILTRO SE ACTUALIZA
            //await viewmodel.LoadOfertas(); 
            //view.BindingContext = viewmodel;
            await Application.Current.MainPage.Navigation.PushModalAsync(view);
            //await App.Current.MainPage.Navigation.PushAsync(view);

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
