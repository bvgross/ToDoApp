using Tarefas.Domain.Entities;
using Tarefas.Domain.Enums;

namespace tarefas.application.dtos;

public class TarefaResponseDTO
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Corpo { get; set; }
    public DateTime DataInicial { get; set; }
    public DateTime DataFinal { get; set; }
    public StatusTarefa Status { get; set; }
    public UsuarioResponseDTO Usuario { get; set; }
    
    public TarefaResponseDTO(){}

    public TarefaResponseDTO(Tarefa tarefa)
    {
        Id = tarefa.Id;
        Titulo = tarefa.Titulo;
        Corpo = tarefa.Corpo;
        DataInicial = tarefa.DataInicial;
        DataFinal = tarefa.DataFinal;
        Status = tarefa.Status;
        Usuario = new UsuarioResponseDTO(tarefa.Usuario);
    }
}