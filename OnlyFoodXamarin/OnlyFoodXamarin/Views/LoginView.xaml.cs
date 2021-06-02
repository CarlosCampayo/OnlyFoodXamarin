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
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
            this.btnregister.Clicked += Btnregister_Clicked;
        }

        private void Btnregister_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new RegisterView());
        }
    }
}