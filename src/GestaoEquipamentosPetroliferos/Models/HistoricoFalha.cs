namespace GestaoEquipamentosPetroliferos.Models;

public class HistoricoFalha
{
    public Guid Id { get; set; }
    public DateTime DataFalha { get; set; }
    public string Descricao { get; set; }
    public string CausaProvavel { get; set; }
    public string AcaoCorretiva { get; set; }
    public DateTime TempoParado { get; set; }
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
                                            DateTime tempoParado,
                                            string responsavel,
                                            Guid equipamentoId,
                                            Guid id = default)
    {
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
                                            DateTime tempoParado,
                                            string responsavel)
    {
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

        if (!historico.Ativo)
            throw new InvalidOperationException("Histórico já está inativo");

        historico.DataDelecao = DateTime.UtcNow;
        historico.Ativo = false;

        return historico;
    }
    public override string ToString()
    {
        return @$"
                    Equipamento: {EquipamentoId}
                    Data da Falha: {DataFalha:dd/MM/yyyy HH:mm}
                    Descrição: {Descricao}
                    Causa Provável: {CausaProvavel}
                    Ação Corretiva: {AcaoCorretiva}
                    Tempo Parado: {TempoParado:dd/MM/yyyy HH:mm}
                    Responsável: {Responsavel}
                    Status: {(Ativo ? "Ativo" : "Inativo")}
                    Registrado em: {DataCriacao:dd/MM/yyyy HH:mm}
                    Última atualização: {DataAtualizacao?.ToString("dd/MM/yyyy HH:mm") ?? "-"}
                ";
    }
}