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
    public partial class CadenasView : ContentPage
    {
        public CadenasView()
        {
            InitializeComponent();
            this.cvcadenas.SelectionChanged += Cvcadenas_SelectionChanged;
        }

        private async void Cvcadenas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CadenasViewModel vm = (CadenasViewModel)this.BindingContext;
            OfertasView view = await vm.MostrarOfertaAsync();
            await Navigation.PushAsync(view);
        }
    }
}