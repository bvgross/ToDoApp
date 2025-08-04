using tarefas.application.dtos;

namespace Tarefas.Domain.Entities;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public string Role { get; set; } = "user";
    
    public List<Tarefa>? Tarefas { get; set; }

    public Usuario(){}

    public Usuario(UsuarioRequestDTO dto)
    {
        Nome = dto.Nome;
        Email = dto.Email;
        Senha = dto.Senha;
    }
}