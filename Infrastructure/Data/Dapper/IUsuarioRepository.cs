using Tarefas.Domain.Entities;

namespace Tarefas.Infrastructure.Data.Dapper;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByIdAsync(int id);
    Task<Usuario?> GetByEmailAsync(string email);
    Task<int> CreateAsync(Usuario usuario);
    Task<bool> UpdateAsync(Usuario usuario);
    Task<bool> DeleteAsync(int id);
}