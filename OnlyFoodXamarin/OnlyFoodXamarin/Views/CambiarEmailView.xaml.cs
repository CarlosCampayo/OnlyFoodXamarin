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
    public partial class CambiarEmailView : ContentPage
    {
        public CambiarEmailView()
        {
            InitializeComponent();
            this.btnGuardar.Clicked += BtnGuardar_Clicked;
        }

        private void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            CambiarEmailViewModel vm = (CambiarEmailViewModel)this.BindingContext;
            vm.CambiarEmail.Execute(1);
            Navigation.PopAsync();
        }
    }
}