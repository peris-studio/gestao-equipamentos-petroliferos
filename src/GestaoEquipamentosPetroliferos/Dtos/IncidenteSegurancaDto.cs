namespace GestaoEquipamentosPetroliferos.Dtos;

public record IncidenteSegurancaDto(DateTime DataIncidente,
                                    TipoIncidente TipoIncidente,
                                    string Descricao,
                                    GravidadeIncidente GravidadeIncidente,
                                    string CausaRaiz,
                                    string AcaoCorretiva,
                                    string Responsavel,
                                    DateOnly DataInvestigacao,
                                    Guid EquipamentoId,
                                    Guid Id = default
                                    );