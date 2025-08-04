using System.ComponentModel.DataAnnotations;
using Tarefas.Domain.Enums;

namespace tarefas.application.dtos;

public class TarefaRequestDTO
{
    [Required(ErrorMessage = "O título da tarefa é obrigatóro")]
    public string Titulo { get; set; } = null!;
    
    [Required(ErrorMessage = "O corpo da tarefa é obrigatóro")]
    public string Corpo { get; set; } = null!;
    
    public DateTime DataFinal {get; set;}
    
    [Required(ErrorMessage = "O id do usuário é obrigatóro")]
    public int UsuarioId { get; set; }
}