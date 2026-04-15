using ApiDotNet.Data;
using ApiDotNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiDotNet.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> ValidarUsuario(string email, string senha)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email);

            if (usuario == null)
                return null;

            bool senhaValida = BCrypt.Net.BCrypt.Verify(senha, usuario.Senha);

            if (!senhaValida)
                return null;

            return usuario;
        }
    }
}