namespace GestaoEquipamentosPetroliferos.Dtos;

public record HistoricoFalhaDto(DateTime DataFalha,
                                string Descricao,
                                string CausaProvavel,
                                string AcaoCorretiva,
                                DateTime TempoParado,
                                string Responsavel,
                                Guid EquipamentoId,
                                Guid Id = default
                                );