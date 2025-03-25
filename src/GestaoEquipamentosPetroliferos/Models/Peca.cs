namespace GestaoEquipamentosPetroliferos.Models;

public class Peca
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public string Numeracao { get; set; }
    public FornecedorPecas FornecedorPecas { get; set; }
    public int QuantidadeEstoque { get; set; }
    public decimal PrecoUnitario { get; set; }
    public string EquipamentoCompativel { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataAtualizacao { get; set; }
    public DateTime? DataDelecao { get; set; }
    public bool Ativo { get; set; }

    // M É T O D O S
    public static Peca Inserir(string nome,
                                string numeracao,
                                string descricao,
                                FornecedorPecas fornecedorPecas,
                                int quantidadeEstoque,
                                decimal precoUnitario,
                                string equipamentoCompativel,
                                Guid id = default)
    {
        ValidarParametrosInsercao(nome, numeracao, fornecedorPecas, quantidadeEstoque, precoUnitario);

        return new Peca()
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id,
            Nome = nome,
            Descricao = descricao,
            Numeracao = numeracao,
            FornecedorPecas = fornecedorPecas,
            QuantidadeEstoque = quantidadeEstoque,
            PrecoUnitario = precoUnitario,
            EquipamentoCompativel = equipamentoCompativel,
            DataCriacao = DateTime.UtcNow,
            Ativo = true
        };
    }

    public static Peca Atualizar(Peca peca,
                                    string nome,
                                    string descricao,
                                    string numeracao,
                                    FornecedorPecas fornecedorPecas,
                                    int quantidadeEstoque,
                                    decimal precoUnitario,
                                    string equipamentoCompativel)
    {
        ValidarEstadoParaAtualizacao(peca);
        ValidarParametrosAtualizacao(nome, numeracao, fornecedorPecas, quantidadeEstoque, precoUnitario);

        peca.Nome = nome;
        peca.Descricao = descricao;
        peca.Numeracao = numeracao;
        peca.FornecedorPecas = fornecedorPecas;
        peca.QuantidadeEstoque = quantidadeEstoque;
        peca.PrecoUnitario = precoUnitario;
        peca.EquipamentoCompativel = equipamentoCompativel;
        peca.DataAtualizacao = DateTime.UtcNow;

        return peca;
    }

    public static Peca Remover(Peca peca)
    {
        if (peca == null)
            throw new ArgumentNullException(nameof(peca), "Peça não pode ser nula");

        if (!peca.Ativo)
            throw new InvalidOperationException("Peça já está inativa");

        peca.DataDelecao = DateTime.UtcNow;
        peca.Ativo = false;

        return peca;
    }

    // V A L I D A Ç Õ E S
    private static void ValidarParametrosInsercao(string nome,
                                                    string numeracao,
                                                    FornecedorPecas fornecedorPecas,
                                                    int quantidadeEstoque,
                                                    decimal precoUnitario)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome obrigatório", nameof(nome));

        if (string.IsNullOrWhiteSpace(numeracao))
            throw new ArgumentException("Numeração obrigatória", nameof(numeracao));

        if (fornecedorPecas == null)
            throw new ArgumentNullException(nameof(fornecedorPecas), "Fornecedor obrigatório");

        if (quantidadeEstoque < 0)
            throw new ArgumentException("Estoque não pode ser negativo", nameof(quantidadeEstoque));

        if (precoUnitario <= 0)
            throw new ArgumentException("Preço unitário inválido", nameof(precoUnitario));
    }

    private static void ValidarParametrosAtualizacao(string nome,
                                                        string numeracao,
                                                        FornecedorPecas fornecedorPecas,
                                                        int quantidadeEstoque,
                                                        decimal precoUnitario)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome obrigatório", nameof(nome));

        if (string.IsNullOrWhiteSpace(numeracao))
            throw new ArgumentException("Numeração obrigatória", nameof(numeracao));

        if (fornecedorPecas == null)
            throw new ArgumentNullException(nameof(fornecedorPecas), "Fornecedor obrigatório");

        if (quantidadeEstoque < 0)
            throw new ArgumentException("Estoque não pode ser negativo", nameof(quantidadeEstoque));

        if (precoUnitario <= 0)
            throw new ArgumentException("Preço unitário inválido", nameof(precoUnitario));
    }

    private static void ValidarEstadoParaAtualizacao(Peca peca)
    {
        if (peca == null)
            throw new ArgumentNullException(nameof(peca));

        if (!peca.Ativo)
            throw new InvalidOperationException("Peça inativa não pode ser atualizada");
    }

    public override string ToString()
    {
        return @$"
                    Nome: {Nome}
                    Numeração: {Numeracao}
                    Descrição: {Descricao ?? "Sem descrição"}
                    Fornecedor: {FornecedorPecas}
                    Estoque: {QuantidadeEstoque}
                    Preço: {PrecoUnitario:C}
                    Compatível com: {EquipamentoCompativel ?? "Nenhum"}
                    Cadastrado em: {DataCriacao:dd/MM/yyyy HH:mm}
                    Status: {(Ativo ? "Ativa" : "Inativa")}
                ";
    }
}