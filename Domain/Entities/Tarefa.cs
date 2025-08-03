using Tarefas.Domain.Enums;

namespace Tarefas.Domain.Entities;

public class Tarefa
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Corpo  { get; set; } = string.Empty;
    public DateTime DataFinal { get; set; }
    public StatusTarefa Status { get; set; } = StatusTarefa.PROGRESSO;
    
    public int UsuarioId { get; set; } 
    public Usuario Usuario { get; set; } = null!; //o null! é pra garantir que o objeto nunca será null, que vou popular via join no Dapper
}