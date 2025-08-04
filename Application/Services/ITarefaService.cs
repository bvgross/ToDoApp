using tarefas.application.dtos;

namespace Tarefas.Application.Services;

public interface ITarefaService
{
    Task<TarefaResponseDTO> CriarTarefaAsync(TarefaRequestDTO dto);
    Task<TarefaResponseDTO?> AtualizarTarefaAsync(int id, TarefaRequestDTO dto);
    Task<TarefaResponseDTO?> BuscarTarefaAsync(int id);
    Task<IEnumerable<TarefaResponseDTO?>> BuscarTarefasPorUsuarioAsync(int id);
    Task<bool> DeletarTarefaAsync(int id);
    Task<TarefaResponseDTO?> MudarStatusTarefaAsync(int id, TarefaStatusRequestDTO dto);
}