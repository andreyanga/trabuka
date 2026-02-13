using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;
using TrabukaApi.Models.Enums;

namespace TrabukaApi.Services
{
    public class RelatorioService : IRelatorioService
    {
        private readonly IRelatorioRepository _relatorioRepository;

        public RelatorioService(IRelatorioRepository relatorioRepository)
        {
            _relatorioRepository = relatorioRepository;
        }

        public async Task<IEnumerable<RelatorioReadDto>> GetAllAsync()
        {
            var relatorios = await _relatorioRepository.GetAllAsync();
            var dtos = new List<RelatorioReadDto>();
            foreach (var relatorio in relatorios)
            {
                dtos.Add(MapToReadDto(relatorio));
            }
            return dtos;
        }

        public async Task<RelatorioReadDto> GetByIdAsync(int id)
        {
            var relatorio = await _relatorioRepository.GetByIdAsync(id);
            return relatorio == null ? null : MapToReadDto(relatorio);
        }

        public async Task<RelatorioReadDto> CreateAsync(RelatorioCreateDto dto)
        {
            var relatorio = new Relatorio
            {
                id_projeto = dto.ProjetoId,
                id_usuario = dto.UsuarioId,
                data_envio = dto.DataEnvio,
                evidencias = dto.Evidencias,
                descricao = dto.Descricao,
                feedback = dto.Feedback,
                status = (StatusRelatorio)dto.Status
            };
            await _relatorioRepository.AddAsync(relatorio);
            return MapToReadDto(relatorio);
        }

        public async Task<RelatorioReadDto> UpdateAsync(int id, RelatorioUpdateDto dto)
        {
            var relatorio = await _relatorioRepository.GetByIdAsync(id);
            if (relatorio == null) return null;
            relatorio.data_envio = dto.DataEnvio;
            relatorio.evidencias = dto.Evidencias;
            relatorio.descricao = dto.Descricao;
            relatorio.feedback = dto.Feedback;
            relatorio.status = (StatusRelatorio)dto.Status;
            _relatorioRepository.Update(relatorio);
            return MapToReadDto(relatorio);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var relatorio = await _relatorioRepository.GetByIdAsync(id);
            if (relatorio == null) return false;
            _relatorioRepository.Delete(relatorio);
            return true;
        }

        private RelatorioReadDto MapToReadDto(Relatorio relatorio)
        {
            return new RelatorioReadDto
            {
                Id = relatorio.id_relatorio,
                ProjetoId = relatorio.id_projeto,
                UsuarioId = relatorio.id_usuario,
                DataEnvio = relatorio.data_envio,
                Evidencias = relatorio.evidencias,
                Descricao = relatorio.descricao,
                Feedback = relatorio.feedback,
                Status = (int)relatorio.status
            };
        }
    }
} 