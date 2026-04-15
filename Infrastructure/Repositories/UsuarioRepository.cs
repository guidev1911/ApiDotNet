using ApiDotNet.Application.Interfaces;
using ApiDotNet.Data;
using ApiDotNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiDotNet.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> Listar()
            => await _context.Usuarios.ToListAsync();

        public async Task<Usuario?> BuscarPorId(int id)
            => await _context.Usuarios.FindAsync(id);

        public async Task<Usuario?> BuscarPorEmail(string email)
            => await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email);

        public async Task<Usuario> Criar(Usuario usuario)
        {
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

        public async Task Salvar()
            => await _context.SaveChangesAsync();
    }
}