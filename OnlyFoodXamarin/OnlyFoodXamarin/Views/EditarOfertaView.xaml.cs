using OnlyFoodXamarin.Models;
using OnlyFoodXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OnlyFoodXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditarOfertaView : ContentPage
    {
        public EditarOfertaView()
        {
            InitializeComponent();
            this.btnpickimage.Clicked += Btnpickimage_Clicked;
        }

        private async void Btnpickimage_Clicked(object sender, EventArgs e)
        {
            FileResult result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions 
            { 
                Title = "Selecciona una FOTO",
            });
            if(result != null)
            {
                var content = new MultipartFormDataContent();
                content.Add(new StreamContent(await result.OpenReadAsync())) ;

                using (var stream = await result.OpenReadAsync()) {
                    //this.resultImg.Source = ImageSource.FromStream(() => stream);
                    EditarOfertaViewModel vm = (EditarOfertaViewModel)this.BindingContext;
                    vm.ResultImagen = stream;
                }
                
            }
        }

    }
}