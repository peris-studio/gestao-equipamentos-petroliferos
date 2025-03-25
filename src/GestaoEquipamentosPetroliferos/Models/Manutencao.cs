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
    public DateOnly ProximaManutencao { get; set; }
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
                                        DateOnly proximaManutencao,
                                        Guid equipamentoId,
                                        Guid id = default)
    {
        ValidarParametrosInsercao(descricao,
                                    dataAgendada,
                                    custoManutencao,
                                    equipamentoId,
                                    tipoManutencao,
                                    statusManutencao,
                                    proximaManutencao);

        return new Manutencao()
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id,
            TipoManutencao = tipoManutencao,
            Descricao = descricao,
            DataAgendada = dataAgendada,
            DataExecucao = dataExecucao,
            CustoManutencao = custoManutencao,
            StatusManutencao = statusManutencao,
            ProximaManutencao = proximaManutencao,
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
                                        DateOnly proximaManutencao)
    {
        ValidarEstadoParaAtualizacao(manutencao);
        ValidarParametrosAtualizacao(descricao,
                                        dataAgendada,
                                        dataExecucao,
                                        custoManutencao,
                                        statusManutencao,
                                        tipoManutencao,
                                        proximaManutencao);

        manutencao.TipoManutencao = tipoManutencao;
        manutencao.Descricao = descricao;
        manutencao.DataAgendada = dataAgendada;
        manutencao.DataExecucao = dataExecucao;
        manutencao.CustoManutencao = custoManutencao;
        manutencao.StatusManutencao = statusManutencao;
        manutencao.ProximaManutencao = proximaManutencao;
        manutencao.DataAtualizacao = DateTime.UtcNow;

        ValidarRegraProximaManutencao(manutencao);

        return manutencao;
    }

    public static Manutencao Remover(Manutencao manutencao)
    {
        if (manutencao == null)
            throw new ArgumentNullException(nameof(manutencao), "Manutenção não pode ser nula");

        if (!manutencao.Ativo)
            throw new InvalidOperationException("Manutenção já está inativa");

        manutencao.DataDelecao = DateTime.UtcNow;
        manutencao.Ativo = false;

        return manutencao;
    }

    // V A L I D A Ç Ã O
    private static void ValidarParametrosInsercao(string descricao,
                                                    DateOnly dataAgendada,
                                                    decimal custoManutencao,
                                                    Guid equipamentoId,
                                                    TipoManutencao tipoManutencao,
                                                    StatusManutencao statusManutencao,
                                                    DateOnly proximaManutencao)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("Descrição obrigatória", nameof(descricao));

        if (dataAgendada < DateOnly.FromDateTime(DateTime.UtcNow))
            throw new ArgumentException("Data agendada não pode ser retroativa", nameof(dataAgendada));

        if (custoManutencao < 0)
            throw new ArgumentException("Custo não pode ser negativo", nameof(custoManutencao));

        if (equipamentoId == Guid.Empty)
            throw new ArgumentException("Equipamento inválido", nameof(equipamentoId));

        if (tipoManutencao == TipoManutencao.Preventiva && statusManutencao == StatusManutencao.Concluida)
        {
            if (proximaManutencao == DateOnly.MinValue)
                throw new ArgumentException("Próxima manutenção obrigatória para preventivas concluídas", nameof(proximaManutencao));
        }
    }

    private static void ValidarParametrosAtualizacao(string descricao,
                                                        DateOnly dataAgendada,
                                                        DateOnly dataExecucao,
                                                        decimal custoManutencao,
                                                        StatusManutencao statusManutencao,
                                                        TipoManutencao tipoManutencao,
                                                        DateOnly proximaManutencao)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("Descrição obrigatória", nameof(descricao));

        if (dataAgendada < DateOnly.FromDateTime(DateTime.UtcNow))
            throw new ArgumentException("Data agendada não pode ser retroativa", nameof(dataAgendada));

        if (dataExecucao > DateOnly.FromDateTime(DateTime.UtcNow))
            throw new ArgumentException("Data de execução não pode ser futura", nameof(dataExecucao));

        if (custoManutencao < 0)
            throw new ArgumentException("Custo não pode ser negativo", nameof(custoManutencao));

        if (!Enum.IsDefined(typeof(TipoManutencao), tipoManutencao))
            throw new ArgumentException("Tipo de manutenção inválido");

        if (!Enum.IsDefined(typeof(StatusManutencao), statusManutencao))
            throw new ArgumentException("Status de manutenção inválido");
    }

    private static void ValidarEstadoParaAtualizacao(Manutencao manutencao)
    {
        if (manutencao == null)
            throw new ArgumentNullException(nameof(manutencao));

        if (!manutencao.Ativo)
            throw new InvalidOperationException("Manutenção inativa não pode ser atualizada");
    }

    private static void ValidarRegraProximaManutencao(Manutencao manutencao)
    {
        if (manutencao.TipoManutencao == TipoManutencao.Preventiva &&
            manutencao.StatusManutencao == StatusManutencao.Concluida &&
            manutencao.ProximaManutencao == DateOnly.MinValue)
        {
            throw new InvalidOperationException("Próxima manutenção é obrigatória para preventivas concluídas");
        }
    }

    public override string ToString()
    {
        return @$"
                    Tipo: {TipoManutencao}
                    Descrição: {Descricao}
                    Agendada: {DataAgendada:dd/MM/yyyy}
                    Execução: {DataExecucao:dd/MM/yyyy}
                    Custo: {CustoManutencao:C}
                    Status: {StatusManutencao}
                    Próxima: {ProximaManutencao:dd/MM/yyyy}
                    Equipamento: {EquipamentoId}
                    Criada em: {DataCriacao:dd/MM/yyyy HH:mm}
                    Ativa: {(Ativo ? "Sim" : "Não")}
            ";
    }
}