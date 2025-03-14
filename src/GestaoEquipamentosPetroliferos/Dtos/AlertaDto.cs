namespace GestaoEquipamentosPetroliferos.Dtos;

public record AlertaDto(TipoAlerta TipoAlerta,
                        string Mensagem,
                        StatusAlerta StatusAlerta,
                        PrioridadeAlerta PrioridadeAlerta,
                        Guid EquipamentoId,
                        Guid PecaId,
                        Guid id = default);