using ApiDotNet.Application.DTOs;
using ApiDotNet.Application.Services;
using ApiDotNet.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiDotNet.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _service;
        private readonly IMapper _mapper;

        public UsuariosController(UsuarioService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            var usuarios = await _service.Listar();

            var usuariosDto = _mapper.Map<List<UsuarioResponseDTO>>(usuarios);

            return Ok(usuariosDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _service.BuscarPorId(id);

            var dto = _mapper.Map<UsuarioResponseDTO>(usuario);

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(UsuarioCreateDTO dto)
        {
            var usuario = _mapper.Map<Usuario>(dto);

            var novoUsuario = await _service.Criar(usuario);

            var response = _mapper.Map<UsuarioResponseDTO>(novoUsuario);

            return CreatedAtAction(
                nameof(GetById),
                new { id = response.Id },
                response
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UsuarioUpdateDTO dto)
        {
            var usuario = _mapper.Map<Usuario>(dto);

            var atualizado = await _service.Atualizar(id, usuario);

            var response = _mapper.Map<UsuarioResponseDTO>(atualizado);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Deletar(id);

            return NoContent();
        }
    }
}