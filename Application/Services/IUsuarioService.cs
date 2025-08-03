using tarefas.application.dtos;

namespace Tarefas.Application.Services;

public interface IUsuarioService
{
    Task<UsuarioResponseDTO> CriarUsuarioAsync(UsuarioRequestDTO dto);
    Task<UsuarioResponseDTO?> BuscarUsuarioAsync(int id);
}