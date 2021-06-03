using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using OnlyFoodXamarin.ViewModels;
using OnlyFoodXamarin.Helpers;
using OnlyFoodXamarin.Repositories;

namespace OnlyFoodXamarin.Services
{
    public class ServiceIoC
    {
        private IContainer container;
        public ServiceIoC()
        {
            this.RegisterDependencies();
        }
        private void RegisterDependencies()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<OnlyFoodService>();
            builder.RegisterType<UploadService>();
            builder.RegisterType<RepositoryRealm>();

            builder.RegisterType<DetalleOfertaViewModel>();
            builder.RegisterType<EliminarCadenaViewModel>();
            builder.RegisterType<EliminarUsuarioViewModel>();
            builder.RegisterType<EliminarUsuarioBuscadorViewModel>();
            builder.RegisterType<NuevaCadenaViewModel>();
            builder.RegisterType<NuevaOfertaViewModel>();
            builder.RegisterType<CadenasViewModel>();
            builder.RegisterType<OfertasViewModel>();
            builder.RegisterType<PerfilViewModel>();
            builder.RegisterType<OfertasUsuarioViewModel>();
            builder.RegisterType<DetalleOfertaUsuarioViewModel>();
            builder.RegisterType<EditarOfertaViewModel>();
            builder.RegisterType<LoginViewModel>();
            builder.RegisterType<SessionService>().SingleInstance();
            builder.RegisterType<RegisterViewModel>();
            builder.RegisterType<CambiarEmailViewModel>();
            builder.RegisterType<CambiarUsernameViewModel>();
            builder.RegisterType<FiltroPopUpViewModel>().SingleInstance();
            
            this.container = builder.Build();
        }
        public FiltroPopUpViewModel FiltroPopUpViewModel
        {
            get { return this.container.Resolve<FiltroPopUpViewModel>(); }
        }
        public CambiarEmailViewModel CambiarEmailViewModel
        {
            get { return this.container.Resolve<CambiarEmailViewModel>(); }
        }
        public CambiarUsernameViewModel CambiarUsernameViewModel
        {
            get { return this.container.Resolve<CambiarUsernameViewModel>(); }
        }
        public RegisterViewModel RegisterViewModel
        {
            get { return this.container.Resolve<RegisterViewModel>(); }
        }
        public SessionService SessionService
        {
            get { return this.container.Resolve<SessionService>(); }
        }
        public LoginViewModel LoginViewModel
        {
            get
            {
                return this.container.Resolve<LoginViewModel>();
            }
        }
        public PerfilViewModel PerfilViewModel
        {
            get
            {
                return this.container.Resolve<PerfilViewModel>();
            }
        }
        public CadenasViewModel CadenasViewModel
        {
            get
            {
                return this.container.Resolve<CadenasViewModel>();
            }
        }
        public OfertasViewModel OfertasViewModel
        {
            get
            {
                return this.container.Resolve<OfertasViewModel>();
            }
        }
        public DetalleOfertaViewModel DetalleOfertaViewModel
        {
            get
            {
                return this.container.Resolve<DetalleOfertaViewModel>();
            }
        }
        public EliminarCadenaViewModel EliminarCadenaViewModel
        {
            get
            {
                return this.container.Resolve<EliminarCadenaViewModel>();
            }
        }
        public EliminarUsuarioBuscadorViewModel EliminarUsuarioBuscadorViewModel
        {
            get
            {
                return this.container.Resolve<EliminarUsuarioBuscadorViewModel>();
            }
        }
        public EliminarUsuarioViewModel EliminarUsuarioViewModel
        {
            get
            {
                return this.container.Resolve<EliminarUsuarioViewModel>();
            }
        }
        public NuevaCadenaViewModel NuevaCadenaViewModel
        {
            get
            {
                return this.container.Resolve<NuevaCadenaViewModel>();
            }
        }
        public NuevaOfertaViewModel NuevaOfertaViewModel
        {
            get
            {
                return this.container.Resolve<NuevaOfertaViewModel>();
            }
        }
        public OfertasUsuarioViewModel OfertasUsuarioViewModel
        {
            get
            {
                return this.container.Resolve<OfertasUsuarioViewModel>();
            }
        }
        public DetalleOfertaUsuarioViewModel DetalleOfertaUsuarioViewModel
        {
            get
            {
                return this.container.Resolve<DetalleOfertaUsuarioViewModel>();
            }
        }

        public EditarOfertaViewModel EditarOfertaViewModel
        {
            get
            {
                return this.container.Resolve<EditarOfertaViewModel>();
            }
        }
    }
}
