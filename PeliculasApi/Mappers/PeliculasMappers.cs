using AutoMapper;
using PeliculasApi.Models;
using PeliculasApi.Models.DTO;

namespace PeliculasApi.Mappers
{
    public class PeliculasMappers : Profile
    {
        public PeliculasMappers()
        {
            CreateMap<Categoria, CategoriaDto>().ReverseMap();
        }
    }
}