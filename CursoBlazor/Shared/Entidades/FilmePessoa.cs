using System.ComponentModel.DataAnnotations.Schema;

namespace CursoBlazor.Shared.Entidades
{
    public class FilmePessoa
    {
        public int IdPessoa { get; set; }
        [ForeignKey("IdPessoa")]
        public virtual Pessoa Pessoa { get; set; }
        public int IdFilme { get; set; }
        [ForeignKey("IdFilme")]
        public virtual Filme Filme { get; set; }
        public string Personagem { get; set; }
        public int Ordem { get; set; }
    }
}
