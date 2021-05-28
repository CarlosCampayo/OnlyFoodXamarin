using OnlyFoodXamarin.Base;
using OnlyFoodXamarin.Helpers;
using OnlyFoodXamarin.Models;
using OnlyFoodXamarin.Services;
using OnlyFoodXamarin.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace OnlyFoodXamarin.ViewModels
{
    public class EliminarUsuarioViewModel:ViewModelBase
    {
        OnlyFoodService service;
        public EliminarUsuarioViewModel(OnlyFoodService service)
        {
            this.service = service;
            //UploadService uploadService = new UploadService();
            //service = new OnlyFoodService(uploadService);
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
        public String Fullname
        {
            get { return this.Usuario.Nombre+" "+this.Usuario.Apellidos; }
        }
        public Command EliminarUsuario
        {
            get
            {
                return new Command(async() =>
                {
                    //añadir accion de api de eliminar User
                    String token = await this.service.GetApiTokenAsync("onlyfoodes@gmail.com", "Admin123");
                    await this.service.DeleteUserAsync(this.Usuario.Id, token);
                    EliminarUsuarioBuscadorViewModel viewModel = App.ServiceLocator.EliminarUsuarioBuscadorViewModel;
                    viewModel.LoadUsuarios();
                    //view.BindingContext = viewModel;
                    Application.Current.MainPage.Navigation.PushModalAsync(new EliminarUsuarioBuscadorView());
                });
            }
        }
    }
}
