using EShop.Application.Interfaces.Repositories;
using EShop.Application.Interfaces.Services;

namespace EShop.Application.Services
{
    public class GestorSesiones(ISesionRepository sesionRepository) : IGestorSesiones
    {
        public async Task EliminarExpiradas()
        {
            await sesionRepository.InactivarExpiradasAsync();
        }
    }
}
