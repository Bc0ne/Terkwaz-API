namespace Terkwaz.Web.Api.Bootstraper
{
    using Autofac;
    using Microsoft.AspNetCore.Hosting;
    using Terkwaz.Data.Context;

    public class DependencyResolver : Module
    {
        private readonly IHostingEnvironment _env;

        public DependencyResolver(IHostingEnvironment env)
        {
            _env = env;
        }

        protected override void Load(ContainerBuilder builder)
        {
            LoadModules(builder);
        }

        private void LoadModules(ContainerBuilder builder)
        {
            builder.RegisterType<TerkwazDbContext>().InstancePerLifetimeScope();
        }
    }
}
