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
    public partial class EliminarUsuarioView : ContentPage
    {
        public EliminarUsuarioView()
        {
            InitializeComponent();
            this.btnEliminar.Clicked += BtnEliminar_Clicked;
        }

        private async void BtnEliminar_Clicked(object sender, EventArgs e)
        {
            EliminarUsuarioViewModel vm = (EliminarUsuarioViewModel)this.BindingContext;
            vm.EliminarUsuario.Execute(1);
            await Navigation.PopAsync();
        }
    }
}