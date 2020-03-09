using System;
using System.Collections.Generic;
using System.Text;

namespace CursoBlazor.Shared.DTO
{
    public class PaginacaoDTO
    {
        public int Pagina { get; set; } = 1;
        public int QuantidadeRegistro { get; set; } = 2;
    }
}
