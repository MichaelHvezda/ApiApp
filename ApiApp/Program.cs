using ApiApp.Data.Entities;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

//namespace ApiApp
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            CreateHostBuilder(args).Build().Run();
//        }

//        public static IHostBuilder CreateHostBuilder(string[] args) =>
//            Host.CreateDefaultBuilder(args).UseServiceProviderFactory(new AutofacServiceProviderFactory())
//                .ConfigureWebHostDefaults(webBuilder =>
//                {
//                    webBuilder
//                        .UseContentRoot(Directory.GetCurrentDirectory())
//                        .UseIISIntegration()
//                        .UseStartup<Startup>();
//                    webBuilder.UseStartup<Startup>()
//                        .CaptureStartupErrors(true)
//                        .ConfigureAppConfiguration((context, config) =>
//                        {
//                            config.AddJsonFile("appsettings.json", false);
//                            config.AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", true);
//                            config.AddEnvironmentVariables();
//                        });
//                });
//    }
//}

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DatabaseContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("DB"), p => p.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName))
   .EnableSensitiveDataLogging(), ServiceLifetime.Transient);

builder.Services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSwaggerGen();


builder.Configuration.AddJsonFile("appsettings.json", false);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true);
builder.Configuration.AddEnvironmentVariables();

var servicesAssembly = typeof(Program).Assembly;
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
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
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors(x => x
         .AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader());

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
    _ = endpoints.MapControllers();
});

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

    db.Database.Migrate();
}
app.Run();
