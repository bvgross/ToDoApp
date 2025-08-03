using Tarefas.Domain.Entities;

namespace tarefas.application.dtos;

public class UsuarioResponseDTO
{
    public int Id { get; set; }
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;
    
    public UsuarioResponseDTO(){}

    public UsuarioResponseDTO(Usuario usuario)
    {
        Id = usuario.Id;
        Nome = usuario.Nome;
        Email = usuario.Email;
        Role = usuario.Role;
    }
}