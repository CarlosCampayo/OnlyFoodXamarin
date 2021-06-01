using OnlyFoodXamarin.Models;
using OnlyFoodXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OnlyFoodXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OfertasUsuarioView : ContentPage
    {
        public OfertasUsuarioView()
        {
            InitializeComponent();
            this.cvOfertas.SelectionChanged += CvOfertas_SelectionChanged;
        }

        private async void CvOfertas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Oferta item = (Oferta)e.CurrentSelection.First();
            OfertasUsuarioViewModel vm = (OfertasUsuarioViewModel)this.BindingContext;
            DetalleOfertaUsuarioView view = await vm.MostrarDetalleOfertaAsync();
            await Navigation.PushAsync(view);

            //DetalleOfertaUsuarioView view = new DetalleOfertaUsuarioView();
            //DetalleOfertaUsuarioViewModel viewmodel =
            //    App.ServiceLocator.DetalleOfertaUsuarioViewModel;
            //viewmodel.Oferta = item;
            //view.BindingContext = viewmodel;

            //await Navigation.PushAsync(view);
        }
    }
}