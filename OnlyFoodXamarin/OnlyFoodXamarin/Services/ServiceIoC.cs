using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using OnlyFoodXamarin.ViewModels;
using OnlyFoodXamarin.Helpers;

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
            builder.RegisterType<DetalleOfertaViewModal>();
            builder.RegisterType<EliminarCadenaViewModel>();
            builder.RegisterType<EliminarUsuarioViewModel>();
            builder.RegisterType<EliminarUsuarioBuscadorViewModel>();
            builder.RegisterType<NuevaCadenaViewModel>();
            builder.RegisterType<NuevaOfertaViewModel>();
            builder.RegisterType<CadenasViewModel>();
            builder.RegisterType<OfertasViewModel>();

            this.container = builder.Build();
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
        public DetalleOfertaViewModal DetalleOfertaViewModal
        {
            get
            {
                return this.container.Resolve<DetalleOfertaViewModal>();
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
    }
}
