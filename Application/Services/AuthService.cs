using ApiDotNet.Application.Interfaces;
using ApiDotNet.Domain.Entities;
using ApiDotNet.Application.Exceptions;

namespace ApiDotNet.Application.Services
{
    public class AuthService
    {
        private readonly IUsuarioRepository _repo;

        public AuthService(IUsuarioRepository repo)
        {
            _repo = repo;
        }

        public async Task<Usuario> ValidarUsuario(string email, string senha)
        {
            var usuario = await _repo.BuscarPorEmail(email);

            if (usuario == null)
                throw new UnauthorizedException("Credenciais inválidas");

            bool senhaValida = BCrypt.Net.BCrypt.Verify(senha, usuario.Senha);

            if (!senhaValida)
                throw new UnauthorizedException("Credenciais inválidas");

            return usuario;
        }

        public async Task<Usuario?> AtualizarRefreshToken(Usuario usuario, string refreshToken)
        {
            usuario.RefreshToken = refreshToken;
            usuario.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _repo.Salvar();

            return usuario;
        }

        public async Task<Usuario?> BuscarPorRefreshToken(string refreshToken)
        {
            var usuarios = await _repo.Listar();

            return usuarios.FirstOrDefault(u => u.RefreshToken == refreshToken);
        }
    }
}