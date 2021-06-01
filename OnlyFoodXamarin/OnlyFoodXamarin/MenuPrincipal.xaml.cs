using OnlyFoodXamarin.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OnlyFoodXamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPrincipal : MasterDetailPage
    {
        public MenuPrincipal()
        {
            InitializeComponent();
            List<MasterPageItem> paginas = new List<MasterPageItem>();
            List<MasterPageItem> paginasUsuario = new List<MasterPageItem>();
            var cadenasView = new MasterPageItem()
            {
                Titulo = "Cadenas",
                PaginaHija = typeof(CadenasView)
            };
            var ofertasView = new MasterPageItem()
            {
                Titulo = "Ofertas",
                PaginaHija = typeof(OfertasView)
            };
            var perfilView = new MasterPageItem()
            {
                Titulo = "Perfil",
                PaginaHija = typeof(PerfilView)
            };
            var nuevaOfertaView = new MasterPageItem()
            {
                Titulo = "Nueva oferta",
                PaginaHija = typeof(NuevaOfertaView)
            };
            var ofertasUsuarioView = new MasterPageItem()
            {
                Titulo = "Mis ofertas",
                PaginaHija = typeof(OfertasUsuarioView)
            };
            var nuevaCadenaView = new MasterPageItem()
            {
                Titulo = "Nueva cadena",
                PaginaHija = typeof(NuevaCadenaView)
            };
            var eliminarCadenaView = new MasterPageItem()
            {
                Titulo = "Eliminar cadena",
                PaginaHija = typeof(EliminarCadenaView)
            };
            var eliminarUsuarioBuscadorView = new MasterPageItem()
            {
                Titulo = "Eliminar usuario",
                PaginaHija = typeof(EliminarUsuarioBuscadorView)
            };
            paginas.Add(cadenasView);
            paginas.Add(ofertasView);
            paginasUsuario.Add(perfilView);
            paginasUsuario.Add(nuevaOfertaView);
            paginasUsuario.Add(ofertasUsuarioView);
            paginasUsuario.Add(nuevaCadenaView);
            paginasUsuario.Add(eliminarCadenaView);
            paginasUsuario.Add(eliminarUsuarioBuscadorView);

            this.listviewMenu.ItemsSource = paginas;
            this.listviewMenuUsuario.ItemsSource = paginasUsuario;

            this.Detail = new NavigationPage(
                (Page)Activator.CreateInstance(typeof(CadenasView)))
            {
                BarBackgroundColor = Color.FromHex("#e41b23"),
                Title = "ss"
            };
            this.Detail.IconImageSource = ImageSource.FromFile("logo.jpg");
            this.listviewMenu.ItemSelected += ListviewMenu_ItemSelected;
            this.listviewMenuUsuario.ItemSelected += ListviewMenu_ItemSelected;
        }

        private void ListviewMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var page= (MasterPageItem)e.SelectedItem;
            if(page != null)
            {
                Type type = page.PaginaHija;
                this.Detail = new NavigationPage(
                    (Page)Activator.CreateInstance(type))
                {
                    BarBackgroundColor = Color.FromHex("#e41b23"),
                };

                this.IsPresented = false;
                this.listviewMenu.SelectedItem = null;
                this.listviewMenuUsuario.SelectedItem = null;
            }
        }
    }
}