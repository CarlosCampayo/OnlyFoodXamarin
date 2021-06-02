using OnlyFoodXamarin.Base;
using OnlyFoodXamarin.Models;
using OnlyFoodXamarin.Repositories;
using OnlyFoodXamarin.Services;
using OnlyFoodXamarin.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace OnlyFoodXamarin.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        RepositoryRealm repositoryRealm;
        OnlyFoodService service;

        public RegisterViewModel(OnlyFoodService service, RepositoryRealm repositoryRealm)
        {
            this.service = service;
            this.repositoryRealm = repositoryRealm;
            this.Usuario = new UsuarioLogin();
        }

        private UsuarioLogin _Usuario;
        public UsuarioLogin Usuario
        {
            get { return _Usuario; }
            set
            {
                this._Usuario = value;
                OnPropertyChanged("Usuario");
            }
        }
        
        public Command Registrar
        {
            get
            {
                return new Command(async() =>
                {
                    await this.service.NewUserAsync(this.Usuario.Email, this.Usuario.UserName, 
                        this.Usuario.Password, this.Usuario.Nombre, this.Usuario.Apellidos, 
                        this.Usuario.FechaNacimiento);

                    Usuario user = await this.service.LoginAsync(this.Usuario.Email, this.Usuario.Password);
                    this.repositoryRealm.InsertarUsuario(user.Id, user.Email, this.Usuario.Password, user.Rol);
                    String token = await this.service.GetApiTokenAsync(this.Usuario.Email, this.Usuario.Password);
                    App.ServiceLocator.SessionService.Token = token;
                    App.ServiceLocator.SessionService.Password = this.Usuario.Password;
                    App.ServiceLocator.SessionService.Usuario = user;
                    App.LoadMainPage();
                    //var masterDetailPage = Application.Current.MainPage as MasterDetailPage;
                    //masterDetailPage.Detail = new NavigationPage(
                    //    (Page)Activator.CreateInstance(typeof(CadenasView)));
                    //masterDetailPage.IsPresented = false;
                });
            }
        }
    }
}
