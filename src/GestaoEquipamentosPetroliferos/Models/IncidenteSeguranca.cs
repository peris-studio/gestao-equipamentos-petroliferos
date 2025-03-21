namespace GestaoEquipamentosPetroliferos.Models;

public class IncidenteSeguranca
{
    public Guid Id { get; set; }
    public DateTime DataIncidente { get; set; }
    public TipoIncidente TipoIncidente { get; set; }
    public string Descricao { get; set; }
    public GravidadeIncidente GravidadeIncidente { get; set; }
    public string CausaRaiz { get; set; }
    public string AcaoCorretiva { get; set; }
    public string Responsavel { get; set; }
    public DateOnly DataInvestigacao { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataAtualizacao { get; set; }
    public DateTime? DataDelecao { get; set; }
    public bool Ativo { get; set; }
    public bool InvestigacaoConcluida { get; set; }
    public Guid EquipamentoId { get; set; }
    public Equipamento Equipamento { get; set; }

    // M É T O D O S
    public static IncidenteSeguranca Inserir(DateTime dataIncidente,
                                                TipoIncidente tipoIncidente,
                                                string descricao,
                                                GravidadeIncidente gravidadeIncidente,
                                                string causaRaiz,
                                                string acaoCorretiva,
                                                string responsavel,
                                                DateOnly dataInvestigacao,
                                                Guid equipamentoId,
                                                Guid id = default)
    {
        return new IncidenteSeguranca
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id,
            DataIncidente = dataIncidente,
            TipoIncidente = tipoIncidente,
            Descricao = descricao,
            GravidadeIncidente = gravidadeIncidente,
            CausaRaiz = causaRaiz,
            AcaoCorretiva = acaoCorretiva,
            Responsavel = responsavel,
            DataInvestigacao = dataInvestigacao,
            EquipamentoId = equipamentoId,
            DataCriacao = DateTime.UtcNow,
            Ativo = true,
            //InvestigacaoConcluida = ValidarConclusaoInvestigacao(causaRaiz, acaoCorretiva, dataInvestigacao)
        };
    }

    public static IncidenteSeguranca Atualizar(IncidenteSeguranca incidente,
                                                DateTime dataIncidente,
                                                TipoIncidente tipoIncidente,
                                                string descricao,
                                                GravidadeIncidente gravidadeIncidente,
                                                string causaRaiz,
                                                string acaoCorretiva,
                                                string responsavel,
                                                DateOnly dataInvestigacao)
    {
        incidente.DataIncidente = dataIncidente;
        incidente.TipoIncidente = tipoIncidente;
        incidente.Descricao = descricao;
        incidente.GravidadeIncidente = gravidadeIncidente;
        incidente.CausaRaiz = causaRaiz;
        incidente.AcaoCorretiva = acaoCorretiva;
        incidente.Responsavel = responsavel;
        incidente.DataInvestigacao = dataInvestigacao;
        incidente.DataAtualizacao = DateTime.UtcNow;
        //incidente.InvestigacaoConcluida = ValidarConclusaoInvestigacao(causaRaiz, acaoCorretiva, dataInvestigacao);

        return incidente;
    }

    public static IncidenteSeguranca Remover(IncidenteSeguranca incidente)
    {
        if (incidente == null)
            throw new ArgumentNullException(nameof(incidente), "Incidente não pode ser nulo.");

        if (!incidente.Ativo)
            throw new InvalidOperationException("Incidente já está inativo");

        incidente.DataDelecao = DateTime.UtcNow;
        incidente.Ativo = false;

        return incidente;
    }

    public override string ToString()
    {
        return @$"
                    [Incidente de Segurança]
                    Equipamento: {EquipamentoId}
                    Data: {DataIncidente:dd/MM/yyyy HH:mm}
                    Tipo: {TipoIncidente}
                    Gravidade: {GravidadeIncidente}
                    Descrição: {Descricao}
                    Causa Raiz: {CausaRaiz ?? "Não investigada"}
                    Ação Corretiva: {AcaoCorretiva ?? "Pendente"}
                    Responsável: {Responsavel}
                    Investigação: {DataInvestigacao:dd/MM/yyyy}
                    Concluído: {(InvestigacaoConcluida ? "Sim" : "Não")}
                    Status: {(Ativo ? "Ativo" : "Inativo")}
                    Registrado em: {DataCriacao:dd/MM/yyyy HH:mm}
                ";
    }
}