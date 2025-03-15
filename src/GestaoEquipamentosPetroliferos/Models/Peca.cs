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

    public static Peca Inserir(string nome
                               string numeracao,
                               string descricao,
                               FornecedorPecas fornecedorPecas,
                               int quantidadeEstoque,
                               decimal precoUnitario,
                               string equipamentoCompativel,
                               Guid id = default
                              )
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome obrigatório", nameof(nome));

        if (quantidadeEstoque < 0)
            throw new ArgumentException("Estoque não pode ser negativo", nameof(quantidadeEstoque));

        if (precoUnitario <= 0)
            throw new ArgumentException("Preço unitário inválido", nameof(precoUnitario));

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
                                 string equipamentoCompativel
                                 )
    {
        if (peca == null)
            throw new ArgumentNullException(nameof(peca));

        if (!peca.Ativo)
            throw new InvalidOperationException("Peça inativa não pode ser atualizada");

        if (quantidadeEstoque < 0)
            throw new ArgumentException("Estoque não pode ser negativo", nameof(quantidadeEstoque));

        if (precoUnitario <= 0)
            throw new ArgumentException("Preço unitário inválido", nameof(precoUnitario));

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
            throw new ArgumentNullException(nameof(peca), "A peça não pode ser nula");

        if (!peca.Ativo)
            throw new InvalidOperationException("Peça já está inativa");

        peca.DataDelecao = DateTime.UtcNow;
        peca.Ativo = false;

        return peca;
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
                    Nome: {Nome}
                    Descrição: {Descricao}
                    Numeração: {Numeracao}
                    Fornecedor das Peças: {FornecedorPecas}
                    Quantidade no Estoque: {QuantidadeEstoque}
                    Preço Unitário: {PrecoUnitario}
                    Equipamentos Compatíveis: {EquipamentoCompativel}
                    Data de Criação: {DataCriacao}
                    Data de Atualização: {DataAtualizacao}
                    Data de Deleção: {DataDelecao}
                    Ativo: {Ativo}
                    ";
    }
}