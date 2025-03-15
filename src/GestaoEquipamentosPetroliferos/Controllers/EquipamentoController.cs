using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestaoEquipamentosPetroliferos.Models;

namespace GestaoEquipamentosPetroliferos.Controllers
{
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
                // Validações básicas
                if (string.IsNullOrWhiteSpace(equipamentoDto.Nome))
                    return BadRequest("Nome é obrigatório");

                if (string.IsNullOrWhiteSpace(equipamentoDto.NumeroSerie))
                    return BadRequest("Número de série é obrigatório");

                if (equipamentoDto.CapacidadeMaxima <= 0)
                    return BadRequest("Capacidade máxima deve ser maior que zero");

                // Gera novo ID se necessário
                if (equipamentoDto.Id == Guid.Empty)
                    equipamentoDto = equipamentoDto with { Id = Guid.NewGuid() };

                // Verifica ID duplicado
                if (await _context.Equipamentos.AnyAsync(e => e.Id == equipamentoDto.Id))
                    return Conflict("ID já está em uso");

                var equipamento = Equipamento.Inserir(  equipamentoDto.Nome,
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

        // GET: api/Equipamento/obter-por-id/5
        [HttpGet("obter-por-id/{id}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("ID inválido");

            var equipamento = await _context.Equipamentos.FindAsync(id);

            if (equipamento == null || !equipamento.Ativo)
                return NotFound("Equipamento não encontrado");

            var equipamentoDto = new EquipamentoDto(equipamento.Id,
                                                    equipamento.Nome,
                                                    equipamento.TipoEquipamento,
                                                    equipamento.FabricanteEquipamento,
                                                    equipamento.NumeroSerie,
                                                    equipamento.DataInstalacao,
                                                    equipamento.DataUltimaManutencao,
                                                    equipamento.LocalizacaoEquipamento,
                                                    equipamento.StatusOperacionalEquipamento,
                                                    equipamento.CapacidadeMaxima,
                                                    equipamento.Especificacoes,
                                                    equipamento.DataCriacao,
                                                    equipamento.DataAtualizacao,
                                                    equipamento.DataDelecao,
                                                    equipamento.Ativo
            );

            return Ok(equipamentoDto);
        }

        // GET: api/Equipamento/listar
        [HttpGet("listar")]
        public async Task<IActionResult> Listar()
        {
            var equipamentos = await _context.Equipamentos.Where(e => e.Ativo).ToListAsync();

            var equipamentosDto = equipamentos.Select(e => new EquipamentoDto(  e.Id,
                                                                                e.Nome,
                                                                                e.TipoEquipamento,
                                                                                e.FabricanteEquipamento,
                                                                                e.NumeroSerie,
                                                                                e.DataInstalacao,
                                                                                e.DataUltimaManutencao,
                                                                                e.LocalizacaoEquipamento,
                                                                                e.StatusOperacionalEquipamento,
                                                                                e.CapacidadeMaxima,
                                                                                e.Especificacoes,
                                                                                e.DataCriacao,
                                                                                e.DataAtualizacao,
                                                                                e.DataDelecao,
                                                                                e.Ativo
            )).ToList();

            return Ok(equipamentosDto);
        }

        // PATCH: api/Equipamento/atualizar/5
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

                Equipamento.Atualizar(  equipamento,
                                        equipamentoDto.Nome,
                                        equipamentoDto.TipoEquipamento,
                                        equipamentoDto.FabricanteEquipamento,
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

        // DELETE: api/Equipamento/remover/5
        [HttpDelete("remover/{id}")]
        public async Task<IActionResult> Remover(Guid id)
        {
            var equipamento = await _context.Equipamentos.FindAsync(id);

            if (equipamento == null || !equipamento.Ativo)
                return NotFound("Equipamento não encontrado");

            Equipamento.Remover(equipamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}