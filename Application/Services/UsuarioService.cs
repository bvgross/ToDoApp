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

    public async Task<UsuarioResponseDTO?> AtualizarUsuarioAsync(int id, UsuarioRequestDTO dto)
    {
        var usuarioExistente = await _usuarioRepository.GetByIdAsync(id);
        if (usuarioExistente == null) return null;
        
        usuarioExistente.Nome = dto.Nome;
        usuarioExistente.Email = dto.Email;
        usuarioExistente.Senha = dto.Senha;
        
        var atualizado = await _usuarioRepository.UpdateAsync(usuarioExistente);
        
        if (!atualizado) return null;
        
        return new UsuarioResponseDTO(usuarioExistente);
    }

    public async Task<UsuarioResponseDTO?> MudarRoleAsync(int id, UsuarioRoleRequestDTO dto)
    {
        var usuarioExistente = await _usuarioRepository.GetByIdAsync(id);
        if (usuarioExistente == null) return null;

        usuarioExistente.Role = dto.Role;
        
        var atualizado = await _usuarioRepository.UpdateAsync(usuarioExistente);
        
        if (!atualizado) return null;
        
        return new UsuarioResponseDTO(usuarioExistente);
    }

    public async Task<UsuarioResponseDTO?> BuscarUsuarioAsync(int id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        
        if (usuario == null) return null;
        
        return new UsuarioResponseDTO(usuario);
    }

    public async Task<bool> DeletarUsuarioAsync(int id)
    {
        var usuarioExistente = await _usuarioRepository.GetByIdAsync(id);
        if (usuarioExistente == null) return false;
        
        await _usuarioRepository.DeleteAsync(id);
        return true;
    }
    
}