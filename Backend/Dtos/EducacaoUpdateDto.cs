namespace TrabukaApi.Dtos
{
    public class EducacaoUpdateDto
    {
        public string Curso { get; set; }
        public string Instituicao { get; set; }
        public DateTime PeriodoInicio { get; set; }
        public DateTime PeriodoFim { get; set; }
        public string Descricao { get; set; }
    }
} 