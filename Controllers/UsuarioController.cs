using ApiDotNet.Models;
using ApiDotNet.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiDotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _service;

        public UsuariosController(UsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var usuarios = await _service.Listar();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _service.BuscarPorId(id);

            if (usuario == null)
                return NotFound("Usuário não encontrado");

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Usuario usuario)
        {
            var novoUsuario = await _service.Criar(usuario);

            return CreatedAtAction(
                nameof(GetById),
                new { id = novoUsuario.Id },
                novoUsuario
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Usuario usuarioAtualizado)
        {
            var usuario = await _service.Atualizar(id, usuarioAtualizado);

            if (usuario == null)
                return NotFound("Usuário não encontrado");

            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletado = await _service.Deletar(id);

            if (!deletado)
                return NotFound("Usuário não encontrado");

            return NoContent();
        }
    }
}