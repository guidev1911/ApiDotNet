using AutoMapper;
using ApiDotNet.Models;
using ApiDotNet.DTOs;

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