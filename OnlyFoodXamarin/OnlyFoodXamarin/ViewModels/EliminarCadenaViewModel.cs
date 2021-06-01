using OnlyFoodXamarin.Base;
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
    public class EliminarCadenaViewModel:ViewModelBase
    {
        OnlyFoodService service;
        public EliminarCadenaViewModel(OnlyFoodService service)
        {
            this.service = service;
            Task.Run(async () =>
            {
                await this.LoadCadenas();
            });
        }

        private ObservableCollection<Cadena> _Cadenas;
        public ObservableCollection<Cadena> Cadenas
        {
            get { return _Cadenas; }
            set
            {
                this._Cadenas = value;
                OnPropertyChanged("Cadenas");
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
        #endregion

        private Cadena _CadenaSeleccionada;
        public Cadena CadenaSeleccionada
        {
            get { return _CadenaSeleccionada; }
            set
            {
                this._CadenaSeleccionada = value;
                OnPropertyChanged("CadenaSeleccionada");
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

        public async Task LoadCadenas()
        {
            this.ShowLoading = true;
            List<Cadena> cadenas = await this.service.GetCadenasAsync();
            this.Cadenas = new ObservableCollection<Cadena>(cadenas);
            this.ShowLoading = false;
        }

        public Command EliminarCadena
        {
            get
            {
                return new Command(async(cadena) =>
                {
                    String token = await this.service.GetApiTokenAsync("onlyfoodes@gmail.com", "Admin123");
                    Cadena c = (Cadena)cadena;
                    await this.service.DeleteCadenaAsync(c.Id,token);
                    this.Mensaje = "Cadena eliminada";
                    await this.LoadCadenas();
                });
            }
        }
    }
}
