﻿using OnlyFoodXamarin.Models;
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
            paginas.Add(loginView);

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

            this.Detail=new NavigationPage(
                (Page)Activator.CreateInstance(typeof(CadenasView)));
            this.listviewMenu.ItemSelected += ListviewMenu_ItemSelected;
            this.listviewMenuUsuario.ItemSelected += ListviewMenu_ItemSelected;
        }

        private void ListviewMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var page= (MasterPageItem)e.SelectedItem;
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
            else
            {
                this.Detail = new NavigationPage(
                                (Page)Activator.CreateInstance(type));
            }
            //this.listviewMenu.SelectedItem = null;
            //this.listviewMenuUsuario.SelectedItem = null;
            this.IsPresented = false;
        }
    }
}