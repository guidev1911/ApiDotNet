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
        public IActionResult Get()
        {
            var usuarios = _service.Listar();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var usuario = _service.BuscarPorId(id);

            if (usuario == null)
                return NotFound("Usuário não encontrado");

            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult Post(Usuario usuario)
        {
            var novoUsuario = _service.Criar(usuario);

            return CreatedAtAction(
                nameof(GetById),
                new { id = novoUsuario.Id },
                novoUsuario
            );
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Usuario usuarioAtualizado)
        {
            var usuario = _service.Atualizar(id, usuarioAtualizado);

            if (usuario == null)
                return NotFound("Usuário não encontrado");

            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deletado = _service.Deletar(id);

            if (!deletado)
                return NotFound("Usuário não encontrado");

            return NoContent();
        }
    }
}