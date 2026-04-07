using EShop.Domain.Entities;

namespace EShop.Application.Interfaces.Repositories
{
    public interface ISesionRepository
    {
        Task<IReadOnlyList<SesionEntity>> ObtenerRegistrosAsync();
        Task<IReadOnlyList<SesionEntity>> ObtenerActivasAsync();
        Task<IReadOnlyList<SesionEntity>> ObtenerExpiradasAsync();
        Task InactivarExpiradasAsync();
    }
}
