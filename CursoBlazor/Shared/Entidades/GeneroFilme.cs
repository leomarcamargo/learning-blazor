using System.ComponentModel.DataAnnotations.Schema;

namespace CursoBlazor.Shared.Entidades
{
    public class GeneroFilme
    {
        public int IdGenero { get; set; }
        [ForeignKey("IdGenero")]
        public virtual Genero Genero { get; set; }
        public int IdFilme { get; set; }
        [ForeignKey("IdFilme")]
        public virtual Filme Filme { get; set; }
    }
}
