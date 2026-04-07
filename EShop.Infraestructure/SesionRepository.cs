using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Entities;
using EShop.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infraestructure
{
    public class SesionRepository(IDbContextFactory<EShopDbContext> dbContextFactory) : ISesionRepository
    {
        public async Task <IReadOnlyList<SesionEntity>> ObtenerRegistrosAsync()
        {
            await using var context = await dbContextFactory.CreateDbContextAsync();
            return await context.Sesiones.AsNoTracking().ToListAsync();
        }
    }
}
