using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CursoBlazor.Shared.Entidades
{
    public class Genero
    {
        public Genero()
        {
            GeneroFilme = new List<GeneroFilme>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }

        public virtual List<GeneroFilme> GeneroFilme { get; set; }
    }
}
