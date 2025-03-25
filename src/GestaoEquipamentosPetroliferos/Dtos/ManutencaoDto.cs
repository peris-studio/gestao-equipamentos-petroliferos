namespace GestaoEquipamentosPetroliferos.Dtos;

public record ManutencaoDto(TipoManutencao TipoManutencao,
                            string Descricao,
                            DateOnly DataAgendada,
                            DateOnly DataExecucao,
                            decimal CustoManutencao,
                            StatusManutencao StatusManutencao,
                            DateOnly ProximaManutencao,
                            Guid EquipamentoId,
                            Guid Id = default);