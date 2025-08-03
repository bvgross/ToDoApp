using Tarefas.Domain.Entities;

namespace Tarefas.Infrastructure.Data.Dapper;

public interface ITarefaRepository
{
    Task<IEnumerable<Tarefa>> GetByUsuarioIdAsync(int usuarioId);
    Task<Tarefa?> GetByIdAsync(int id);
    Task<int> CreateAsync(Tarefa tarefa);
    Task<bool> UpdateAsync(Tarefa tarefa);
    Task<bool> DeleteAsync(int id);
}