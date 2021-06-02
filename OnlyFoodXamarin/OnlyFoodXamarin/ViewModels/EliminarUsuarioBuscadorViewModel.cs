using OnlyFoodXamarin.Base;
using OnlyFoodXamarin.Helpers;
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
    public class EliminarUsuarioBuscadorViewModel: ViewModelBase
    {
        OnlyFoodService service;
        public EliminarUsuarioBuscadorViewModel(OnlyFoodService service)
        {
            this.service = service;
            Task.Run(async () =>
            {
                await this.LoadUsuariosAsync();
            });
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
        #endregion

        private ObservableCollection<Usuario> _Usuarios;
        public ObservableCollection<Usuario> Usuarios
        {
            get { return _Usuarios; }
            set
            {
                this._Usuarios = value;
                OnPropertyChanged("Usuarios");
            }
        }

        private Usuario _UsuarioSeleccionado;
        public Usuario UsuarioSeleccionado
        {
            get { return _UsuarioSeleccionado; }
            set
            {
                this._UsuarioSeleccionado = value;
                Task.Run(async() => {
                    await this.SeleccionarUsuarioAsync();
                });
                
                OnPropertyChanged("UsuarioSeleccionado");
            }
        }

        private String _Filtro;
        public String Filtro
        {
            get { return _Filtro; }
            set
            {
                this._Filtro = value;
                OnPropertyChanged("Filtro");
            }
        }

        public async Task LoadUsuariosAsync()
        {
            this.ShowLoading = true;
            String token = await this.service.GetApiTokenAsync("onlyfoodes@gmail.com", "Admin123");
            List<Usuario> usuarios = await this.service.GetUsuariosAsync(token);
            this.Usuarios = new ObservableCollection<Usuario>(usuarios);
            this.ShowLoading = false;
        }

        public async Task<EliminarUsuarioView> SeleccionarUsuarioAsync()
        {
            EliminarUsuarioViewModel viewModel = App.ServiceLocator.EliminarUsuarioViewModel;
            EliminarUsuarioView view = new EliminarUsuarioView();
            viewModel.Usuario = this.UsuarioSeleccionado;
            view.BindingContext = viewModel;
            return view;
            //var masterDetailPage = Application.Current.MainPage as MasterDetailPage;
            //masterDetailPage.Detail = new NavigationPage(view);
            //masterDetailPage.IsPresented = false;
        }

        private async Task BuscarUsuariosAsync()
        {
            this.ShowLoading = true;
            String token = App.ServiceLocator.SessionService.Token;
            //String token = await this.service.GetApiTokenAsync("onlyfoodes@gmail.com", "Admin123");
            if(this.Filtro != "")
            {
                List<Usuario> usuarios = await this.service.GetUsuariosByEmailOrUsernameAsync(this.Filtro, token);
                this.Usuarios = new ObservableCollection<Usuario>(usuarios);
            } else
            {
                await this.LoadUsuariosAsync();
            }
            this.ShowLoading = false;
        }

        public Command BuscarUsuarios
        {
            get
            {
                return new Command(async() =>
                {
                    await this.BuscarUsuariosAsync();
                });
            }
        }
    }
}
