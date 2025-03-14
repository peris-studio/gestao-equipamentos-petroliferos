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
        var novoAlerta = new Alerta()
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

        return novoAlerta;
    }

    public static Alerta Atualizar(Alerta alerta,
                                   TipoAlerta tipoAlerta,
                                   string mensagem,
                                   PrioridadeAlerta prioridadeAlerta)
    {
        if (alerta == null)
        {
            throw new ArgumentNullException(nameof(alerta), "O alerta não pode ser nulo");
        }

        if (string.IsNullOrEmpty(mensagem))
        {
            throw new ArgumentException("A mensagem não pode ser nula ou vazia", nameof(mensagem));
        }

        if (!Enum.IsDefined(typeof(TipoAlerta), tipoAlerta))
        {
            throw new ArgumentException("Tipo de alerta inválido", nameof(tipoAlerta));
        }

        if (!Enum.IsDefined(typeof(PrioridadeAlerta), prioridadeAlerta))
        {
            throw new ArgumentException("Prioridade de alerta inválida", nameof(prioridadeAlerta));
        }

        alerta.TipoAlerta = tipoAlerta;
        alerta.Mensagem = mensagem;
        alerta.PrioridadeAlerta = prioridadeAlerta;
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