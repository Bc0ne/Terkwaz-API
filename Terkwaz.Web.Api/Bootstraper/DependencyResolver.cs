namespace Terkwaz.Web.Api.Bootstraper
{
    using Autofac;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Options;
    using Terkwaz.Data.Context;
    using Terkwaz.Data.Repositories;
    using Terkwaz.Web.Api.Identity;

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
            builder.RegisterType<UserRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<BlogRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.Register(context => context.Resolve<IOptions<IdentityConfig>>().Value);
        }
    }
}
