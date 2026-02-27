using ApiDotNet.Data;
using ApiDotNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiDotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var usuarios = _context.Usuarios.ToList();
            return Ok(usuarios);
        }

        [HttpPost]
        public IActionResult Post(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return Created("", usuario);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var usuario = _context.Usuarios.Find(id);

            if (usuario == null)
                return NotFound("Usuário não encontrado");

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();

            return NoContent(); 
        }
    }
}