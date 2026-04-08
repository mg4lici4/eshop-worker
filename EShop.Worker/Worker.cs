using EShop.Application.Interfaces.Services;

namespace EShop.Worker
{
    public class Worker(IGestorSesiones gestorSesiones, ILogger<Worker> logger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    await gestorSesiones.EliminarExpiradas();
                    if (logger.IsEnabled(LogLevel.Information))
                    {
                        logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    }
                    await Task.Delay(1000, stoppingToken);
                }
            }
            catch (OperationCanceledException)
            {
                // cancelación normal
            }
            finally
            {
                // 👇 AQUÍ va la lógica de apagado
                Console.WriteLine("Liberando recursos, cerrando conexiones...");
            }
        }
    }
}
