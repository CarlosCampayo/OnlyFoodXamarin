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
    public class FiltroPopUpViewModel : ViewModelBase
    {
        OnlyFoodService service;

        public FiltroPopUpViewModel(OnlyFoodService service)
        {
            this.service = service;
            //this.Cadenas = new ObservableCollection<Cadena>();
            //if (Filtro == null)
            //{
            //    this.Filtro = new FiltroOfertas();
            //    this.Filtro.Preciomax = 100;
            //    this.Filtro.Preciomin = 0;
            //}
        }
        private ObservableCollection<Cadena> _Cadenas;
        public ObservableCollection<Cadena> Cadenas
        {
            get { return this._Cadenas; }
            set
            {
                this._Cadenas = value;
                OnPropertyChanged("Cadenas");
            }
        }
        private int _Pagina;
        public int Pagina
        {
            get { return this._Pagina; }
            set
            {
                this._Pagina = value;
                OnPropertyChanged("Pagina");
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
        public async Task LoadCadenasAsync()
        {
            this.ShowLoading = true;
            List<Cadena> cadenas = await this.service.GetCadenasAsync();
            this.Cadenas = new ObservableCollection<Cadena>(cadenas);
            this.ShowLoading = false;
        }
        public Command Buscar
        {
            get
            {
                return new Command(async () =>
                {
                    //List<int> lista = new List<int>();
                    //foreach(Cadena c in this.CadenaSeleccionada)
                    //{
                    //    lista.Add(c.Id);
                    //}
                    //this.Filtro.IdCadenas = lista;
                    if (this.Pagina == 1)
                    {
                        OfertasView view = new OfertasView();
                        OfertasViewModel viewmodel = App.ServiceLocator.OfertasViewModel;
                        viewmodel.Filtro = this.Filtro;
                        await viewmodel.LoadOfertas();
                        view.BindingContext = viewmodel;
                        var masterdetail = App.Current.MainPage as MasterDetailPage;
                        masterdetail.Detail = new NavigationPage(view);
                    }
                    else
                    {
                        OfertasUsuarioView view = new OfertasUsuarioView();
                        OfertasUsuarioViewModel viewmodel = App.ServiceLocator.OfertasUsuarioViewModel;
                        viewmodel.Filtro = this.Filtro;
                        await viewmodel.CargarMisOfertasAsync();
                        view.BindingContext = viewmodel;
                        var masterdetail = App.Current.MainPage as MasterDetailPage;
                        masterdetail.Detail = new NavigationPage(view);
                    }
                    await PopupNavigation.Instance.PopAsync();
                });
            }
        }
        public Command Meter
        {
            get
            {
                return new Command((cadena) =>
                {
                    Cadena c = (Cadena)cadena;
                    int cad = c.Id;
                    //int cad = int.Parse(cadena as String);
                    if (this.Filtro.IdCadenas.Contains(cad))
                    {
                        this.Filtro.IdCadenas.Remove(cad);
                    }
                    else
                    {
                        this.Filtro.IdCadenas.Add(cad);
                    }
                });
            }
        }
    }
}
