using TrabukaApi.Dtos;

namespace TrabukaApi.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardResumoDto> GetDashboardResumoAsync(int usuarioId);
    }
} 