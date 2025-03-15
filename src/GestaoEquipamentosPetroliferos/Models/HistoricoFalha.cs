namespace GestaoEquipamentosPetroliferos.Models;

public class HistoricoFalha
{
    public Guid Id { get; set; }
    public DateTime DataFalha { get; set; }
    public string Descricao { get; set; }
    public string CausaProvavel { get; set; }
    public string AcaoCorretiva { get; set; }
    public TimeSpan TempoParado { get; set; }
    public string Responsavel { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataAtualizacao { get; set; }
    public DateTime? DataDelecao { get; set; }
    public bool Ativo { get; set; }
    public Guid EquipamentoId { get; set; }
    public Equipamento Equipamento { get; set; }

    // M É T O D O S
    public static HistoricoFalha Inserir(DateTime dataFalha,
                                         string descricao,
                                         string causaProvavel,
                                         string acaoCorretiva,
                                         TimeSpan tempoParado,
                                         string responsavel,
                                         Guid equipamentoId,
                                         Guid id = default
                                         )
    {
        // Validações
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("Descrição obrigatória.", nameof(descricao));

        if (dataFalha > DateTime.UtcNow)
            throw new ArgumentException("Data da falha não pode ser futura.", nameof(dataFalha));

        if (tempoParado <= TimeSpan.Zero)
            throw new ArgumentException("Tempo parado inválido.", nameof(tempoParado));

        return new HistoricoFalha
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id,
            DataFalha = dataFalha,
            Descricao = descricao,
            CausaProvavel = causaProvavel,
            AcaoCorretiva = acaoCorretiva,
            TempoParado = tempoParado,
            Responsavel = responsavel,
            EquipamentoId = equipamentoId,
            DataCriacao = DateTime.UtcNow,
            Ativo = true
        };
    }

    public static HistoricoFalha Atualizar(HistoricoFalha historico,
                                           DateTime dataFalha,
                                           string descricao,
                                           string causaProvavel,
                                           string acaoCorretiva,
                                           TimeSpan tempoParado,
                                           string responsavel
                                           )
    {
        if (historico == null)
            throw new ArgumentNullException(nameof(historico), "Histórico não encontrado.");

        if (!historico.Ativo)
            throw new InvalidOperationException("Histórico inativo não pode ser atualizado.");

        historico.DataFalha = dataFalha;
        historico.Descricao = descricao;
        historico.CausaProvavel = causaProvavel;
        historico.AcaoCorretiva = acaoCorretiva;
        historico.TempoParado = tempoParado;
        historico.Responsavel = responsavel;
        historico.DataAtualizacao = DateTime.UtcNow;

        return historico;
    }

    public static HistoricoFalha Remover(HistoricoFalha historico)
    {
        if (historico == null)
            throw new ArgumentNullException(nameof(historico), "Histórico não pode ser nulo.");

        historico.DataDelecao = DateTime.UtcNow;
        historico.Ativo = false;

        return historico;
    }

    // Método auxiliar para histórico
    public static bool FalhaCritica(TimeSpan tempoParado)
    {
        return tempoParado >= TimeSpan.FromHours(24);
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
                    Equipamento: {EquipamentoId}
                    Data da Falha: {DataFalha}
                    Descrição: {Descricao}
                    Causa Provável: {CausaProvavel}
                    Ação Corretiva: {AcaoCorretiva}
                    Tempo Parado: {TempoParado}
                    Responsável: {Responsavel}
                    Data de Criação: {DataCriacao}
                    Data de Atualização: {DataAtualizacao}
                    Data de Deleção: {DataDelecao}
                    Ativo: {Ativo}
                    ";
    }
}