using Microsoft.AspNetCore.Mvc;
using tarefas.application.dtos;
using Tarefas.Domain.Entities;
using Tarefas.Domain.Enums;
using Tarefas.Infrastructure.Data.Dapper;

namespace Tarefas.Application.Services;

public class TarefaService : ITarefaService
{
    private readonly ITarefaRepository _tarefaRepository;
    private readonly IUsuarioRepository _usuarioRepository;

    public TarefaService(ITarefaRepository tarefaRepository, IUsuarioRepository usuarioRepository)
    {
        _tarefaRepository = tarefaRepository;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<TarefaResponseDTO> CriarTarefaAsync(TarefaRequestDTO dto)
    {
        if (dto.UsuarioId <= 0)
            throw new Exception("ID de usuário inválido.");
        var usuario = await _usuarioRepository.GetByIdAsync(dto.UsuarioId);
        if (usuario == null)
            throw new Exception("Usuário não encontrado.");
        
        var tarefa = new Tarefa(dto);
        tarefa.Usuario = usuario;
        
        var id = await _tarefaRepository.CreateAsync(tarefa);
        tarefa.Id = id;

        return new TarefaResponseDTO(tarefa);
    }

    public async Task<TarefaResponseDTO?> AtualizarTarefaAsync(int id, TarefaRequestDTO dto)
    {
        var tarefaExistente = await _tarefaRepository.GetByIdAsync(id);
        if (tarefaExistente == null) return null;
        
        tarefaExistente.Titulo = dto.Titulo;
        tarefaExistente.Corpo = dto.Corpo;
        tarefaExistente.DataFinal = dto.DataFinal;

        if (tarefaExistente.DataFinal < DateTime.Now && tarefaExistente.Status == StatusTarefa.PROGRESSO)
        {
            tarefaExistente.Status = StatusTarefa.PENDENTE;
        }
        
        var atualizado = await  _tarefaRepository.UpdateAsync(tarefaExistente);
        
        if (!atualizado) return null;
        
        return new TarefaResponseDTO(tarefaExistente);
    }

    public async Task<TarefaResponseDTO?> MudarStatusTarefaAsync(int id, TarefaStatusRequestDTO dto)
    {
        var tarefaExistente = await _tarefaRepository.GetByIdAsync(id);
        if (tarefaExistente == null) return null;
        
        tarefaExistente.Status = dto.Status;

        if (tarefaExistente.DataFinal < DateTime.Now && tarefaExistente.Status == StatusTarefa.PROGRESSO)
        {
            tarefaExistente.Status = StatusTarefa.PENDENTE;
        }
        
        var atualizado = await  _tarefaRepository.UpdateAsync(tarefaExistente);
        
        if (!atualizado) return null;
        
        return new TarefaResponseDTO(tarefaExistente);
    }
    
    public async Task<TarefaResponseDTO?> BuscarTarefaAsync(int id)
    {
        var tarefa = await _tarefaRepository.GetByIdAsync(id);
        if (tarefa == null) return null;

        if (tarefa.DataFinal < DateTime.Now && tarefa.Status == StatusTarefa.PROGRESSO)
        {
            tarefa.Status = StatusTarefa.PENDENTE;
            await _tarefaRepository.UpdateAsync(tarefa);
        }
        
        return new TarefaResponseDTO(tarefa);
    }

    public async Task<IEnumerable<TarefaResponseDTO?>> BuscarTarefasPorUsuarioAsync(int id)
    {
        var tarefas = await _tarefaRepository.GetByUsuarioIdAsync(id);
        
        if (tarefas == null || !tarefas.Any()) 
            return Enumerable.Empty<TarefaResponseDTO>();
        
        foreach (var tarefa in tarefas) 
        {
            if (tarefa.DataFinal < DateTime.Now && tarefa.Status == StatusTarefa.PROGRESSO)
            {
                tarefa.Status = StatusTarefa.PENDENTE;
                await _tarefaRepository.UpdateAsync(tarefa);
            }
        }
        
        var tarefasDTO = tarefas.Select(t => new TarefaResponseDTO(t));

        return tarefasDTO;
    }

    public async Task<bool> DeletarTarefaAsync(int id)
    {
        var tarefaExistente = await _tarefaRepository.GetByIdAsync(id);
        if (tarefaExistente == null) return false;

        await _tarefaRepository.DeleteAsync(id);
        return true;
    }
}