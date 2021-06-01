﻿using OnlyFoodXamarin.Base;
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
                //this.SeleccionarUsuarioEliminar.Execute(1);
                Task.Run(async() => {
                    this.SeleccionarUsuarioAsync();
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

        private async Task SeleccionarUsuarioAsync()
        {

            EliminarUsuarioViewModel viewModel = App.ServiceLocator.EliminarUsuarioViewModel;
            EliminarUsuarioView view = new EliminarUsuarioView();
            viewModel.Usuario = this.UsuarioSeleccionado;
            view.BindingContext = viewModel;
            var masterDetailPage = Application.Current.MainPage as MasterDetailPage;
            masterDetailPage.Detail = new NavigationPage(view)
            {
                BarBackgroundColor = Color.FromHex("e41b23")
            };
            masterDetailPage.IsPresented = false;
        }

        public Command BuscarUsuarios
        {
            get
            {
                return new Command(async() =>
                {
                    this.ShowLoading = true;
                    //llamada de api de BuscarUsuario
                    String token = await this.service.GetApiTokenAsync("onlyfoodes@gmail.com", "Admin123");
                    List<Usuario> usuarios = await this.service.GetUsuariosByEmailOrUsernameAsync(this.Filtro, token);
                    this.Usuarios = new ObservableCollection<Usuario>(usuarios);
                    this.ShowLoading = false;
                });
            }
        }
    }
}
