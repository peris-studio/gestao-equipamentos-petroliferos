namespace GestaoEquipamentosPetroliferos.Models;

public class Equipamento
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public TipoEquipamento TipoEquipamento { get; set; }
    public FabricanteEquipamento FabricanteEquipamento { get; set; }
    public string NumeroSerie { get; set; }
    public DateOnly DataInstalacao { get; set; }
    public DateOnly DataUltimaManutencao { get; set; }
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
                                        Guid id = default)
    {
        return new Equipamento
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id,
            Nome = nome,
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
    }

    public static Equipamento Atualizar(Equipamento equipamento,
                                        string nome,
                                        TipoEquipamento tipoEquipamento,
                                        FabricanteEquipamento fabricanteEquipamento,
                                        string numeroSerie,
                                        DateOnly dataInstalacao,
                                        DateOnly dataUltimaManutencao,
                                        LocalizacaoEquipamento localizacaoEquipamento,
                                        StatusOperacionalEquipamento statusOperacionalEquipamento,
                                        decimal capacidadeMaxima,
                                        string especificacoes)
    {
        equipamento.Nome = nome;
        equipamento.TipoEquipamento = tipoEquipamento;
        equipamento.FabricanteEquipamento = fabricanteEquipamento;
        equipamento.NumeroSerie = numeroSerie;
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
        if (!equipamento.Ativo)
            throw new InvalidOperationException("Equipamento já está inativo");

        equipamento.DataDelecao = DateTime.UtcNow;
        equipamento.Ativo = false;

        return equipamento;
    }

    public override string ToString()
    {
        return @$"
                    Nome: {Nome}
                    Tipo: {TipoEquipamento}
                    Fabricante: {FabricanteEquipamento}
                    Nº Série: {NumeroSerie}
                    Instalação: {DataInstalacao:dd/MM/yyyy}
                    Última Manutenção: {DataUltimaManutencao:dd/MM/yyyy}
                    Localização: {LocalizacaoEquipamento}
                    Status: {StatusOperacionalEquipamento}
                    Capacidade: {CapacidadeMaxima:N2}
                    Especificações: {Especificacoes}
                    Criado em: {DataCriacao:dd/MM/yyyy HH:mm}
                    Última atualização: {DataAtualizacao?.ToString("dd/MM/yyyy HH:mm") ?? "-"}
                    Ativo: {(Ativo ? "Sim" : "Não")}
                    ";
    }
}