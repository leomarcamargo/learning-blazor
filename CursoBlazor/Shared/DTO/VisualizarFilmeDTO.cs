using System;
using System.Collections.Generic;
using System.Text;
using CursoBlazor.Shared.Entidades;

namespace CursoBlazor.Shared.DTO
{
    public class VisualizarFilmeDTO
    {
        public Filme Filme { get; set; }
        public List<Genero> Generos { get; set; }
        public List<Pessoa> Atores { get; set; }
        public int VotoUsuario { get; set; }
        public double MediaVotos { get; set; }
    }
}
