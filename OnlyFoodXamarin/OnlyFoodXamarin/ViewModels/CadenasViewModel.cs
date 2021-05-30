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
    public class CadenasViewModel : ViewModelBase
    {
        OnlyFoodService service;

        public CadenasViewModel(OnlyFoodService service)
        {
            this.service = service;
            Task.Run(async () =>
            {
                await this.LoadCadenas();
            });
            //List<Cadena> cadenasLocal = new List<Cadena>()
            //{
            //    new Cadena()
            //    {
            //        Id = 1,
            //        Nombre = "Burguer King",
            //        Descripcion = "Burger King, también conocida como BK, es una cadena de establecimientos de comida rápida estadounidense con sede central en Miami, fundada por James McLamore y David Edgerton, presente a nivel internacional y especializada principalmente en la elaboración de hamburguesas.",
            //        Imagen = "https://onlyfood.blob.core.windows.net/imagenes/bklogo%20-%20copia.png",
            //        Web = "https://www.burgerking.es/"
            //    },
            //    new Cadena()
            //    {
            //        Id = 2,
            //        Nombre = "KFC",
            //        Descripcion = "Burger King, también conocida como BK, es una cadena de establecimientos de comida rápida estadounidense con sede central en Miami, fundada por James McLamore y David Edgerton, presente a nivel internacional y especializada principalmente en la elaboración de hamburguesas.",
            //        Imagen = "https://onlyfood.s3.amazonaws.com/kfc.png",
            //        Web = "https://www.burgerking.es/"
            //    },
            //    new Cadena()
            //    {
            //        Id = 3,
            //        Nombre = "McDonald's",
            //        Descripcion = "Burger King, también conocida como BK, es una cadena de establecimientos de comida rápida estadounidense con sede central en Miami, fundada por James McLamore y David Edgerton, presente a nivel internacional y especializada principalmente en la elaboración de hamburguesas.",
            //        Imagen = "https://onlyfood.s3.amazonaws.com/McDonaldsEmblema.png",
            //        Web = "https://www.burgerking.es/"
            //    },
            //};

            //this.Cadenas = new ObservableCollection<Cadena>(cadenasLocal);
        }

        private ObservableCollection<Cadena> _Cadenas;
        public ObservableCollection<Cadena> Cadenas {
            get { return this._Cadenas; }
            set
            {
                this._Cadenas = value;
                // Solo usa el parametro si el command lo tiene declarado
                //this.MostrarOfertas.Execute(1);  
                //this.MostrarOfertasFunction();
                OnPropertyChanged("Cadenas");
            }
        }
        private Cadena _CadenaSeleccionada;
        public Cadena CadenaSeleccionada
        {
            get { return this._CadenaSeleccionada; }
            set
            {
                this._CadenaSeleccionada = value;
                // Solo usa el parametro si el command lo tiene declarado
                //this.MostrarOfertasFunction();
                //Task.Run(async () =>
                //{
                //    await this.MostrarOfertasFunction();
                //});
                OnPropertyChanged("CadenaSeleccionada");
                this.MostrarOfertas.Execute(1);
            }
        }
        public async Task LoadCadenas()
        {
            List<Cadena> cadenas = await this.service.GetCadenasAsync();
            this.Cadenas = new ObservableCollection<Cadena>(cadenas);
        }
        private async Task MostrarOfertasFunction()
        {
            //int? idcadena;
            //if (this.CadenaSeleccionada != null)
            //{
            //    idcadena = this.CadenaSeleccionada.Id;
                
            //} else
            //{
            //    idcadena = null;
            //}

            OfertasView view = new OfertasView();
            OfertasViewModel viewmodel = App.ServiceLocator.OfertasViewModel;
            viewmodel.Filtro.IdCadenas.Add(this.CadenaSeleccionada.Id);
            await viewmodel.LoadOfertas();
            //view.BindingContext = viewmodel;
            Application.Current.MainPage.Navigation.PushModalAsync(view);
        }

        
        public Command MostrarOfertas
        {
            get
            {
                return new Command(async() =>
                {
                    await this.MostrarOfertasFunction();
                });
            }
        }

        
    }
}
