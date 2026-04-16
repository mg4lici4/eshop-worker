using EShop.Application.Helpers;
using EShop.Application.Interfaces.Services;

namespace EShop.Worker
{
    public class Worker(IGestorSesiones gestorSesiones, ILogger<Worker> logger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                logger.LogInformation("Inicio de ejecución de worker: {time}", FechaHelper.ActualUTC());
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        await gestorSesiones.EliminarExpiradas();
                    }
                    catch (OperationCanceledException)
                    {
                        throw;
                    }
                    catch (Exception e)
                    {
                        logger.LogError("Message: {Message}, StackTrace: {e.StackTrace}", e.Message, e.StackTrace);
                    }
                    
                    await Task.Delay(1000, stoppingToken);
                }                               
            }
            catch (OperationCanceledException)
            {
                logger.LogInformation("Servicio detenido. Liberando recursos, cerrando conexiones...");
            }
            finally
            {
                logger.LogInformation("Liberando recursos, cerrando conexiones...");
            }
        }
    }
}
