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
    public partial class PerfilView : ContentPage
    {
        public PerfilView()
        {
            InitializeComponent();
            this.btnclose.Clicked += Btnclose_Clicked;
            this.btnEmail.Clicked += BtnEmail_Clicked;
            this.btnUserName.Clicked += BtnUserName_Clicked;
        }

        private async void BtnUserName_Clicked(object sender, EventArgs e)
        {
            PerfilViewModel vm = (PerfilViewModel)this.BindingContext;
            CambiarUsernameView view = await vm.CambiarUsername();
            await Navigation.PushAsync(view);
        }

        private async void BtnEmail_Clicked(object sender, EventArgs e)
        {
            PerfilViewModel vm = (PerfilViewModel)this.BindingContext;
            CambiarEmailView view = await vm.CambiarEmail();
            await Navigation.PushAsync(view);
        }

        private async void Btnclose_Clicked(object sender, EventArgs e)
        {
            PerfilViewModel vm = (PerfilViewModel)this.BindingContext;
            await vm.CerrarSesion();
        }
    }
}