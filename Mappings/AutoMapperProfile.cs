using AutoMapper;
using CompucorVtas.DTOs;
using CompucorVtas.Models;

namespace CompucorVtas.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Producto, ProductoDTO>()
                .ForMember(dest => dest.CategoriaNombre, opt => opt.MapFrom(src => src.Categoria!.Nombre))
                .ForMember(dest => dest.CategoriaId, opt => opt.MapFrom(src => src.CategoriaId));

            CreateMap<ProductoCreateDTO, Producto>();
        }
    }
}

