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
    public class LoginViewModel : ViewModelBase
    {
        OnlyFoodService service;
        RepositoryRealm repositoryRealm;

        public LoginViewModel(OnlyFoodService service,RepositoryRealm repositoryRealm)
        {
            this.service = service;
            this.repositoryRealm = repositoryRealm;
            this.Usuario = new LoginModel();
        }

        private LoginModel _Usuario;
        public LoginModel Usuario
        {
            get { return _Usuario; }
            set
            {
                this._Usuario = value;
                OnPropertyChanged("Usuario");
            }
        }

        private String _Mensaje;
        public String Mensaje
        {
            get { return _Mensaje; }
            set
            {
                this._Mensaje = value;
                OnPropertyChanged("Mensaje");
            }
        }

        public Command Login
        {
            get
            {
                return new Command(async () =>
                {
                    //añadir accion de api de eliminar User
                    Usuario user = await this.service.LoginAsync(this.Usuario.Email, this.Usuario.Password);
                    if (user != null)
                    {
                        String token = await this.service.GetApiTokenAsync(this.Usuario.Email, this.Usuario.Password);
                        this.repositoryRealm.InsertarUsuario(user.Id, user.Email, this.Usuario.Password, user.Rol);
                        App.ServiceLocator.SessionService.Token = token;
                        App.ServiceLocator.SessionService.Password = this.Usuario.Password;
                        App.ServiceLocator.SessionService.Usuario = user;

                        App.LoadMainPage();
                    }
                    else
                    {
                        this.Mensaje = "Email o contraseña incorrectos";
                    }
                });
            }
        }
    }
}
