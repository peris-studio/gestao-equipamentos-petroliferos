namespace GestaoEquipamentosPetroliferos.Controllers;

[Route("api/[controller]")]
[ApiController]

public class AlertaController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AlertaController(ApplicationDbContext context)
    {
        _context = context;
    }

    // POST: api/Alerta/inserir
    [HttpPost("inserir")]
    public async Task<IActionResult> Inserir([FromBody] AlertaDto alertaDto)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(alertaDto.Mensagem))
                return BadRequest("Mensagem obrigatória");

            if (alertaDto.EquipamentoId == Guid.Empty)
                return BadRequest("Equipamento inválido");

            if (alertaDto.PecaId == Guid.Empty)
                return BadRequest("Peça inválida");

            if (alertaDto.Id == Guid.Empty)
                alertaDto = alertaDto with { Id = Guid.NewGuid() };

            if (await _context.Alertas.AnyAsync(a => a.Id == alertaDto.Id))
                return Conflict("ID indisponível");

            var alerta = Alerta.Inserir(alertaDto.TipoAlerta,
                                        alertaDto.Mensagem,
                                        alertaDto.StatusAlerta,
                                        alertaDto.PrioridadeAlerta,
                                        alertaDto.EquipamentoId,
                                        alertaDto.PecaId,
                                        alertaDto.Id
            );

            _context.Alertas.Add(alerta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObterPorId), new { id = alerta.Id }, new
            {
                mensagem = "Alerta criado com sucesso!",
                alerta = alertaDto
            });
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

    // GET: api/Alerta/obter-por-id
    [HttpGet("obter-por-id/{id}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest("ID inválido");

        var alerta = await _context.Alertas
                                   .AsNoTracking()
                                   .FirstOrDefaultAsync(a => a.Id == id);

        if (alerta == null || !alerta.Ativo)
            return NotFound("Alerta não encontrado");

        var alertaDto = new AlertaDto(alerta.TipoAlerta,
                                        alerta.Mensagem,
                                        alerta.StatusAlerta,
                                        alerta.PrioridadeAlerta,
                                        alerta.EquipamentoId,
                                        alerta.PecaId,
                                        alerta.Id
        );

        return Ok(alertaDto);
    }

    // GET: api/Alerta/listar
    [HttpGet("listar")]
    public async Task<IActionResult> Listar()
    {
        var alertasDto = alertas.Select(a => new AlertaDto(a.TipoAlerta,
                                                            a.Mensagem,
                                                            a.StatusAlerta,
                                                            a.PrioridadeAlerta,
                                                            a.EquipamentoId,
                                                            a.PecaId,
                                                            a.Id
        )).ToList();

        return Ok(alertasDto);
    }

    // PATCH: api/Alerta/atualizar
    [HttpPatch("atualizar/{id}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] AlertaDto alertaDto)
    {
        try
        {
            var alerta = await _context.Alertas.FindAsync(id);

            if (alerta == null)
                return NotFound("Alerta não encontrado");

            if (alertaDto.Id != id)
                return BadRequest("ID não pode ser alterado");

            Alerta.Atualizar(alerta,
                                alertaDto.TipoAlerta,
                                alertaDto.Mensagem,
                                alertaDto.PrioridadeAlerta,
                                alertaDto.StatusAlerta
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

    // DELETE: api/Alerta/remover
    [HttpDelete("remover/{id}")]
    public async Task<IActionResult> Remover(Guid id)
    {
        var alerta = await _context.Alertas.FindAsync(id);

        if (alerta == null || !alerta.Ativo)
            return NotFound("Alerta não encontrado");

        Alerta.Remover(alerta);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}