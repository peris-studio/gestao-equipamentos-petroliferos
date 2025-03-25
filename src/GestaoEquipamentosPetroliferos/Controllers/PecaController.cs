namespace GestaoEquipamentosPetroliferos.Controllers;

[Route("api/[controller]")]
[ApiController]

public class PecaController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PecaController(ApplicationDbContext context)
    {
        _context = context;
    }

    // POST: api/Peca/inserir
    [HttpPost("inserir")]
    public async Task<IActionResult> Inserir([FromBody] PecaDto pecaDto)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(pecaDto.Nome))
                return BadRequest("Nome é obrigatório");

            if (pecaDto.QuantidadeEstoque < 0)
                return BadRequest("Estoque não pode ser negativo");

            if (pecaDto.PrecoUnitario <= 0)
                return BadRequest("Preço unitário inválido");

            if (pecaDto.Id == Guid.Empty)
                pecaDto = pecaDto with { Id = Guid.NewGuid() };

            if (await _context.Pecas.AnyAsync(p => p.Id == pecaDto.Id))
                return Conflict("ID já está em uso");

            var peca = Peca.Inserir(pecaDto.Nome,
                                    pecaDto.Numeracao,
                                    pecaDto.Descricao,
                                    pecaDto.FornecedorPecas,
                                    pecaDto.QuantidadeEstoque,
                                    pecaDto.PrecoUnitario,
                                    pecaDto.EquipamentoCompativel,
                                    pecaDto.Id
            );

            _context.Pecas.Add(peca);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObterPorId), new { id = peca.Id }, new
            {
                mensagem = "Peça criada com sucesso!",
                peca = pecaDto
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

    // GET: api/Peca/obter-por-id
    [HttpGet("obter-por-id/{id}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest("ID inválido");

        var peca = await _context.Pecas.FindAsync(id);

        if (peca == null || !peca.Ativo)
            return NotFound("Peça não encontrada");

        var pecaDto = new PecaDto(peca.Nome,
                                    peca.Descricao,
                                    peca.Numeracao,
                                    peca.FornecedorPecas,
                                    peca.QuantidadeEstoque,
                                    peca.PrecoUnitario,
                                    peca.EquipamentoCompativel,
                                    peca.Id
        );

        return Ok(pecaDto);
    }

    // GET: api/Peca/listar
    [HttpGet("listar")]
    public async Task<IActionResult> Listar()
    {
        var pecasDto = pecas.Select(p => new PecaDto(p.Nome,
                                                        p.Descricao,
                                                        p.Numeracao,
                                                        p.FornecedorPecas,
                                                        p.QuantidadeEstoque,
                                                        p.PrecoUnitario,
                                                        p.EquipamentoCompativel,
                                                        p.Id
        )).ToList();

        return Ok(pecasDto);
    }

    // PATCH: api/Peca/atualizar
    [HttpPatch("atualizar/{id}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] PecaDto pecaDto)
    {
        try
        {
            var peca = await _context.Pecas.FindAsync(id);

            if (peca == null)
                return NotFound("Peça não encontrada");

            if (pecaDto.Id != id)
                return BadRequest("ID não pode ser alterado");

            Peca.Atualizar(peca,
                            pecaDto.Nome,
                            pecaDto.Descricao,
                            pecaDto.Numeracao,
                            pecaDto.FornecedorPecas,
                            pecaDto.QuantidadeEstoque,
                            pecaDto.PrecoUnitario,
                            pecaDto.EquipamentoCompativel
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

    // DELETE: api/Peca/remover
    [HttpDelete("remover/{id}")]
    public async Task<IActionResult> Remover(Guid id)
    {
        var peca = await _context.Pecas.FindAsync(id);

        if (peca == null || !peca.Ativo)
            return NotFound("Peça não encontrada");

        Peca.Remover(peca);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}