namespace GestaoEquipamentosPetroliferos.Models;

public class Equipamento
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public TipoEquipamento TipoEquipamento { get; set; }
    public FabricanteEquipamento FabricanteEquipamento { get; set; }
    public string NumeroSerie { get; set; }
    public DateOnly DataInstalacao { get; set; }
    public DataOnly DataUltimaManutencao { get; set; }
    public LocalizacaoEquipamento LocalizacaoEquipamento { get; set; }
    public StatusOperacionalEquipamento StatusOperacionalEquipamento { get; set; }
    public decimal CapacidadeMaxima { get; set; }
    public string Especificacoes { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataAtualizacao { get; set; }
    public DateTime? DataDelecao { get; set; }
    public bool Ativo { get; set; }


    // M É T O D O S
    public static Equipamento Inserir(string nome,
                                      TipoEquipamento tipoEquipamento,
                                      FabricanteEquipamento fabricanteEquipamento,
                                      string numeroSerie,
                                      DateOnly dataInstalacao,
                                      DateOnly dataUltimaManutencao,
                                      LocalizacaoEquipamento localizacaoEquipamento,
                                      StatusOperacionalEquipamento statusOperacionalEquipamento,
                                      decimal capacidadeMaxima,
                                      string especificacoes,
                                      Guid id = default
                                    )
    {
        // Validações
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome não pode ser vazio.", nameof(nome));
        if (string.IsNullOrWhiteSpace(numeroSerie))
            throw new ArgumentException("Número de série obrigatório", nameof(numeroSerie));
        if (capacidadeMaxima <= 0)
            throw new ArgumentException("Capacidade deve ser positiva.", nameof(capacidadeMaxima));
        if (dataUltimaManutencao > DateOnly.FromDateTime(DateTime.UtcNow))
            throw new ArgumentException("Data de manutenção não pode ser futura.", nameof(dataUltimaManutencao));

        var novoEquipamento = new Equipamento()
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id,
            TipoEquipamento = tipoEquipamento,
            FabricanteEquipamento = fabricanteEquipamento,
            NumeroSerie = numeroSerie,
            DataInstalacao = dataInstalacao,
            DataUltimaManutencao = dataUltimaManutencao,
            LocalizacaoEquipamento = localizacaoEquipamento,
            StatusOperacionalEquipamento = statusOperacionalEquipamento,
            CapacidadeMaxima = capacidadeMaxima,
            Especificacoes = especificacoes,
            DataCriacao = DateTime.UtcNow,
            Ativo = true
        };

        return novoEquipamento;
    }

    public static Equipamento Atualizar(Equipamento equipamento,
                                        string nome,
                                        TipoEquipamento tipoEquipamento,
                                        FabricanteEquipamento fabricanteEquipamento,
                                        DateOnly dataInstalacao,
                                        DateOnly dataUltimaManutencao,
                                        LocalizacaoEquipamento localizacaoEquipamento,
                                        StatusOperacionalEquipamento statusOperacionalEquipamento,
                                        decimal capacidadeMaxima,
                                        string especificacoes
                                        )
    {
        if (equipamento == null)
            throw new ArgumentNullException(nameof(equipamento));
        if (!equipamento.Ativo)
            throw new InvalidOperationException("Equipamento inativo não pode ser atualizado.");
        if (dataUltimaManutencao > DateOnly.FromDateTime(DateTime.UtcNow))
            throw new ArgumentException("Data de manutenção inválida.", nameof(dataUltimaManutencao));
        if (dataUltimaManutencao > DateOnly.FromDateTime(DateTime.UtcNow))
            throw new ArgumentException("Data futura inválida", nameof(dataUltimaManutencao));

        equipamento.Nome = nome;
        equipamento.TipoEquipamento = tipoEquipamento;
        equipamento.FabricanteEquipamento = fabricanteEquipamento;
        equipamento.DataInstalacao = dataInstalacao;
        equipamento.DataUltimaManutencao = dataUltimaManutencao;
        equipamento.LocalizacaoEquipamento = localizacaoEquipamento;
        equipamento.StatusOperacionalEquipamento = statusOperacionalEquipamento;
        equipamento.CapacidadeMaxima = capacidadeMaxima;
        equipamento.Especificacoes = especificacoes;
        equipamento.DataAtualizacao = DateTime.UtcNow;

        return equipamento;
    }

    public static Equipamento Remover(Equipamento equipamento)
    {
        if (equipamento == null)
        {
            throw new ArgumentNullException(nameof(equipamento), "Equipamento não pode ser nulo");
        }

        equipamento.DataDelecao = DateTime.UtcNow;
        equipamento.Ativo = false;

        return equipamento;
    }

    public static bool StringParaBool(string principal)
    {
        return principal.ToLower() == "sim";
    }

    public override string ToString()
    {
        string status = Ativo ? "Ativo" : "Inativo";

        return $@"
                    Nome: {Nome}
                    Tipo de Equipamento: {TipoEquipamento}
                    Fabricante: {FabricanteEquipamento}
                    Número da Série: {NumeroSerie}
                    Data de Instalação: {DataInstalacao}
                    Data da Última Manutenção: {DataUltimaManutencao}
                    Localização: {LocalizacaoEquipamento}
                    Status Operacional do Equipamento: {StatusOperacionalEquipamento}
                    Capacidade Máxima: {CapacidadeMaxima}
                    Especificações: {Especificacoes}
                    Data de Criação: {DataCriacao}
                    Data de Atualização: {DataAtualizacao}
                    Data de Deleção: {DataDelecao}
                    Ativo: {Ativo}
                    ";
    }
}