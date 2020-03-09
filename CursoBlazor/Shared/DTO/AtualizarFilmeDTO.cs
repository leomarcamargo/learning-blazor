using System;
using System.Collections.Generic;
using System.Text;
using CursoBlazor.Shared.Entidades;

namespace CursoBlazor.Shared.DTO
{
    public class AtualizarFilmeDTO
    {
        public Filme Filme { get; set; }
        public List<Pessoa> Atores { get; set; }
        public List<Genero> GenerosSelecionados { get; set; }
        public List<Genero> GenerosNaoSelecionados { get; set; }
    }
}
