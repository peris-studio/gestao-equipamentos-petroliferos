namespace GestaoEquipamentosPetroliferos.Controllers;

[Route("api/[controller]")]
[ApiController]

public class EquipamentoController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public EquipamentoController(ApplicationDbContext context)
    {
        _context = context;
    }

    // POST: api/Equipamento/inserir
    [HttpPost("inserir")]
    public async Task<IActionResult> Inserir([FromBody] EquipamentoDto equipamentoDto)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(equipamentoDto.Nome))
                return BadRequest("Nome é obrigatório");

            if (string.IsNullOrWhiteSpace(equipamentoDto.NumeroSerie))
                return BadRequest("Número de série é obrigatório");

            if (equipamentoDto.CapacidadeMaxima <= 0)
                return BadRequest("Capacidade máxima deve ser maior que zero");

            // Verifica ID duplicado
            if (await _context.Equipamentos.AnyAsync(e => e.Id == equipamentoDto.Id))
                return Conflict("ID indisponível");

            var equipamento = Equipamento.Inserir(equipamentoDto.Nome,
                                                    equipamentoDto.TipoEquipamento,
                                                    equipamentoDto.FabricanteEquipamento,
                                                    equipamentoDto.NumeroSerie,
                                                    equipamentoDto.DataInstalacao,
                                                    equipamentoDto.DataUltimaManutencao,
                                                    equipamentoDto.LocalizacaoEquipamento,
                                                    equipamentoDto.StatusOperacionalEquipamento,
                                                    equipamentoDto.CapacidadeMaxima,
                                                    equipamentoDto.Especificacoes,
                                                    equipamentoDto.Id
            );

            _context.Equipamentos.Add(equipamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObterPorId), new { id = equipamento.Id }, new
            {
                mensagem = "Equipamento criado com sucesso!",
                equipamento = equipamentoDto
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, new { title = "Erro no banco de dados", detail = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { title = "Erro interno", detail = ex.Message });
        }
    }

    // GET: api/Equipamento/obter-por-id/
    [HttpGet("obter-por-id/{id}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest("ID inválido");

        var equipamento = await _context.Equipamentos.FindAsync(id);

        var equipamentoDto = new EquipamentoDto(equipamento.Nome,
                                                equipamento.TipoEquipamento,
                                                equipamento.FabricanteEquipamento,
                                                equipamento.NumeroSerie,
                                                equipamento.DataInstalacao,
                                                equipamento.DataUltimaManutencao,
                                                equipamento.LocalizacaoEquipamento,
                                                equipamento.StatusOperacionalEquipamento,
                                                equipamento.CapacidadeMaxima,
                                                equipamento.Especificacoes,
                                                equipamento.Id
        );

        return Ok(equipamentoDto);
    }

    // GET: api/Equipamento/listar
    [HttpGet("listar")]
    public async Task<IActionResult> Listar()
    {
        var equipamentosDto = equipamentos.Select(e => new EquipamentoDto(e.Nome,
                                                                            e.TipoEquipamento,
                                                                            e.FabricanteEquipamento,
                                                                            e.NumeroSerie,
                                                                            e.DataInstalacao,
                                                                            e.DataUltimaManutencao,
                                                                            e.LocalizacaoEquipamento,
                                                                            e.StatusOperacionalEquipamento,
                                                                            e.CapacidadeMaxima,
                                                                            e.Especificacoes,
                                                                            e.Id
        )).ToList();

        return Ok(equipamentosDto);
    }

    // PATCH: api/Equipamento/atualizar
    [HttpPatch("atualizar/{id}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] EquipamentoDto equipamentoDto)
    {
        try
        {
            var equipamento = await _context.Equipamentos.FindAsync(id);

            if (equipamento == null)
                return NotFound("Equipamento não encontrado");

            if (equipamentoDto.Id != id)
                return BadRequest("ID não pode ser alterado");

            Equipamento.Atualizar(equipamento,
                                    equipamentoDto.Nome,
                                    equipamentoDto.TipoEquipamento,
                                    equipamentoDto.FabricanteEquipamento,
                                    equipamentoDto.NumeroSerie,
                                    equipamentoDto.DataInstalacao,
                                    equipamentoDto.DataUltimaManutencao,
                                    equipamentoDto.LocalizacaoEquipamento,
                                    equipamentoDto.StatusOperacionalEquipamento,
                                    equipamentoDto.CapacidadeMaxima,
                                    equipamentoDto.Especificacoes
            );

            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE: api/Equipamento/remover
    [HttpDelete("remover/{id}")]
    public async Task<IActionResult> Remover(Guid id)
    {
        var equipamento = await _context.Equipamentos.FindAsync(id);

        Equipamento.Remover(equipamento);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}