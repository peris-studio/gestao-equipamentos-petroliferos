namespace GestaoEquipamentosPetroliferos.Controllers;

[Route("api/[controller]")]
[ApiController]

public class IncidenteSegurancaController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public IncidenteSegurancaController(ApplicationDbContext context)
    {
        _context = context;
    }

    // POST: api/IncidenteSeguranca/inserir
    [HttpPost("inserir")]
    public async Task<IActionResult> Inserir([FromBody] IncidenteSegurancaDto incidenteSegurancaDto)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(incidenteSegurancaDto.Descricao))
                return BadRequest("Descrição é obrigatória");

            if (incidenteSegurancaDto.DataIncidente > DateTime.UtcNow)
                return BadRequest("Data do incidente não pode ser futura");

            if (incidenteSegurancaDto.Id == Guid.Empty)
                incidenteSegurancaDto = incidenteSegurancaDto with { Id = Guid.NewGuid() };

            if (await _context.IncidentesSeguranca.AnyAsync(i => i.Id == incidenteSegurancaDto.Id))
                return Conflict("ID já está em uso");

            var incidenteSeguranca = IncidenteSeguranca.Inserir(incidenteSegurancaDto.DataIncidente,
                                                                incidenteSegurancaDto.TipoIncidente,
                                                                incidenteSegurancaDto.Descricao,
                                                                incidenteSegurancaDto.GravidadeIncidente,
                                                                incidenteSegurancaDto.CausaRaiz,
                                                                incidenteSegurancaDto.AcaoCorretiva,
                                                                incidenteSegurancaDto.Responsavel,
                                                                incidenteSegurancaDto.DataInvestigacao,
                                                                incidenteSegurancaDto.EquipamentoId,
                                                                incidenteSegurancaDto.Id);

            _context.IncidentesSeguranca.Add(incidenteSeguranca);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObterPorId), new { id = incidenteSeguranca.Id }, new
            {
                mensagem = "Incidente de segurança criado com sucesso!",
                incidenteSeguranca = incidenteSegurancaDto
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

    // GET: api/IncidenteSeguranca/obter-por-id/5
    [HttpGet("obter-por-id/{id}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest("ID inválido");

        var incidenteSeguranca = await _context.IncidentesSeguranca.FindAsync(id);

        if (incidenteSeguranca == null || !incidenteSeguranca.Ativo)
            return NotFound("Incidente de segurança não encontrado");

        var incidenteSegurancaDto = new IncidenteSegurancaDto(incidenteSeguranca.DataIncidente,
                                                                incidenteSeguranca.TipoIncidente,
                                                                incidenteSeguranca.Descricao,
                                                                incidenteSeguranca.GravidadeIncidente,
                                                                incidenteSeguranca.CausaRaiz,
                                                                incidenteSeguranca.AcaoCorretiva,
                                                                incidenteSeguranca.Responsavel,
                                                                incidenteSeguranca.DataInvestigacao,
                                                                incidenteSeguranca.EquipamentoId,
                                                                incidenteSeguranca.Id
        );

        return Ok(incidenteSegurancaDto);
    }

    // GET: api/IncidenteSeguranca
    [HttpGet("listar")]
    public async Task<IActionResult> Listar()
    {
        var incidentesSegurancaDto = incidentesSeguranca.Select(i => new IncidenteSegurancaDto(i.DataIncidente,
                                                                                                i.TipoIncidente,
                                                                                                i.Descricao,
                                                                                                i.GravidadeIncidente,
                                                                                                i.CausaRaiz,
                                                                                                i.AcaoCorretiva,
                                                                                                i.Responsavel,
                                                                                                i.DataInvestigacao,
                                                                                                i.EquipamentoId,
                                                                                                i.Id
        )).ToList();

        return Ok(incidentesSegurancaDto);
    }

    // PATCH: api/IncidenteSeguranca/atualizar/5
    [HttpPatch("atualizar/{id}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] IncidenteSegurancaDto incidenteSegurancaDto)
    {
        try
        {
            var incidenteSeguranca = await _context.IncidentesSeguranca.FindAsync(id);

            if (incidenteSeguranca == null)
                return NotFound("Incidente de segurança não encontrado");

            if (incidenteSegurancaDto.Id != id)
                return BadRequest("ID não pode ser alterado");

            IncidenteSeguranca.Atualizar(incidenteSeguranca,
                                            incidenteSegurancaDto.DataIncidente,
                                            incidenteSegurancaDto.TipoIncidente,
                                            incidenteSegurancaDto.Descricao,
                                            incidenteSegurancaDto.GravidadeIncidente,
                                            incidenteSegurancaDto.CausaRaiz,
                                            incidenteSegurancaDto.AcaoCorretiva,
                                            incidenteSegurancaDto.Responsavel,
                                            incidenteSegurancaDto.DataInvestigacao
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

    // DELETE: api/IncidenteSeguranca/remover/5
    [HttpDelete("remover/{id}")]
    public async Task<IActionResult> Remover(Guid id)
    {
        var incidenteSeguranca = await _context.IncidentesSeguranca.FindAsync(id);

        IncidenteSeguranca.Remover(incidenteSeguranca);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}