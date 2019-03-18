namespace Terkwaz.Web.Api
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Autofac.Extensions.DependencyInjection;
    public class Program
    {
        public static void Main(string[] args)
        {
            WebHostBuilder(args).Run();
        }

        public static IWebHost WebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
            .ConfigureServices(services => services.AddAutofac())
            .Build();
    }
}
