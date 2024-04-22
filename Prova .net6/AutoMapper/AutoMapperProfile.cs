using AutoMapper;
using Prova_.net6.Models;

namespace Prova_.net6.DTOs
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Boleto, Boleto>(); // Mapeia Boleto para Boleto
            CreateMap<Banco, Banco>(); // Mapeia Banco para Banco
        }
    }
}
