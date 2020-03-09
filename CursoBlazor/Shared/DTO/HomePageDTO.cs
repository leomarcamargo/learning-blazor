using System;
using System.Collections.Generic;
using System.Text;
using CursoBlazor.Shared.Entidades;

namespace CursoBlazor.Shared.DTO
{
    public class HomePageDTO
    {
        public List<Filme> EmCartaz { get; set; }
        public List<Filme> ProximosLancamentos { get; set; }
    }
}
