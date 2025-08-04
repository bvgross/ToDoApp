using System.ComponentModel.DataAnnotations;

namespace tarefas.application.dtos;

public class UsuarioRequestDTO
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    public required string Nome { get; init; }

    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
    public required string Email { get; init; }

    [Required(ErrorMessage = "A senha é obrigatória.")]
    public required string Senha { get; init; }

    [Required(ErrorMessage = "A confirmação de senha é obrigatória.")]
    [Compare("Senha", ErrorMessage = "As senhas não coincidem.")]
    public required string ConfirmaSenha { get; init; }
}