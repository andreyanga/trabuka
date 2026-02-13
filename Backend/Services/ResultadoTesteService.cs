using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;

namespace TrabukaApi.Services
{
    public class ResultadoTesteService : IResultadoTesteService
    {
        private readonly IResultadoTesteRepository _resultadoTesteRepository;

        public ResultadoTesteService(IResultadoTesteRepository resultadoTesteRepository)
        {
            _resultadoTesteRepository = resultadoTesteRepository;
        }

        public async Task<IEnumerable<ResultadoTesteReadDto>> GetAllAsync()
        {
            var resultados = await _resultadoTesteRepository.GetAllAsync();
            var dtos = new List<ResultadoTesteReadDto>();
            foreach (var resultado in resultados)
            {
                dtos.Add(MapToReadDto(resultado));
            }
            return dtos;
        }

        public async Task<ResultadoTesteReadDto> GetByIdAsync(int id)
        {
            var resultado = await _resultadoTesteRepository.GetByIdAsync(id);
            return resultado == null ? null : MapToReadDto(resultado);
        }

        public async Task<ResultadoTesteReadDto> CreateAsync(ResultadoTesteCreateDto dto)
        {
            var resultado = new ResultadoTeste
            {
                id_teste = dto.TesteId,
                id_usuario = dto.UsuarioId,
                pontuacao = dto.Pontuacao,
                nivel_atribuido = dto.NivelAtribuido,
                data_conclusao = dto.DataConclusao
            };
            await _resultadoTesteRepository.AddAsync(resultado);
            return MapToReadDto(resultado);
        }

        public async Task<ResultadoTesteReadDto> UpdateAsync(int id, ResultadoTesteUpdateDto dto)
        {
            var resultado = await _resultadoTesteRepository.GetByIdAsync(id);
            if (resultado == null) return null;
            resultado.pontuacao = dto.Pontuacao;
            resultado.nivel_atribuido = dto.NivelAtribuido;
            resultado.data_conclusao = dto.DataConclusao;
            _resultadoTesteRepository.Update(resultado);
            return MapToReadDto(resultado);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var resultado = await _resultadoTesteRepository.GetByIdAsync(id);
            if (resultado == null) return false;
            _resultadoTesteRepository.Delete(resultado);
            return true;
        }

        private ResultadoTesteReadDto MapToReadDto(ResultadoTeste resultado)
        {
            return new ResultadoTesteReadDto
            {
                Id = resultado.id_resultado,
                TesteId = resultado.id_teste,
                UsuarioId = resultado.id_usuario,
                Pontuacao = resultado.pontuacao,
                NivelAtribuido = resultado.nivel_atribuido,
                DataConclusao = resultado.data_conclusao
            };
        }
    }
} 