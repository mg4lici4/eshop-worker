using EShop.Application.Interfaces.Repositories;
using EShop.Application.Interfaces.Services;
using EShop.Application.Services;
using EShop.Infraestructure.Persistence;
using EShop.Infraestructure.Repositories;
using EShop.Worker;
using Microsoft.EntityFrameworkCore;

const string WKESHOP_BD= "WkEShop_Bd";


var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "Worker EShop";
});

var eshopBd = builder.Configuration.GetConnectionString(WKESHOP_BD) ?? Environment.GetEnvironmentVariable(WKESHOP_BD);

builder.Services.AddDbContextFactory<EShopDbContext>(options =>
                    options.UseOracle(eshopBd));

builder.Services.AddTransient<ISesionRepository, SesionRepository>();

builder.Services.AddTransient<IGestorSesiones, GestorSesiones>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
