namespace GestaoEquipamentosPetroliferos.Dtos;

public record AlertaDto(TipoManutencao TipoManutencao,
                        string Descricao,
                        DateOnly DataAgendada,
                        DateOnly DataExecucao,
                        decimal CustoManutencao,
                        StatusManutencao StatusManutencao,
                        DateOnly? ProximaManutencao,
                        Guid EquipamentoId,
                        Guid Id = default);