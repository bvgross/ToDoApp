using System.Data;
using Dapper;
using Tarefas.Domain.Entities;

namespace Tarefas.Infrastructure.Data.Dapper;

public class TarefaRepository : ITarefaRepository
{
    private readonly IDbConnection _connection;

    public TarefaRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<IEnumerable<Tarefa>> GetByUsuarioIdAsync(int usuarioId)
    {
        var sql = @"SELECT t.*, u.Id, u.Nome, u.Email, u.Senha, u.Role
                    FROM application.tarefa t 
                    JOIN application.usuario u ON t.UsuarioId = u.Id 
                    WHERE t.UsuarioId = @usuarioId";

        var result = await _connection.QueryAsync<Tarefa, Usuario, Tarefa>(
            sql, 
            (tarefa, usuario) =>
            {
                tarefa.Usuario = usuario;
                return tarefa;
            },
            new { usuarioId = usuarioId },
            splitOn: "Id"
        );

        return result;
    }

    public async Task<Tarefa?> GetByIdAsync(int id)
    {
        var sql = @"SELECT t.*, u.Id, u.Nome, u.Email, u.Senha, u.Role
                    FROM application.tarefa t 
                    JOIN application.usuario u ON t.UsuarioId = u.Id 
                    WHERE t.Id = @Id";
        
        var result = await _connection.QueryAsync<Tarefa, Usuario, Tarefa>(
            sql,
            (t, u) =>
            {
                t.Usuario = u;
                return t;
            },
            new { Id = id },
            splitOn: "Id"
        );

        return result.FirstOrDefault();
    }
    
    public async Task<int> CreateAsync(Tarefa tarefa)
    {
        var sql = @"INSERT INTO application.tarefa (Titulo, Corpo, DataFinal, UsuarioId, Status)
                    VALUES (@Titulo, @Corpo, @DataFinal, @UsuarioId, @Status);
                    RETURNING Id";

        return await _connection.ExecuteScalarAsync<int>(sql, tarefa);
    }

    public async Task<bool> UpdateAsync(Tarefa tarefa)
    {
        var sql = @"UPDATE application.tarefa
                    SET Titulo = @Titulo, Corpo = @Corpo, DataFinal = @DataFinal, UsuarioId = @UsuarioId, Status = @Status
                    WHERE Id = @Id";

        var rows = await _connection.ExecuteAsync(sql, tarefa);
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var sql = @"DELETE FROM application.tarefa WHERE Id = @Id";
        var rows = await _connection.ExecuteAsync(sql, new { Id = id });
        return rows > 0;
    }
}