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
    public class CambiarEmailViewModel : ViewModelBase
    {
        RepositoryRealm repositoryRealm;
        OnlyFoodService service;
        public CambiarEmailViewModel(OnlyFoodService service, RepositoryRealm repositoryRealm)
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

        public Command CambiarEmail
        {
            get
            {
                return new Command(async() =>
                {
                    String token = App.ServiceLocator.SessionService.Token;
                    int idUsuario = App.ServiceLocator.SessionService.Usuario.Id;
                    await this.service.EditEmailUserAsync(idUsuario, this.Usuario.Email, token);
                    App.ServiceLocator.SessionService.Usuario.Email = this.Usuario.Email;
                    this.repositoryRealm.DeleteUsuario(idUsuario);
                    this.repositoryRealm.InsertarUsuario(
                    App.ServiceLocator.SessionService.Usuario.Id,
                    App.ServiceLocator.SessionService.Usuario.Email,
                    App.ServiceLocator.SessionService.Password,
                    App.ServiceLocator.SessionService.Usuario.Rol);
                });
            }
        }
    }
}
