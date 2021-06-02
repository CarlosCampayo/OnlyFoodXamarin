using OnlyFoodXamarin.Base;
using OnlyFoodXamarin.Models;
using OnlyFoodXamarin.Repositories;
using OnlyFoodXamarin.Services;
using OnlyFoodXamarin.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OnlyFoodXamarin.ViewModels
{
    public class PerfilViewModel : ViewModelBase
    {
        RepositoryRealm repositoryRealm;
        OnlyFoodService service;

        public PerfilViewModel(OnlyFoodService service, RepositoryRealm repositoryRealm)
        {
            this.service = service;
            this.repositoryRealm = repositoryRealm;
            Task.Run(async () =>
            {
                await this.CargarUsuario();
            });
        }

        private Usuario _Usuario;
        public Usuario Usuario
        {
            get { return _Usuario; }
            set
            {
                this._Usuario = value;
                OnPropertyChanged("Usuario");
            }
        }

        public async Task CargarUsuario()
        {
            //String token = await this.service.GetApiTokenAsync("onlyfoodes@gmail.com", "Admin123");
            String token = App.ServiceLocator.SessionService.Token;
            int idUsuario = App.ServiceLocator.SessionService.Usuario.Id;
            this.Usuario = await this.service.GetUserByIdAsync(idUsuario, token);
            //this.Usuario = await this.service.GetUserByIdAsync(this.Usuario.Id,token);
        }
        //public async Task<OfertasView> CambiarEmailAsycn()
        //{
        //    OfertasView view = new OfertasView();
        //    OfertasViewModel viewmodel = App.ServiceLocator.OfertasViewModel;

        //    FiltroOfertas filtroOfertas = new FiltroOfertas();
        //    filtroOfertas.IdCadenas = new List<int>();
        //    filtroOfertas.IdCadenas.Add(this.CadenaSeleccionada.Id);
        //    filtroOfertas.Preciomax = 100;
        //    filtroOfertas.Preciomin = 0;
        //    viewmodel.Filtro = filtroOfertas;
        //    await viewmodel.LoadOfertas();
        //    view.BindingContext = viewmodel;
        //    return view;

        //    //var masterDetailPage = Application.Current.MainPage as MasterDetailPage;
        //    //masterDetailPage.Detail = new NavigationPage(view);
        //    //masterDetailPage.IsPresented = false;
        //}
        
        public async Task<CambiarUsernameView> CambiarUsername()
        {
            String token = App.ServiceLocator.SessionService.Token;
            int idUsaurio = App.ServiceLocator.SessionService.Usuario.Id;

            CambiarUsernameView view = new CambiarUsernameView();
            CambiarUsernameViewModel viewmodel = App.ServiceLocator.CambiarUsernameViewModel;
            viewmodel.Usuario = this.Usuario;
            view.BindingContext = viewmodel;
            return view;
        }

        public async Task<CambiarEmailView> CambiarEmail()
        {
            String token = App.ServiceLocator.SessionService.Token;
            int idUsaurio = App.ServiceLocator.SessionService.Usuario.Id;

            CambiarEmailView view = new CambiarEmailView();
            CambiarEmailViewModel viewmodel = App.ServiceLocator.CambiarEmailViewModel;
            viewmodel.Usuario = this.Usuario;
            view.BindingContext = viewmodel;
            return view;
        }

        public Command EliminarCuenta
        {
            get
            {
                return new Command(async() =>
                {
                    //String token = await this.service.GetApiTokenAsync("onlyfoodes@gmail.com", "Admin123");
                    String token = App.ServiceLocator.SessionService.Token;
                    int idUsaurio = App.ServiceLocator.SessionService.Usuario.Id;
                    await this.service.DeleteUserAsync(this.Usuario.Id, token);

                    LoginView view = new LoginView();
                    this.repositoryRealm.DeleteUsuario(idUsaurio);
                    this.EliminarUserSession();
                    //var masterDetailPage = Application.Current.MainPage as MasterDetailPage;
                    //masterDetailPage.Detail = new NavigationPage(new CadenasView());
                    //masterDetailPage.IsPresented = false;
                    App.LoadMainPage();
                });
            }
        }
        public Command CerrarSesion
        {
            get
            {
                return new Command(() =>
                {
                    int idUsaurio = App.ServiceLocator.SessionService.Usuario.Id;
                    this.repositoryRealm.DeleteUsuario(idUsaurio);
                    this.EliminarUserSession();
                    App.LoadMainPage();
                });
            }
        }

        private void EliminarUserSession()
        {
            App.ServiceLocator.SessionService.Password = null;
            App.ServiceLocator.SessionService.Token = null;
            App.ServiceLocator.SessionService.Usuario = null;
        }
    }
}
