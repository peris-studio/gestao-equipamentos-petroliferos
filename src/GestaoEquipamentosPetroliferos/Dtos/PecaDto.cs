namespace GestaoEquipamentosPetroliferos.Dtos;

public record PecaDto(string Nome,
                      string Descricao,
                      string Numeracao,
                      FornecedorPeca FornecedorPeca,
                      int QuantidadeEstoque,
                      decimal PrecoUnitario,
                      string EquipamentoCompativel,
                      Guid Id = default);