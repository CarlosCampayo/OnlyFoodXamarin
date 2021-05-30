using OnlyFoodXamarin.Base;
using OnlyFoodXamarin.Models;
using OnlyFoodXamarin.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OnlyFoodXamarin.ViewModels
{
    public class PerfilViewModel : ViewModelBase
    {
        OnlyFoodService service;
        public PerfilViewModel(OnlyFoodService service)
        {
            this.service = service;
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
            String token = await this.service.GetApiTokenAsync("onlyfoodes@gmail.com", "Admin123");
            this.Usuario = await this.service.GetUserByIdAsync(1, token);
            //this.Usuario = await this.service.GetUserByIdAsync(this.Usuario.Id,token);
        }
        public Command CambiarPaswword
        {
            get
            {
                return new Command(() =>
                {
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
                    String token = await this.service.GetApiTokenAsync("onlyfoodes@gmail.com", "Admin123");
                    await this.service.DeleteUserAsync(this.Usuario.Id, token);
                    //LoginView view = new LoginView();
                    //LoginViewModel viewmodel = App.ServiceLocator.LoginViewModel;
                    //viewmodel.Usuario = this.Usuario;
                    ////view.BindingContext = viewmodel;
                    //Application.Current.MainPage.Navigation.PushModalAsync(view);
                });
            }
        }
    }
}
