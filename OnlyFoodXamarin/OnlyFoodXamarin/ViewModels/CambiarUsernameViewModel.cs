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
    public class CambiarUsernameViewModel : ViewModelBase
    {
        RepositoryRealm repositoryRealm;
        OnlyFoodService service;
        public CambiarUsernameViewModel(OnlyFoodService service, RepositoryRealm repositoryRealm)
        {
            this.service = service;
            this.repositoryRealm = repositoryRealm;
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
        public Command CambiarUsername
        {
            get
            {
                return new Command(async () =>
                {
                    String token = App.ServiceLocator.SessionService.Token;
                    int idUsuario = App.ServiceLocator.SessionService.Usuario.Id;
                    await this.service.EditUsernameUserAsync(idUsuario, this.Usuario.UserName, token);
                });
            }
        }
    }
}
