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
    public partial class CambiarUsernameView : ContentPage
    {
        public CambiarUsernameView()
        {
            InitializeComponent();
            this.btnGuardar.Clicked += BtnGuardar_Clicked;
        }

        private void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            CambiarUsernameViewModel vm = (CambiarUsernameViewModel)this.BindingContext;
            vm.CambiarUsername.Execute(1);
            Navigation.PopAsync();
        }
    }
}