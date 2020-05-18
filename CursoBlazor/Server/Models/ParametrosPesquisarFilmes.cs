using CursoBlazor.Shared.DTO;

namespace CursoBlazor.Server.Models
{
    public class ParametrosPesquisarFilmes
    {
        public int Pagina { get; set; } = 1;
        public int QuantidadeRegistros { get; set; } = 12;
        public PaginacaoDTO Paginacao =>
            new PaginacaoDTO
            {
                Pagina = Pagina,
                QuantidadeRegistro = QuantidadeRegistros
            };
        public string Titulo { get; set; }
        public int GeneroId { get; set; }
        public int SalaId { get; set; }
        public bool EmCartaz { get; set; }
        public bool Estreias { get; set; }
        public bool MaisVotados { get; set; }
    }
}
