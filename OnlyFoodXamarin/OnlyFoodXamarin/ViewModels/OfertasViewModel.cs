using OnlyFoodXamarin.Base;
using OnlyFoodXamarin.Models;
using OnlyFoodXamarin.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OnlyFoodXamarin.ViewModels
{
    public class OfertasViewModel : ViewModelBase
    {
        private ObservableCollection<Oferta> _Ofertas;
        public ObservableCollection<Oferta> Ofertas
        {
            get { return this._Ofertas; }
            set
            {
                this._Ofertas = value;
                OnPropertyChanged("Ofertas");
            }
        }

        public OfertasViewModel(int idcadena)
        {
            List<Oferta> ofertaslocal = new List<Oferta>()
            {
                new Oferta()
                {
                    Id = 1,
                    Codigo = "D105",
                    Descripcion="Cono de nata por 0,50€",
                    IdCadena=1,
                    IdUsuario=1,
                    Imagen= "https://onlyfood.s3.amazonaws.com/longchickenbk.jpg",
                    Precio=5.49,
                    Titulo="Menu mediano: Hamburguesa long chicken con patatas y bebida medianas",
                    Web= "https://www.burgerking.es/"
                },
                new Oferta()
                {
                    Id = 2,
                    Codigo = "B110",
                    Descripcion="Cono de nata por 0,50€",
                    IdCadena=2,
                    IdUsuario=1,
                    Imagen= "https://onlyfood.s3.amazonaws.com/B110.jpg",
                    Precio=24.99,
                    Titulo="8 TIRAS DE PECHUGA + 6 PIEZAS DE POLLO + 4 BEBIDAS + 4 PATATAS por 24,99€",
                    Web= "https://www.burgerking.es/"
                },
                new Oferta()
                {
                    Id = 3,
                    Codigo = "D105",
                    Descripcion="Cono de nata por 0,50€",
                    IdCadena=3,
                    IdUsuario=1,
                    Imagen= "https://onlyfood.s3.amazonaws.com/cbo.jpg",
                    Precio=5.45,
                    Titulo="Menú CBO: Hamburguesa CBO, bebida y patatas pequeñas",
                    Web= "https://www.burgerking.es/"
                }
            };

            this.Ofertas = new ObservableCollection<Oferta>
                (ofertaslocal.Where(x => x.IdCadena == idcadena));
        }
    }
}
