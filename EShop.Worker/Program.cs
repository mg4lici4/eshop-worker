using EShop.Application.Interfaces.Repositories;
using EShop.Application.Interfaces.Services;
using EShop.Application.Services;
using EShop.Infraestructure.Persistence;
using EShop.Infraestructure.Repositories;
using EShop.Worker;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

const string ESHOP_BD= "EShop_Worker";


var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "Worker EShop";
});

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .WriteTo.File(Path.Combine(AppContext.BaseDirectory, "logs", "log-.txt"), rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Logging.AddSerilog();

builder.Services.AddDbContextFactory<EShopDbContext>(options =>
                    options.UseOracle(builder.Configuration.GetConnectionString(ESHOP_BD)));

builder.Services.AddTransient<ISesionRepository, SesionRepository>();

builder.Services.AddTransient<IGestorSesiones, GestorSesiones>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
