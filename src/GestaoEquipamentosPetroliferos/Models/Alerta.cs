namespace GestaoEquipamentosPetroliferos.Models;

public class Alerta
{
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
                                 Guid id = default
                                 )
    {
        if (string.IsNullOrWhiteSpace(mensagem))
            throw new ArgumentException("Mensagem obrigatória", nameof(mensagem));

        if (equipamentoId == Guid.Empty)
            throw new ArgumentException("Equipamento inválido", nameof(equipamentoId));

        if (pecaId == Guid.Empty)
            throw new ArgumentException("Peça inválida", nameof(pecaId));

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
                                   PrioridadeAlerta prioridadeAlerta
                                   StatusAlerta statusAlerta)
    {
        if (alerta == null)
            throw new ArgumentNullException(nameof(alerta));

        if (!alerta.Ativo)
            throw new InvalidOperationException("Alerta inativo não pode ser atualizado");

        if (string.IsNullOrWhiteSpace(mensagem))
            throw new ArgumentException("Mensagem obrigatória", nameof(mensagem));

        if (!Enum.IsDefined(typeof(TipoAlerta), tipoAlerta))
            throw new ArgumentException("Tipo inválido", nameof(tipoAlerta));

        if (!Enum.IsDefined(typeof(PrioridadeAlerta), prioridadeAlerta))
            throw new ArgumentException("Prioridade inválida", nameof(prioridadeAlerta));

        if (!Enum.IsDefined(typeof(StatusAlerta), statusAlerta))
            throw new ArgumentException("Status inválido", nameof(statusAlerta));

        alerta.TipoAlerta = tipoAlerta;
        alerta.Mensagem = mensagem;
        alerta.PrioridadeAlerta = prioridadeAlerta;
        alerta.StatusAlerta = statusAlerta;
        alerta.DataAtualizacao = DateTime.UtcNow;

        return alerta;
    }

    public static Alerta Remover(Alerta alerta)
    {
        if (alerta == null)
        {
            throw new ArgumentNullException(nameof(alerta), "O alerta não pode ser nulo");
        }

        alerta.DataDelecao = DateTime.UtcNow;
        alerta.Ativo = false;

        return alerta;
    }

    public static bool StringParaBool(string principal)
    {
        return principal.ToLower() == "sim";
    }

    public override string ToString()
    {
        string principal = Principal ? "Sim" : "Não";
        string status = Ativo ? "Ativo" : "Inativo";

        return $@"
                    Tipo de Alerta: {TipoAlerta}
                    Mensagem: {Mensagem}
                    Status de Alerta: {StatusAlerta}
                    Prioridade de Alerta: {PrioridadeAlerta}
                    Equipamentos: {EquipamentoId}
                    Peças: {PecaId}
                    Data de Criação: {DataCriacao}
                    Data de Atualização: {DataAtualizacao}
                    Data de Deleção: {DataDelecao}
                    Ativo: {Ativo}
                    ";
    }
}