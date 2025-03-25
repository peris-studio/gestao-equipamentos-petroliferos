namespace GestaoEquipamentosPetroliferos.Controllers;

[Route("api/[controller]")]
[ApiController]

public class HistoricoFalhaController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public HistoricoFalhaController(ApplicationDbContext context)
    {
        _context = context;
    }

    // POST: api/HistoricoFalha/inserir
    [HttpPost("inserir")]
    public async Task<IActionResult> Inserir([FromBody] HistoricoFalhaDto historicoFalhaDto)
    {
        try
        {
            if (historicoFalhaDto.Id == Guid.Empty)
                return BadRequest("História inválida.");

            // Verifica se o ID já existe no banco de dados
            if (await _context.HistoricoFalhas.AnyAsync(h => h.Id == historicoFalhaDto.Id))
                return Conflict("ID indisponível.");

            if (string.IsNullOrEmpty(historicoFalhaDto.Descricao))
                return BadRequest("Descrição é obrigatória.");

            if (string.IsNullOrEmpty(historicoFalhaDto.CausaProvavel))
                return BadRequest("Causa provável é obrigatória.");

            if (string.IsNullOrEmpty(historicoFalhaDto.AcaoCorretiva))
                return BadRequest("Ação corretiva é obrigatória.");

            if (historicoFalhaDto.EquipamentoId == Guid.Empty)
                return BadRequest("Equipamento não encontrado ou inválido.");

            // Validação de datas (DataFalha não pode ser no futuro)
            if (historicoFalhaDto.DataFalha > DateTime.Now)
                return BadRequest("A data da falha não pode ser no futuro.");

            if (historicoFalhaDto.Id == Guid.Empty)
                historicoFalhaDto = historicoFalhaDto with { Id = Guid.NewGuid() };

            if (await _context.HistoricoFalhas.AnyAsync(h => h.Id == historicoFalhaDto.Id))
                return Conflict("ID indisponível");

            var historicoFalha = HistoricoFalha.Inserir(historicoFalhaDto.DataFalha,
                                                        historicoFalhaDto.Descricao,
                                                        historicoFalhaDto.CausaProvavel,
                                                        historicoFalhaDto.AcaoCorretiva,
                                                        historicoFalhaDto.TempoParado,
                                                        historicoFalhaDto.Responsavel,
                                                        historicoFalhaDto.EquipamentoId,
                                                        historicoFalhaDto.Id
            );

            _context.HistoricoFalhas.Add(historicoFalha);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObterPorId), new { id = historicoFalha.Id }, new
            {
                mensagem = "Histórico de falha criado com sucesso!",
                historicoFalha = historicoFalhaDto
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
            return StatusCode(500, new { title = "Erro interno", detail = ex.Message, exception = ex.ToString() });
        }
    }

    // GET: api/HistoricoFalha/obter-por-id/
    [HttpGet("obter-por-id/{id}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest("ID inválido");

        var historicoFalha = await _context.HistoricoFalhas
                                           .AsNoTracking()
                                           .FirstOrDefaultAsync(h => h.Id == id);

        if (historicoFalha == null)
            return NotFound("Histórico de falha não encontrado");

        var historicoFalhaDto = new HistoricoFalhaDto(historicoFalha.DataFalha,
                                                        historicoFalha.Descricao,
                                                        historicoFalha.CausaProvavel,
                                                        historicoFalha.AcaoCorretiva,
                                                        historicoFalha.TempoParado,
                                                        historicoFalha.Responsavel,
                                                        historicoFalha.EquipamentoId,
                                                        historicoFalha.Id
        );

        return Ok(historicoFalhaDto);
    }

    // GET: api/HistoricoFalha/listar
    [HttpGet("listar")]
    public async Task<IActionResult> Listar()
    {

        var historicoFalhas = await _context.HistoricoFalhas.ToListAsync();

        var historicoFalhasDto = historicoFalhas.Select(h => new HistoricoFalhaDto(h.DataFalha,
                                                                                    h.Descricao,
                                                                                    h.CausaProvavel,
                                                                                    h.AcaoCorretiva,
                                                                                    h.TempoParado,
                                                                                    h.Responsavel,
                                                                                    h.EquipamentoId,
                                                                                    h.Id
        )).ToList();

        return Ok(historicoFalhasDto);
    }

    // PATCH: api/HistoricoFalha/atualizar/
    [HttpPatch("atualizar/{id}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] HistoricoFalhaDto historicoFalhaDto)
    {
        try
        {
            var historicoFalha = await _context.HistoricoFalhas.FindAsync(id);
            if (historicoFalha == null)
                return NotFound("Histórico de falha não encontrado");

            HistoricoFalha.Atualizar(historicoFalha,
                                        historicoFalhaDto.DataFalha,
                                        historicoFalhaDto.Descricao,
                                        historicoFalhaDto.CausaProvavel,
                                        historicoFalhaDto.AcaoCorretiva,
                                        historicoFalhaDto.TempoParado,
                                        historicoFalhaDto.Responsavel
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

    // DELETE: api/HistoricoFalha/remover/
    [HttpDelete("remover/{id}")]
    public async Task<IActionResult> Remover(Guid id)
    {
        var historicoFalha = await _context.HistoricoFalhas.FindAsync(id);

        if (historicoFalha == null)
            return NotFound("Histórico de falha não encontrado");

        HistoricoFalha.Remover(historicoFalha);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}