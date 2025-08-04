using Microsoft.AspNetCore.Mvc;
using tarefas.application.dtos;
using Tarefas.Application.Services;
using Tarefas.Domain.Entities;
using Tarefas.Infrastructure.Data.Dapper;

namespace Tarefas.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{
    
    private readonly IUsuarioService  _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }
    
    [HttpPost]
    public async Task<IActionResult> RegistrarUsuario([FromBody] UsuarioRequestDTO dto)
    {
        var usuarioResponse = await _usuarioService.CriarUsuarioAsync(dto);
        
        return CreatedAtAction(nameof(GetById), new { id = usuarioResponse.Id }, usuarioResponse);
    }

    [HttpPut("{id}/atualizar")]
    public async Task<IActionResult> AtualizarUsuario([FromRoute] int id, [FromBody] UsuarioRequestDTO dto)
    {
        var usuarioAtualizado = await _usuarioService.AtualizarUsuarioAsync(id, dto);
        
        if (usuarioAtualizado == null) return NotFound();
        
        return Ok(usuarioAtualizado);
    }

    [HttpPut("{id}/role")]
    public async Task<IActionResult> MudarRole([FromRoute] int id, [FromBody] UsuarioRoleRequestDTO dto)
    {
        var usuarioAtualizado = await _usuarioService.MudarRoleAsync(id, dto);
        
        if (usuarioAtualizado == null) return NotFound();
        
        return Ok(usuarioAtualizado);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var usuarioResponse = await _usuarioService.BuscarUsuarioAsync(id);
        
        if (usuarioResponse == null) return NotFound();
        
        return Ok(usuarioResponse);
    }

    [HttpDelete("{id}/deletar")]
    public async Task<IActionResult> DeletarUsuario(int id)
    {
        var deletado = await _usuarioService.DeletarUsuarioAsync(id);
        
        if (!deletado) return NotFound();

        return NoContent();
    }
    
}