namespace GestaoEquipamentosPetroliferos.Models;

public class Manutencao
{
    public Guid Id { get; set; }
    public TipoManutencao TipoManutencao { get; set; }
    public string Descricao { get; set; }
    public DateOnly DataAgendada { get; set; }
    public DateOnly DataExecucao { get; set; }
    public decimal CustoManutencao { get; set; }
    public StatusManutencao StatusManutencao { get; set; }
    public DateOnly? ProximaManutencao { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataAtualizacao { get; set; }
    public DateTime? DataDelecao { get; set; }
    public bool Ativo { get; set; }
    public Guid EquipamentoId { get; set; }
    public Equipamento Equipamento { get; set; }


    // M É T O D O S
    public static Manutencao Inserir(TipoManutencao tipoManutencao,
                                     string descricao,
                                     DateOnly dataAgendada,
                                     DateOnly dataExecucao,
                                     decimal custoManutencao,
                                     StatusManutencao statusManutencao,
                                     DateOnly proximaManutencao?,
                                     Guid equipamentoId,
                                     Guid id = default
                                    )
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("Descrição obrigatória", nameof(descricao));

        if (dataAgendada < DateOnly.FromDateTime(DateTime.UtcNow))
            throw new ArgumentException("Data agendada não pode ser retroativa", nameof(dataAgendada));

        if (custoManutencao < 0)
            throw new ArgumentException("Custo não pode ser negativo", nameof(custoManutencao));

        return new Manutencao()
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id,
            TipoManutencao = tipoManutencao,
            DataAgendada = dataAgendada,
            DataExecucao = dataExecucao,
            CustoManutencao = custoManutencao,
            StatusManutencao = statusManutencao,
            ProximaManutencao = proximaManutencao ??,
            EquipamentoId = equipamentoId,
            DataCriacao = DateTime.UtcNow,
            Ativo = true
        };
    }

    public static Manutencao Atualizar(Manutencao manutencao,
                                       TipoManutencao tipoManutencao,
                                       string descricao,
                                       DateOnly dataAgendada,
                                       DateOnly dataExecucao,
                                       decimal custoManutencao,
                                       StatusManutencao statusManutencao,
                                       DateOnly proximaManutencao?
                                      )
    {
        if (manutencao == null)
            throw new ArgumentNullException(nameof(manutencao));

        if (!manutencao.Ativo)
            throw new InvalidOperationException("Manutenção inativa não pode ser atualizada");

        // Validação de consistência
        if (statusManutencao == StatusManutencao.Concluida && dataExecucao > DateOnly.FromDateTime(DateTime.UtcNow))
            throw new InvalidOperationException("Data de execução não pode ser futura para manutenções concluídas");

        manutencao.TipoManutencao = tipoManutencao;
        manutencao.Descricao = descricao;
        manutencao.DataAgendada = dataAgendada;
        manutencao.DataExecucao = dataExecucao;
        manutencao.CustoManutencao = custoManutencao;
        manutencao.StatusManutencao = statusManutencao;
        manutencao.ProximaManutencao = proximaManutencao;
        manutencao.DataAtualizacao = DateTime.UtcNow;

        // Regra para próxima manutenção obrigatória
        if (manutencao.TipoManutencao == TipoManutencao.Preventiva &&
            manutencao.StatusManutencao == StatusManutencao.Concluida &&
            !manutencao.ProximaManutencao.HasValue)
        {
            throw new InvalidOperationException("Próxima manutenção é obrigatória para preventivas concluídas");
        }

        return manutencao;
    }

    public static Manutencao Remover(Manutencao manutencao)
    {
        if (manutencao == null)
            throw new ArgumentNullException(nameof(manutencao), "A manutenção não pode ser nula");

        if (!manutencao.Ativo)
            throw new InvalidOperationException("Manutenção já está inativa");

        manutencao.DataDelecao = DateTime.UtcNow;
        manutencao.Ativo = false;

        return manutencao;
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
                    Tipo de Manutencao: {TipoManutencao}
                    Descricao: {Descricao}
                    Data Agendada: {DataAgendada}
                    Data de Execução: {DataExecucao}
                    Responsável {Responsavel}
                    Custo da Manutenção: {CustoManutencao}
                    Status da Manutenção: {StatusManutencao}
                    Próxima Manutenção: {ProximaManutencao}
                    Equipamentos: {EquipamentoId}
                    Data de Criação: {DataCriacao}
                    Data de Atualização: {DataAtualizacao}
                    Data de Deleção: {DataDelecao}
                    Ativo: {Ativo}
                    ";
    }
}