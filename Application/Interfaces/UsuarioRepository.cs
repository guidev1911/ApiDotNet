using ApiDotNet.Domain.Entities;

namespace ApiDotNet.Application.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> Listar();
        Task<Usuario?> BuscarPorId(int id);
        Task<Usuario?> BuscarPorEmail(string email);
        Task<Usuario> Criar(Usuario usuario);
        Task<Usuario?> Atualizar(int id, Usuario usuario);
        Task<bool> Deletar(int id);
        Task Salvar();
    }
}