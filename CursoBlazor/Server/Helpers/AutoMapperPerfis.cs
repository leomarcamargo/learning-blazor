using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CursoBlazor.Shared.Entidades;

namespace CursoBlazor.Server.Helpers
{
    public class AutoMapperPerfis : Profile
    {
        public AutoMapperPerfis()
        {
            CreateMap<Pessoa, Pessoa>()
                .ForMember(x => x.Foto, opt => opt.Ignore());

            CreateMap<Filme, Filme>()
                .ForMember(x => x.Poster, opt => opt.Ignore());
        }
    }
}
