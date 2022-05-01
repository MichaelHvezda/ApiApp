using ApiApp.Data.Entities;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFrameworkCore;
using System.IO;

namespace ApiApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<DatabaseContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DB")), ServiceLifetime.Transient);

            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSwaggerGen();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var servicesAssembly = typeof(Program).Assembly;

            builder.RegisterInstance<IDateTimeProvider>(new LocalDateTimeProvider());

            builder.Register(r => new AspNetCoreUnitOfWorkRegistry(r.Resolve<IHttpContextAccessor>(), new AsyncLocalUnitOfWorkRegistry()))
               .As<IUnitOfWorkRegistry>()
               .SingleInstance();
            builder.RegisterType<EntityFrameworkUnitOfWorkProvider<DatabaseContext>>()
               .AsImplementedInterfaces()
               .SingleInstance();

            builder.RegisterAssemblyTypes(servicesAssembly)
               .AsClosedTypesOf(typeof(IRepository<,>))
               .AsImplementedInterfaces()
               .AsSelf()
               .InstancePerDependency();
            builder.RegisterAssemblyTypes(servicesAssembly)
               .AsClosedTypesOf(typeof(IQuery<,>))
               .AsImplementedInterfaces()
               .AsSelf()
               .InstancePerDependency();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();


            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Trip api");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
