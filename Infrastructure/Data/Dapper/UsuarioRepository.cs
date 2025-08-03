using System.Data;
using Dapper;
using Tarefas.Domain.Entities;

namespace Tarefas.Infrastructure.Data.Dapper;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly IDbConnection _connection;

    public UsuarioRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<Usuario?> GetByIdAsync(int id)
    {
        var sql = "SELECT * FROM application.usuario WHERE Id = @Id";
        return await _connection.QueryFirstOrDefaultAsync<Usuario>(sql, new { Id = id });
    }

    public async Task<Usuario?> GetByEmailAsync(string email)
    {
        var sql = "SELECT * FROM application.usuario WHERE Email = @Email";
        return await _connection.QueryFirstOrDefaultAsync<Usuario>(sql, new { Email = email });
    }
    
    public async Task<int> CreateAsync(Usuario usuario)
    {
        var sql = @"INSERT INTO application.usuario (Nome, Email, Senha, Role)
                    VALUES (@Nome, @Email, @Senha, @Role)
                    RETURNING Id";

        return await _connection.ExecuteScalarAsync<int>(sql, usuario);
    }

    public async Task<bool> UpdateAsync(Usuario usuario)
    {
        var sql = @"UPDATE application.usuario
                    SET Nome = @Nome, Email = @Email, Senha = @Senha, Role = @Role
                    WHERE Id = @Id";

        var rows = await _connection.ExecuteAsync(sql, usuario);
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var sql = "DELETE FROM application.usuario WHERE Id = @Id";
        var rows = await _connection.ExecuteAsync(sql, new { Id = id });
        return rows > 0;
    }
}