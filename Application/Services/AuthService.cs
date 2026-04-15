using ApiDotNet.Application.Interfaces;
using ApiDotNet.Domain.Entities;

namespace ApiDotNet.Application.Services
{
    public class AuthService
    {
        private readonly IUsuarioRepository _repo;

        public AuthService(IUsuarioRepository repo)
        {
            _repo = repo;
        }

        public async Task<Usuario?> ValidarUsuario(string email, string senha)
        {
            var usuario = await _repo.BuscarPorEmail(email);

            if (usuario == null)
                return null;

            bool senhaValida = BCrypt.Net.BCrypt.Verify(senha, usuario.Senha);

            if (!senhaValida)
                return null;

            return usuario;
        }
    }
}