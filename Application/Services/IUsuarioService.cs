using tarefas.application.dtos;

namespace Tarefas.Application.Services;

public interface IUsuarioService
{
    Task<UsuarioResponseDTO> CriarUsuarioAsync(UsuarioRequestDTO dto);
    Task<UsuarioResponseDTO?> AtualizarUsuarioAsync(int id, UsuarioRequestDTO dto);
    Task<UsuarioResponseDTO?> BuscarUsuarioAsync(int id);
    Task<bool> DeletarUsuarioAsync(int id);
    Task<UsuarioResponseDTO?> MudarRoleAsync(int id, UsuarioRoleRequestDTO dto);
}