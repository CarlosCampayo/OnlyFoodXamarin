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
    public partial class EliminarUsuarioBuscadorView : ContentPage
    {
        public EliminarUsuarioBuscadorView()
        {
            InitializeComponent();
            this.lvUsuarios.ItemSelected += LvUsuarios_ItemSelected;
            this.searchBox.TextChanged += SearchBox_TextChanged;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            EliminarUsuarioBuscadorViewModel vm = (EliminarUsuarioBuscadorViewModel)this.BindingContext;
            vm.BuscarUsuarios.Execute(1);
        }

        private async void LvUsuarios_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            EliminarUsuarioBuscadorViewModel vm = (EliminarUsuarioBuscadorViewModel)this.BindingContext;
            EliminarUsuarioView view = await vm.SeleccionarUsuarioAsync();
            await Navigation.PushAsync(view);
        }
    }
}