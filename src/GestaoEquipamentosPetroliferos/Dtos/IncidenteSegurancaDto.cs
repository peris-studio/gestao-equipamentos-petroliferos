namespace GestaoEquipamentosPetroliferos.Dtos;

public record IncidenteSegurancaDto(DateTime TipoIncidente,
                                    string Descricao,
                                    GravidadeIncidente GravidadeIncidente,
                                    string CausaRaiz,
                                    string AcaoCorretiva,
                                    string Responsavel,
                                    DateOnly DataInvestigacao,
                                    Guid EquipamentoId,
                                    Guid Id = default
                                    );