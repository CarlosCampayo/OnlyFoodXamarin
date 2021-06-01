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
        public Command CambiarPaswword
        {
            get
            {
                return new Command(() =>
                {
                    String token = App.ServiceLocator.SessionService.Token;
                    int idUsaurio = App.ServiceLocator.SessionService.Usuario.Id;
                    //CambiarPasswordView view = new CambiarPasswordView();
                    //CambiarPasswordViewViewModel viewmodel = App.ServiceLocator.CambiarPasswordViewViewModel;
                    //viewmodel.Usuario = this.Usuario;
                    ////view.BindingContext = viewmodel;
                    //Application.Current.MainPage.Navigation.PushModalAsync(view);
                });
            }
        }
        public Command CambiarUsername
        {
            get
            {
                return new Command(() =>
                {
                    String token = App.ServiceLocator.SessionService.Token;
                    int idUsaurio = App.ServiceLocator.SessionService.Usuario.Id;
                    //CambiarUsernameView view = new CambiarUsernameView();
                    //CambiarUsernameViewViewModel viewmodel = App.ServiceLocator.CambiarUsernameViewViewModel;
                    //viewmodel.Usuario = this.Usuario;
                    ////view.BindingContext = viewmodel;
                    //Application.Current.MainPage.Navigation.PushModalAsync(view);
                });
            }
        }
        public Command CambiarEmail
        {
            get
            {
                return new Command(() =>
                {
                    String token = App.ServiceLocator.SessionService.Token;
                    int idUsaurio = App.ServiceLocator.SessionService.Usuario.Id;
                    //CambiarEmailView view = new CambiarEmailView();
                    //CambiarEmailViewViewModel viewmodel = App.ServiceLocator.CambiarEmailViewViewModel;
                    //viewmodel.Usuario = this.Usuario;
                    ////view.BindingContext = viewmodel;
                    //Application.Current.MainPage.Navigation.PushModalAsync(view);
                });
            }
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
                    App.ServiceLocator.SessionService.Password = null;
                    App.ServiceLocator.SessionService.Token = null;
                    App.ServiceLocator.SessionService.Usuario = null;
                    //var masterDetailPage = Application.Current.MainPage as MasterDetailPage;
                    //masterDetailPage.Detail = new NavigationPage(new CadenasView());
                    //masterDetailPage.IsPresented = false;
                    App.LoadMainPage();


                    //await Application.Current.MainPage.Navigation.PushAsync(new MenuPrincipal());

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
                    App.ServiceLocator.SessionService.Password = null;
                    App.ServiceLocator.SessionService.Token = null;
                    App.ServiceLocator.SessionService.Usuario = null;
                    App.LoadMainPage();
                });
            }
        }
    }
}
