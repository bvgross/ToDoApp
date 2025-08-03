using Microsoft.AspNetCore.Mvc;
using tarefas.application.dtos;
using Tarefas.Domain.Entities;
using Tarefas.Infrastructure.Data.Dapper;

namespace Tarefas.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<UsuarioResponseDTO> CriarUsuarioAsync(UsuarioRequestDTO dto)
    {
        var usuario = new Usuario(dto);

        var id = await _usuarioRepository.CreateAsync(usuario);
        usuario.Id = id;
        
        return new UsuarioResponseDTO(usuario);
    }

    public async Task<UsuarioResponseDTO?> BuscarUsuarioAsync(int id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        
        if (usuario == null) return null;
        
        return new UsuarioResponseDTO(usuario);
    }
}