using AutoMapper;
using ApiDotNet.Domain.Entities;
using ApiDotNet.Application.DTOs;

namespace ApiDotNet.Mappings
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            // Entity -> Response
            CreateMap<Usuario, UsuarioResponseDTO>();

            // CreateDTO -> Entity
            CreateMap<UsuarioCreateDTO, Usuario>();

            // UpdateDTO -> Entity
            CreateMap<UsuarioUpdateDTO, Usuario>();
        }
    }
}