using EShop.Application.Helpers;
using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using EShop.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infraestructure.Repositories
{
    public class SesionRepository(IDbContextFactory<EShopDbContext> dbContextFactory) : ISesionRepository
    {
        public async Task InactivarExpiradasAsync()
        {
            var fechaActual = FechaHelper.ActualUTC();
            await using var context = await dbContextFactory.CreateDbContextAsync();
            await context.Sesiones
                .Where(s => s.FechaExpiracion <= fechaActual && s.Activo == 1)
                .ExecuteUpdateAsync(s => s
                .SetProperty(x => x.Activo, 0)
                .SetProperty(x => x.FechaActualizacion, fechaActual));
        }

        public async Task<IReadOnlyList<SesionEntity>> ObtenerActivasAsync()
        {
            await using var context = await dbContextFactory.CreateDbContextAsync();
            return await context.Sesiones.AsNoTracking().Where(s => s.Activo == 1).ToListAsync();
        }

        public async Task<IReadOnlyList<SesionEntity>> ObtenerExpiradasAsync()
        {
            var fechaActual = FechaHelper.ActualUTC();
            await using var context = await dbContextFactory.CreateDbContextAsync();
            return await context.Sesiones.AsNoTracking().Where(s => s.FechaExpiracion <= fechaActual && s.Activo == 1).ToListAsync();
        }

        public async Task <IReadOnlyList<SesionEntity>> ObtenerRegistrosAsync()
        {
            await using var context = await dbContextFactory.CreateDbContextAsync();
            return await context.Sesiones.AsNoTracking().ToListAsync();
        }
    }
}
