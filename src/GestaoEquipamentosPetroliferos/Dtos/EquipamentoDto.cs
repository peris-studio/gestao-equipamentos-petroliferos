namespace GestaoEquipamentosPetroliferos.Dtos;

public record AlertaDto(string nome,
                        TipoEquipamento TipoEquipamento,
                        FabricanteEquipamento FabricanteEquipamento,
                        string NumeroSerie,
                        DateOnly DataInstalacao,
                        DateOnly DataUltimaManutencao,
                        LocalizacaoEquipamento LocalizacaoEquipamento,
                        StatusOperacionalEquipamento StatusOperacionalEquipamento,
                        decimal CapacidadeMaxima,
                        string Especificacoes,
                        Guid Id = default);