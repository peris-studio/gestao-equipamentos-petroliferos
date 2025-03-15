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
                                             Guid id = default
                                            )
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("Descrição obrigatória.", nameof(descricao));

        if (dataIncidente > DateTime.UtcNow)
            throw new ArgumentException("Data do incidente não pode ser futura.", nameof(dataIncidente));

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
            InvestigacaoConcluida = false
        };
    }

    public static IncidenteSeguranca Atualizar(IncidenteSeguranca incidenteSeguranca
                                               DateTime dataIncidente,
                                               TipoIncidente tipoIncidente,
                                               string descricao,
                                               GravidadeIncidente gravidadeIncidente,
                                               string causaRaiz,
                                               string acaoCorretiva,
                                               string responsavel,
                                               DateOnly dataInvestigacao
                                              )
    {
        if (incidenteSeguranca == null)
            throw new ArgumentNullException(nameof(incidenteSeguranca));

        if (!incidenteSeguranca.Ativo)
            throw new InvalidOperationException("Incidente inativo não pode ser atualizado.");

        incidenteSeguranca.DataIncidente = dataIncidente;
        incidenteSeguranca.TipoIncidente = tipoIncidente;
        incidenteSeguranca.Descricao = descricao;
        incidenteSeguranca.GravidadeIncidente = gravidadeIncidente;
        incidenteSeguranca.CausaRaiz = causaRaiz;
        incidenteSeguranca.AcaoCorretiva = acaoCorretiva;
        incidenteSeguranca.Responsavel = responsavel;
        incidenteSeguranca.DataInvestigacao = dataInvestigacao;
        incidenteSeguranca.DataAtualizacao = DateTime.UtcNow;
        incidenteSeguranca.InvestigacaoConcluida = true;

        incidenteSeguranca.InvestigacaoConcluida = ValidarConclusaoInvestigacao(incidenteSeguranca.CausaRaiz,
                                                                                incidenteSeguranca.AcaoCorretiva,
                                                                                incidenteSeguranca.DataInvestigacao
                                                                               );

        return incidenteSeguranca;
    }

    public static IncidenteSeguranca Remover(IncidenteSeguranca incidenteSeguranca)
    {
        incidenteSeguranca.DataDelecao = DateTime.UtcNow;
        incidenteSeguranca.Ativo = false;

        return incidenteSeguranca;
    }

    private static bool ValidarConclusaoInvestigacao(string causaRaiz, string acaoCorretiva, DateOnly dataInvestigacao)
    {
        return !string.IsNullOrWhiteSpace(causaRaiz) &&
               !string.IsNullOrWhiteSpace(acaoCorretiva) &&
               dataInvestigacao <= DateOnly.FromDateTime(DateTime.UtcNow);
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
                    Data do Incidente: {DataIncidente}
                    Tipo de Incidente: {TipoIncidente}
                    Descrição: {Descricao}
                    Gravidade: {GravidadeIncidente}
                    Causa Raiz: {CausaRaiz}
                    Ação Corretiva: {AcaoCorretiva}
                    Responsável: {Responsavel}
                    Data de Investigação: {DataInvestigacao}
                    Data de Criação: {DataCriacao}
                    Data de Atualização: {DataAtualizacao}
                    Data de Deleção: {DataDelecao}
                    Ativo: {Ativo}
                    Investigação Concluída?: {InvestigacaoConcluida}
                    ";
    }
}