using OnlyFoodXamarin.Base;
using OnlyFoodXamarin.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace OnlyFoodXamarin.ViewModels
{
    public class EliminarUsuarioBuscadorViewModel: ViewModelBase
    {
        public EliminarUsuarioBuscadorViewModel()
        {
            this.Usuarios = new ObservableCollection<Usuario>();
            this.Usuarios.Add(new Usuario { Email = "a@a.es", UserName = "Pepe Ramirez" });
        }
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
    }
}
