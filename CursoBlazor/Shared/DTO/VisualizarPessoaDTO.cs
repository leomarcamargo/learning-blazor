using System.Collections.Generic;
using CursoBlazor.Shared.Entidades;

namespace CursoBlazor.Shared.DTO
{
    public class VisualizarPessoaDTO
    {
        public Pessoa Pessoa { get; set; }
        public List<Filme> Filmes { get; set; }
    }
}
