using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CursoBlazor.Shared.Entidades
{
    public class Sala
    {
        public Sala()
        {
            SalaFilme = new List<SalaFilme>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }

        public string Codigo { get; set; }

        public virtual List<SalaFilme> SalaFilme { get; set; }
    }
}
