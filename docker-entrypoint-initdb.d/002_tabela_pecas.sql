CREATE TABLE public."Pecas" (
    "Id" UUID PRIMARY KEY,
    "Nome" VARCHAR(225) NOT NULL,
    "Descricao" TEXT NOT NULL,               
    "Numeracao" VARCHAR(50) NOT NULL,                  
    "FornecedorPecas" INTEGER NOT NULL,        
    "QuantidadeEstoque" INTEGER NOT NULL,  
    "PrecoUnitario" DECIMAL(18,2),
    "EquipamentoCompativel" TEXT NOT NULL,                  
    "DataCriacao" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "DataAtualizacao" TIMESTAMP,
    "DataDelecao" TIMESTAMP,
    "Ativo" BOOLEAN NOT NULL
);

INSERT INTO public."Pecas" ("Id",
                            "Nome",
                            "Descricao",
                            "Numeracao",
                            "FornecedorPecas",
                            "QuantidadeEstoque",
                            "PrecoUnitario",
                            "EquipamentoCompativel",
                            "DataCriacao",
                            "Ativo") 
VALUES 
(
    '',
    'Junta Tórica 150mm',
    'Junta para vedação de alta pressão',
    'JT-150-2024',
    3,  -- Valor correspondente a FornecedorPecas.OilParts
    200,
    89.90,
    'Bomba XPTO-3000; Válvula VMAX',
    CURRENT_TIMESTAMP,
    TRUE
);