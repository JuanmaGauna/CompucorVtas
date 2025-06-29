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
                .ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => src.Categoria != null ? src.Categoria.Nombre : "(Sin categoría)"));

            CreateMap<ProductoCreateDTO, Producto>();
        }
    }
}
