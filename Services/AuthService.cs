using ApiDotNet.Data;
using ApiDotNet.Models;
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
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email && u.Senha == senha);
        }
    }
}