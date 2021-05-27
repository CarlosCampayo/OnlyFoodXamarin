using OnlyFoodXamarin.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OnlyFoodXamarin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new CadenasView();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
