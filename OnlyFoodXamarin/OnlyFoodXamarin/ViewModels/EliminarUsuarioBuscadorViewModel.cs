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
            //this.Usuarios = new ObservableCollection<Usuario>();
            //this.Usuarios.Add(new Usuario { Email = "a@a.es", UserName = "Pepe Ramirez" });
            //this.Usuarios.Add(new Usuario { Email = "a@23a.es", UserName = "Pepe Ramirez2" });
            //UploadService uploadService = new UploadService();
            //service = new OnlyFoodService(uploadService);
            //this.Usuarios = new ObservableCollection<Usuario>();
            this.ShowLoading = true;
            Task.Run(async () =>
            {
                await this.LoadUsuarios();
                this.ShowLoading = false;
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
                //this.SeleccionarUsuarioEliminar.Execute(1);
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
        public async Task LoadUsuarios()
        {
            String token = await this.service.GetApiTokenAsync("onlyfoodes@gmail.com", "Admin123");
            List<Usuario> usuarios = await this.service.GetUsuariosAsync(token);
            this.Usuarios = new ObservableCollection<Usuario>(usuarios);
        }
        private async Task LoadUsuariosFiltro()
        {
            String token = await this.service.GetApiTokenAsync("onlyfoodes@gmail.com", "Admin123");
            List<Usuario> usuarios = await this.service.GetUsuariosAsync(token);
            this.Usuarios = new ObservableCollection<Usuario>(usuarios);
        }
        public Command SeleccionarUsuarioEliminar
        {
            get
            {
                return new Command(() =>
                {
                    EliminarUsuarioViewModel viewModel = App.ServiceLocator.EliminarUsuarioViewModel;
                    EliminarUsuarioView view = new EliminarUsuarioView();
                    viewModel.Usuario = this.UsuarioSeleccionado;
                    view.BindingContext = viewModel;
                    Application.Current.MainPage.Navigation.PushModalAsync(view);
                });
            }
        }
        public Command BuscarUsuarios
        {
            get
            {
                return new Command(async() =>
                {
                    //llamada de api de BuscarUsuario
                    String token = await this.service.GetApiTokenAsync("onlyfoodes@gmail.com", "Admin123");
                    List<Usuario> usuarios = await this.service.GetUsuariosByEmailOrUsernameAsync(this.Filtro, token);
                    this.Usuarios = new ObservableCollection<Usuario>(usuarios);
                });
            }
        }
    }
}
