namespace Terkwaz.Web.Api
{
    using Autofac;
    using AutoMapper;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Swashbuckle.AspNetCore.Swagger;
    using System.Text;
    using Terkwaz.Data.Context;
    using Terkwaz.Web.Api.Blogs;
    using Terkwaz.Web.Api.Bootstraper;
    using Terkwaz.Web.Api.Identity;

    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var contection = Configuration["ConnectionStrings:TerkwazDbConnection"];
            services.AddDbContext<TerkwazDbContext>(options => options.UseSqlServer(contection));

            services.Configure<IdentityConfig>(Configuration.GetSection("IdentitySettings"));

            var identitySettings = new IdentityConfig();
            Configuration.GetSection("IdentitySettings")?.Bind(identitySettings);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = identitySettings.Issuer,
                        ValidAudience = identitySettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(identitySettings.SecurityKey))
                    };
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Version = "v1",
                        Title = "Terkwaz APIs",
                        Description = "Terkwaz - APIs documentation",
                        TermsOfService = "none",
                        Contact = new Contact
                        {
                            Name = "Mahmoud",
                            Email = "m.moghni99@gmail.com"
                        }
                    });
            });

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new UserMapper());
                cfg.AddProfile(new BlogMapper());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var contextService = serviceScope.ServiceProvider.GetService<TerkwazDbContext>();

                if (contextService.AllMigrationsApplied())
                {
                    contextService.EnsureMigrated();
                }
            }

            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "Terkwaz APIs v1");
                c.RoutePrefix = "docs";
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        public void ConfigureContainer(ContainerBuilder builder) =>
            builder.RegisterModule(new DependencyResolver(HostingEnvironment));
    }
}
