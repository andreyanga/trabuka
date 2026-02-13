using TrabukaApi.Dtos;

namespace TrabukaApi.Interfaces
{
    public interface IHeaderService
    {
        Task<HeaderJovemDto> GetHeaderJovemAsync(int usuarioId);
    }
} 