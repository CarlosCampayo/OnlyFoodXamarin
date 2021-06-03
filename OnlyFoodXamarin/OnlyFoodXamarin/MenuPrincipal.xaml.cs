using OnlyFoodXamarin.Models;
using OnlyFoodXamarin.Repositories;
using OnlyFoodXamarin.Services;
using OnlyFoodXamarin.ViewModels;
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
            RepositoryRealm repositoryRealm = new RepositoryRealm();
            OnlyFoodService service = new OnlyFoodService(new Helpers.UploadService());
            List<MasterPageItem> paginas = new List<MasterPageItem>();
            List<MasterPageItem> paginasUsuario = new List<MasterPageItem>();
            var loginView = new MasterPageItem()
            {
                Titulo = "Login",
                PaginaHija = typeof(LoginView)
            };
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
            UsuarioLoginRealm user = repositoryRealm.GetUsuarioLogin();
            if (user == null)
            {
                this.labelmenuusuario.Text = "";
                paginas.Add(loginView);
            }
            else
            {
                this.labelmenuusuario.Text = "Menu usuario";
                App.ServiceLocator.SessionService.Password = user.Password;
                App.ServiceLocator.SessionService.Usuario = new Usuario()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email=user.Email,
                    Rol=user.Rol
                };
                Task.Run(async () =>
                {
                    App.ServiceLocator.SessionService.Token = await service.GetApiTokenAsync(user.Email, user.Password);
                    App.ServiceLocator.SessionService.Usuario = await service.GetUserByIdAsync(user.Id, App.ServiceLocator.SessionService.Token);
                });
                paginasUsuario.Add(perfilView);
                paginasUsuario.Add(ofertasUsuarioView);
                paginasUsuario.Add(nuevaOfertaView);
                if (user.Rol == 1)
                {
                    paginasUsuario.Add(nuevaCadenaView);
                    paginasUsuario.Add(eliminarCadenaView);
                    paginasUsuario.Add(eliminarUsuarioBuscadorView);
                }
            }
            paginas.Add(cadenasView);
            paginas.Add(ofertasView);

            this.listviewMenu.ItemsSource = paginas;
            this.listviewMenuUsuario.ItemsSource = paginasUsuario;

            this.Detail = new NavigationPage(
                (Page)Activator.CreateInstance(typeof(CadenasView)));
            this.listviewMenu.ItemSelected += ListviewMenu_ItemSelected;
            this.listviewMenuUsuario.ItemSelected += ListviewMenu_ItemSelected;
        }

        private void ListviewMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var page= (MasterPageItem)e.SelectedItem;
            if(page != null)
            {
                Type type = page.PaginaHija;
                if (type == typeof(OfertasView))
                {
                    OfertasView view = new OfertasView();
                    OfertasViewModel viewmodel = App.ServiceLocator.OfertasViewModel;
                    FiltroOfertas filtroOfertas = new FiltroOfertas();
                    filtroOfertas.IdCadenas = new List<int>();
                    filtroOfertas.Preciomax = 100;
                    filtroOfertas.Preciomin = 0;
                    viewmodel.Filtro = filtroOfertas;
                    Task.Run(async () =>
                    {
                        await viewmodel.LoadOfertas();
                    });
                    view.BindingContext = viewmodel;
                    Detail = new NavigationPage(view);
                }
                else if (type == typeof(OfertasUsuarioView)){
                    OfertasUsuarioView view = new OfertasUsuarioView();
                    OfertasUsuarioViewModel viewmodel = App.ServiceLocator.OfertasUsuarioViewModel;
                    FiltroOfertas filtroOfertas = new FiltroOfertas();
                    filtroOfertas.IdCadenas = new List<int>();
                    filtroOfertas.Preciomax = 100;
                    filtroOfertas.Preciomin = 0;
                    viewmodel.Filtro = filtroOfertas;
                    Task.Run(async () =>
                    {
                        await viewmodel.CargarMisOfertasAsync();
                    });
                    view.BindingContext = viewmodel;
                    Detail = new NavigationPage(view);
                }else
                {
                    this.Detail = new NavigationPage(
                                    (Page)Activator.CreateInstance(type));
                }
                this.listviewMenu.SelectedItem = null;
                this.listviewMenuUsuario.SelectedItem = null;
                this.IsPresented = false;
            }
        }
    }
}