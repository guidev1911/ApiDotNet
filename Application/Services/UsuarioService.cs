using ApiDotNet.Application.Interfaces;
using ApiDotNet.Domain.Entities;

namespace ApiDotNet.Application.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repo;

        public UsuarioService(IUsuarioRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Usuario>> Listar()
            => await _repo.Listar();

        public async Task<Usuario?> BuscarPorId(int id)
            => await _repo.BuscarPorId(id);

        public async Task<Usuario> Criar(Usuario usuario)
        {
            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);
            return await _repo.Criar(usuario);
        }

        public async Task<Usuario?> Atualizar(int id, Usuario usuario)
            => await _repo.Atualizar(id, usuario);

        public async Task<bool> Deletar(int id)
            => await _repo.Deletar(id);
    }
}