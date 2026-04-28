using ApiDotNet.Application.Interfaces;
using ApiDotNet.Application.Exceptions;
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

        public async Task<Usuario> BuscarPorId(int id)
        {
            var usuario = await _repo.BuscarPorId(id);

            if (usuario == null)
                throw new NotFoundException("Usuário não encontrado");

            return usuario;
        }

        public async Task<Usuario> Criar(Usuario usuario)
        {
            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);
            return await _repo.Criar(usuario);
        }

        public async Task<Usuario> Atualizar(int id, Usuario usuario)
        {
            var usuarioAtualizado = await _repo.Atualizar(id, usuario);

            if (usuarioAtualizado == null)
                throw new NotFoundException("Usuário não encontrado");

            return usuarioAtualizado;
        }

        public async Task<bool> Deletar(int id)
        {
            var deletado = await _repo.Deletar(id);

            if (!deletado)
                throw new NotFoundException("Usuário não encontrado");

            return true;
        }
    }
}