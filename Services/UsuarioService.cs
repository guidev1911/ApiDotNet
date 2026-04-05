using ApiDotNet.Data;
using ApiDotNet.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiDotNet.Services
{
    public class UsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> Listar()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario?> BuscarPorId(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<Usuario> Criar(Usuario usuario)
        {
            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario?> Atualizar(int id, Usuario usuarioAtualizado)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return null;

            usuario.Nome = usuarioAtualizado.Nome;
            usuario.Email = usuarioAtualizado.Email;

            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<bool> Deletar(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return false;

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}