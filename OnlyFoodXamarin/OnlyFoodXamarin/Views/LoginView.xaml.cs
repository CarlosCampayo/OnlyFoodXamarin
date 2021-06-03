using OnlyFoodXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            this.btnLogin.Clicked += BtnLogin_Clicked;
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            LoginViewModel vm = (LoginViewModel)this.BindingContext;
            bool aux = await vm.Login();
            if(aux == true)
            {
                vm.showProgress = true;
                await this.progressBar.ProgressTo(1, 500, Easing.Linear);
                App.LoadMainPage();
            }
        }

        private async void Btnregister_Clicked(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new RegisterView());
        }
    }
}