namespace GestaoEquipamentosPetroliferos.Dtos;

public record HistoricoFalhaDto(string Descricao,
                                string CausaProvavel,
                                string AcaoCorretiva,
                                Timespan TempoParado,
                                string Responsavel,
                                Guid EquipamentoId,
                                Guid Id = default
                                );