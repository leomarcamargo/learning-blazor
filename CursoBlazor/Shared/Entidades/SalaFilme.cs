using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CursoBlazor.Shared.Entidades
{
    public class SalaFilme
    {
        public int IdSala { get; set; }
        [ForeignKey("IdSala")]
        public virtual Sala Sala { get; set; }
        public int IdFilme { get; set; }
        [ForeignKey("IdFilme")]
        public virtual Filme Filme { get; set; }
    }
}
