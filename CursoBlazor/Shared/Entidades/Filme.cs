using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursoBlazor.Shared.Entidades
{
    public class Filme
    {
        public Filme()
        {
            GeneroFilme = new List<GeneroFilme>();
            SalaFilme = new List<SalaFilme>();
            FilmePessoa = new List<FilmePessoa>();
            VotoFilme = new List<VotoFilme>();
        }

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "O título é obrigatório")]
        public string Titulo { get; set; }
        public string Resumo { get; set; }
        public bool EmCartaz { get; set; }
        public string Trailer { get; set; }
        [Required(ErrorMessage = "Informe a data de lançamento")]
        public DateTime? DataLancamento { get; set; }
        public string Poster { get; set; }
        public string TituloCurto
        {
            get
            {
                if (string.IsNullOrEmpty(Titulo))
                {
                    return string.Empty;
                }

                if (Titulo.Length > 60)
                {
                    return Titulo.Substring(0, 60) + "...";
                }

                return Titulo;
            }
        }

        public virtual List<GeneroFilme> GeneroFilme { get; set; }
        public virtual List<SalaFilme> SalaFilme { get; set; }
        public virtual List<FilmePessoa> FilmePessoa { get; set; }
        public virtual List<VotoFilme> VotoFilme { get; set; }
    }
}
