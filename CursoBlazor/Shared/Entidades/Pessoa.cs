using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursoBlazor.Shared.Entidades
{
    public class Pessoa
    {
        public Pessoa()
        {
            FilmePessoa = new List<FilmePessoa>();
        }
        
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }
        public string Biografia { get; set; }
        public string Foto { get; set; }
        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        public DateTime? DataNascimento { get; set; }
        public virtual List<FilmePessoa> FilmePessoa { get; set; }

        [NotMapped]
        public string Personagem { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Pessoa p2)
            {
                return Id == p2.Id;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
