using OnlyFoodXamarin.Helpers;
using OnlyFoodXamarin.Models;
using OnlyFoodXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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
            this.btnEditar.Clicked += BtnEditar_Clicked;
        }

        private async void BtnEditar_Clicked(object sender, EventArgs e)
        {
            EditarOfertaViewModel vm = (EditarOfertaViewModel)this.BindingContext;
            vm.EditarOfertaAsync();
            await Navigation.PopAsync();
        }

        private async void Btnpickimage_Clicked(object sender, EventArgs e)
        {
            FileResult result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions 
            { 
                Title = "Selecciona una FOTO",
            });
            if(result != null)
            {
                Stream stream2 = await result.OpenReadAsync();
                this.resultImg.Source = ImageSource.FromStream(() => stream2);
                using (var stream = await result.OpenReadAsync()) {
                    EditarOfertaViewModel vm = (EditarOfertaViewModel)this.BindingContext;
                    UploadService service = new UploadService();
                    vm.NewImagen = await service.UploadImageBlobAzureAsycn(stream, result.FileName);
                }
            }
        }

    }
}