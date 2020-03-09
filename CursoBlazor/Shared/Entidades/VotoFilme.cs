using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CursoBlazor.Shared.Entidades
{
    public class VotoFilme
    {
        [Key]
        public int Id { get; set; }
        public int IdFilme { get; set; }
        [ForeignKey("IdFilme")]
        public virtual Filme Filme { get; set; }
        public int Voto { get; set; }
        public DateTime DataVoto { get; set; }
    }
}
