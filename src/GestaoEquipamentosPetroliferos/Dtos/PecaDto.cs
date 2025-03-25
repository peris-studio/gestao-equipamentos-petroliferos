namespace GestaoEquipamentosPetroliferos.Dtos;

public record PecaDto(string Nome,
                      string Descricao,
                      string Numeracao,
                      FornecedorPecas FornecedorPecas,
                      int QuantidadeEstoque,
                      decimal PrecoUnitario,
                      string EquipamentoCompativel,
                      Guid Id = default);