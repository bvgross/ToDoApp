using Microsoft.AspNetCore.Mvc;
using tarefas.application.dtos;
using Tarefas.Application.Services;
using Tarefas.Infrastructure.Data.Dapper;

namespace Tarefas.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TarefaController : ControllerBase
{
    private readonly ITarefaService  _tarefaService;

    public TarefaController(ITarefaService tarefaService)
    {
        _tarefaService = tarefaService;
    }

    [HttpPost]
    public async Task<IActionResult> RegistrarTarefa([FromBody] TarefaRequestDTO dto)
    {
        var tarefaResponse = await _tarefaService.CriarTarefaAsync(dto);
        
        return CreatedAtAction(nameof(BuscarPorId), new {id = tarefaResponse.Id}, tarefaResponse);
    }

    [HttpPut("{id}/atualizar")]
    public async Task<IActionResult> AtualizarTarefa([FromRoute] int id, [FromBody] TarefaRequestDTO dto)
    {
        var tarefaAtualizada = await _tarefaService.AtualizarTarefaAsync(id, dto);

        if (tarefaAtualizada == null) return NotFound();
        
        return Ok(tarefaAtualizada);
    }
    
    [HttpPut("{id}/status")]
    public async Task<IActionResult> AtualizarStatusTarefa([FromRoute] int id, [FromBody] TarefaStatusRequestDTO dto)
    {
        if (dto.Status == null)
            return BadRequest("Status não informado ou inválido.");
        var tarefaAtualizada = await _tarefaService.MudarStatusTarefaAsync(id, dto);

        if (tarefaAtualizada == null) return NotFound();
        
        return Ok(tarefaAtualizada);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarPorId(int id)
    {
        var tarefaResponse = await _tarefaService.BuscarTarefaAsync(id);
        
        if (tarefaResponse == null) return NotFound();
        
        return Ok(tarefaResponse);
    }
    
    [HttpGet("usuario/{usuarioId}")]
    public async Task<IActionResult> BuscarTarefasPorUsuario([FromRoute] int usuarioId)
    {
        var tarefas = await _tarefaService.BuscarTarefasPorUsuarioAsync(usuarioId);

        return Ok(tarefas);
    }

    [HttpDelete("{id}/deletar")]
    public async Task<IActionResult> DeletarTarefa([FromRoute] int id)
    {
        var deletado = await _tarefaService.DeletarTarefaAsync(id);
        
        if (!deletado) return NotFound();

        return NoContent();
    }
}