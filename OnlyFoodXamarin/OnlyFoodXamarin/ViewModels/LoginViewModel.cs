using OnlyFoodXamarin.Base;
using OnlyFoodXamarin.Models;
using OnlyFoodXamarin.Repositories;
using OnlyFoodXamarin.Services;
using OnlyFoodXamarin.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
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
            this.showProgress = false;
            this.ShowLoading = false;
            this.Usuario = new LoginModel();
        }

        private bool _showProgress;
        public bool showProgress
        {
            get { return _showProgress; }
            set
            {
                this._showProgress = value;
                OnPropertyChanged("showProgress");
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

        public async Task<bool> Login()
        {
            this.ShowLoading = true;
            bool aux;

            //añadir accion de api de eliminar User
            Usuario user = await this.service.LoginAsync(this.Usuario.Email, this.Usuario.Password);
            if (user != null)
            {
                this.Mensaje = "";
                String token = await this.service.GetApiTokenAsync(this.Usuario.Email, this.Usuario.Password);
                this.repositoryRealm.InsertarUsuario(user.Id, user.Email, this.Usuario.Password, user.Rol);
                App.ServiceLocator.SessionService.Token = token;
                App.ServiceLocator.SessionService.Password = this.Usuario.Password;
                App.ServiceLocator.SessionService.Usuario = user;
                aux = true;
            }
            else
            {
                this.Mensaje = "Email o contraseña incorrectos";
                aux = false;
            }
            this.ShowLoading = false;
            return aux;
        }
    }
}
