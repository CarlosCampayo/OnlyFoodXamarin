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
    public partial class OfertasView : ContentPage
    {
        public OfertasView()
        {
            InitializeComponent();
            this.cvOfertas.SelectionChanged += CvOfertas_SelectionChanged;
        }

        private async void CvOfertas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OfertasViewModel vm = (OfertasViewModel)this.BindingContext;
            DetalleOfertaView view = await vm.MostrarDetalleOfertaAsync();
            await Navigation.PushAsync(view);
        }
    }
}