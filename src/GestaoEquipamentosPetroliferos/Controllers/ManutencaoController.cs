namespace GestaoEquipamentosPetroliferos.Controllers;

[Route("api/[controller]")]
[ApiController]

public class ManutencaoController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ManutencaoController(ApplicationDbContext context)
    {
        _context = context;
    }

    // POST: api/Manutencao/inserir
    [HttpPost("inserir")]
    public async Task<IActionResult> Inserir([FromBody] ManutencaoDto manutencaoDto)
    {
        try
        {
            // Validações básicas
            if (string.IsNullOrWhiteSpace(manutencaoDto.Descricao))
                return BadRequest("Descrição é obrigatória");

            if (manutencaoDto.DataAgendada < DateOnly.FromDateTime(DateTime.UtcNow))
                return BadRequest("Data agendada não pode ser retroativa");

            if (manutencaoDto.CustoManutencao < 0)
                return BadRequest("Custo não pode ser negativo");

            if (manutencaoDto.Id == Guid.Empty)
                manutencaoDto = manutencaoDto with { Id = Guid.NewGuid() };

            if (await _context.Manutencoes.AnyAsync(m => m.Id == manutencaoDto.Id))
                return Conflict("ID já está em uso");

            var manutencao = Manutencao.Inserir(manutencaoDto.TipoManutencao,
                                                manutencaoDto.Descricao,
                                                manutencaoDto.DataAgendada,
                                                manutencaoDto.DataExecucao,
                                                manutencaoDto.CustoManutencao,
                                                manutencaoDto.StatusManutencao,
                                                manutencaoDto.ProximaManutencao,
                                                manutencaoDto.EquipamentoId,
                                                manutencaoDto.Id
            );

            _context.Manutencoes.Add(manutencao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObterPorId), new { id = manutencao.Id }, new
            {
                mensagem = "Manutenção criada com sucesso!",
                manutencao = manutencaoDto
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

    // GET: api/Manutencao/obter-por-id
    [HttpGet("obter-por-id/{id}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest("ID inválido");

        var manutencao = await _context.Manutencoes.FindAsync(id);

        if (manutencao == null || !manutencao.Ativo)
            return NotFound("Manutenção não encontrada");

        var manutencaoDto = new ManutencaoDto(manutencao.TipoManutencao,
                                                manutencao.Descricao,
                                                manutencao.DataAgendada,
                                                manutencao.DataExecucao,
                                                manutencao.CustoManutencao,
                                                manutencao.StatusManutencao,
                                                manutencao.ProximaManutencao,
                                                manutencao.EquipamentoId,
                                                manutencao.Id
        );

        return Ok(manutencaoDto);
    }

    // GET: api/Manutencao/listar
    [HttpGet("listar")]
    public async Task<IActionResult> Listar()
    {
        var manutencoes = await _context.Manutencoes.Where(m => m.Ativo).ToListAsync();

        var manutencoesDto = manutencoes.Select(m => new ManutencaoDto(m.TipoManutencao,
                                                                        m.Descricao,
                                                                        m.DataAgendada,
                                                                        m.DataExecucao,
                                                                        m.CustoManutencao,
                                                                        m.StatusManutencao,
                                                                        m.ProximaManutencao,
                                                                        m.EquipamentoId,
                                                                        m.Id
        )).ToList();

        return Ok(manutencoesDto);
    }

    // PATCH: api/Manutencao/atualizar
    [HttpPatch("atualizar/{id}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] ManutencaoDto manutencaoDto)
    {
        try
        {
            var manutencao = await _context.Manutencoes.FindAsync(id);

            if (manutencao == null)
                return NotFound("Manutenção não encontrada");

            if (manutencaoDto.Id != id)
                return BadRequest("ID não pode ser alterado");

            Manutencao.Atualizar(manutencao,
                                    manutencaoDto.TipoManutencao,
                                    manutencaoDto.Descricao,
                                    manutencaoDto.DataAgendada,
                                    manutencaoDto.DataExecucao,
                                    manutencaoDto.CustoManutencao,
                                    manutencaoDto.StatusManutencao,
                                    manutencaoDto.ProximaManutencao
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

    // DELETE: api/Manutencao/remover
    [HttpDelete("remover/{id}")]
    public async Task<IActionResult> Remover(Guid id)
    {
        var manutencao = await _context.Manutencoes.FindAsync(id);

        if (manutencao == null || !manutencao.Ativo)
            return NotFound("Manutenção não encontrada");

        Manutencao.Remover(manutencao);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}