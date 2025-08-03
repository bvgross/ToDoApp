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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var usuarioResponse = await _usuarioService.BuscarUsuarioAsync(id);
        
        if (usuarioResponse == null) return NotFound();
        
        return Ok(usuarioResponse);
    }
    
}