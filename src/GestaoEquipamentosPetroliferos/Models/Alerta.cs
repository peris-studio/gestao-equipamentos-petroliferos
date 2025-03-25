namespace GestaoEquipamentosPetroliferos.Models;

public class Alerta
{
    // Propriedades
    public Guid Id { get; set; }
    public TipoAlerta TipoAlerta { get; set; }
    public string Mensagem { get; set; }
    public StatusAlerta StatusAlerta { get; set; }
    public PrioridadeAlerta PrioridadeAlerta { get; set; }
    public Guid EquipamentoId { get; set; }
    public Equipamento Equipamento { get; set; }
    public Guid PecaId { get; set; }
    public Peca Peca { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataAtualizacao { get; set; }
    public DateTime? DataDelecao { get; set; }
    public bool Ativo { get; set; }

    // M É T O D O S

    public static Alerta Inserir(TipoAlerta tipoAlerta,
                                 string mensagem,
                                 StatusAlerta statusAlerta,
                                 PrioridadeAlerta prioridadeAlerta,
                                 Guid equipamentoId,
                                 Guid pecaId,
                                 Guid id = default)
    {
        return new Alerta()
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id,
            TipoAlerta = tipoAlerta,
            Mensagem = mensagem,
            StatusAlerta = statusAlerta,
            PrioridadeAlerta = prioridadeAlerta,
            EquipamentoId = equipamentoId,
            PecaId = pecaId,
            DataCriacao = DateTime.UtcNow,
            Ativo = true
        };
    }

    public static Alerta Atualizar(Alerta alerta,
                                    TipoAlerta tipoAlerta,
                                    string mensagem,
                                    PrioridadeAlerta prioridadeAlerta,
                                    StatusAlerta statusAlerta)
    {
        alerta.TipoAlerta = tipoAlerta;
        alerta.Mensagem = mensagem;
        alerta.PrioridadeAlerta = prioridadeAlerta;
        alerta.StatusAlerta = statusAlerta;
        alerta.DataAtualizacao = DateTime.UtcNow;

        return alerta;
    }

    public static Alerta Remover(Alerta alerta)
    {
        if (!alerta.Ativo)
            throw new InvalidOperationException("Alerta já está inativo");

        alerta.DataDelecao = DateTime.UtcNow;
        alerta.Ativo = false;

        return alerta;
    }
    public override string ToString()
    {
        return @$"
                    Tipo de Alerta: {TipoAlerta}
                    Mensagem: {Mensagem}
                    Status: {StatusAlerta}
                    Prioridade: {PrioridadeAlerta}
                    Equipamento: {EquipamentoId}
                    Peça: {PecaId}
                    Criado em: {DataCriacao:dd/MM/yyyy HH:mm}
                    Última atualização: {DataAtualizacao?.ToString("dd/MM/yyyy HH:mm") ?? "-"}
                    Ativo: {(Ativo ? "Sim" : "Não")}
                ";
    }
}