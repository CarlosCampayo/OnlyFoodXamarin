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
    public partial class DetalleOfertaUsuarioView : ContentPage
    {
        public DetalleOfertaUsuarioView()
        {
            InitializeComponent();
            this.btnEliminar.Clicked += BtnEliminar_Clicked;
        }

        private async void BtnEliminar_Clicked(object sender, EventArgs e)
        {
            bool respuesta = await DisplayAlert("Confirmar", 
                "¿Está seguro que quiere eliminar la oferta?", "Sí", "No");
            if(respuesta)
            {
                DetalleOfertaUsuarioViewModel vm = 
                    (DetalleOfertaUsuarioViewModel)this.BindingContext;
                this.btnEliminar.Command = vm.Eliminar;
                this.btnEliminar.Command.Execute(1);
            }
        }
    }
}